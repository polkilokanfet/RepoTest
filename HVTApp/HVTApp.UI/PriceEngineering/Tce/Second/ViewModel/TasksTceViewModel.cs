using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.Services.Storage;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Tce.Second
{
    public abstract class TasksTceViewModel : BaseDetailsViewModel<TasksTceWrapper, PriceEngineeringTasks, AfterSavePriceEngineeringTasksEvent>
    {
        public virtual bool AllowEdit => GlobalAppProperties.UserIsBackManager;

        protected TasksTceViewModel(IUnityContainer container) : base(container)
        {
            this.ViewModelIsLoaded += () =>
            {
                RaisePropertyChanged(nameof(AllowEdit));
                this.Item.PropertyChanged += (sender, args) => RaisePropertyChanged(nameof(AllowEdit));
            };
        }

        protected override void AfterLoading()
        {
            this.Item.LoadFilesRequest +=
                task =>
                {
                    this.LoadZipInfo(task);
                    //var files = task.FilesTechnicalRequirements.Where(x => x.IsActual).ToList();
                    //if (files.Any())
                    //    Container.Resolve<IFilesStorageService>().CopyFilesFromStorage(files, GlobalAppProperties.Actual.TechnicalRequrementsFilesPath);
                };

            base.AfterLoading();
        }

        private void LoadZipInfo(PriceEngineeringTask priceEngineeringTask)
        {
            var files = priceEngineeringTask.GetFileCopyInfoEntities();

            var filesStorageService = Container.Resolve<IFilesStorageService>();
            try
            {
                var zipFilePath = filesStorageService.GetZipFolder(files, $"{priceEngineeringTask.Number}_{DateTime.Now.ToShortDateString().ReplaceUncorrectSimbols()}");
                if (string.IsNullOrEmpty(zipFilePath) == false)
                {
                    var historyFilePath = Container.Resolve<IPrintPriceEngineering>().PrintHistoryPriceEngineeringTask(priceEngineeringTask.Id);
                    if (string.IsNullOrEmpty(historyFilePath) == false)
                    {
                        filesStorageService.AddFilesToZip(zipFilePath, new []{historyFilePath});
                        System.Diagnostics.Process.Start(Path.GetDirectoryName(zipFilePath));
                    }
                }
            }
            catch (IOException e)
            {
                Container.Resolve<IMessageService>().Message(e.GetType().ToString(), e.Message);
            }
        }

        public void Load(PriceEngineeringTask priceEngineeringTask)
        {
            throw new NotImplementedException();
            //IsLoaded = false;
            //UnitOfWork = Container.Resolve<IUnitOfWork>();
            //priceEngineeringTask = UnitOfWork.Repository<PriceEngineeringTask>().GetById(priceEngineeringTask.Id);
            //Item = item == null ? CreateWrapper(entity) : CreateWrapper(item);
            //AfterLoading();
        }
    }
}