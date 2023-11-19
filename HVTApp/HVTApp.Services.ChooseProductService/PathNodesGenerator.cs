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

           //��������� ������ ����� � ���������
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
                            //� ���� ������ ���� �������� �� �����
                            .Where(node => targetRelation.RequiredParameters.Contains(node.Parameter))
                            //� ���� ���� � ������ ������ ���� ��������� �����
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
                throw new Exception($"������������ ���������� �����: {result.Where(node => node.IsValid() == false).ToStringEnum()}");

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

                //����� ��� ��������� ���� �� ������� (����������)
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

            //��������� ������ ����� � ���������
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
                            //� ���� ������ ���� �������� �� �����
                            .Where(node => targetRelation.RequiredParameters.Contains(node.Parameter))
                            //� ���� ���� � ������ ������ ���� ��������� �����
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
            if (_nodes.Contains(node))
                throw new ArgumentException(@"����� ���� ��� ��������", nameof(node));

            if (_nodes.Select(x => x.Parameter.ParameterGroup).Contains(node.Parameter.ParameterGroup))
                throw new ArgumentException(@"������ ��������� ��������� ���������� �� ����� ������", nameof(node));

            //var parentNode = _nodes.SingleOrDefault(x => node.GetPathToStart().Contains(x));
            //if (parentNode != null)
            //    _nodes.Remove(parentNode);

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
                //���� ����� ���� ��� ���� � ������� - ���������� ����������
                if (_nodes.Contains(node))
                    continue;

                //���� � ������� ��� ���� �������� �� ��� �� ������
                var sameGroupNode = _nodes.SingleOrDefault(x => x.Parameter.ParameterGroup.Equals(node.Parameter.ParameterGroup));
                if (sameGroupNode != null)
                {
                    //������� ���� ���� � ��� �� ���� ��������� ����
                    _nodes.Remove(sameGroupNode);
                    foreach (var pathNode in _nodes.Where(x => x.GetPathToStart().Contains(sameGroupNode) && x.Relation.RequiredParameters.Contains(sameGroupNode.Parameter)).ToList())
                    {
                        _nodes.Remove(pathNode);
                    }
                }

                _nodes.Add(node);
            }
        }

        public IEnumerable<Parameter> GetParameters()
        {
            return this.Nodes.Select(node => node.Parameter).Distinct();
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
                    var rr = c.Nodes.Select(x => x.Parameter).Union(c2.Nodes.Select(x => x.Parameter)).GroupBy(x => x.ParameterGroup).Any(x => x.Count() != 1);
                    if (rr)
                    {
                        _chains.Add(chainCopy.GetCopy());
                    }
                    c.AddChain(c2);
                    _chains.Add(c);
                    
                }
            }
        }
    }
}