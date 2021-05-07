using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Services.GetProductService.Complects
{
    public class ComplectsViewModel : ViewModelBase
    {
        private Complect _selectedItem;
        public ObservableCollection<Complect> Items { get; }

        public Complect SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
                ((DelegateCommand)SelectCommand).RaiseCanExecuteChanged();
            }
        }

        public bool IsSelected { get; private set; } = false;

        public ICommand SelectCommand { get; }
        public ICommand NewComplectCommand { get; }

        public ComplectsViewModel(IUnityContainer container) : base(container)
        {
            var complectsParameter = GlobalAppProperties.Actual.ComplectsParameter;
        var complects = UnitOfWork.Repository<Product>().Find(x => x.ProductBlock.Parameters.ContainsById(complectsParameter)).Select(x => new Complect(x));
            Items = new ObservableCollection<Complect>(complects);

            SelectCommand = new DelegateCommand(
                () =>
                {
                    IsSelected = true;
                    SelectEvent?.Invoke();
                }, 
                () => SelectedItem != null);

            NewComplectCommand = new DelegateCommand(
                () =>
                {
                    var complectViewModel = Container.Resolve<ComplectViewModel>();
                    complectViewModel.ShowDialog();
                    if (complectViewModel.IsSaved)
                    {
                        var complect = new Complect(complectViewModel.Product.Model);
                        Items.Add(complect);
                        SelectedItem = complect;
                    }
                });
        }

        public void ShowDialog()
        {
            var complectsWindow = new ComplectsWindow(this);
            complectsWindow.ShowDialog();
        }

        public event Action SelectEvent;
    }
}