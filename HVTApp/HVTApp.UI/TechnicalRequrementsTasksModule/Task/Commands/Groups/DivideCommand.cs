using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class DivideCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public DivideCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {

            var result = MessageService.ConfirmationDialog("–азбиение", "ƒействительно хотите разбить выбранную строку?", defaultNo: true);
            if (result == false) return;

            var technicalRequrementsWrapper = (TechnicalRequrements2Wrapper)ViewModel.SelectedItem;
            var salesUnit = technicalRequrementsWrapper.SalesUnits.First();

            var salesUnitsToDivide = technicalRequrementsWrapper.SalesUnits.ToList();
            salesUnitsToDivide.Remove(salesUnit);

            //создаем новые строки
            foreach (var unit in salesUnitsToDivide)
            {
                technicalRequrementsWrapper.SalesUnits.Remove(unit);

                //создаем новую строку
                var newTechnicalRequrements = new TechnicalRequrements
                {
                    SalesUnits = new List<SalesUnit> { unit.Model },
                    Comment = technicalRequrementsWrapper.Comment
                };
                var newTechnicalRequrementsWrapper = new TechnicalRequrements2Wrapper(newTechnicalRequrements);

                //добавл€ем в новую строку файлы
                foreach (var fileWrapper in technicalRequrementsWrapper.Files)
                {
                    newTechnicalRequrementsWrapper.Files.Add(fileWrapper);
                }

                ViewModel.TechnicalRequrementsTaskWrapper.Requrements.Add(newTechnicalRequrementsWrapper);
            }
        }

        protected override bool CanExecuteMethod()
        {
            return !ViewModel.IsStarted &&
                   ViewModel.SelectedItem is TechnicalRequrements2Wrapper &&
                   ((TechnicalRequrements2Wrapper) ViewModel.SelectedItem).Amount > 1;
        }
    }
}