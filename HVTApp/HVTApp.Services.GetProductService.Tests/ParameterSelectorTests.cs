using System;
using System.Linq;
using Castle.Core.Internal;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Services.GetProductService.Tests
{
    [TestClass]
    public class ParameterSelectorTests
    {
        private Parameter _breaker, _transformator;
        private Parameter _v110, _v220, _v500;
        private Parameter _c2500, _c3150;
        private ParameterSelector _parameterSelectorEqType;

        [TestInitialize]
        public void Init()
        {
            _breaker = new Parameter();
            _transformator = new Parameter();
            ParameterGroup eqType = new ParameterGroup().AddParameters(new[] { _breaker, _transformator });

            _v110 = new Parameter().AddRequiredPreviousParameters(new[] { _breaker });
            _v220 = new Parameter().AddRequiredPreviousParameters(new[] { _breaker });
            _v500 = new Parameter().AddRequiredPreviousParameters(new[] { _breaker });
            ParameterGroup voltage = new ParameterGroup().AddParameters(new[] { _v110, _v220, _v500 });

            _c2500 = new Parameter().AddRequiredPreviousParameters(new[] { _breaker, _v110 })
                                    .AddRequiredPreviousParameters(new[] { _breaker, _v220 });
            _c3150 = new Parameter().AddRequiredPreviousParameters(new[] { _breaker, _v110 })
                                    .AddRequiredPreviousParameters(new[] { _breaker, _v220 })
                                    .AddRequiredPreviousParameters(new[] { _breaker, _v500 });

            _parameterSelectorEqType = new ParameterSelector(eqType.Parameters);
        }

        [TestMethod]
        public void ParameterSelectorHasPreSelectedParameter()
        {
            Assert.IsTrue(_parameterSelectorEqType.SelectedParameterFlaged != null);
        }

        [TestMethod]
        public void ParameterSelectorHasSelectedParameter()
        {
            //var parameters = _breaker.GroupId.Parameters;
            //ParameterSelector parameterSelector = new ParameterSelector(parameters, parameters.Last());
            //Assert.AreEqual(parameterSelector.SelectedParameterFlaged.ParameterId, parameters.Last());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Выбран параметр не из списка.")]
        public void ParameterSelectorSelectedParameterException()
        {
            Assert.IsFalse(_parameterSelectorEqType.ParametersFlaged.Select(x => x.Parameter).Contains(_v110));
            _parameterSelectorEqType.SelectedParameter = (_v110);
        }

        //[TestMethod]
        [ExpectedException(typeof(ArgumentException), "Параметр не актуален")]
        public void ParameterSelectorSelectedParameterIsNotActualException()
        {
            _parameterSelectorEqType.ParametersFlaged.ForEach(x => x.IsActual = false);
            _parameterSelectorEqType.SelectedParameter = (_parameterSelectorEqType.ParametersFlaged.Last().Parameter);
        }

        [TestMethod]
        public void ParameterSelectorSelectedParameterIsNotActual()
        {
            var selectedParameter = _parameterSelectorEqType.SelectedParameterFlaged = _parameterSelectorEqType.ParametersFlaged.Last();
            var parameterWithActualFlag = _parameterSelectorEqType.ParametersFlaged.Single(x => Equals(selectedParameter, x));
            parameterWithActualFlag.IsActual = false;

            //как только выбранный параметр потерял свою актуальность, меняем его на актуальный
            Assert.IsFalse(Equals(_parameterSelectorEqType.SelectedParameterFlaged, selectedParameter));
        }

        [TestMethod]
        public void ParameterSelectorAutoSelectParameter()
        {
            foreach (var parameterWithActualFlag in _parameterSelectorEqType.ParametersFlaged)
                parameterWithActualFlag.IsActual = false;
            Assert.IsTrue(_parameterSelectorEqType.SelectedParameterFlaged == null);

            var pwaf = _parameterSelectorEqType.ParametersFlaged.Last();
            pwaf.IsActual = true;
            Assert.IsTrue(Equals(_parameterSelectorEqType.SelectedParameterFlaged, pwaf));
        }
    }
}