using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Prism.Commands;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompanyDetailsWindowModel : BaseDetailsViewModel<CompanyWrapper, Company>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISelectService _selectService;

        public CompanyDetailsWindowModel(IUnitOfWork unitOfWork, ISelectService selectService, CompanyWrapper item) :
            base(item)
        {
            _unitOfWork = unitOfWork;
            _selectService = selectService;

            Forms = new ObservableCollection<CompanyFormWrapper>(_unitOfWork.CompanyForms.GetAll());

            SelectParentCompanyCommand = new DelegateCommand(SelectParentCompanyCommand_Execute);
            RemoveParentCompanyCommand = new DelegateCommand(RemoveParentCompanyCommand_Execute);
            AddActivityFieldCommand = new DelegateCommand(AddActivityFieldCommand_Execute);
        }

        public DelegateCommand SelectParentCompanyCommand { get; }
        public DelegateCommand RemoveParentCompanyCommand { get; }
        public DelegateCommand AddActivityFieldCommand { get; }

        public CompanyWrapper Company => Item;

        public ObservableCollection<CompanyFormWrapper> Forms { get; }

        private void AddActivityFieldCommand_Execute()
        {
            var fields = _unitOfWork.ActivityFields.GetAll().Except(Company.ActivityFilds);
            var field = _selectService.SelectItem(fields);
            if (field != null && !Company.ActivityFilds.Contains(field))
                Company.ActivityFilds.Add(field);
        }

        private void RemoveParentCompanyCommand_Execute()
        {
            //если головная компания не назначена
            if (Company.ParentCompany == null) return;
            //удаляем из списка дочерних компаний бывшей головной компании текущую компанию
            Company.ParentCompany.ChildCompanies.Remove(Company);
            //удалаем головную компанию текущей компании
            Company.ParentCompany = null;
        }

        private void SelectParentCompanyCommand_Execute()
        {
            //компании, которые не могут быть головной (дочернии и т.д.)
            IEnumerable<CompanyWrapper> exceptCompanies = Company.GetAllChilds().Concat(new[] {this.Company});
            //возможные головные компании
            IEnumerable<CompanyWrapper> possibleParents = _unitOfWork.Companies.GetAll().Except(exceptCompanies);
            //выбор одной из компаний
            CompanyWrapper possibleParent = _selectService.SelectItem(possibleParents, Company.ParentCompany);

            if (possibleParent != null && !Equals(possibleParent, Company.ParentCompany))
            {
                RemoveParentCompanyCommand_Execute();
                Company.ParentCompany = possibleParent;
            }
        }
    }
}
