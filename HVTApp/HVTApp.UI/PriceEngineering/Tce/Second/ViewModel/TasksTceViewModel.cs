using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
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
            IList<IFileCopyStorage> files = new List<IFileCopyStorage>();
            foreach (var pet in priceEngineeringTask.GetAllPriceEngineeringTasks().ToList())
            {
                foreach (var fileTechnicalRequirement in pet.FilesTechnicalRequirements)
                {
                    files.Add(new FileCopyStorage(fileTechnicalRequirement, $"{pet.GetDirectoryName()}-TechReq", GlobalAppProperties.Actual.TechnicalRequrementsFilesPath));
                }

                foreach (var answer in pet.FilesAnswers)
                {
                    files.Add(new FileCopyStorage(answer, $"{pet.GetDirectoryName()}-Answer", GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath));
                }
            }

            var filesStorageService = Container.Resolve<IFilesStorageService>();
            try
            {
                var zipFilePath = filesStorageService.GetZipFolder(files, $"{priceEngineeringTask.Number}_{DateTime.Now.ToShortDateString().ReplaceUncorrectSimbols()}");
                if (string.IsNullOrEmpty(zipFilePath) == false)
                {
                    var rr = Container.Resolve<IPrintPriceEngineering>().PrintPriceEngineeringTask(priceEngineeringTask.Id);
                    if (string.IsNullOrEmpty(rr) == false)
                    {
                        filesStorageService.AddFilesToZip(zipFilePath, new []{rr});
                        System.Diagnostics.Process.Start(Path.GetDirectoryName(zipFilePath));
                    }
                }
            }
            catch (IOException e)
            {
                Container.Resolve<IMessageService>().Message(e.GetType().ToString(), e.Message);
            }
        }

        private class FileCopyStorage : IFileCopyStorage
        {
            public IFileStorage File { get; }
            public string DestinationDirectoryName { get; }
            public string SourcePath { get; }

            public FileCopyStorage(IFileStorage file, string destinationDirectoryNameName, string sourcePath)
            {
                File = file;
                DestinationDirectoryName = destinationDirectoryNameName;
                SourcePath = sourcePath;
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