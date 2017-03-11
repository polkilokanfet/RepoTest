﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using HVTApp.Model.Wrapper.Annotations;

namespace HVTApp.Model.Wrapper
{
    public class Observable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
