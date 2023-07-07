using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Products.Parameters
{
    public partial class BlocksControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(BlocksControl), new PropertyMetadata(default(string)));

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }


        public static readonly DependencyProperty SelectedParameterInBlockProperty = DependencyProperty.Register(
            "SelectedParameterInBlock", typeof(Parameter), typeof(BlocksControl), new PropertyMetadata(default(Parameter)));

        public Parameter SelectedParameterInBlock
        {
            get { return (Parameter) GetValue(SelectedParameterInBlockProperty); }
            set { SetValue(SelectedParameterInBlockProperty, value); }
        }


        public static readonly DependencyProperty SelectedBlockProperty = DependencyProperty.Register(
            "SelectedBlock", typeof(ProductBlock), typeof(BlocksControl), new PropertyMetadata(default(ProductBlock)));

        public ProductBlock SelectedBlock
        {
            get { return (ProductBlock) GetValue(SelectedBlockProperty); }
            set { SetValue(SelectedBlockProperty, value); }
        }


        public static readonly DependencyProperty BlocksProperty = DependencyProperty.Register(
            "Blocks", typeof(IEnumerable<ProductBlock>), typeof(BlocksControl), new PropertyMetadata(default(IEnumerable<ProductBlock>)));

        public IEnumerable<ProductBlock> Blocks
        {
            get { return (IEnumerable<ProductBlock>) GetValue(BlocksProperty); }
            set { SetValue(BlocksProperty, value); }
        }

        public static readonly DependencyProperty LoadBlocksCommandProperty = DependencyProperty.Register(
            "LoadBlocksCommand", typeof(ICommand), typeof(BlocksControl), new PropertyMetadata(default(ICommand)));

        public ICommand LoadBlocksCommand
        {
            get { return (ICommand) GetValue(LoadBlocksCommandProperty); }
            set { SetValue(LoadBlocksCommandProperty, value); }
        }


        public static readonly DependencyProperty AddByBlockCommandProperty = DependencyProperty.Register(
            "AddByBlockCommand", typeof(ICommand), typeof(BlocksControl), new PropertyMetadata(default(ICommand)));

        public ICommand AddByBlockCommand
        {
            get { return (ICommand) GetValue(AddByBlockCommandProperty); }
            set { SetValue(AddByBlockCommandProperty, value); }
        }

        public static readonly DependencyProperty AddCommandProperty = DependencyProperty.Register(
            "AddCommand", typeof(ICommand), typeof(BlocksControl), new PropertyMetadata(default(ICommand)));

        public ICommand AddCommand
        {
            get { return (ICommand) GetValue(AddCommandProperty); }
            set { SetValue(AddCommandProperty, value); }
        }


        public static readonly DependencyProperty RemoveCommandProperty = DependencyProperty.Register(
            "RemoveCommand", typeof(ICommand), typeof(BlocksControl), new PropertyMetadata(default(ICommand)));

        public ICommand RemoveCommand
        {
            get { return (ICommand) GetValue(RemoveCommandProperty); }
            set { SetValue(RemoveCommandProperty, value); }
        }


        public static readonly DependencyProperty ParametersProperty = DependencyProperty.Register(
            "Parameters", typeof(IEnumerable<Parameter>), typeof(BlocksControl), new PropertyMetadata(default(IEnumerable<Parameter>)));

        public IEnumerable<Parameter> Parameters
        {
            get { return (IEnumerable<Parameter>) GetValue(ParametersProperty); }
            set { SetValue(ParametersProperty, value); }
        }


        public static readonly DependencyProperty SelectedParameterProperty = DependencyProperty.Register(
            "SelectedParameter", typeof(Parameter), typeof(BlocksControl), new PropertyMetadata(default(Parameter)));

        public Parameter SelectedParameter
        {
            get { return (Parameter) GetValue(SelectedParameterProperty); }
            set { SetValue(SelectedParameterProperty, value); }
        }

        public BlocksControl()
        {
            InitializeComponent();
        }
    }
}
