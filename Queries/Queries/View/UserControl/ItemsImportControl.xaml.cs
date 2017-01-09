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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Queries.View.UserControl
{
    /// <summary>
    /// Interaction logic for ItemsImport.xaml
    /// </summary>
    public partial class ItemsImportControl
    {
        public ItemsImportControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Multiselect = false,
                DefaultExt = ".xlsx",
                Filter = "xls|*.xls|xlsx|*.xlsx"

            };
            var r = fileDialog.ShowDialog();
            if (r.HasValue && r.Value)
            {
                TextBox.Text = fileDialog.FileName;
            }
        }
    }
}
