using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HVTApp.Model.Tests
{
    [TestClass]
    public class BaseEntityTest
    {
        [TestMethod]
        public void EqulsWorks()
        {
            CompanyForm companyForm1 = new CompanyForm {Id = 1, FullName = "fn1", ShortName = "sn1"};
            CompanyForm companyForm2 = new CompanyForm { Id = 2, FullName = "fn2", ShortName = "sn2" };

            Company company = new Company {Id = 1};

            Assert.IsTrue((companyForm1 as BaseEntity) != null);
            
            Assert.IsFalse(companyForm1.Equals(company));

            Assert.IsFalse(companyForm1.Equals(companyForm2));

            companyForm2.Id = 1;
            Assert.IsTrue(companyForm1.Equals(companyForm2));
        }

        IEnumerable<Type> GetBaseTypes(Type type)
        {
            List<Type> result = new List<Type>();
            while (type.BaseType != null)
            {
                result.Add(type.BaseType);
                type = type.BaseType;
            }
            return result;
        }
    }
}
