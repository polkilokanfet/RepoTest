using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.Market.Items;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class UnionProjectsCommand : DelegateCommandBase
    {
        private readonly Market2ViewModel _viewModel;
        private readonly IUnityContainer _container;
        private readonly IMessageService _messageService;

        public UnionProjectsCommand(Market2ViewModel viewModel, IUnityContainer container)
        {
            _viewModel = viewModel;
            _container = container;
            _messageService = container.Resolve<IMessageService>();
        }

        protected override void Execute(object parameter)
        {
            if (_messageService.ShowYesNoMessageDialog("Объединить проекты.", "Вы уверены, что хотите объединить проекты?", defaultNo: true) != MessageDialogResult.Yes)
                return;

            List<ProjectItem> projectItems = _viewModel.SelectedProjectItems.ToList();
            List<Project> projects = projectItems.Select(projectItem => projectItem.Project).Distinct().ToList();

            Project targetProject = _container.Resolve<ISelectService>().SelectItem(projects);
            if (targetProject == null) return;

            ProjectItem targetProjectItem = projectItems.First(projectItem => projectItem.Project.Id == targetProject.Id);

            IUnitOfWork unitOfWork = _container.Resolve<IUnitOfWork>();
            targetProject = unitOfWork.Repository<Project>().GetById(targetProject.Id);
            foreach (var projectItem in projectItems)
            {
                if (projectItem.Project.Id == targetProject.Id)
                    continue;

                Project project = unitOfWork.Repository<Project>().GetById(projectItem.Project.Id);
                if (project == null)
                    continue;

                //переносим юниты
                var salesUnits = unitOfWork.Repository<SalesUnit>().Find(salesUnit => salesUnit.Project.Id == project.Id);
                salesUnits.ForEach(salesUnit => salesUnit.Project = targetProject);

                //переносим ТКП
                var offers = unitOfWork.Repository<Offer>().Find(offer => offer.Project.Id == project.Id);
                offers.ForEach(offer => offer.Project = targetProject);

                //переносим конкурсы
                var tenders = unitOfWork.Repository<Tender>().Find(tender => tender.Project.Id == project.Id);
                tenders.ForEach(tender => tender.Project = targetProject);

                //переносим заметки
                var notes = project.Notes.ToList();
                targetProject.Notes.AddRange(notes);
                notes.ForEach(note => project.Notes.Remove(note));

                unitOfWork.Repository<Project>().Delete(project);
            }

            unitOfWork.SaveChanges();

            _viewModel.ReloadCommand.Execute(null);
        }

        protected override bool CanExecute(object parameter)
        {
            return _viewModel.SelectedProjectItems != null &&
                   _viewModel.SelectedProjectItems.Select(projectItem => projectItem.Project.Id).Distinct().Count() > 1;
        }
    }
}