using CommunityToolkit.Mvvm.Input;
namespace CookBook.WPF.Commands
{
    public interface IEditCommand<T>
    {
        IRelayCommand? NewCommand { get; set; }
        IRelayCommand? NewItemCommand { get; set; }
        IRelayCommand? ChangeItemCommand { get; set; }
        IAsyncRelayCommand<T> SaveCommandAsync { get; set; }
        IAsyncRelayCommand<T> DeleteCommandAsync { get; set; }
        IRelayCommand? DeleteItemCommand { get; set; }
        IRelayCommand? AddItemCommand { get; set; }
        IRelayCommand? RemoveItemCommand { get; set; }

    }
 
}
