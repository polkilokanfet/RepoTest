using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public static class Ext
    {
        /// <summary>
        /// �������� �� ������ ����������, ������� � ����� ������ � ������������� �����������.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Parameter> GetUsefull(this IEnumerable<Parameter> targetParameters, ProductRelation relation)
        {
            var parameters = targetParameters as List<Parameter> ?? targetParameters.ToList();

            //������������ ���������
            var goodParams = relation.ChildProductParameters.ToList();
            var groups = goodParams.Select(x => x.ParameterGroup).Distinct();
            //��������, ��������� ������ � ������� (������)
            var badParams = parameters.Where(x => groups.Contains(x.ParameterGroup)).Except(goodParams).ToList();
            //������������ ����������� �����
            var ints = Intercect(goodParams).ToList();

            //��� ���� ���� ����������
            var paths = parameters.SelectMany(x => x.Paths()).ToList();

            //��������� ���� � ������� �����������
            paths = paths.Where(x => !x.Parameters.Intersect(badParams).Any()).ToList();
            //��������� ���� ��� ������� ����������
            paths = paths.Where(x => x.Parameters.Intersect(goodParams).Any()).ToList();
            //��������� ���� ��� �����������
            paths = paths.Where(x => ints.AllContainsIn(x.Parameters)).ToList();

            var result = paths.SelectMany(x => x.Parameters).Distinct().ToList();
            return result;
        }

        /// <summary>
        /// ������������ ����������� � ����� ����������.
        /// ����� ������ ����� ������� ���� ����������.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static IEnumerable<Parameter> Intercect(IEnumerable<Parameter> parameters)
        {
            var pmtrs = parameters as Parameter[] ?? parameters.ToArray();

            var result = pmtrs.First().Paths().SelectMany(x => x.Parameters).Distinct().ToList();
            foreach (var parameter in pmtrs)
            {
                var toIntersect = parameter.Paths().SelectMany(x => x.Parameters).Distinct().ToList();
                result = result.Intersect(toIntersect).ToList();
            }

            return result;
        }

        /// <summary>
        /// ���� ������������ ����������.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        static List<Parameter> RequiredParametersField(Parameter parameter)
        {
            var result = new List<Parameter> {parameter};

            foreach (var requiredParameter in parameter.ParameterRelations.SelectMany(x => x.RequiredParameters).Distinct())
            {
                result.Add(requiredParameter);
                result.AddRange(RequiredParametersField(requiredParameter));
            }

            return result.Distinct().ToList();
        }
    }
}