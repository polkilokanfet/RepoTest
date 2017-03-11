using Infragistics.Controls.Menus;
using Prism.Interactivity;

namespace HVTApp.Infrastructure.Prism
{
    public class XamDataTreeCommandBehavior : CommandBehaviorBase<XamDataTree>
    {
        public XamDataTreeCommandBehavior(XamDataTree tree) : base(tree)
        {
            tree.ActiveNodeChanged += TreeOnActiveNodeChanged;
        }

        private void TreeOnActiveNodeChanged(object sender, ActiveNodeChangedEventArgs eventArgs)
        {
            var param = eventArgs.NewActiveTreeNode.Data as NavigationItem;
            CommandParameter = param?.NavigationUri;
            ExecuteCommand(CommandParameter);
        }
    }
}
