using System.Collections.Generic;
using System.Linq;
using AutoFixture.AutoEF;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;
using Ploeh.AutoFixture;

namespace HVTApp.Model.Tests
{
    public static class FixtureTest
    {
        public static Fixture GetFixture()
        {
            var fixture = new Fixture();

            fixture.Customize(new EntityCustomization(new DbContextEntityTypesProvider(typeof(HVTAppContext))));

            //отключаем поведение - бросать ошибку при обнаружении циклической связи
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            //подключаем поведение - останавливаться на стандартной глубине рекурсии
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            fixture.Customize<ParameterGroup>(p => p.With(x => x.Parameters, new List<Parameter>()));
            fixture.Customize<Country>(c => c.Without(x => x.Capital));

            return fixture;
        }
    }
}