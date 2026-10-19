using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>Edits PIN-policy values reported by the selected authenticator.</summary>
public partial class ChangePinPolicyDialog : Window
{
    /// <summary>Limits the additional relying-party IDs when the context reports a maximum.</summary>
    private readonly uint? _maximumRpIds;

    /// <summary>Initializes nullable defaults from the selected authenticator's context.</summary>
    /// <param name="minimumPinLength">Reported minimum length, or null when unreported.</param>
    /// <param name="forcePinChange">Reported forced-change state, or null when unreported.</param>
    /// <param name="maximumRpIds">Reported maximum additional relying-party IDs, or null when unreported.</param>
    public ChangePinPolicyDialog(uint? minimumPinLength, bool? forcePinChange, uint? maximumRpIds)
    {
        _maximumRpIds = maximumRpIds;
        InitializeComponent();
        MinimumPinLengthBox.Text = minimumPinLength?.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
        ForcePinChangeBox.IsChecked = forcePinChange;
        AddHandler(TextBoxBase.TextChangedEvent, new TextChangedEventHandler(OnInputChanged));
        AddHandler(ToggleButton.CheckedEvent, new RoutedEventHandler(OnInputChanged));
        AddHandler(ToggleButton.UncheckedEvent, new RoutedEventHandler(OnInputChanged));
        AddHandler(ToggleButton.IndeterminateEvent, new RoutedEventHandler(OnInputChanged));
        Loaded += (_, _) =>
        {
            AcceptButton.IsEnabled = ValidatePolicy();
            ChangeMinimumPinLengthBox.Focus();
        };
    }

    /// <summary>Copies the validated policy fields for submission.</summary>
    /// <returns>The accepted policy request.</returns>
    public PinPolicyChangeInput TakePolicy() => new(
        ChangeMinimumPinLengthBox.IsChecked == true ? uint.Parse(MinimumPinLengthBox.Text, CultureInfo.InvariantCulture) : null,
        ForcePinChangeBox.IsChecked, GetRelyingPartyIds());

    /// <summary>Splits additional relying-party IDs without inventing an unreported current list.</summary>
    /// <returns>Nonempty, trimmed relying-party IDs.</returns>
    private string[] GetRelyingPartyIds() =>
        RelyingPartyIdsBox.Text.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    /// <summary>Validates numeric limits, reported list capacity, and the presence of a policy request.</summary>
    /// <returns>True when a valid request can be submitted.</returns>
    private bool ValidatePolicy()
    {
        string text = MinimumPinLengthBox.Text;
        string? minimumError = null;
        bool changeMinimum = ChangeMinimumPinLengthBox.IsChecked == true;
        if (changeMinimum &&
            (!uint.TryParse(text, NumberStyles.None, CultureInfo.InvariantCulture, out uint minimum) || minimum < 4 || minimum > 63))
        {
            minimumError = "Minimum PIN length must be between 4 and 63.";
        }
        string[] rpIds = GetRelyingPartyIds();
        bool validMinimum = PinValidation.SetError(MinimumPinLengthBox, minimumError);
        bool validIds = PinValidation.SetError(RelyingPartyIdsBox,
            _maximumRpIds.HasValue && rpIds.Length > _maximumRpIds.Value
                ? $"At most {_maximumRpIds.Value} additional relying-party IDs are supported." : null);
        return validMinimum && validIds && (changeMinimum || rpIds.Length > 0 || ForcePinChangeBox.IsChecked == true);
    }

    /// <summary>Updates WPF validation feedback as the policy fields change.</summary>
    /// <param name="sender">Changed input control.</param>
    /// <param name="e">Input change event.</param>
    private void OnInputChanged(object sender, RoutedEventArgs e) => AcceptButton.IsEnabled = ValidatePolicy();

    /// <summary>Accepts only a valid policy request.</summary>
    /// <param name="sender">Change policy button.</param>
    /// <param name="e">Click event.</param>
    private void OnAccept(object sender, RoutedEventArgs e)
    {
        if (ValidatePolicy())
        {
            DialogResult = true;
        }
    }
}
