using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CookBook.WPF.ViewModels
{
    public abstract class PagerViewModel : ObservableObject
    {
		private string? _searchKey;
		public string? SearchKey
		{
			get { return _searchKey; }
			set { SetProperty(ref _searchKey, value); page = 1; }
		}

		public bool ascending;

		private string? _sortKey;
		public string? SortKey
		{
			get { return _sortKey; }
			set 
			{
				
				if (value == _sortKey)
				{
					
					ascending = !ascending;
				}
                _sortKey = value;
                LoadData();
			}
		}

		#region Oldaltördelés
		private int _itemsPerPage = 20;
		public int ItemsPerPage
		{
			get { return _itemsPerPage; }
			set { _itemsPerPage = value; LoadData(); }
		}

		public ObservableCollection<int> ItemsPerPageList { get; set; }

		private string _currentPage;
		public string CurrentPage
		{
			get { return _currentPage; }
			set { SetProperty(ref _currentPage, value); }
		}

		protected int page = 1;
		private int pageCount;

		private int _totalItems;
		public int TotalItems
		{
			get { return _totalItems; }
			set 
			{
				
				pageCount = (value - 1) / _itemsPerPage + 1;
			
				SetProperty(ref _totalItems, value);
			
				CurrentPage = page + "/" + pageCount;
			
                if (page > pageCount)
                {
                    page = pageCount;
                }
            }
		}
        #endregion

        public IRelayCommand FirstPageCommand { get; set; }
		public IRelayCommand PreviousPageCommand { get; set; }
		public IRelayCommand NextPageCommand { get; set; }
		public IRelayCommand LastPageCommand { get; set; }
		public IRelayCommand LoadDataCommand { get; set; }

        public PagerViewModel()
		{
			ascending = true;
			LoadDataCommand = new RelayCommand(() => LoadData());
			FirstPageCommand = new RelayCommand(FirstPage);
			PreviousPageCommand = new RelayCommand(PreviousPage);
			NextPageCommand = new RelayCommand(NextPage);
			LastPageCommand = new RelayCommand(LastPage);
			ItemsPerPageList = new ObservableCollection<int> { 10, 20, 50, 100 };
        }

		// Az öröklött osztályokban valósul meg
		protected abstract Task LoadData();

		private void FirstPage()
		{
			page = 1;
			LoadData();
		}

		private void PreviousPage() 
		{
			if (page > 1)
			{
                page--;
                LoadData();
            }
		}

		private void NextPage()
		{
			if (page < pageCount)
			{
                page++;
                LoadData();
            }
		}

		private void LastPage()
		{
			page = pageCount;
			LoadData();
		}
    }
}
