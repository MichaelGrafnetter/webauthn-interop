using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Fido2NetLib;
using Fido2NetLib.Objects;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WebAuthnApi Api { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Api = new WebAuthnApi();
        }

        private void Authenticate_Button_Click(object sender, RoutedEventArgs e)
        {
            var options = AssertionOptionsEditor.Options;

            try
            {
                var response = Api.AuthenticatorGetAssertion(options, Fido2NetLib.Objects.AuthenticatorAttachment.Platform);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            var options = CreateAttestationRequest();
            try
            {
                var response = Api.AuthenticatorMakeCredential(options);
            }
            catch(Exception ex)
            {
                ShowError(ex);
            }
        }

        private CredentialCreateOptions CreateAttestationRequest()
        {
            // TODO: Create GUI for CredentialCreateOptions

            var user = new Fido2User
            {
                Name = "michael.grafnetter@outlook.com",
                DisplayName = "Michael Grafnetter",
                Id = Base64Url.Decode("TUY65dH-Otl4jMdTRvlFQ1aApACYsuqGKSPQDQc1Bd4WVyw")
            };

            var rp = new PublicKeyCredentialRpEntity("login.microsoft.com", "Microsoft", null);

            byte[] challenge = Encoding.ASCII.GetBytes("CbWTU93Ppbgok1glyka*K9sZSWkqpK3qS1ldeLJxsI4k3jMLIi3dl8VDx10siTGd8U5SNj8yyMIbqXQH!apXGnrhWmYlg2GNdEGddIkO03cql!kKVgKi*MqEIl9aPqmJdYuRMjrEYlIyzi4*wP0YSyA$");

            var authenticator = new AuthenticatorSelection()
            {
                AuthenticatorAttachment = AuthenticatorAttachment.Platform,
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
                Challenge = challenge,
                Attestation = AttestationConveyancePreference.Direct,
                AuthenticatorSelection = authenticator,
                Extensions = extensions,
                PubKeyCredParams = credParams,
                ExcludeCredentials = excludedCredentials,
                Timeout = 60000
            };

            return options;
        }

        private AssertionOptions CreateAssertionRequest()
        {
            // TODO: Create GUI for AssertionOptions

            byte[] challenge = Encoding.ASCII.GetBytes("O.eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCJ9.eyJhdWQiOiJ1cm46bWljcm9zb2Z0OmZpZG86Y2hhbGxlbmdlIiwiaXNzIjoiaHR0cHM6Ly9sb2dpbi5taWNyb3NvZnQuY29tIiwiaWF0IjoxNjAyNDQ1NDI0LCJuYmYiOjE2MDI0NDU0MjQsImV4cCI6MTYwMjQ0NTcyNH0.KMarezIE6LW3NG99LemMpYHLeiiFJe2NLz04z2Q5hsJLx6hbHpXzhoEfElZi4NaNgvBvv37vv7BqdWbUa6iYWaKI73eXhPuj42IddNE2nqdLzjSX-PCiAeBSi7xMNapW9feAj8zjg9pfCluS8ly_mgYlhBUVMlulL5bR40JQU2bQ6m9PMt1-eFO9A55KbJ_BFSwrsFSJxS612wxceM4cjepEZEGj7QO1bZYLOROnmTk8wcojqWjVpvXnCRocbhFp8CEqM4ucFVsTXiQ4X66aykHbUdKOxCmvB_a20N1fPKhKrS-Hh50jCOPNpwbNVJN7AOhMRyrMiTW2qQb8suihRw");

            var options = new AssertionOptions()
            {
                RpId = "login.microsoft.com",
                Challenge = challenge,
                UserVerification = UserVerificationRequirement.Required,
                Timeout = 60000
                // Extensions
                // AllowCredentials
            };

            return options;
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
