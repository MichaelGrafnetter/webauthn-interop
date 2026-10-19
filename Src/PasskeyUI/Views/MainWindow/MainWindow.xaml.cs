using System.ComponentModel;
using System.Windows;

namespace DSInternals.Win32.WebAuthn.PasskeyUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>Allows the final close after pending management work has released its resources.</summary>
        private bool _managementClosed;

        /// <summary>Prevents repeated close requests from starting duplicate shutdown operations.</summary>
        private bool _managementClosing;

        public MainWindow()
        {
            InitializeComponent();
            Closing += OnClosing;
        }

        /// <summary>Waits for native cancellation before releasing the management session and closing the window.</summary>
        /// <param name="sender">Main window.</param>
        /// <param name="e">Cancelable closing request.</param>
        private async void OnClosing(object? sender, CancelEventArgs e)
        {
            if (_managementClosed || DataContext is not MainWindowViewModel viewModel)
            {
                return;
            }
            e.Cancel = true;
            if (_managementClosing)
            {
                return;
            }
            _managementClosing = true;
            await System.Threading.Tasks.Task.WhenAll(
                viewModel.SecurityKeyManagementViewModel.DisposeAsync().AsTask(),
                viewModel.RoamingCredentialsViewModel.DisposeAsync().AsTask());
            _managementClosed = true;
            Dispatcher.BeginInvoke(Close);
        }
    }
}
