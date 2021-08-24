using System;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public partial class UserLookupListViewModel
    {
        public DelegateLogCommand RemovePasswordCommand { get; private set; }

        protected override void InitSpecialCommands()
        {
            RemovePasswordCommand = new DelegateLogCommand(
                () =>
                {
                    var dr = MessageService.ShowYesNoMessageDialog("Сброс пароля", "Вы действительно хотите сбросить пароль выбранному пользователю?");
                    if (dr != MessageDialogResult.Yes) return;

                    var unitOfWork = Container.Resolve<IUnitOfWork>();
                    var user = unitOfWork.Repository<User>().GetById(SelectedItem.Id);
                    user.Password = Guid.Empty;
                    unitOfWork.SaveChanges();
                    EventAggregator.GetEvent<AfterSaveUserEvent>().Publish(user);
                }, 
                () => SelectedItem != null);

            this.SelectedLookupChanged += lookup => RemovePasswordCommand.RaiseCanExecuteChanged();
        }
    }
}