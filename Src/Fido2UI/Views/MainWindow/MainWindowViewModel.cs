using System;
using System.Net;
using System.Text;
using System.Windows.Input;
using DSInternals.Win32.WebAuthn.COSE;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public class MainWindowViewModel : BindableBase
    {
        private WebAuthnApi _api { get; set; }
        private IAttestationOptionsViewModel AttestationOptionsViewModel { get; set; }
        private IAssertionOptionsViewModel AssertionOptionsViewModel { get; set; }
        private ICredentialManagementViewModel CredentialManagementViewModel { get; set; }
        private IDialogService DialogService { get; set; }

        public MainWindowViewModel(
            WebAuthnApi api,
            IAttestationOptionsViewModel attestationOptionsViewModel,
            IAssertionOptionsViewModel assertionOptionsViewModel,
            ICredentialManagementViewModel credentialManagementViewModel,
            IDialogService dialogService)
        {
            // Save dependencies
            _api = api;
            AttestationOptionsViewModel = attestationOptionsViewModel;
            AssertionOptionsViewModel = assertionOptionsViewModel;
            CredentialManagementViewModel = credentialManagementViewModel;
            DialogService = dialogService;

            // Initialize commands
            RegisterCommand = new DelegateCommand(OnRegister);
            AuthenticateCommand = new DelegateCommand(OnAuthenticate);
            ListPlatformCredentialsCommand = new DelegateCommand(OnListCredentials);
            LoadMicrosoftOptionsCommand = new DelegateCommand(OnLoadMicrosoftOptions);
            LoadGoogleOptionsCommand = new DelegateCommand(OnLoadGoogleOptions);
            LoadFacebookOptionsCommand = new DelegateCommand(OnLoadFacebookOptions);
        }

        public ICommand RegisterCommand { get; private set; }
        public ICommand AuthenticateCommand { get; private set; }
        public ICommand ListPlatformCredentialsCommand { get; private set; }
        public ICommand LoadMicrosoftOptionsCommand { get; private set; }
        public ICommand LoadGoogleOptionsCommand { get; private set; }
        public ICommand LoadFacebookOptionsCommand { get; private set; }

        private string _attestationResponse;
        public string AttestationResponse
        {
            get => _attestationResponse;
            set => SetProperty(ref _attestationResponse, value);
        }

        private string _assertionResponse;
        public string AssertionResponse
        {
            get => _assertionResponse;
            set => SetProperty(ref _assertionResponse, value);
        }

        private string _credentialManagerResponse;
        public string CredentialManagerResponse
        {
            get => _credentialManagerResponse;
            set => SetProperty(ref _credentialManagerResponse, value);
        }

        private void OnRegister()
        {
            try
            {
                // Clear the results window first
                this.AttestationResponse = null;

                var response = _api.AuthenticatorMakeCredential(
                    AttestationOptionsViewModel.RelyingPartyEntity,
                    AttestationOptionsViewModel.UserEntity,
                    AttestationOptionsViewModel.Challenge,
                    AttestationOptionsViewModel.UserVerificationRequirement,
                    AttestationOptionsViewModel.AuthenticatorAttachment,
                    AttestationOptionsViewModel.RequireResidentKey,
                    AttestationOptionsViewModel.PublicKeyCredentialParameters,
                    AttestationOptionsViewModel.AttestationConveyancePreference,
                    AttestationOptionsViewModel.Timeout,
                    AttestationOptionsViewModel.ClientExtensions
                    );

                this.AttestationResponse = JsonConvert.SerializeObject(response, Formatting.Indented);
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
                // Clear the results window first
                this.AssertionResponse = null;

                var response = _api.AuthenticatorGetAssertion(
                    AssertionOptionsViewModel.RelyingPartyId,
                    AssertionOptionsViewModel.Challenge,
                    AssertionOptionsViewModel.UserVerificationRequirement,
                    AssertionOptionsViewModel.AuthenticatorAttachment,
                    AssertionOptionsViewModel.Timeout
                );

                this.AssertionResponse = JsonConvert.SerializeObject(response, Formatting.Indented);
            }
            catch (Exception ex)
            {
                var parameters = new DialogParameters($"Message={ex.Message}");
                DialogService.ShowDialog(nameof(NotificationDialog), parameters, null);
            }
        }

        private void OnListCredentials()
        {
            try
            {
                // Clear the results window first
                this.CredentialManagerResponse = null;

                var credentials = WebAuthnApi.GetPlatformCredentialList(
                    CredentialManagementViewModel.RelyingPartyId,
                    CredentialManagementViewModel.IsBrowserPrivateMode);

                this.CredentialManagerResponse = JsonConvert.SerializeObject(credentials, Formatting.Indented);
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
            AssertionOptionsViewModel.RelyingPartyId = "login.microsoft.com";
            AssertionOptionsViewModel.Challenge = Base64UrlConverter.FromBase64UrlString("Ty5leUowZVhBaU9pSktWMVFpTENKaGJHY2lPaUpTVXpJMU5pSXNJbmcxZENJNklqVlBaamxRTlVZNVowTkRkME50UmpKQ1QwaEllRVJFVVMxRWF5SjkuZXlKaGRXUWlPaUoxY200NmJXbGpjbTl6YjJaME9tWnBaRzg2WTJoaGJHeGxibWRsSWl3aWFYTnpJam9pYUhSMGNITTZMeTlzYjJkcGJpNXRhV055YjNOdlpuUXVZMjl0SWl3aWFXRjBJam94TmpBNU1qYzNOVFE0TENKdVltWWlPakUyTURreU56YzFORGdzSW1WNGNDSTZNVFl3T1RJM056ZzBPSDAubDNyeF9JTnNLT0hsZTR5azdvSmk3MG0yMUNsV2lWWklJMGxRdVhJbWZ0N1RMX0ppcTRpc0Uza05vRjR6X0cyYlFhdDdaOG55dVRZamNkTmsxSm5OT0k1ZXBMMUIwNkR4N21OU05sZ3ZlWWhKR05faVB4RC1lOVJkVXJvNjlPLWx1cHRPUjVQX3B6dUpWU0dGTFEwLXBZUHE5NzlEVmI2ZF9pMHYxbjBKakd3bkxIMVE5b3ZRSEJzR1E1YzFvMUhNSDNBdEltZjI0Zk9McHBOT2s3WXM5OXlIdFM5VkFwcTNmbG5vT3VxWVpXQzBDMnJjNXpsdzAwR3p5OGV6NWZra010SDNKRGpmbWZ1aWxfb1RCc20yQUIwX1Y5NjZxRlJuMGxsWXcteXlBTDlISU1LelM1TU01aDhpb1lFakxrZlRGblpJandUVERpOHV3SG1LMkVueFpn");
            AssertionOptionsViewModel.UserVerificationRequirement = UserVerificationRequirement.Required;
            AssertionOptionsViewModel.AuthenticatorAttachment = AuthenticatorAttachment.Any;
            AssertionOptionsViewModel.ClientExtensions = null;
            AssertionOptionsViewModel.Timeout = 120000;

            // Attestation:
            AttestationOptionsViewModel.UserEntity = new UserInformation() {
                Name = "john.doe@outlook.com",
                DisplayName = "John Doe",
                Id = Convert.FromBase64String("vzcamjpGT/VqULKaTqxpNCAGx66PkL10thAmvwmroAAga3lW/x8wTCBUEfL5aquu5g==")
            };

            AttestationOptionsViewModel.RelyingPartyEntity = new RelyingPartyInformation()
            {
                Name = "Microsoft",
                Id = "login.microsoft.com",
                Icon = String.Empty
            };

            AttestationOptionsViewModel.ClientExtensions = new AuthenticationExtensionsClientInputs()
            {
                CredProtect = UserVerification.Optional,
                HmacSecret = true
            };

            AttestationOptionsViewModel.Challenge = Encoding.ASCII.GetBytes("CbWTU93Ppbgok1glyka*K9sZSWkqpK3qS1ldeLJxsI4k3jMLIi3dl8VDx10siTGd8U5SNj8yyMIbqXQH!apXGnrhWmYlg2GNdEGddIkO03cql!kKVgKi*MqEIl9aPqmJdYuRMjrEYlIyzi4*wP0YSyA$");
            AttestationOptionsViewModel.AuthenticatorAttachment = AuthenticatorAttachment.CrossPlatform;
            AttestationOptionsViewModel.UserVerificationRequirement = UserVerificationRequirement.Required;
            AttestationOptionsViewModel.RequireResidentKey = true;
            AttestationOptionsViewModel.AttestationConveyancePreference = AttestationConveyancePreference.Direct;
            AttestationOptionsViewModel.PublicKeyCredentialParameters = new[] { Algorithm.ES256, Algorithm.RS256 };
            AttestationOptionsViewModel.Timeout = 120000;

            // Credential Management
            CredentialManagementViewModel.RelyingPartyId = "login.microsoft.com";
        }

        private void OnLoadFacebookOptions()
        {
            // Assertion:
            AssertionOptionsViewModel.RelyingPartyId = "facebook.com";
            AssertionOptionsViewModel.Challenge = Base64UrlConverter.FromBase64UrlString("QmFQclh3QUFBQURTQkZoZzczeExFUVo2c0dNVndxdjI1ajc0eVhZVkx2c2JUemZtWlc4RkFSM3VDV2ZvTHpENFlXdnZSTTI4bnQzZG1nd1ZoSEg2WWhTRmgxNnpuX3NsVmJJMA");
            AssertionOptionsViewModel.UserVerificationRequirement = UserVerificationRequirement.Preferred;
            AssertionOptionsViewModel.AuthenticatorAttachment = AuthenticatorAttachment.CrossPlatform;
            AssertionOptionsViewModel.Timeout = 60000;

            AssertionOptionsViewModel.ClientExtensions = new AuthenticationExtensionsClientInputs()
            {
                AppID = "https://www.facebook.com/u2f/app_id/?uid=740592393&s=AY66Df2A7Ed9rniipaIgG81SH6QO0rlmffyygOtXMZPrBg"
            };

            // Attestation:
            AttestationOptionsViewModel.UserEntity = new UserInformation()
            {
                Name = "john.doe",
                DisplayName = "John Doe",
                Id = new byte[] { 0x37, 0x34, 0x37, 0x35, 0x39, 0x32, 0x33, 0x39, 0x33 }
            };

            AttestationOptionsViewModel.RelyingPartyEntity = new RelyingPartyInformation()
            {
                Name = "Facebook",
                Id = "facebook.com",
                Icon = String.Empty
            };

            AttestationOptionsViewModel.Challenge = Base64UrlConverter.FromBase64UrlString("YnFQclh3QUFBQUFpRUZtN1BBSFpURkR1TFlBeFJldzVPdEtWOHY3Y24xV0JUQ0puRVhvN0FSMVN6eVktNGNrSmU3Z1h1NXZPaVJjX2lpUGU2ZzNMaDAySURaQTlQeXVSbVcwUQ");
            AttestationOptionsViewModel.AuthenticatorAttachment = AuthenticatorAttachment.CrossPlatform;
            AttestationOptionsViewModel.UserVerificationRequirement = UserVerificationRequirement.Preferred;
            AttestationOptionsViewModel.RequireResidentKey = false;
            AttestationOptionsViewModel.AttestationConveyancePreference = AttestationConveyancePreference.None;
            AttestationOptionsViewModel.PublicKeyCredentialParameters = new[] { Algorithm.ES256 };
            AttestationOptionsViewModel.ClientExtensions = null;
            AttestationOptionsViewModel.Timeout = 60000;

            // Credential Management
            CredentialManagementViewModel.RelyingPartyId = "facebook.com";
        }

        private void OnLoadGoogleOptions()
        {
            // Assertion:
            AssertionOptionsViewModel.RelyingPartyId = "google.com";
            AssertionOptionsViewModel.Challenge = Base64UrlConverter.FromBase64UrlString("YnFQclh3QUFBQUFpRUZtN1BBSFpURkR1TFlBeFJldzVPdEtWOHY3Y24xV0JUQ0puRVhvN0FSMVN6eVktNGNrSmU3Z1h1NXZPaVJjX2lpUGU2ZzNMaDAySURaQTlQeXVSbVcwUQ");
            AssertionOptionsViewModel.UserVerificationRequirement = UserVerificationRequirement.Discouraged;
            AssertionOptionsViewModel.AuthenticatorAttachment = AuthenticatorAttachment.Any;
            AssertionOptionsViewModel.Timeout = 30000;

            AssertionOptionsViewModel.ClientExtensions = new AuthenticationExtensionsClientInputs()
            {
                AppID = "https://www.gstatic.com/securitykey/origins.json"
            };

            // Attestation:
            AttestationOptionsViewModel.UserEntity = new UserInformation()
            {
                Name = "john.doe",
                DisplayName = "John Doe",
                Id = new byte[] { 0x37, 0x34, 0x37, 0x35, 0x39, 0x32, 0x33, 0x39, 0x33 }
            };

            AttestationOptionsViewModel.RelyingPartyEntity = new RelyingPartyInformation()
            {
                Id = "https://www.gstatic.com/securitykey/origins.json"
            };

            AttestationOptionsViewModel.Challenge = Base64UrlConverter.FromBase64UrlString("YnFQclh3QUFBQUFpRUZtN1BBSFpURkR1TFlBeFJldzVPdEtWOHY3Y24xV0JUQ0puRVhvN0FSMVN6eVktNGNrSmU3Z1h1NXZPaVJjX2lpUGU2ZzNMaDAySURaQTlQeXVSbVcwUQ");
            AttestationOptionsViewModel.AuthenticatorAttachment = AuthenticatorAttachment.CrossPlatform;
            AttestationOptionsViewModel.UserVerificationRequirement = UserVerificationRequirement.Discouraged;
            AttestationOptionsViewModel.RequireResidentKey = false;
            AttestationOptionsViewModel.AttestationConveyancePreference = AttestationConveyancePreference.None;
            AttestationOptionsViewModel.PublicKeyCredentialParameters = new[] { Algorithm.ES256 };
            AttestationOptionsViewModel.ClientExtensions = null;
            AttestationOptionsViewModel.Timeout = 30000;

            // Credential Management
            CredentialManagementViewModel.RelyingPartyId = "google.com";
        }
    }
}
