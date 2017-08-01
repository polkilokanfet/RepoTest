using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.Model.POCOs;
using HVTApp.Services.ChooseService.Annotations;

namespace HVTApp.Services.GetProductService
{
    public class ParametersToSelect : INotifyPropertyChanged
    {
        private Parameter _selectedParameter;
        private IList<Parameter> _parameters; 

        public ParametersToSelect(IEnumerable<Parameter> parameters)
        {
            _parameters = new List<Parameter>(parameters);
        }

        public ObservableCollection<Parameter> Parameters { get; set; } = new ObservableCollection<Parameter>();

        public Parameter SelectedParameter
        {
            get { return _selectedParameter; }
            set
            {
                if (_selectedParameter.Equals(value)) return;
                _selectedParameter = value;
                OnPropertyChanged();
            }
        }


        public void ReloadParameters()
        {
            
        }


        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}