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
                    //GetComplexParameterRelations(allParameters);
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
                .SelectMany(node => GetChains(node, new ParametersChain()))
                .Select(chain => new ProductBlock(){Parameters = chain.GetParameters().ToList()});
        }


        private static IEnumerable<ParametersChain> GetChains(PathNode inputNode, ParametersChain inputChain)
        {
            inputChain.AddNode(inputNode);

            if (inputNode.NextPathNodes.Any() == false)
            {
                yield return inputChain;
            }
            else
            {
                var result = new ChainsContainer();

                //делим все следующие узлы по группам (параметров)
                var nextNodesGroups = inputNode.NextPathNodes
                    .GroupBy(node => node.Parameter.ParameterGroup)
                    .ToList();

                foreach (var nextNodesGroup in nextNodesGroups)
                {
                    var chains = nextNodesGroup
                        .SelectMany(node => GetChains(node, inputChain.GetCopy()))
                        .ToList();

                    result.Add(chains);
                }

                foreach (var m in result.Chains)
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

    internal class ParametersChain
    {
        private readonly List<PathNode> _nodes = new List<PathNode>();
        public IEnumerable<PathNode> Nodes => _nodes;

        public void AddNode(PathNode node)
        {
            if (_nodes.Any(x => x.GetPathToStart().Contains(node)))
                throw new ArgumentException(@"Попытка добавления", nameof(node));

            var parentNode = _nodes.SingleOrDefault(x => node.GetPathToStart().Contains(x));
            if (parentNode != null)
                _nodes.Remove(parentNode);

            _nodes.Add(node);
        }

        public ParametersChain GetCopy()
        {
            var result = new ParametersChain();
            this.Nodes.ForEach(result.AddNode);
            return result;
        }

        public void AddChain(ParametersChain chain)
        {
            foreach (var node in chain.Nodes)
            {
                this.AddNode(node);
            }
        }

        public IEnumerable<Parameter> GetParameters()
        {
            return this.Nodes.SelectMany(pathNode => pathNode.GetPathToStartParameter(true)).Distinct();
        }
    }

    internal class ChainsContainer
    {
        private List<ParametersChain> _chains;
        public IEnumerable<ParametersChain> Chains => _chains;

        public void Add(IEnumerable<ParametersChain> chains)
        {
            if (_chains == null)
            {
                _chains = chains.ToList();
                return;
            }

            var chainsToAdd = chains as ParametersChain[] ?? chains.ToArray();
            foreach (var chainInContainer in _chains.ToList())
            {
                var chainCopy = chainInContainer.GetCopy();
                _chains.Remove(chainInContainer);

                foreach (var c2 in chainsToAdd)
                {
                    var c = chainCopy.GetCopy();
                    c.AddChain(c2);
                    _chains.Add(c);
                    
                }
            }
        }
    }
}