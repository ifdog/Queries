using System.Windows;
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
                Filter = "xls|*.xls"

            };
            var r = fileDialog.ShowDialog();
            if (r.HasValue && r.Value)
            {
                TextBox.Text = fileDialog.FileName;
            }
        }
    }
}
