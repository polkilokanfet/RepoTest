using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModelConstructor : PriceEngineeringTaskViewModel
    {
        /// <summary>
        /// Выбрать блок продукта
        /// </summary>
        public DelegateLogCommand SelectProductBlockCommand { get; private set; }

        public DelegateLogCommand AddAnswerFilesCommand { get; private set; }

        #region ctors

        public PriceEngineeringTaskViewModelConstructor(IUnityContainer container, IUnitOfWork unitOfWork, PriceEngineeringTask priceEngineeringTask) : base(container, unitOfWork, priceEngineeringTask)
        {
        }

        public PriceEngineeringTaskViewModelConstructor(IUnityContainer container, IUnitOfWork unitOfWork, IEnumerable<SalesUnit> salesUnits) : base(container, unitOfWork, salesUnits)
        {
            throw new System.NotImplementedException();
        }

        public PriceEngineeringTaskViewModelConstructor(IUnityContainer container, IUnitOfWork unitOfWork, Product product) : base(container, unitOfWork, product)
        {
            throw new System.NotImplementedException();
        }
        

        #endregion

        protected override void InCtor()
        {
            base.InCtor();

            SelectProductBlockCommand = new DelegateLogCommand(
                () =>
                {
                    var department = UnitOfWork.Repository<DesignDepartment>()
                        .Find(designDepartment => designDepartment.ProductBlockIsSuitable(ProductBlockManager.Model))
                        .FirstOrDefault();

                    if (department == null)
                        return;

                    var requiredParameters = department.ParameterSets
                        .FirstOrDefault(x => x.Parameters.AllContainsInById(ProductBlockManager.Model.Parameters));

                    var getProductService = Container.Resolve<IGetProductService>();
                    var originProductBlock = this.ProductBlockEngineer.Model;
                    var selectedProductBlock = getProductService.GetProductBlock(originProductBlock, requiredParameters.Parameters);
                    if (originProductBlock.Id != selectedProductBlock.Id)
                    {
                        this.ProductBlockEngineer = new ProductBlockEmptyWrapper(selectedProductBlock);
                    }
                },
                () =>
                {
                    return true;
                });

            AddAnswerFilesCommand = new DelegateLogCommand(
                () =>
                {
                    var openFileDialog = new OpenFileDialog
                    {
                        Multiselect = true,
                        RestoreDirectory = true
                    };

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //копируем каждый файл
                        foreach (var fileName in openFileDialog.FileNames)
                        {
                            var fileWrapper = new PriceEngineeringTaskFileAnswerWrapper(new PriceEngineeringTaskFileAnswer())
                            {
                                Name = Path.GetFileNameWithoutExtension(fileName).LimitLengh(50),
                                Path = fileName
                            };
                            this.FilesAnswers.Add(fileWrapper);
                        }
                    }
                });
        }
    }
}