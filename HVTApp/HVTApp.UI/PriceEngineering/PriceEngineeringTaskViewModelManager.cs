using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModelManager : PriceEngineeringTaskViewModel
    {
        public DelegateLogCommand AddTechnicalRequrementsFilesCommand { get; private set; }
        public DelegateLogCommand RemoveTechnicalRequrementsFilesCommand { get; private set; }

        #region ctors

        public PriceEngineeringTaskViewModelManager(IUnityContainer container, IUnitOfWork unitOfWork, IEnumerable<SalesUnit> salesUnits) : base(container, unitOfWork, salesUnits)
        {
        }

        public PriceEngineeringTaskViewModelManager(IUnityContainer container, IUnitOfWork unitOfWork, Product product) : base(container, unitOfWork, product)
        {
        }

        public PriceEngineeringTaskViewModelManager(IUnityContainer container, IUnitOfWork unitOfWork, PriceEngineeringTask priceEngineeringTask) : base(container, unitOfWork, priceEngineeringTask)
        {
        }
        
        #endregion

        protected override void InCtor()
        {
            base.InCtor();

            AddTechnicalRequrementsFilesCommand = new DelegateLogCommand(
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
                            var fileWrapper = new PriceEngineeringTaskFileTechnicalRequirementsWrapper(new PriceEngineeringTaskFileTechnicalRequirements())
                            {
                                Name = Path.GetFileNameWithoutExtension(fileName).LimitLengh(50),
                                Path = fileName
                            };
                            this.FilesTechnicalRequirements.Add(fileWrapper);
                        }
                    }
                });

            RemoveTechnicalRequrementsFilesCommand = new DelegateLogCommand(
                () =>
                {
                    if (UnitOfWork.Repository<PriceEngineeringTask>().GetById(Id) == null)
                    {
                        this.FilesTechnicalRequirements.Remove(SelectedTechnicalRequrementsFile);
                    }
                },
                () => this.SelectedTechnicalRequrementsFile != null);

            this.SelectedTechnicalRequrementsFileIsChanged += () =>
            {
                RemoveTechnicalRequrementsFilesCommand.RaiseCanExecuteChanged();
            };
        }


    }
}