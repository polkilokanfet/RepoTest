using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    internal static class PathNodesGenerator
    {
        public static IEnumerable<PathNode> GetPathNodes(IEnumerable<Parameter> allParameters)
        {
            var parameters = allParameters as List<Parameter> ?? allParameters.ToList();

            //параметры начала
            var originParameters = parameters.Where(parameter => parameter.IsOrigin).ToList();

            //их сразу в результат
            var result = originParameters.Select(parameter => new PathNode(parameter, null, null)).ToList();

            var relationsDictionary = new Dictionary<Parameter, List<ParameterRelation>>();
            parameters.Where(x => x.IsOrigin == false).ForEach(x => relationsDictionary.Add(x, x.ParameterRelations.ToList()));

            while (relationsDictionary.Any())
            {
                var rr = result.Count;
                parameters = relationsDictionary.Select(x => x.Key).ToList();
                //var ss = result.Select(x => x.GetPathToOriginString()).OrderBy(x => x).ToStringEnum(Environment.NewLine);
                foreach (var parameter in parameters)
                {
                    foreach (var relation in relationsDictionary[parameter].ToList())
                    {
                        var nodes = result
                            //в узле должен быть параметр из связи
                            .Where(node => relation.RequiredParameters.Contains(node.Parameter))
                            //в пути узла к началу должны быть параметры связи
                            .Where(node => relation.RequiredParameters.AllContainsIn(node.GetPathToStartParameter(true)))
                            .Where(node => node.GetPathToStartParameter(true).Select(p => p.ParameterGroup).Contains(parameter.ParameterGroup) == false)
                            .ToList();

                        foreach (var node in nodes)
                        {
                            result.Add(node.AddNextPathNode(parameter, relation));
                            if (relationsDictionary.ContainsKey(parameter))
                            {
                                relationsDictionary[parameter].Remove(relation);
                                if (relationsDictionary[parameter].Any() == false)
                                    relationsDictionary.Remove(parameter);
                            }
                        }
                        //result.AddRange(nodes.Select(node => node.AddNextPathNode(parameter, relation)));
                    }
                }

                if (result.Count == rr) 
                    break;
            }

            if (result.Any(x => x.IsValid() == false))
                throw new Exception("Сформирован невалидный узел");

            return result;
        }

        public static IEnumerable<ProductBlock> GetAllBlocks(IEnumerable<PathNode> nodes)
        {
            return nodes
                .Where(node => node.IsStartNode)
                .SelectMany(node => GetParametersChains(node, new List<Parameter>()))
                .Select(parameters => new ProductBlock(){Parameters = parameters.ToList()});
        }


        private static IEnumerable<IEnumerable<Parameter>> GetParametersChains(PathNode inputNode, IEnumerable<Parameter> inputParameters)
        {
            var parametersWithCurrent = inputParameters.Union(new[] {inputNode.Parameter}).ToList();

            if (parametersWithCurrent.GroupBy(x => x.ParameterGroup).Any(x => x.Count() != 1))
                throw new Exception();


            if (inputNode.NextPathNodes.Any() == false)
            {
                yield return parametersWithCurrent;
            }
            else
            {
                List<IEnumerable<Parameter>> result = null;

                //делим все следующие узлы по группам (параметров)
                var nextNodesGroups = inputNode.NextPathNodes
                    .GroupBy(node => node.Parameter.ParameterGroup)
                    .ToList();

                do
                {
                    var targetNodesGroup = nextNodesGroups.First();
                    nextNodesGroups.Remove(targetNodesGroup);

                    var chains = targetNodesGroup
                        .SelectMany(node => GetParametersChains(node, parametersWithCurrent).ToList())
                        .ToList();

                    if (chains.Any(x => x.GroupBy(parameter => parameter.ParameterGroup).Any(q => q.Count() != 1)))
                        throw new Exception();

                    if (result == null)
                    {
                        result = chains;
                    }
                    else
                    {
                        foreach (var r in result.ToList())
                        {
                            result.Remove(r);
                            foreach (var targetNodeParametersChain in chains)
                            {
                                var mbr = r.Union(targetNodeParametersChain).Distinct().ToList();

                                if (mbr.GroupBy(x => x.ParameterGroup).Any(x => x.Count() != 1))
                                    result.Add(targetNodeParametersChain);
                                else
                                    result.Add(mbr);
                            }
                        }
                    }
                } while (nextNodesGroups.Any());

                foreach (var m in result)
                {
                    yield return m;
                }
            }

        }
    }
}