using System;
using System.Collections.Generic;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Cryptography;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Dialogs;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class KeyGenerationDialogViewModel : BindableBase, IDialogAware
{
    private readonly IDialogService _dialogService;

    public KeyGenerationDialogViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
        SaveCommand = new DelegateCommand(OnSave);
    }

    public string Title { get; set; } = "Generate Key Pair";

    public DialogCloseListener RequestClose { get; }

    public DelegateCommand SaveCommand { get; }

    public Algorithm SelectedAlgorithm
    {
        get;
        set => SetProperty(ref field, value);
    } = Algorithm.ES256;

    public IList<KeyValuePair<Algorithm, string>> Algorithms => AuthenticatorPreset.AlgorithmItems;

    public bool CanCloseDialog() => true;

    public void OnDialogClosed() { }

    public void OnDialogOpened(IDialogParameters parameters)
    {
        var p = KeyGenerationDialogParameters.From(parameters);
        if (p.DefaultAlgorithm.HasValue)
            SelectedAlgorithm = p.DefaultAlgorithm.Value;
    }

    private void OnSave()
    {
        try
        {
            string pem = SoftwareAuthenticator.GenerateKeyPairPem(SelectedAlgorithm);

            var dialog = new SaveFileDialog
            {
                Title = "Save Generated Private Key",
                Filter = "PEM files (*.pem)|*.pem",
                DefaultExt = ".pem",
                FileName = $"webauthn-{SelectedAlgorithm.ToString().ToLowerInvariant()}-key.pem"
            };

            if (dialog.ShowDialog() == true)
            {
                System.IO.File.WriteAllText(dialog.FileName, pem);

                RequestClose.Invoke(new KeyGenerationDialogResult
                {
                    FilePath = dialog.FileName,
                    Algorithm = SelectedAlgorithm
                }.ToDialogParameters(), ButtonResult.OK);
            }
        }
        catch (Exception ex)
        {
            _dialogService.ShowNotificationDialog(ex.Message);
        }
    }
}
