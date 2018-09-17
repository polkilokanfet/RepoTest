using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Model.POCOs
{
    [Designation("Параметр")]
    public partial class Parameter : BaseEntity, IComparable
    {
        [Designation("Группа"), Required, OrderStatus(5)]
        public virtual ParameterGroup ParameterGroup { get; set; }

        [Designation("Значение"), Required, MaxLength(50), OrderStatus(4)]
        public string Value { get; set; }

        [Designation("Ограничения")]
        public virtual List<ParameterRelation> ParameterRelations { get; set; } = new List<ParameterRelation>();

        [Designation("Начало?")]
        public bool IsOrigin => !ParameterRelations.Any();

        public override string ToString()
        {
            return Value;
        }

        public int CompareTo(object obj)
        {
            var first = this;
            var second = obj as Parameter;

            if (!first.ParameterRelations.Any() && !second.ParameterRelations.Any())
                return 0;

            if (!first.ParameterRelations.Any())
                return -1;

            if (!second.ParameterRelations.Any())
                return 1;

            if (first.ParameterRelations.Any(x => x.RequiredParameters.Select(rp => rp.Id).Contains(second.Id)))
                return -1;
            if (second.ParameterRelations.Any(x => x.RequiredParameters.Select(rp => rp.Id).Contains(first.Id)))
                return 1;
            return 0;
        }

        /// <summary>
        /// Все возможные пути к начальному параметру от этого.
        /// </summary>
        /// <returns></returns>
        public List<PathToOrigin> Paths()
        {
            return Paths(null).Where(x => x.IsFull).ToList();
        }


        private IEnumerable<PathToOrigin> Paths(PathToOrigin path = null)
        {
            path = path ?? new PathToOrigin();

            if (!path.Parameters.Contains(this))
            {
                path.Parameters.Add(this);
            }

            //если достигли начала
            if (this.IsOrigin) yield return path;

            foreach (var relation in ParameterRelations)
            {
                foreach (var requiredParameter in relation.RequiredParameters)
                {
                    var parameters = path.Parameters.ToList();
                    var relations = path.Relations.ToList();

                    parameters.Add(requiredParameter);
                    relations.Add(relation);

                    var newPath = new PathToOrigin(parameters, relations);

                    foreach (var pathToOrigin in requiredParameter.Paths(newPath).ToList())
                    {
                        yield return pathToOrigin;
                    }
                }
            }
        }
    }

    public class PathToOrigin
    {
        public List<Parameter> Parameters { get; } = new List<Parameter>();
        public List<ParameterRelation> Relations { get; } = new List<ParameterRelation>();

        public bool IsFull => !Relations.Any() || Relations.SelectMany(x => x.RequiredParameters).Distinct().AllContainsIn(Parameters);

        public PathToOrigin()
        {
            
        }

        public PathToOrigin(IEnumerable<Parameter> parameters, IEnumerable<ParameterRelation> relations) : this()
        {
            Parameters.AddRange(parameters);
            Relations.AddRange(relations);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Parameters.ForEach(x => sb.Append($"{x} => "));
            return sb.ToString();
        }
    }
}