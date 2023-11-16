using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    /// <summary>
    /// ���� ����
    /// </summary>
    internal class PathNode
    {
        public PathNode PreviousPathNode { get; }

        private List<PathNode> _nextPathNodes = new List<PathNode>();
        public IEnumerable<PathNode> NextPathNodes => _nextPathNodes;

        /// <summary>
        /// �������� � ����
        /// </summary>
        public Parameter Parameter { get; }

        public ParameterRelation Relation { get; }

        public PathNode(Parameter parameter, PathNode previousPathNode, ParameterRelation relation)
        {
            Parameter = parameter;
            PreviousPathNode = previousPathNode;
            Relation = relation;
        }

        public PathNode AddNextPathNode(Parameter parameter, ParameterRelation relation)
        {
            var node = new PathNode(parameter, this, relation);
            _nextPathNodes.Add(node);
            return node;
        }

        private List<Parameter> _pathToOrigin;
        public IEnumerable<Parameter> GetPathToOrigin()
        {
            if (_pathToOrigin == null)
            {
                _pathToOrigin = new List<Parameter>();
                var node = this.PreviousPathNode;
                while (node != null)
                {
                    _pathToOrigin.Add(node.Parameter);
                    node = node.PreviousPathNode;
                }
            }

            return _pathToOrigin;
        }
    }

    internal static class PathNodesGenerator
    {
        public static IEnumerable<PathNode> GetPathNodes(IEnumerable<Parameter> allParameters)
        {
            var parameters = allParameters as List<Parameter> ?? allParameters.ToList();

            //��������� ������
            var originParameters = parameters.Where(parameter => parameter.IsOrigin).ToList();

            //�� ����� � ���������
            var result = originParameters.Select(parameter => new PathNode(parameter, null, null)).ToList();

            while (parameters.Any())
            {
                parameters = parameters.Where(parameter => parameter.ParameterRelations.Except(result.Select(node => node.Relation)).Any()).ToList();
                foreach (var parameter in parameters)
                {
                    var relations = parameter.ParameterRelations.Except(result.Select(node => node.Relation));
                    foreach (var relation in relations)
                    {
                        var nodes = result
                            //� ���� ������ ���� �������� �� �����
                            .Where(node => relation.RequiredParameters.Contains(node.Parameter))
                            //� ���� ���� � ������ ������ ���� ���������� ��������� �����
                            .Where(node => relation.RequiredParameters.Except(new[] {node.Parameter}).AllContainsIn(node.GetPathToOrigin()));

                        result.AddRange(nodes.Select(node => node.AddNextPathNode(parameter, relation)));
                    }
                }
            }

            return result;
        }
    }
}