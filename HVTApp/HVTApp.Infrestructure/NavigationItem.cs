using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HVTApp.Infrastructure
{
    public class NavigationItem : INavigationItem
    {
        public string Caption { get; set; }
        public bool IsExpended { get; set; } = true;
        public Uri NavigationUri { get; set; }
        public ObservableCollection<NavigationItem> Items { get; } = new ObservableCollection<NavigationItem>();

        public NavigationItem(string caption, Type viewType)
        {
            if(string.IsNullOrEmpty(caption)) throw new ArgumentException(nameof(caption));
            if (viewType == null) throw new ArgumentNullException(nameof(viewType));

            Caption = caption;
            NavigationUri = new Uri(viewType.FullName, UriKind.Relative);
        }

    }
}
