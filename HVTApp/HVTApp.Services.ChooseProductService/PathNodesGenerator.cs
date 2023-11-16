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
            var parameters = allParameters as List<Parameter> ?? allParameters.ToList();

            //параметры начала
            var originParameters = parameters.Where(parameter => parameter.IsOrigin).ToList();

            //их сразу в результат
            var result = originParameters.Select(parameter => new PathNode(parameter, null, null)).ToList();

            var relationsDictionary = new Dictionary<Parameter, List<ParameterRelation>>();
            parameters.Where(x => x.IsOrigin == false).ForEach(x => relationsDictionary.Add(x, x.ParameterRelations.ToList()));

            while (relationsDictionary.Any())
            {
                parameters = relationsDictionary.Select(x => x.Key).ToList();
                var ss = result.Select(x => x.GetPathToOriginString()).OrderBy(x => x).ToStringEnum(Environment.NewLine);
                foreach (var parameter in parameters)
                {
                    foreach (var relation in relationsDictionary[parameter].ToList())
                    {
                        var nodes = result
                            //в узле должен быть параметр из связи
                            .Where(node => relation.RequiredParameters.Contains(node.Parameter))
                            //в пути узла к началу должны быть параметры связи
                            .Where(node => relation.RequiredParameters.AllContainsIn(node.GetPathToOrigin().Union(new[] { node.Parameter })))
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
            }

            return result;
        }

        public static IEnumerable<ProductBlock> GetAllBlocks(IEnumerable<PathNode> nodes)
        {
            return nodes.Where(x => x.IsStartNode).Select(x => GetBlock(new ProductBlock(), x));
        }

        private static ProductBlock GetBlock(ProductBlock block, PathNode node)
        {
            block.Parameters.Add(node.Parameter);
            foreach (var nextNode in node.NextPathNodes)
            {
                if (CanGo(block, nextNode)) 
                    GetBlock(block, nextNode);
            }

            return block;
        }

        private static bool CanGo(ProductBlock block, PathNode node)
        {
            if (block.Parameters.Contains(node.Parameter)) return false;
            if (block.Parameters.Select(parameter => parameter.ParameterGroup).Contains(node.Parameter.ParameterGroup)) return false;
            return true;
        }
    }
}