﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HVTApp.Model.Wrapper.Annotations;

namespace HVTApp.Model.Wrapper
{
    public class Observable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        private readonly List<WhoRised> _whoRisedEventPropertyChanged = new List<WhoRised>();

        protected virtual void OnPropertyChanged(object sender, string propertyName)
        {
            WhoRised whoRised = new WhoRised(sender, propertyName);
            if (!_whoRisedEventPropertyChanged.Contains(whoRised))
            {
                _whoRisedEventPropertyChanged.Add(whoRised);
                //OnPropertyChanged(propertyName);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                _whoRisedEventPropertyChanged.Remove(whoRised);
            }
        }

        protected event ComplexPropertyChangedEventHandler ComplexPropertyChanged;

        protected virtual void OnComplexPropertyChanged(object oldpropval, object newpropval, [CallerMemberName] string propertyname = null)
        {
            ComplexPropertyChanged?.Invoke(oldpropval, newpropval, propertyname);
        }
    }

    public delegate void ComplexPropertyChangedEventHandler(object oldPropVal, object newPropVal, string propertyName);
}
