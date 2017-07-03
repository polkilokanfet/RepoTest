using System;
using System.Linq;
using AutoFixture.AutoEF;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;
using LazyEntityGraph.AutoFixture;
using LazyEntityGraph.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace HVTApp.Services.GetProductService.Tests
{
    [TestClass]
    public class GetProductServiceTests
    {
        [TestInitialize]
        public void InitializeMethod()
        {
            Fixture fixture = new Fixture();

            //var customization = new LazyEntityGraphCustomization(
            //    ModelMetadataGenerator.LoadFromCodeFirstContext(str => new HVTAppContext(str), true));
            fixture.Customize(new EntityCustomization(new DbContextEntityTypesProvider(typeof(HVTAppContext))));

            //отключаем поведение - бросать ошибку при обнаружении циклической связи
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            //подключаем поведение - останавливаться на стандартной глубине рекурсии
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var nnn = fixture.Create<Project>();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
