using CookBook.Models.Models;
using CookBook.WPF.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
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
        private void GetEmpty(object sender, RoutedEventArgs e)
        {
            ingredient.SelectedItem= null;
        }
       private void NewIngredient(object sender, RoutedEventArgs e)
        {
            IngredientName.Visibility = Visibility.Collapsed;
            Quantity.Visibility = Visibility.Collapsed;
            chooseIngredient.Visibility = Visibility.Collapsed;
            Measure.Visibility = Visibility.Collapsed;
            OldIngButton.Visibility = Visibility.Visible;

            newIngredientName.Visibility = Visibility.Visible;
            newQuantity.Visibility = Visibility.Visible;
            chooseNewIngredient.Visibility = Visibility.Visible;
            NewIngButton.Visibility= Visibility.Collapsed;
        }
        private void OldIngredient(object sender, RoutedEventArgs e)
        {
            newIngredientName.Visibility = Visibility.Collapsed;
            newQuantity.Visibility = Visibility.Collapsed;
            chooseNewIngredient.Visibility = Visibility.Collapsed;
        
            NewIngButton.Visibility = Visibility.Visible;
            IngredientName.Visibility = Visibility.Visible;
            Quantity.Visibility = Visibility.Visible;
            chooseIngredient.Visibility = Visibility.Visible;
            Measure.Visibility = Visibility.Visible;
            OldIngButton.Visibility = Visibility.Collapsed;
        }
    }
}