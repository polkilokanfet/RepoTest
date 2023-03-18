using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandStart : DoStepCommand
    {
        protected override ScriptStep Step => ScriptStep.Start;
        protected override string ConfirmationMessage => "Вы уверены, что хотите запустить проработку?";

        public DoStepCommandStart(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationArgsItem> GetEventServiceItems()
        {
            if(ViewModel.Model.UserConstructor != null)
                yield return new NotificationArgsItem(ViewModel.Model.UserConstructor, Role.Constructor, $"Проработайте ТСП: {ViewModel.Model}");
            else if (ViewModel.Model.DesignDepartment != null)
                yield return new NotificationArgsItem(ViewModel.Model.DesignDepartment.Head, Role.DesignDepartmentHead, $"Поручите ТСП: {ViewModel.Model}");
        }

        protected override bool CanExecuteMethod()
        {
            return base.CanExecuteMethod() && ViewModel.IsChanged;
        }

        protected override string GetStatusComment()
        {
            var sb = new StringBuilder();
            if (this.ViewModel.FilesTechnicalRequirements.IsChanged)
            {
                sb.AppendLine("Внесены изменения в Техническое Задание.");

                var actualFiles = this.ViewModel.FilesTechnicalRequirements
                    .Where(file => file.IsActual)
                    .OrderBy(file => file.CreationMoment)
                    .ToList();
                if (actualFiles.Any())
                {
                    sb.AppendLine("Актуальные файлы:");
                    actualFiles.ForEach(file => sb.AppendLine($" + {file.CreationMoment} {file.Name}"));
                }

                var notActualFiles = this.ViewModel.FilesTechnicalRequirements
                    .Where(file => file.IsActual == false)
                    .OrderBy(file => file.CreationMoment)
                    .ToList();
                if (notActualFiles.Any())
                {
                    sb.AppendLine("Не актуальные файлы:");
                    notActualFiles.ForEach(file => sb.AppendLine($" - {file.CreationMoment} {file.Name}"));
                }
            }

            return sb.ToString().TrimEnd('\n', '\r');
        }
    }
}