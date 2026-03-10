using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Commands;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

public partial class AuthenticatorPresetMenu : UserControl
{
    public static readonly DependencyProperty PresetSelectedCommandProperty =
        DependencyProperty.Register(
            nameof(PresetSelectedCommand),
            typeof(ICommand),
            typeof(AuthenticatorPresetMenu));

    public AuthenticatorPresetMenu()
    {
        InternalSelectCommand = new DelegateCommand<AuthenticatorPreset>(OnPresetSelected);
        InitializeComponent();
    }

    public IReadOnlyList<AuthenticatorPreset> KnownPresets => AuthenticatorPreset.KnownPresets;

    public ICommand InternalSelectCommand { get; }

    public ICommand PresetSelectedCommand
    {
        get => (ICommand)GetValue(PresetSelectedCommandProperty);
        set => SetValue(PresetSelectedCommandProperty, value);
    }

    private void OnPresetSelected(AuthenticatorPreset preset)
    {
        PresetSelectedCommand?.Execute(preset);
    }
}
