using System.Windows.Controls;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>Displays security-key management tabs backed by PIN and touch dialogs.</summary>
public partial class SecurityKeyManagementView : UserControl
{
    /// <summary>Initializes management controls without selecting a device.</summary>
    public SecurityKeyManagementView()
    {
        InitializeComponent();
    }
}
