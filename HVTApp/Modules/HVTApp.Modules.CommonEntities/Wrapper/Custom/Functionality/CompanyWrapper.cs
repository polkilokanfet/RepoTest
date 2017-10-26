using System;
using System.Collections.Generic;

namespace HVTApp.UI.Wrapper
{
    public partial class CompanyWrapper
    {
        public IEnumerable<CompanyWrapper> GetAllChilds()
        {
            //List<CompanyWrapper> childs = this.ChildCompanies.ToList();
            //List<CompanyWrapper> result = new List<CompanyWrapper>(childs);

            //foreach (var child in childs)
            //    result.AddRange(child.GetAllChilds());

            //return result;
            throw new NotImplementedException();
        }
    }
}