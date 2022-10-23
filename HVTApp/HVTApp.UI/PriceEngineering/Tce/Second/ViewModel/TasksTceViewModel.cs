using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
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
        public virtual bool AllowEdit => GlobalAppProperties.User.RoleCurrent == Role.BackManager;

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
                    files.Add(new FileCopyStorage(fileTechnicalRequirement, $"{pet.Number}-TechReq", GlobalAppProperties.Actual.TechnicalRequrementsFilesPath));
                }

                foreach (var answer in pet.FilesAnswers)
                {
                    files.Add(new FileCopyStorage(answer, $"{pet.Number}-Answer", GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath));
                }
            }

            Container.Resolve<IFilesStorageService>().GetZipFolder(files, $"{priceEngineeringTask.Number}_{DateTime.Now.ToShortDateString().ReplaceUncorrectSimbols()}");
        }

        private class FileCopyStorage : IFileCopyStorage
        {
            public IFileStorage File { get; }
            public string TargetPath { get; }

            public string SourcePath { get; }

            public FileCopyStorage(IFileStorage file, string targetPathName, string sourcePath)
            {
                File = file;
                TargetPath = targetPathName;
                SourcePath = sourcePath;
            }
        }
    }
}