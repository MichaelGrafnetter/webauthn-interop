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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var config = new Fido2Configuration()
            {
                ChallengeSize = 32,
                Origin = "login.microsoft.com",
                ServerDomain = "login.microsoft.com",
                ServerName = "Microsoft",
                Timeout = 60000
            };

            var user = new Fido2User
            {
                Name = "michael.grafnetter@outlook.com",
                DisplayName = "Michael Grafnetter",
                Id = Base64Url.Decode("TUY65dH-Otl4jMdTRvlFQ1aApACYsuqGKSPQDQc1Bd4WVyw")
            };

            var authenticator = new AuthenticatorSelection
            {
                AuthenticatorAttachment = Fido2NetLib.Objects.AuthenticatorAttachment.CrossPlatform,
                RequireResidentKey = true,
                UserVerification = Fido2NetLib.Objects.UserVerificationRequirement.Required,
            };

            byte[] challenge = Encoding.ASCII.GetBytes("CbWTU93Ppbgok1glyka*K9sZSWkqpK3qS1ldeLJxsI4k3jMLIi3dl8VDx10siTGd8U5SNj8yyMIbqXQH!apXGnrhWmYlg2GNdEGddIkO03cql!kKVgKi*MqEIl9aPqmJdYuRMjrEYlIyzi4*wP0YSyA$");

            var excludedCredentials = new List<PublicKeyCredentialDescriptor>()
            {
                new PublicKeyCredentialDescriptor(Base64Url.Decode("lz6_hw1jzaRNhhu9dt_M1Q=")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("Zod6YhgNV2dQeT3v8ekjRpU0nVlEkPlpXF5Vx6f4P9g=")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("sx2P4XkPO6TUoSf0pMEm3zi5gdwVrIRjiYvuTFRAkNMe_jVsntSgkyG5aV8er5GCA_G1X2idph-8lhhMFX3aaAyBCQIAAA="))
            };

            var extensions = new WinExtensionsIn()
            {
                HmacSecret = true,
                CredProtect = UserVerification.Optional
            };

            var options = CredentialCreateOptions.Create(
                config,
                challenge,
                user,
                authenticator,
                Fido2NetLib.Objects.AttestationConveyancePreference.Direct,
                excludedCredentials,
                extensions
            );

            options.PubKeyCredParams = new List<PubKeyCredParam>()
            {
                new PubKeyCredParam() { Alg = -7, Type = PublicKeyCredentialType.PublicKey },
                new PubKeyCredParam() { Alg = -257, Type = PublicKeyCredentialType.PublicKey }
            };

            var webauthn = new WebAuthnApi();
            var response = webauthn.AuthenticatorMakeCredential(options);
        }
    }
}
