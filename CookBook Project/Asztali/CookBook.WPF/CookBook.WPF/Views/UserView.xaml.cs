using CookBook.WPF.ViewModels;
using System.Windows.Controls;


namespace CookBook.WPF.Views 
{ 


    public partial class UserView : UserControl
    {
        public UserView()
        {
            InitializeComponent();
        }
        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            DataGridColumn column = e.Column;
            UserViewModel? viewModel = DataContext as UserViewModel;
            if (viewModel != null)
            {
                viewModel.SortKey = column.SortMemberPath;
            }
            e.Handled = true;
        }
    }
}
