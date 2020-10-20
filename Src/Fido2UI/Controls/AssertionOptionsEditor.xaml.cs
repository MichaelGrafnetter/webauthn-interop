using System.Windows;
using System.Windows.Controls;
using Fido2NetLib;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    /// <summary>
    /// Interaction logic for AssertionOptionsEditor.xaml
    /// </summary>
    public partial class AssertionOptionsEditor : UserControl
    {
        public AssertionOptions Options
        {
            get
            {
                return ((AssertionOptionsModel)DataContext).Options;
            }
            set
            {
                ((AssertionOptionsModel)DataContext).Options = value;
            }
        }

        //Using a DependencyProperty as the backing store for MyProperty.This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty OptionsProperty =
        //    DependencyProperty.Register(nameof(Options), typeof(AssertionOptions), typeof(AssertionOptionsEditor), new PropertyMetadata(default(AssertionOptions)));

        public AssertionOptionsEditor()
        {
            InitializeComponent();
        }
    }
}
