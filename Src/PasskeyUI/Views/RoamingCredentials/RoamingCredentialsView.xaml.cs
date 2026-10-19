using System.Windows.Controls;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>Lists roaming credentials through a selection, PIN, and enumeration workflow.</summary>
public partial class RoamingCredentialsView : UserControl
{
    /// <summary>Initializes listing controls without selecting a device.</summary>
    public RoamingCredentialsView()
    {
        InitializeComponent();
    }
}
