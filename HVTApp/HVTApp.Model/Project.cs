using System;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        /// <summary>
        /// Ориентировочная дата реализации проекта.
        /// </summary>
        public DateTime EstimatedDate { get; set; }

        public virtual User Manager { get; set; }
        public virtual List<ProductsMainGroup> ProductsMainGroups { get; set; } 
        public virtual List<Tender> Tenders { get; set; } 
        public virtual List<Offer> Offers { get; set; }


        
        /// <summary>
        /// Общая стоимость проекта.
        /// </summary>
        public double Sum => ProductsMainGroups.Sum(x => x.SumWithVat);


        
        /// <summary>
        /// Проектировщик
        /// </summary>
        public Company ProjectMaker => Tenders.FirstOrDefault(x => x.Type == TenderType.ToProject)?.Winner;

        /// <summary>
        /// Подрядчик.
        /// </summary>
        public Company Worker
        {
            get
            {
                Company company = Tenders.FirstOrDefault(x => x.Type == TenderType.ToWork)?.Winner;
                if (company != null) return company;

                return Tenders.FirstOrDefault(x => x.Type == TenderType.ToWorkAndSupply)?.Winner;
            }
        }

        /// <summary>
        /// Поставщик.
        /// </summary>
        public Company Supplier
        {
            get
            {
                Company company = Tenders.FirstOrDefault(x => x.Type == TenderType.ToSupply)?.Winner;
                if (company != null) return company;

                return Tenders.FirstOrDefault(x => x.Type == TenderType.ToWorkAndSupply)?.Winner;
            }
        }

    }
}