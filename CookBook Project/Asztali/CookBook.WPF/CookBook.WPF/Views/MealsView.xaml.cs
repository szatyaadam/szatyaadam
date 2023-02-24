using CookBook.WPF.ViewModels;

using System.ComponentModel;
using System.Data;
using System.Printing;
using System.Windows;
using System.Windows.Controls;


namespace CookBook.WPF.Views
{
    public partial class MealsView : UserControl
    {
        public MealsView()=>InitializeComponent();
   
        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            DataGridColumn column = e.Column;
            MealsViewModel? viewModel = DataContext as MealsViewModel;
            if (viewModel != null)
            {
                viewModel.SortKey = column.SortMemberPath;
                column.SortDirection = viewModel.ascending ?
                    ListSortDirection.Ascending : ListSortDirection.Descending;
            }

        }
        RowDefinition? rowDef1;

        private void addRow(object sender, RoutedEventArgs e)
        {
           
            rowDef1 = new RowDefinition();
            grid1.RowDefinitions.Add(rowDef1);
            

        }



    }
}