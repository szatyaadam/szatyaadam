using System.ComponentModel;
using System.Net.Http.Headers;

namespace CookBook.ApiClient.Repositories
{
    public class BaseAPIRepository
    {
        protected HttpClient client;
        protected DelegatingHandler? _handler;
        protected string _baseUrl;
        protected string _path;

        public BaseAPIRepository(string baseUrl = null, string path = null, DelegatingHandler? handler = null)
        {
            // Ha nincs a paraméternek értéke, akkor automatikusan ezt vesz fel
            _baseUrl = baseUrl ?? "http://localhost:5250/";
            _path = path;
            _handler = handler;

            // HTTP handler hozzáadása, ha létezik
            client = handler == null ? new HttpClient() : new HttpClient(handler) { BaseAddress = new Uri(_baseUrl) };
            // alap URL beállítása
            if (!string.IsNullOrEmpty(_baseUrl))
            {
                client.BaseAddress = new Uri(_baseUrl);
            }
            // client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
