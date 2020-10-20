using System.Windows;
using System.Windows.Controls;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    /// <summary>
    /// Interaction logic for TimeoutEditor.xaml
    /// </summary>
    public partial class TimeoutEditor : UserControl
    {
        public uint Timeout
        {
            get { return (uint)GetValue(TimeoutProperty); }
            set { SetValue(TimeoutProperty, value); }
        }

        public static readonly DependencyProperty TimeoutProperty =
            DependencyProperty.Register(nameof(Timeout), typeof(uint), typeof(TimeoutEditor), new PropertyMetadata(uint.MinValue));


        public TimeoutEditor()
        {
            InitializeComponent();
        }
    }
}
