using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Fido2NetLib;
using Fido2NetLib.Objects;
using Prism.Commands;
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
            LoadMicrosoftOptionsCommand = new DelegateCommand(OnLoadMicrosoftOptions);
            LoadGoogleOptionsCommand = new DelegateCommand(OnLoadGoogleOptions);
            LoadFacebookOptionsCommand = new DelegateCommand(OnLoadFacebookOptions);
        }

        public ICommand RegisterCommand { get; private set; }
        public ICommand AuthenticateCommand { get; private set; }
        public ICommand LoadMicrosoftOptionsCommand { get; private set; }
        public ICommand LoadGoogleOptionsCommand { get; private set; }
        public ICommand LoadFacebookOptionsCommand { get; private set; }

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

        private void OnLoadMicrosoftOptions()
        {
            // Assertion:
            var assertionOptions = new AssertionOptions()
            {
                RpId = "login.microsoft.com",
                Challenge = Base64Url.Decode("Ty5leUowZVhBaU9pSktWMVFpTENKaGJHY2lPaUpTVXpJMU5pSXNJbmcxZENJNklqVlBaamxRTlVZNVowTkRkME50UmpKQ1QwaEllRVJFVVMxRWF5SjkuZXlKaGRXUWlPaUoxY200NmJXbGpjbTl6YjJaME9tWnBaRzg2WTJoaGJHeGxibWRsSWl3aWFYTnpJam9pYUhSMGNITTZMeTlzYjJkcGJpNXRhV055YjNOdlpuUXVZMjl0SWl3aWFXRjBJam94TmpBNU1qYzNOVFE0TENKdVltWWlPakUyTURreU56YzFORGdzSW1WNGNDSTZNVFl3T1RJM056ZzBPSDAubDNyeF9JTnNLT0hsZTR5azdvSmk3MG0yMUNsV2lWWklJMGxRdVhJbWZ0N1RMX0ppcTRpc0Uza05vRjR6X0cyYlFhdDdaOG55dVRZamNkTmsxSm5OT0k1ZXBMMUIwNkR4N21OU05sZ3ZlWWhKR05faVB4RC1lOVJkVXJvNjlPLWx1cHRPUjVQX3B6dUpWU0dGTFEwLXBZUHE5NzlEVmI2ZF9pMHYxbjBKakd3bkxIMVE5b3ZRSEJzR1E1YzFvMUhNSDNBdEltZjI0Zk9McHBOT2s3WXM5OXlIdFM5VkFwcTNmbG5vT3VxWVpXQzBDMnJjNXpsdzAwR3p5OGV6NWZra010SDNKRGpmbWZ1aWxfb1RCc20yQUIwX1Y5NjZxRlJuMGxsWXcteXlBTDlISU1LelM1TU01aDhpb1lFakxrZlRGblpJandUVERpOHV3SG1LMkVueFpn"),
                UserVerification = UserVerificationRequirement.Required,
                Timeout = 120000
            };

            AssertionOptionsViewModel.Options = assertionOptions;
            AssertionOptionsViewModel.SelectedAuthenticatorAttachment = null;

            // Attestation:
            var options = new CredentialCreateOptions()
            {
                User = new Fido2User
                {
                    Name = "john.doe@outlook.com",
                    DisplayName = "John Doe",
                    Id = Convert.FromBase64String("vzcamjpGT/VqULKaTqxpNCAGx66PkL10thAmvwmroAAga3lW/x8wTCBUEfL5aquu5g==")
                },
                Rp = new PublicKeyCredentialRpEntity("login.microsoft.com", "Microsoft", String.Empty),
                Challenge = Encoding.ASCII.GetBytes("CbWTU93Ppbgok1glyka*K9sZSWkqpK3qS1ldeLJxsI4k3jMLIi3dl8VDx10siTGd8U5SNj8yyMIbqXQH!apXGnrhWmYlg2GNdEGddIkO03cql!kKVgKi*MqEIl9aPqmJdYuRMjrEYlIyzi4*wP0YSyA$"),
                Attestation = AttestationConveyancePreference.Direct,
                AuthenticatorSelection = new AuthenticatorSelection()
                {
                    AuthenticatorAttachment = AuthenticatorAttachment.CrossPlatform,
                    UserVerification = UserVerificationRequirement.Required,
                    RequireResidentKey = true
                },
                Extensions = new WinExtensionsIn()
                {
                    HmacSecret = true,
                    CredProtect = UserVerification.Optional
                },
                PubKeyCredParams = new List<PubKeyCredParam>()
                {
                    new PubKeyCredParam() { Alg = -7, Type = PublicKeyCredentialType.PublicKey },
                    new PubKeyCredParam() { Alg = -257, Type = PublicKeyCredentialType.PublicKey }
                },
                ExcludeCredentials = new List<PublicKeyCredentialDescriptor>()
                {
                    new PublicKeyCredentialDescriptor(Base64Url.Decode("lz6_hw1jzaRNhhu9dt_M1Q=")),
                    new PublicKeyCredentialDescriptor(Base64Url.Decode("Zod6YhgNV2dQeT3v8ekjRpU0nVlEkPlpXF5Vx6f4P9g=")),
                    new PublicKeyCredentialDescriptor(Base64Url.Decode("sx2P4XkPO6TUoSf0pMEm3zi5gdwVrIRjiYvuTFRAkNMe_jVsntSgkyG5aV8er5GCA_G1X2idph-8lhhMFX3aaAyBCQIAAA="))
                },
                Timeout = 120000
            };

            AttestationOptionsViewModel.Options = options;
        }

        private void OnLoadFacebookOptions()
        {
            // Assertion:
            var assertionOptions = new AssertionOptions()
            {
                RpId = "facebook.com",
                Timeout = 60000,
                Challenge = Base64Url.Decode("QmFQclh3QUFBQURTQkZoZzczeExFUVo2c0dNVndxdjI1ajc0eVhZVkx2c2JUemZtWlc4RkFSM3VDV2ZvTHpENFlXdnZSTTI4bnQzZG1nd1ZoSEg2WWhTRmgxNnpuX3NsVmJJMA"),
                UserVerification = UserVerificationRequirement.Preferred,
                Extensions = new AuthenticationExtensionsClientInputs()
                {
                    AppID = "https://www.facebook.com/u2f/app_id/?uid=740592393&s=AY66Df2A7Ed9rniipaIgG81SH6QO0rlmffyygOtXMZPrBg",
                }
            };

            AssertionOptionsViewModel.Options = assertionOptions;
            AssertionOptionsViewModel.SelectedAuthenticatorAttachment = AuthenticatorAttachment.CrossPlatform;

            // Attestation:
            var attestationOptions = new CredentialCreateOptions()
            {
                User = new Fido2User
                {
                    Name = "john.doe",
                    DisplayName = "John Doe",
                    Id = new byte[] { 0x37, 0x34, 0x37, 0x35, 0x39, 0x32, 0x33, 0x39, 0x33 }
                },
                Rp = new PublicKeyCredentialRpEntity("facebook.com", "Facebook", String.Empty),
                Challenge = Base64Url.Decode("YnFQclh3QUFBQUFpRUZtN1BBSFpURkR1TFlBeFJldzVPdEtWOHY3Y24xV0JUQ0puRVhvN0FSMVN6eVktNGNrSmU3Z1h1NXZPaVJjX2lpUGU2ZzNMaDAySURaQTlQeXVSbVcwUQ"),
                Attestation = AttestationConveyancePreference.None,
                AuthenticatorSelection = new AuthenticatorSelection()
                {
                    AuthenticatorAttachment = AuthenticatorAttachment.CrossPlatform,
                    RequireResidentKey = false,
                    UserVerification = UserVerificationRequirement.Preferred
                },
                PubKeyCredParams = new List<PubKeyCredParam>()
                {
                    new PubKeyCredParam() { Alg = -7, Type = PublicKeyCredentialType.PublicKey }
                },
                ExcludeCredentials = new List<PublicKeyCredentialDescriptor>()
                {
                    new PublicKeyCredentialDescriptor(Convert.FromBase64String("vyI8VLKQTU57glEnMutpB39pY3amRqpFZoCgWIlxRdgOukrkboelrv1sOmjkttnuDflSm3NqiqjTHJWaFSt6X/Hq89uwirQrexUTzO0yJU0Qzs+kTU3iJm1hVy8XY7Rf")),
                    new PublicKeyCredentialDescriptor(Convert.FromBase64String("5W0+UWjiBBpJL2sVJ4JwI4LeE/u9RO10GqXEIBhccoupuNdwcW++J/x2JC/I8cjQL9cmWnOsZWyGkcgzck0StA=="))
                },
                Timeout = 60000
            };

            AttestationOptionsViewModel.Options = attestationOptions;
        }

        private void OnLoadGoogleOptions()
        {
            // Assertion:
            var assertionOptions = new AssertionOptions()
            {
                RpId = "google.com",
                Timeout = 30000,
                Challenge = Base64Url.Decode("YnFQclh3QUFBQUFpRUZtN1BBSFpURkR1TFlBeFJldzVPdEtWOHY3Y24xV0JUQ0puRVhvN0FSMVN6eVktNGNrSmU3Z1h1NXZPaVJjX2lpUGU2ZzNMaDAySURaQTlQeXVSbVcwUQ"),
                Extensions = new AuthenticationExtensionsClientInputs()
                {
                    AppID = "https://www.gstatic.com/securitykey/origins.json"
                },
                UserVerification = UserVerificationRequirement.Discouraged
            };

            AssertionOptionsViewModel.Options = assertionOptions;
            AssertionOptionsViewModel.SelectedAuthenticatorAttachment = null;

            // Attestation:
            var attestationOptions = new CredentialCreateOptions()
            {
                User = new Fido2User(),
                Rp = new PublicKeyCredentialRpEntity("https://www.gstatic.com/securitykey/origins.json", null, null),
                Challenge = Base64Url.Decode("YnFQclh3QUFBQUFpRUZtN1BBSFpURkR1TFlBeFJldzVPdEtWOHY3Y24xV0JUQ0puRVhvN0FSMVN6eVktNGNrSmU3Z1h1NXZPaVJjX2lpUGU2ZzNMaDAySURaQTlQeXVSbVcwUQ"),
                Attestation = AttestationConveyancePreference.None,
                AuthenticatorSelection = new AuthenticatorSelection()
                {
                    AuthenticatorAttachment = AuthenticatorAttachment.CrossPlatform,
                    RequireResidentKey = false,
                    UserVerification = UserVerificationRequirement.Discouraged
                },
                PubKeyCredParams = new List<PubKeyCredParam>()
                {
                    new PubKeyCredParam() { Alg = -7, Type = PublicKeyCredentialType.PublicKey }
                },
                ExcludeCredentials = new List<PublicKeyCredentialDescriptor>()
                {
                    new PublicKeyCredentialDescriptor(Convert.FromBase64String("vyI8VLKQTU57glEnMutpB39pY3amRqpFZoCgWIlxRdgOukrkboelrv1sOmjkttnuDflSm3NqiqjTHJWaFSt6X/Hq89uwirQrexUTzO0yJU0Qzs+kTU3iJm1hVy8XY7Rf")),
                    new PublicKeyCredentialDescriptor(Convert.FromBase64String("5W0+UWjiBBpJL2sVJ4JwI4LeE/u9RO10GqXEIBhccoupuNdwcW++J/x2JC/I8cjQL9cmWnOsZWyGkcgzck0StA=="))
                },
                Timeout = 30000
            };

            AttestationOptionsViewModel.Options = attestationOptions;
        }
    }
}
