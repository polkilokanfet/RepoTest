﻿using System.Windows;
using System.Windows.Controls;
using HVTApp.UI.PriceEngineering.ViewModel;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class CommonInformationControlInspector : UserControl
    {
        public static readonly DependencyProperty TasksViewModelProperty = DependencyProperty.Register(
            "TasksViewModel", typeof(TasksViewModelInspector), typeof(CommonInformationControlInspector), new PropertyMetadata(default(TasksViewModelInspector)));

        public TasksViewModelInspector TasksViewModel
        {
            get => (TasksViewModelInspector) GetValue(TasksViewModelProperty);
            set => SetValue(TasksViewModelProperty, value);
        }

        public CommonInformationControlInspector()
        {
            InitializeComponent();
        }
    }
}