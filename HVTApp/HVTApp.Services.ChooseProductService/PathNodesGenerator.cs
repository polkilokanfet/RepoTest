using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    internal static class PathNodesGenerator
    {
        public static IEnumerable<PathNode> GetPathNodes(IEnumerable<Parameter> allParameters)
        {
            var parameters = allParameters.ToList();

           //параметры начала сразу в результат
            var result = parameters
                .Where(parameter => parameter.IsOrigin)
                .Select(parameter => new PathNode(parameter))
                .ToList();

            while (parameters.Any())
            {
                var rr = result.Count;
                var relationsInResult = result.Select(node => node.Relation).ToList();
                parameters = parameters.Where(parameter => parameter.ParameterRelations.Except(relationsInResult).Any()).ToList();

                foreach (var targetParameter in parameters)
                {
                    foreach (var targetRelation in targetParameter.ParameterRelations.Except(relationsInResult))
                    {
                        var targetNodes = result
                            //в узле должен быть параметр из связи
                            .Where(node => targetRelation.RequiredParameters.Contains(node.Parameter))
                            //в пути узла к началу должны быть параметры связи
                            .Where(node => targetRelation.RequiredParameters.AllContainsIn(node.GetPathToStartParameter(true)))
                            .ToList();

                        result.AddRange(targetNodes.Select(node => node.AddNextPathNode(targetParameter, targetRelation)));
                    }
                }

                if (result.Count == rr)
                {
                    GetComplexParameterRelations(allParameters);
                    break;
                }
            }

            if (result.Any(node => node.IsValid() == false))
                throw new Exception($"Сформированы невалидные узелы: {result.Where(node => node.IsValid() == false).ToStringEnum()}");

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


        public static Dictionary<Parameter, IEnumerable<ParameterRelation>> GetComplexParameterRelations(IEnumerable<Parameter> allParameters)
        {
            var parameters = allParameters.ToList();
            var result = new Dictionary<Parameter, IEnumerable<ParameterRelation>>();

            //параметры начала сразу в результат
            var nodes = parameters
                .Where(parameter => parameter.IsOrigin)
                .Select(parameter => new PathNode(parameter))
                .ToList();

            while (parameters.Any())
            {
                var rr = nodes.Count;
                var relationsInResult = nodes.Select(node => node.Relation).ToList();
                parameters = parameters.Where(parameter => parameter.ParameterRelations.Except(relationsInResult).Any()).ToList();

                foreach (var targetParameter in parameters)
                {
                    foreach (var targetRelation in targetParameter.ParameterRelations.Except(relationsInResult))
                    {
                        var targetNodes = nodes
                            //в узле должен быть параметр из связи
                            .Where(node => targetRelation.RequiredParameters.Contains(node.Parameter))
                            //в пути узла к началу должны быть параметры связи
                            .Where(node => targetRelation.RequiredParameters.AllContainsIn(node.GetPathToStartParameter(true)))
                            .ToList();

                        nodes.AddRange(targetNodes.Select(node => node.AddNextPathNode(targetParameter, targetRelation)));
                    }
                }

                if (nodes.Count == rr)
                    break;
            }

            foreach (var parameter in parameters)
            {
                result.Add(parameter, parameter.ParameterRelations.Except(nodes.Select(node => node.Relation)));
            }

            return result;
        }

    }
}