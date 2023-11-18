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

                if (result.Count == rr) 
                    break;
            }

            return result;
        }

        public static IEnumerable<ProductBlock> GetAllBlocks(IEnumerable<PathNode> nodes)
        {
            return nodes
                .Where(x => x.IsStartNode)
                .SelectMany(x => GetParametersChains(x, new List<Parameter>()))
                .Select(x => new ProductBlock(){Parameters = x.ToList()});

            return nodes.Where(x => x.IsStartNode).SelectMany(x => GetBlocks(new ProductBlock(), x));
        }

        private static IEnumerable<ProductBlock> GetBlocks(ProductBlock block, PathNode node)
        {
            block.Parameters.Add(node.Parameter);
            if (node.NextPathNodes.Any() == false)
            {
                yield return block;
            }
            else
            {
                foreach (var nextPathNode in node.NextPathNodes)
                {
                    var pb = new ProductBlock() {Parameters = block.Parameters.ToList()};
                    foreach (var productBlock in GetBlocks(pb, nextPathNode))
                    {
                        yield return productBlock;
                    }
                }
            }

            //List<ProductBlock> result = new List<ProductBlock>() {block};

            //var p = block.Parameters.ToList();

            //block.Parameters.Add(node.Parameter);
            //foreach (var nextNode in node.NextPathNodes)
            //{
            //    if (CanGo(block, nextNode))
            //    {
            //        result.AddRange(GetBlocks(block, nextNode));
            //    }
            //    else
            //    {
            //        var pb = new ProductBlock {Parameters = p.ToList()};
            //        result.AddRange(GetBlocks(pb, nextNode));
            //    }
            //}

            //return result.Distinct();
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


        private static IEnumerable<IEnumerable<Parameter>> GetParametersChains(PathNode node, IEnumerable<Parameter> inputParameters)
        {
            var pp = inputParameters.Union(new[] {node.Parameter}).ToList();

            if (node.NextPathNodes.Any() == false)
            {
                yield return pp;
            }
            else
            {
                List<IEnumerable<Parameter>> result = null;

                var nextNodesGroups = node.NextPathNodes.GroupBy(x => x.Parameter.ParameterGroup).ToList();

                do
                {
                    var targetNodesGroup = nextNodesGroups.First();
                    nextNodesGroups.Remove(targetNodesGroup);

                    var chains = targetNodesGroup.SelectMany(x => GetParametersChains(x, pp).ToList()).ToList();

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

                                result.Add(r.Union(targetNodeParametersChain).Distinct().ToList());
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