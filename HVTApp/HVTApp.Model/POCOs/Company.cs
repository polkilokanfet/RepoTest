﻿using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Company : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public virtual CompanyForm Form { get; set; }
        public virtual Company ParentCompany { get; set; }
        public virtual Address Address { get; set; }
        public virtual BankDetails BankDetails { get; set; }
        public virtual List<Company> ChildCompanies { get; set; } = new List<Company>();
        public virtual List<ActivityField> ActivityFilds { get; set; } = new List<ActivityField>();
        public virtual List<Employee> Employees { get; set; } = new List<Employee>();



        public override string ToString()
        {
            return $"{FullName}, {Form.ShortName}";
        }
    }
}