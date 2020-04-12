using System;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Directum
{
    public class DirectumTaskRouteViewModel : ViewModelBase, IDialogRequestClose
    {
        private DateTime _finishPlan = DateTime.Now.AddDays(1).SkipWeekend();
        private DirectimTaskRouteItemWrapper _selectedDirectumTaskRouteItem;

        public DirectumTaskRouteWrapper DirectumTaskRoute { get; }

        public DateTime FinishPlan
        {
            get { return _finishPlan; }
            set
            {
                if (Equals(_finishPlan, value)) return;
                _finishPlan = value;
                DirectumTaskRoute.Items.ForEach(x => x.FinishPlan = value);
                ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
            }
        }

        public DirectimTaskRouteItemWrapper SelectedDirectumTaskRouteItem
        {
            get { return _selectedDirectumTaskRouteItem; }
            set
            {
                _selectedDirectumTaskRouteItem = value;
                OnPropertyChanged();
                ((DelegateCommand)RemovePerformerCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand AddPerformerCommand { get; }
        public ICommand RemovePerformerCommand { get; }
        public ICommand OkCommand { get; }

        public DirectumTaskRouteViewModel(IUnityContainer container, DirectumTaskRouteWrapper route) : base(container)
        {
            DirectumTaskRoute = route;

            AddPerformerCommand = new DelegateCommand(
                () =>
                {
                    var users = UnitOfWork.Repository<User>().Find(x => x.Employee.Company.Id == GlobalAppProperties.Actual.OurCompany.Id);
                    DirectumTaskRoute.Items.Select(x => x.Performer.Model).ForEach(x => users.RemoveIfContainsById(x));
                    var performer = Container.Resolve<ISelectService>().SelectItem(users);
                    if (performer != null)
                    {
                        var index = DirectumTaskRoute.Items.Count + 1;
                        var item = new DirectimTaskRouteItemWrapper(new DirectimTaskRouteItem())
                        {
                            Index = index,
                            Performer = new UserWrapper(performer),
                            FinishPlan = FinishPlan
                        };

                        DirectumTaskRoute.Items.Add(item);
                        ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
                    }
                });

            RemovePerformerCommand = new DelegateCommand(
                () =>
                {
                    DirectumTaskRoute.Items.Remove(SelectedDirectumTaskRouteItem);
                    SelectedDirectumTaskRouteItem = null;
                    ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
                },
                () => SelectedDirectumTaskRouteItem != null);

            OkCommand = new DelegateCommand(
                () =>
                {
                    if(DirectumTaskRoute.IsValid && DirectumTaskRoute.IsChanged)
                        DirectumTaskRoute.AcceptChanges();
                    CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
                },
                () => DirectumTaskRoute != null && DirectumTaskRoute.IsValid);
        }


        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}