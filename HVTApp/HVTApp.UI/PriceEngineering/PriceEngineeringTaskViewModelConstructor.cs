using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.BlockChooser;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModelConstructor : PriceEngineeringTaskViewModel
    {
        public override bool IsTarget => UserConstructor != null && Equals(Model.UserConstructor.Id, GlobalAppProperties.User.Id);

        public override bool IsEditMode
        {
            get
            {
                switch (Status)
                {
                    case PriceEngineeringTaskStatusEnum.Started:
                    case PriceEngineeringTaskStatusEnum.RejectedByManager:
                        return true;

                    case PriceEngineeringTaskStatusEnum.Created:
                    case PriceEngineeringTaskStatusEnum.Stopped:
                    case PriceEngineeringTaskStatusEnum.RejectedByConstructor:
                    case PriceEngineeringTaskStatusEnum.FinishedByConstructor:
                    case PriceEngineeringTaskStatusEnum.Accepted:
                        return false;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

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

                    var requiredParameters = department
                        .ParameterSets
                        .FirstOrDefault(x => x.Parameters.AllContainsInById(ProductBlockManager.Model.Parameters));

                    var originProductBlock = this.ProductBlockEngineer.Model;
                    var productBlockStructureCostViewModel = new ProductBlockStructureCostViewModel(Container, originProductBlock, requiredParameters);
                    var selectedProductBlock = productBlockStructureCostViewModel.Run();
                    if (productBlockStructureCostViewModel.Result == true && originProductBlock.Id != selectedProductBlock.Id)
                    {
                        this.ProductBlockEngineer = new ProductBlockEmptyWrapper(UnitOfWork.Repository<ProductBlock>().GetById(selectedProductBlock.Id));
                    }
                },
                () => IsTarget && IsEditMode);

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