using System.Windows;
using System.Windows.Controls;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    /// <summary>
    /// Interaction logic for TimeoutEditor.xaml
    /// </summary>
    public partial class TimeoutEditor : UserControl
    {
        public int Timeout
        {
            get { return (int)GetValue(TimeoutProperty); }
            set { SetValue(TimeoutProperty, value); }
        }

        public static readonly DependencyProperty TimeoutProperty =
            DependencyProperty.Register(nameof(Timeout), typeof(int), typeof(TimeoutEditor), new PropertyMetadata(0));


        public TimeoutEditor()
        {
            InitializeComponent();
        }
    }
}
