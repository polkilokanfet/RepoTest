using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Products.StructureCostsNumbers
{
    public class StructureCostsNumbersViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private StructureCostsNumber _selectedItem;

        public IEnumerable<StructureCostsNumber> Items { get; private set; }

        public StructureCostsNumber SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (Equals(_selectedItem, value))
                    return;

                _selectedItem = value;

                if (_selectedItem != null && _selectedItem.IsChanged)
                    _selectedItem.RejectChanges();

                RaisePropertyChanged();
            }
        }

        public DelegateLogCommand SaveCommand { get; }

        public StructureCostsNumbersViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Load();

            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    _selectedItem.AcceptChanges();
                    _unitOfWork.SaveChanges();
                    SaveCommand.RaiseCanExecuteChanged();
                },
                () => SelectedItem != null && SelectedItem.IsValid && SelectedItem.IsChanged);
        }

        private void Load()
        {
            var departments = _unitOfWork.Repository<DesignDepartment>()
                .Find(department => department.Head.Id == GlobalAppProperties.User.Id);

            var blocks = _unitOfWork.Repository<ProductBlock>()
                .Find(block => departments.Any(department => department.ProductBlockIsSuitable(block)));

            Items = new List<StructureCostsNumber>(
                blocks
                    .OrderBy(block => block)
                    .Select(block => new StructureCostsNumber(block)));

            Items.ForEach(x => x.PropertyChanged += ItemOnPropertyChanged);
        }

        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.SaveCommand.RaiseCanExecuteChanged();
        }
    }
}