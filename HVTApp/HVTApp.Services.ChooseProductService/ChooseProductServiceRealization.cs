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

        public IList<UnionOfParameters> UnionsOfParameters { get; }
        public IEnumerable<ParameterWrapper> SelectedParameters => UnionsOfParameters.Where(x => x.IsActual).Select(x => x.SelectedParameter);

        public ChooseProductServiceRealization(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var parameters = unitOfWork.Parameters.GetAll().OrderBy(x => x.Rank);

            UnionsOfParameters = new List<UnionOfParameters>();
            var groups = parameters.Select(x => x.Group).Distinct();
            foreach (var group in groups)
            {
                var unionOfParameters = new UnionOfParameters(parameters.Where(x => Equals(x.Group, group)).OrderBy(x => x.Value));
                UnionsOfParameters.Add(unionOfParameters);
                unionOfParameters.UnionChanged += OnUnionOfParametersChanged;
            }
        }

        private void OnUnionOfParametersChanged(object sender, EventArgs eventArgs)
        {
            foreach (var unionOfParameters in UnionsOfParameters)
                unionOfParameters.RefreshParametersToSelect(SelectedParameters);
        }


        public Product ChooseProduct(ProductWrapper product = null)
        {
            IDialogRequestClose wm = new SelectParametersWindowModel(this.UnionsOfParameters, product);
            SelectParametersWindow window = new SelectParametersWindow();
            window.DataContext = wm;
            wm.CloseRequested += (sender, args) => { window.Close(); };
            
            window.ShowDialog();
            return null;
        }
    }
}
