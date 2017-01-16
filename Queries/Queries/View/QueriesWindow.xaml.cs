using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Queries
{
    /// <summary>
    /// Interaction logic for QueryWindow.xaml
    /// </summary>
    public partial class QueriesWindow : Window
    {
        public QueriesWindow()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count<1)return;
            this.DynGrid.Children.Clear();
            var t = Type.GetType($"Queries.View.UserControl.{e.AddedItems[0]}Control") ??
                    Type.GetType($"Queries.View.UserControl.NotFoundControl");
            this.DynGrid.Children.Add(Activator.CreateInstance(t) as UIElement);
        }

		private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();
		}
	}
}
