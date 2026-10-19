using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>Edits an optional numeric PIN length with bounded increment and decrement buttons.</summary>
public partial class PinLengthEditor : UserControl
{
    /// <summary>Identifies the numeric text; empty text preserves the existing PIN policy.</summary>
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text), typeof(string), typeof(PinLengthEditor),
        new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    /// <summary>Gets or sets the numeric text, validated against PIN policy limits when applied.</summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>Initializes numeric input and spinner controls.</summary>
    public PinLengthEditor() => InitializeComponent();

    /// <summary>Rejects typed characters other than ASCII digits.</summary>
    /// <param name="sender">Numeric text box.</param>
    /// <param name="e">Proposed text input.</param>
    private void OnPreviewTextInput(object sender, TextCompositionEventArgs e) =>
        e.Handled = !IsNumeric(e.Text);

    /// <summary>Rejects clipboard content containing nonnumeric characters.</summary>
    /// <param name="sender">Numeric text box.</param>
    /// <param name="e">Clipboard paste request.</param>
    private void OnPasting(object sender, DataObjectPastingEventArgs e)
    {
        if (!e.DataObject.GetDataPresent(DataFormats.UnicodeText) ||
            e.DataObject.GetData(DataFormats.UnicodeText) is not string text || !IsNumeric(text))
        {
            e.CancelCommand();
        }
    }

    /// <summary>Checks that all input characters are ASCII decimal digits.</summary>
    /// <param name="text">Proposed text.</param>
    /// <returns>True for numeric or empty text.</returns>
    private static bool IsNumeric(string text)
    {
        foreach (char character in text)
        {
            if (character < '0' || character > '9')
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>Increments the length within the supported limits.</summary>
    /// <param name="sender">Up button.</param>
    /// <param name="e">Click event.</param>
    private void OnIncrease(object sender, RoutedEventArgs e) => Step(1);

    /// <summary>Decrements the length within the supported limits.</summary>
    /// <param name="sender">Down button.</param>
    /// <param name="e">Click event.</param>
    private void OnDecrease(object sender, RoutedEventArgs e) => Step(-1);

    /// <summary>Supports the up and down keyboard arrows and rejects spaces.</summary>
    /// <param name="sender">Numeric editor.</param>
    /// <param name="e">Key event.</param>
    private void OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key is Key.Up or Key.Down)
        {
            Step(e.Key == Key.Up ? 1 : -1);
            e.Handled = true;
        }
        else if (e.Key == Key.Space)
        {
            e.Handled = true;
        }
    }

    /// <summary>Updates the bound text while preserving its binding and optional blank state.</summary>
    /// <param name="delta">Increment or decrement.</param>
    private void Step(int delta)
    {
        int value = int.TryParse(Text, NumberStyles.None, CultureInfo.InvariantCulture, out int parsed)
            ? Math.Clamp(parsed + delta, 4, 63) : 4;
        SetCurrentValue(TextProperty, value.ToString(CultureInfo.InvariantCulture));
    }
}
