using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    /// <summary>
    /// Узел пути
    /// </summary>
    internal class PathNode
    {
        public PathNode PreviousPathNode { get; }

        private List<PathNode> _nextPathNodes = new List<PathNode>();
        public IEnumerable<PathNode> NextPathNodes => _nextPathNodes;

        public bool IsStartNode => PreviousPathNode == null;
        public bool IsFinishNode => NextPathNodes.Any() == false;

        /// <summary>
        /// Параметр в узле
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

        public string GetPathToOriginString()
        {
            var rr = GetPathToOrigin().ToList();
            rr.Reverse();
            rr.Add(this.Parameter);
            return rr.ToStringEnum(" => ");
        }
    }
}