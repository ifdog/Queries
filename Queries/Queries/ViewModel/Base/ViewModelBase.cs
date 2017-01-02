using System.ComponentModel;
using System.Windows;

namespace Queries.ViewModel.Base
{
    public class ViewModelBase<TViewType> : INotifyPropertyChanged
    where TViewType : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public TViewType View { get; }

        public ViewModelBase(TViewType view)
        {
            this.View = view;
            this.View.DataContext = this;
        }
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
