using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Tce.Second
{
    public class TasksTceViewModelBackManagerBoss : TasksTceViewModel
    {
        public DelegateLogCommand InstructCommand { get; }

        public TasksTceViewModelBackManagerBoss(IUnityContainer container) : base(container)
        {            
            InstructCommand = new DelegateLogCommand(
                () =>
                {
                    var users = UnitOfWork.Repository<User>().Find(x => x.Roles.Select(r => r.Role).Contains(Role.BackManager));
                    var user = container.Resolve<ISelectService>().SelectItem(users);
                    if (user != null)
                    {
                        this.Item.BackManager = new UserEmptyWrapper(user);
                        SaveCommand.Execute(null);
                        InstructCommand.RaiseCanExecuteChanged();
                    }
                },
                () => this.Item != null && Item.IsValid );

            this.ViewModelIsLoaded += () => InstructCommand.RaiseCanExecuteChanged();
        }
    }
}