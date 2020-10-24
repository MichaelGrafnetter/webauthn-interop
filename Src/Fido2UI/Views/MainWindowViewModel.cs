using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Fido2NetLib;
using Fido2NetLib.Objects;
using Microsoft.Xaml.Behaviors;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public class MainWindowViewModel : BindableBase
    {
        private WebAuthnApi Api { get; set; }
        private IAttestationOptionsViewModel AttestationOptionsViewModel { get; set; }
        private IAssertionOptionsViewModel AssertionOptionsViewModel { get; set; }
        private IDialogService DialogService { get; set; }

        public MainWindowViewModel(
            WebAuthnApi api,
            IAttestationOptionsViewModel attestationOptionsViewModel,
            IAssertionOptionsViewModel assertionOptionsViewModel,
            IDialogService dialogService)
        {
            // Save dependencies
            Api = api;
            AttestationOptionsViewModel = attestationOptionsViewModel;
            AssertionOptionsViewModel = assertionOptionsViewModel;
            DialogService = dialogService;

            // Initialize commands
            RegisterCommand = new DelegateCommand(OnRegister);
            AuthenticateCommand = new DelegateCommand(OnAuthenticate);
            LoadOptionsCommand = new DelegateCommand(OnLoadOptions);
        }

        public ICommand RegisterCommand { get; private set; }
        public ICommand AuthenticateCommand { get; private set; }

        public ICommand LoadOptionsCommand { get; private set; }


        private void OnRegister()
        {
            try
            {
                var response = Api.AuthenticatorMakeCredential(AttestationOptionsViewModel.Options);
            }
            catch (Exception ex)
            {
                var parameters = new DialogParameters($"Message={ex.Message}");
                DialogService.ShowDialog(nameof(NotificationDialog), parameters, null);
            }
        }

        private void OnAuthenticate()
        {
            try
            {
                var response = Api.AuthenticatorGetAssertion(AssertionOptionsViewModel.Options, AssertionOptionsViewModel.SelectedAuthenticatorAttachment);
            }
            catch (Exception ex)
            {
                var parameters = new DialogParameters($"Message={ex.Message}");
                DialogService.ShowDialog(nameof(NotificationDialog), parameters, null);
            }
        }

        private void OnLoadOptions()
        {
            byte[] assertionChallenge = Encoding.ASCII.GetBytes("O.eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCJ9.eyJhdWQiOiJ1cm46bWljcm9zb2Z0OmZpZG86Y2hhbGxlbmdlIiwiaXNzIjoiaHR0cHM6Ly9sb2dpbi5taWNyb3NvZnQuY29tIiwiaWF0IjoxNjAyNDQ1NDI0LCJuYmYiOjE2MDI0NDU0MjQsImV4cCI6MTYwMjQ0NTcyNH0.KMarezIE6LW3NG99LemMpYHLeiiFJe2NLz04z2Q5hsJLx6hbHpXzhoEfElZi4NaNgvBvv37vv7BqdWbUa6iYWaKI73eXhPuj42IddNE2nqdLzjSX-PCiAeBSi7xMNapW9feAj8zjg9pfCluS8ly_mgYlhBUVMlulL5bR40JQU2bQ6m9PMt1-eFO9A55KbJ_BFSwrsFSJxS612wxceM4cjepEZEGj7QO1bZYLOROnmTk8wcojqWjVpvXnCRocbhFp8CEqM4ucFVsTXiQ4X66aykHbUdKOxCmvB_a20N1fPKhKrS-Hh50jCOPNpwbNVJN7AOhMRyrMiTW2qQb8suihRw");

            var assertionOptions = new AssertionOptions()
            {
                RpId = "login.microsoft.com",
                Challenge = assertionChallenge,
                UserVerification = UserVerificationRequirement.Required,
                Timeout = 60000
            };

            AssertionOptionsViewModel.Options = assertionOptions;
            AssertionOptionsViewModel.SelectedAuthenticatorAttachment = null;

            var user = new Fido2User
            {
                Name = "michael.grafnetter@outlook.com",
                DisplayName = "Michael Grafnetter",
                Id = Base64Url.Decode("TUY65dH-Otl4jMdTRvlFQ1aApACYsuqGKSPQDQc1Bd4WVyw")
            };

            var rp = new PublicKeyCredentialRpEntity("login.microsoft.com", "Microsoft", null);

            byte[] credentialCreateChallenge = Encoding.ASCII.GetBytes("CbWTU93Ppbgok1glyka*K9sZSWkqpK3qS1ldeLJxsI4k3jMLIi3dl8VDx10siTGd8U5SNj8yyMIbqXQH!apXGnrhWmYlg2GNdEGddIkO03cql!kKVgKi*MqEIl9aPqmJdYuRMjrEYlIyzi4*wP0YSyA$");

            var authenticator = new AuthenticatorSelection()
            {
                AuthenticatorAttachment = null,
                UserVerification = UserVerificationRequirement.Required,
                RequireResidentKey = true
            };

            var extensions = new WinExtensionsIn()
            {
                HmacSecret = true,
                CredProtect = UserVerification.Optional
            };

            var credParams = new List<PubKeyCredParam>()
            {
                new PubKeyCredParam() { Alg = -7, Type = PublicKeyCredentialType.PublicKey },
                new PubKeyCredParam() { Alg = -257, Type = PublicKeyCredentialType.PublicKey }
            };

            var excludedCredentials = new List<PublicKeyCredentialDescriptor>()
            {
                new PublicKeyCredentialDescriptor(Base64Url.Decode("lz6_hw1jzaRNhhu9dt_M1Q=")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("Zod6YhgNV2dQeT3v8ekjRpU0nVlEkPlpXF5Vx6f4P9g=")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("sx2P4XkPO6TUoSf0pMEm3zi5gdwVrIRjiYvuTFRAkNMe_jVsntSgkyG5aV8er5GCA_G1X2idph-8lhhMFX3aaAyBCQIAAA="))
            };

            var options = new CredentialCreateOptions()
            {
                User = user,
                Rp = rp,
                Challenge = credentialCreateChallenge,
                Attestation = AttestationConveyancePreference.Direct,
                AuthenticatorSelection = authenticator,
                Extensions = extensions,
                PubKeyCredParams = credParams,
                ExcludeCredentials = excludedCredentials,
                Timeout = 60000
            };

            AttestationOptionsViewModel.Options = options;
        }
    }
}
