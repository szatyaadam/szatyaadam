using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System;
using System.ComponentModel;

namespace CookBook.WPF.Commands
{
    public interface IEditCommand<T>
    {
        IRelayCommand NewCommand { get; set; }
        IAsyncRelayCommand<T> SaveCommandAsync { get; set; }
        IAsyncRelayCommand<T> DeleteCommandAsync { get; set; }
    }
 
}
