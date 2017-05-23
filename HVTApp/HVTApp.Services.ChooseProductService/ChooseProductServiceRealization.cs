using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.Services.ChooseProductService
{
    public class ChooseProductServiceRealization : IChooseProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEnumerable<ParameterWrapper> _parameters;

        public IEnumerable<UnionOfParameters> UnionsOfParameters
        {
            get
            {
                foreach (var group in _parameters.Select(x => x.Group).Distinct())
                    yield return new UnionOfParameters(_parameters.Where(x => Equals(x.Group, group)).OrderBy(x => x.Value));
            }
        }

        public ChooseProductServiceRealization(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _parameters = unitOfWork.Parameters.GetAll().OrderBy(x => x.Rank);
        }

        public Product ChooseProduct(ProductWrapper originProduct = null)
        {
            IDialogRequestClose windowModel = new SelectParametersWindowModel(UnionsOfParameters, originProduct);
            SelectParametersWindow window = new SelectParametersWindow();
            window.DataContext = windowModel;
            windowModel.CloseRequested += (sender, args) =>
            {
                window.Close();
            };
            
            window.ShowDialog();
            return null;
        }
    }
}
