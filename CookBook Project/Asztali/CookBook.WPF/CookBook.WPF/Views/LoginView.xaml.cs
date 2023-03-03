using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Text.RegularExpressions;
using CookBook.WPF.ViewModels;
using CookBook.Models.Models;

namespace CookBook.WPF.Views
{
    public partial class LoginView : UserControl
    {
      
        public LoginView() => InitializeComponent();
        public bool Authentication { get; set; }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (UserName.Text.Length == 0)
            {
                
                UserName.Text = "Enter the UserName.";
                UserName.Select(0, UserName.Text.Length);
                password.Clear();
                UserName.Focus();
            }
            else if (!Regex.IsMatch(UserName.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                UserName.Text = "Enter a valid UserName.";
                UserName.Select(0, UserName.Text.Length);
                password.Clear();
                UserName.Focus();
                BadKeyValuePair();
            }
            else
            {
                Authentication = true;
                Buttons.Visibility = Visibility.Hidden;
                signedIn.Visibility= Visibility.Visible;
            }
        }
   
       
        private void GetEmpty_Click(object sender, RoutedEventArgs e)
        {
               if (UserName.Text == "Enter a valid UserName."|| UserName.Text == "Enter a UserName.")
                {
                UserName.Clear();

                }

        }
        private void BadKeyValuePair()
        {
            if (Authentication!=true)
            {
                wrongInput.Visibility = Visibility.Visible;
            }
        }
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Authentication= false;
            Buttons.Visibility = Visibility.Visible;
            signedIn.Visibility = Visibility.Hidden;
            wrongInput.Visibility = Visibility.Hidden;
            password.Clear();
            UserName.Clear();
        }


    }
}  
