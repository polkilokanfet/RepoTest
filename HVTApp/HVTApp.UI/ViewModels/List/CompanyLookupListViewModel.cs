using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class CompanyLookupListViewModel
    {
        protected override void OnAfterSaveEntity(Company company)
        {
            base.OnAfterSaveEntity(company);
            var companies = Lookups.Where(companyLookup => companyLookup.ParentCompany != null && companyLookup.ParentCompany.Id == company.Id);
            foreach (var companyLookup in companies)
            {
                companyLookup.Entity.ParentCompany = company;
                companyLookup.Refresh();
            }
        }

        protected override bool UnionItemsAction(IUnitOfWork unitOfWork, Company mainCompany, List<Company> otherItems)
        {
            foreach (var otherCompany in otherItems)
            {
                //������ �������� � ������������
                List<Company> companies = unitOfWork.Repository<Company>().Find(company => Equals(company.ParentCompany, otherCompany)).ToList();
                companies.ForEach(company => company.ParentCompany = mainCompany);

                //������ ������������ � ����������
                List<Contract> contracts = unitOfWork.Repository<Contract>().Find(contract => Equals(contract.Contragent, otherCompany)).ToList();
                contracts.ForEach(contract => contract.Contragent = mainCompany);

                //������ �������� � �����������
                List<Employee> employees = unitOfWork.Repository<Employee>().Find(employee => Equals(employee.Company, otherCompany)).ToList();
                employees.ForEach(employee => employee.Company = mainCompany);

                //������ �������� � ��������
                List<Facility> facilities = unitOfWork.Repository<Facility>().Find(facility => Equals(facility.OwnerCompany, otherCompany)).ToList();
                facilities.ForEach(employee => employee.OwnerCompany = mainCompany);
                
                //������ ��������-�������������
                List<SalesUnit> salesUnits = unitOfWork.Repository<SalesUnit>().Find(salesUnit => Equals(salesUnit.Producer, otherCompany)).ToList();
                salesUnits.ForEach(salesUnit => salesUnit.Producer = mainCompany);

                //������ �������� � �������� - ����������
                List<Tender> tenders = unitOfWork.Repository<Tender>().Find(tender => Equals(tender.Winner, otherCompany)).ToList();
                tenders.ForEach(tender => tender.Winner = mainCompany);

                //������ �������� � �������� - ���������
                tenders = unitOfWork.Repository<Tender>().Find(tender => tender.Participants.Contains(otherCompany)).ToList();
                foreach (var tender in tenders)
                {
                    tender.Participants.Remove(otherCompany);
                    tender.Participants.Add(mainCompany);
                }
            }

            return true;
        }
    }
}