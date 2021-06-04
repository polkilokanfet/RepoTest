using HVTApp.Model.POCOs;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Products.LaborHours
{
    public class Block : BindableBase
    {
        private bool _hasLaborHours;
        public ProductBlock ProductBlock { get; }

        public bool HasLaborHours
        {
            get => _hasLaborHours;
            set
            {
                _hasLaborHours = value;
                RaisePropertyChanged();
            }
        }

        public Block(ProductBlock productBlock)
        {
            ProductBlock = productBlock;
        }
    }
}