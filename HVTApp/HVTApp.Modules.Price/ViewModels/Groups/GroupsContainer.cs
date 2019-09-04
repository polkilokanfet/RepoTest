using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels.Groups
{
    public class GroupsContainer<TGroup> : ObservableCollection<TGroup>
        where TGroup : GroupBase<TGroup>
    {
        private TGroup _selectedGroup;

        public TGroup SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                if (Equals(_selectedGroup, value)) return;
                _selectedGroup = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedGroup)));
                SelectedGroupChanged?.Invoke(_selectedGroup);
            }
        }

        public event Action<TGroup> SelectedGroupChanged;
    }
}