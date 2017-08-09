using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
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
            Assert.IsTrue(_parameterSelectorEqType.SelectedParameter != null);
        }

        [TestMethod]
        public void ParameterSelectorHasSelectedParameter()
        {
            var parameters = _breaker.Group.Parameters;
            ParameterSelector parameterSelector = new ParameterSelector(parameters, parameters.Last());
            Assert.AreEqual(parameterSelector.SelectedParameter, parameters.Last());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Выбран параметр не из списка.")]
        public void ParameterSelectorSelectedParameterException()
        {
            Assert.IsFalse(_parameterSelectorEqType.ParametersWithActualFlag.Select(x => x.Parameter).Contains(_v110));
            _parameterSelectorEqType.SelectedParameter = _v110;
        }

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException), "Параметр не актуален")]
        //public void ParameterSelectorSelectedParameterIsNotActualException()
        //{
        //    _parameterSelectorEqType.ParametersWithActualFlag.ForEach(x => x.IsActual = false);
        //    _parameterSelectorEqType.SelectedParameter = _parameterSelectorEqType.ParametersWithActualFlag.Last().Parameter;
        //}

        [TestMethod]
        public void ParameterSelectorSelectedParameterIsNotActual()
        {
            var selectedParameter = _parameterSelectorEqType.SelectedParameter;
            var parameterWithActualFlag = _parameterSelectorEqType.ParametersWithActualFlag.Single(x => Equals(selectedParameter, x.Parameter));
            parameterWithActualFlag.IsActual = false;

            Assert.IsFalse(Equals(_parameterSelectorEqType.SelectedParameter, selectedParameter));
        }

        [TestMethod]
        public void ParameterSelectorAutoSelectParameter()
        {
            foreach (var parameterWithActualFlag in _parameterSelectorEqType.ParametersWithActualFlag)
                parameterWithActualFlag.IsActual = false;
            Assert.IsTrue(_parameterSelectorEqType.SelectedParameter == null);

            var pwaf = _parameterSelectorEqType.ParametersWithActualFlag.Last();
            pwaf.IsActual = true;
            Assert.IsTrue(Equals(_parameterSelectorEqType.SelectedParameter, pwaf.Parameter));
        }
    }
}