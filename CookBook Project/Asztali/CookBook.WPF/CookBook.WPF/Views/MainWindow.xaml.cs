using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace CookBook.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer _timer;
        TimeSpan _time;
        int log;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        { 
            Application.Current.Shutdown();
        }


        private void Timer(object sender, RoutedEventArgs e)
        {
            if (App.currentUser != null)
            {
                Login.Header = "Kijelentkezés";
                if (_time == TimeSpan.Zero||log==0)
                {
                    _time = TimeSpan.FromMinutes(45);
                    _timer = new DispatcherTimer(new TimeSpan(0, 0, 1),
                        DispatcherPriority.Normal, delegate
                        {
                             tbTime.Header = _time.ToString("c");
                            if (_time == TimeSpan.Zero)
                            {   
                                _timer.Stop();
                            }
                            _time = _time.Add(TimeSpan.FromSeconds(-1));
                        }, Application.Current.Dispatcher);
                    _timer.Start();
                    log++;
                }
            }
            if (_time < TimeSpan.Zero||App.currentUser==null)
            {
               App.currentUser = null;
                _time = TimeSpan.Zero;
                log= 0;
                Login.Header = "Bejelentkezés";
            }
            if (_time.TotalMinutes < 5 && App.currentUser != null&& log!=2)
            {
                log = 1;
                if (MessageBox.Show("If the time is out, do you want to automatically refresh the Token?",
                    "Refresh",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.IsRefreshing = true;
                    _time = TimeSpan.FromMinutes(45);
                }
            }
        }
        private void LogOut(object sender, RoutedEventArgs e)
        {
            if(Login.Header=="Kijelentkezés")
            {
            _time = TimeSpan.Zero;
            App.currentUser = null;
                log = 0;
            }
        }
        private void SizeBtn_Click(object sender, RoutedEventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Maximized: WindowState = WindowState.Normal; break;
                case WindowState.Normal: WindowState = WindowState.Maximized; break;
                default: WindowState = WindowState.Normal; break;
            }
        }
        private void DesktopBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}

