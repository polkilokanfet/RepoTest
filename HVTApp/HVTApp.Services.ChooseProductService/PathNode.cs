using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    internal class PathNodeComplex
    {

    }

    /// <summary>
    /// Узел пути
    /// </summary>
    internal class PathNode
    {
        public PathNode PreviousPathNode { get; }

        private readonly List<PathNode> _nextPathNodes = new List<PathNode>();
        public IEnumerable<PathNode> NextPathNodes => _nextPathNodes;

        public bool IsStartNode => PreviousPathNode == null;
        public bool IsFinishNode => NextPathNodes.Any() == false;

        /// <summary>
        /// Параметр в узле
        /// </summary>
        public Parameter Parameter { get; }

        public ParameterRelation Relation { get; }

        /// <summary>
        /// Конструктор для стартовых узлов
        /// </summary>
        /// <param name="parameter"></param>
        public PathNode(Parameter parameter)
        {
            Parameter = parameter;
        }

        private PathNode(Parameter parameter, PathNode previousPathNode, ParameterRelation relation) : this(parameter)
        {
            PreviousPathNode = previousPathNode;
            Relation = relation;
        }

        public PathNode AddNextPathNode(Parameter parameter, ParameterRelation relation)
        {
            if(this.GetPathToStartParameter(true).Select(p => p.ParameterGroup).Contains(parameter.ParameterGroup))
                throw new ArgumentException(@"В пути к началу не должно быть пересечений по группам параметров", nameof(parameter));

            var node = new PathNode(parameter, this, relation);
            _nextPathNodes.Add(node);
            return node;
        }

        private List<Parameter> _pathToOrigin;
        /// <summary>
        /// Вернуть путь от текущего узла к стартовому
        /// </summary>
        /// <param name="includeNodeParameter">Включить в путь параметр этого узла</param>
        /// <returns></returns>
        public IEnumerable<Parameter> GetPathToStartParameter(bool includeNodeParameter = false)
        {
            if (_pathToOrigin == null)
            {
                _pathToOrigin = this.PreviousPathNode == null 
                    ? new List<Parameter>() 
                    : this.PreviousPathNode.GetPathToStartParameter(true).ToList();
            }

            return includeNodeParameter 
                ? new[] {this.Parameter}.Union(_pathToOrigin) 
                : _pathToOrigin;
        }

        public IEnumerable<PathNode> GetPathToStart()
        {
            var node = this;
            do
            {
                yield return node;
                node = node.PreviousPathNode;
            } while (node != null);
        }

        public bool IsValid()
        {
            //в пути должны быть только параметры с уникальными группами
            if (this.GetPathToStartParameter(true)
                .GroupBy(x => x.ParameterGroup)
                .All(x => x.Count() == 1) == false)
                return false;

            return true;
        }

        public override string ToString()
        {
            return $"Param: [{Parameter}]; Relation: [{Relation}]";
        }
    }
}