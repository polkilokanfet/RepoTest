using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public class ProjectTypes : IEnumerable<ProductType>
    {
        private readonly ProjectWrapper1 _projectWrapper1;
        private readonly List<ProductType> _productTypes;
        private ProductType _selectedProductType;

        public ProductType SelectedProductType
        {
            get => _selectedProductType;
            set
            {
                if (Equals(_selectedProductType, value)) return;
                _selectedProductType = value;
                this._projectWrapper1.ProjectTypeId = value.Id;
            }
        }

        public ProjectTypes(IUnitOfWork unitOfWork, ProjectWrapper1 projectWrapper1)
        {
            _productTypes = unitOfWork.Repository<ProductType>().GetAllAsNoTracking();
            _projectWrapper1 = projectWrapper1;
            if (_projectWrapper1.ProjectTypeId != Guid.Empty)
                _selectedProductType = _productTypes.Single(productType => productType.Id == _projectWrapper1.ProjectTypeId);
        }

        public IEnumerator<ProductType> GetEnumerator()
        {
            return _productTypes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}