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

        [Designation("Значение"), Required, MaxLength(150), OrderStatus(4)]
        public string Value { get; set; }

        [Designation("Ограничения")]
        public virtual List<ParameterRelation> ParameterRelations { get; set; } = new List<ParameterRelation>();

        [Designation("Ранг"), Required]
        public int Rang { get; set; } = 0;

        [Designation("Комментарий"), MaxLength(75)]
        public string Comment { get; set; }

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
            //если пути еще не формировались или связи изменились
            if (_pathsToOrigin == null || !this.ParameterRelations.MembersAreSame(_previousParameterRelations))
            {
                _previousParameterRelations = this.ParameterRelations.ToList();
                _pathsToOrigin = Paths(null).Where(x => x.IsFull).Distinct(new PathComparer()).ToList();
            }

            return _pathsToOrigin;
        }
        List<PathToOrigin> _pathsToOrigin;
        List<ParameterRelation> _previousParameterRelations = new List<ParameterRelation>();
        

        private IEnumerable<PathToOrigin> Paths(PathToOrigin path = null, ParameterRelation parameterRelation = null)
        {
            path = path ?? new PathToOrigin();

            if (!path.Parameters.Contains(this))
            {
                path.Parameters.Add(this);
            }

            if (parameterRelation != null)
            {
                foreach (var parameter in parameterRelation.RequiredParameters)
                {
                    if (!path.Parameters.Contains(parameter))
                    {
                        path.Parameters.Add(parameter);
                    }
                }
            }


            //если достигли начала
            if (this.IsOrigin)
            {
                yield return path;
                yield break;
            }

            foreach (var relation in ParameterRelations)
            {
                if (path.Relations.Any())
                {
                    var skip = false;
                    foreach (var rp in path.Relations.SelectMany(x => x.RequiredParameters))
                    {
                        if (!relation.RequiredParameters.AllContainsIn(rp.Cloud()))
                        {
                            skip = true;
                            break;
                        }
                    }
                    if (skip) continue;
                }

                foreach (var requiredParameter in relation.RequiredParameters)
                {
                    var parameters = path.Parameters.ToList();
                    var relations = path.Relations.ToList();

                    if (!parameters.Contains(requiredParameter))
                        parameters.Add(requiredParameter);
                    relations.Add(relation);

                    var newPath = new PathToOrigin(parameters, relations);

                    foreach (var pathToOrigin in requiredParameter.Paths(newPath, relation).ToList())
                    {
                        yield return pathToOrigin;
                    }
                }
            }
        }

        public IEnumerable<Parameter> Cloud()
        {
            yield return this;

            foreach (var parameter in this.ParameterRelations.SelectMany(x => x.RequiredParameters))
            {
                foreach (var parameter1 in parameter.Cloud())
                {
                    yield return parameter1;
                }
            }
        }

        /// <summary>
        /// Получить подобный параметр (со связями)
        /// </summary>
        /// <returns></returns>
        public Parameter GetSimilarParameter()
        {
            //создаем подобный парметр
            var result = new Parameter
            {
                Value = $"{this.Value} - подобный параметр",
                ParameterGroup = this.ParameterGroup,
                Rang = this.Rang,
                Comment = this.Comment
            };

            foreach (var parameterRelation in this.ParameterRelations)
            {
                var similarParameterRelation = new ParameterRelation
                {
                    ParameterId = result.Id,
                    RequiredParameters = parameterRelation.RequiredParameters.ToList()
                };
                result.ParameterRelations.Add(similarParameterRelation);
            }

            return result;
        }
    }

    public class PathComparer : IEqualityComparer<PathToOrigin>
    {
        public bool Equals(PathToOrigin x, PathToOrigin y)
        {
            return x.Parameters.MembersAreSame(y.Parameters);
        }

        public int GetHashCode(PathToOrigin obj)
        {
            return 0;
        }
    }

    public class PathToOrigin
    {
        public List<Parameter> Parameters { get; } = new List<Parameter>();
        public List<ParameterRelation> Relations { get; } = new List<ParameterRelation>();

        public bool IsFull
        {
            get
            {
                return !Relations.Any() ||
                       Relations.SelectMany(x => x.RequiredParameters).Distinct().AllContainsIn(Parameters);
            }
        }

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
            var sb = new StringBuilder();
            Parameters.ForEach(x => sb.Append($"{x} => "));
            return sb.ToString();
        }
    }
}