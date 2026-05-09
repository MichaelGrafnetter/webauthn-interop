using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Interop;
using Prism.Commands;
using Prism.Dialogs;
using Prism.Mvvm;
using Prism.Navigation.Regions;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class MainWindowViewModel : BindableBase
{
    private static readonly JsonSerializerOptions _indentedJson = new() { WriteIndented = true };
    private static readonly WebAuthnJsonContext _indentedJsonContext = new(_indentedJson);

    private WebAuthnApi _api { get; set; }

    private IDialogService DialogService { get; set; }

    private IRegionManager RegionManager { get; set; }

    public MainWindowViewModel(
        WebAuthnApi api,
        IAttestationOptionsViewModel attestationOptionsViewModel,
        IAssertionOptionsViewModel assertionOptionsViewModel,
        ICredentialManagementViewModel credentialManagementViewModel,
        IDialogService dialogService,
        IRegionManager regionManager)
    {
        // Save dependencies
        _api = api;
        AttestationOptionsViewModel = attestationOptionsViewModel;
        AssertionOptionsViewModel = assertionOptionsViewModel;
        CredentialManagementViewModel = credentialManagementViewModel;
        DialogService = dialogService;
        RegionManager = regionManager;

        // Initialize commands
        ResetCommand = new DelegateCommand(OnReset);
        RegisterCommand = new AsyncDelegateCommand(OnRegister, () => AttestationOptionsViewModel.IsFormValid);
        AuthenticateCommand = new AsyncDelegateCommand(OnAuthenticate, () => AssertionOptionsViewModel.IsFormValid);
        SignAttestationCommand = new DelegateCommand(OnSignAttestation, () => AttestationOptionsViewModel.IsFormValid);
        SignAssertionCommand = new DelegateCommand(OnSignAssertion, () => AssertionOptionsViewModel.IsFormValid);
        ListPlatformCredentialsCommand = new DelegateCommand(OnListCredentials);
        LoadMicrosoftOptionsCommand = new DelegateCommand(OnLoadMicrosoftOptions);
        LoadGoogleOptionsCommand = new DelegateCommand(OnLoadGoogleOptions);
        LoadFacebookOptionsCommand = new DelegateCommand(OnLoadFacebookOptions);
        OpenHyperLinkCommand = new DelegateCommand<string>(OnOpenHyperLink);
        DeleteCredentialCommand = new DelegateCommand<CredentialDetails>(OnDeleteCredential);
        TestCredentialCommand = new AsyncDelegateCommand<CredentialDetails>(OnTestCredential);

        // Subscribe to form validity changes to update button states
        if (attestationOptionsViewModel is INotifyPropertyChanged attestationNotify)
        {
            attestationNotify.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(IAttestationOptionsViewModel.IsFormValid))
                {
                    ((DelegateCommandBase)RegisterCommand).RaiseCanExecuteChanged();
                    ((DelegateCommandBase)SignAttestationCommand).RaiseCanExecuteChanged();
                }
            };
        }

        if (assertionOptionsViewModel is INotifyPropertyChanged assertionNotify)
        {
            assertionNotify.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(IAssertionOptionsViewModel.IsFormValid))
                {
                    ((DelegateCommandBase)AuthenticateCommand).RaiseCanExecuteChanged();
                    ((DelegateCommandBase)SignAssertionCommand).RaiseCanExecuteChanged();
                }
            };
        }
    }

    public IAttestationOptionsViewModel AttestationOptionsViewModel { get; private set; }
    public IAssertionOptionsViewModel AssertionOptionsViewModel { get; private set; }
    public ICredentialManagementViewModel CredentialManagementViewModel { get; private set; }

    public ICommand ResetCommand { get; private set; }
    public ICommand RegisterCommand { get; private set; }
    public ICommand AuthenticateCommand { get; private set; }
    public ICommand SignAttestationCommand { get; private set; }
    public ICommand SignAssertionCommand { get; private set; }
    public ICommand ListPlatformCredentialsCommand { get; private set; }
    public ICommand LoadMicrosoftOptionsCommand { get; private set; }
    public ICommand LoadGoogleOptionsCommand { get; private set; }
    public ICommand LoadFacebookOptionsCommand { get; private set; }
    public ICommand OpenHyperLinkCommand { get; private set; }
    public ICommand DeleteCredentialCommand { get; private set; }
    public ICommand TestCredentialCommand { get; private set; }

    public int SelectedTabIndex
    {
        get;
        set => SetProperty(ref field, value);
    }

    public string? AttestationResponse
    {
        get;
        set => SetProperty(ref field, value);
    }

    public string? AssertionResponse
    {
        get;
        set => SetProperty(ref field, value);
    }

    public ObservableCollection<CredentialDetails> Credentials { get; } = [];

    private void OnReset()
    {
        AttestationOptionsViewModel.ResetOptionsCommand.Execute(null);
        AssertionOptionsViewModel.ResetOptionsCommand.Execute(null);
        CredentialManagementViewModel.ResetFilterCommand.Execute(null);
        AttestationResponse = null;
        AssertionResponse = null;
    }

    private void OnDeleteCredential(CredentialDetails? credential)
    {
        if (credential?.CredentialId == null)
        {
            return;
        }

        var rpId = credential.RelyingPartyInformation?.Id ?? "Unknown";
        var userName = credential.UserInformation?.Name ?? "Unknown";
        var message = $"Are you sure you want to delete this credential?\n\nRP ID: {rpId}\nUser Name: {userName}";

        DialogService.ShowConfirmationDialog(new ConfirmationDialogParameters
        {
            Message = message,
            Title = "Delete Credential"
        }, confirmed =>
        {
            if (!confirmed)
                return;

            try
            {
                WebAuthnApi.DeletePlatformCredential(credential.CredentialId);
                Credentials.Remove(credential);
            }
            catch (Exception ex)
            {
                DialogService.ShowNotificationDialog(ex.Message);
            }
        });
    }

    private async Task OnTestCredential(CredentialDetails? credential)
    {
        if (credential?.CredentialId == null || credential.RelyingPartyInformation?.Id == null)
        {
            return;
        }

        try
        {
            // Generate a random challenge for the test
            byte[] challenge = new byte[ApiConstants.DefaultChallengeLength];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(challenge);
            }

            // Create allowed credentials list with the specific credential ID
            List<PublicKeyCredentialDescriptor> allowCredentials =
            [
                new(credential.CredentialId, AuthenticatorTransport.Internal)
            ];

            // Perform authentication
            var response = await _api.AuthenticatorGetAssertionAsync(
                credential.RelyingPartyInformation.Id,
                challenge,
                UserVerificationRequirement.Preferred,
                AuthenticatorAttachment.Platform,
                ApiConstants.DefaultTimeoutMilliseconds,
                allowCredentials,
                windowHandle: WindowHandle.MainWindow
            );

            // On success, show the result in the Authentication tab
            this.AssertionResponse = JsonSerializer.Serialize(response, _indentedJsonContext.AssertionPublicKeyCredential);

            // Pre-fill the assertion options view model for easier retesting
            AssertionOptionsViewModel.ResetOptionsCommand.Execute(null);
            AssertionOptionsViewModel.RelyingPartyId = credential.RelyingPartyInformation.Id;
            AssertionOptionsViewModel.Challenge = challenge;
            AssertionOptionsViewModel.UserVerificationRequirement = UserVerificationRequirement.Preferred;
            AssertionOptionsViewModel.AuthenticatorAttachment = AuthenticatorAttachment.Platform;
            AssertionOptionsViewModel.CredentialHint = PublicKeyCredentialHint.ClientDevice;

            // Switch to the Authentication tab (index 2)
            this.SelectedTabIndex = 2;
        }
        catch (Exception ex)
        {
            DialogService.ShowNotificationDialog(ex.Message);
        }
    }

    private void OnOpenHyperLink(string link)
    {
        Process.Start(new ProcessStartInfo()
        {
            FileName = link,
            UseShellExecute = true
        });
    }

    private static string[]? GetCredentialHints(PublicKeyCredentialHint hint)
    {
        string? hintValue = hint.ToJsonString();
        return hintValue != null ? [hintValue] : null;
    }

    private async Task OnRegister()
    {
        try
        {
            // Clear the results window first
            this.AttestationResponse = null;

            // Convert single hint to array if specified
            string[]? credentialHints = GetCredentialHints(AttestationOptionsViewModel.CredentialHint);

            var response = await _api.AuthenticatorMakeCredentialAsync(
                AttestationOptionsViewModel.RelyingPartyEntity,
                AttestationOptionsViewModel.UserEntity,
                AttestationOptionsViewModel.Challenge,
                AttestationOptionsViewModel.UserVerificationRequirement,
                AttestationOptionsViewModel.AuthenticatorAttachment,
                residentKey: AttestationOptionsViewModel.ResidentKey,
                AttestationOptionsViewModel.PublicKeyCredentialParameters?.ToArray(),
                AttestationOptionsViewModel.AttestationConveyancePreference,
                AttestationOptionsViewModel.Timeout,
                extensions: AttestationOptionsViewModel.ClientExtensions,
                excludeCredentials: null,
                enterpriseAttestation: AttestationOptionsViewModel.EnterpriseAttestation,
                browserInPrivateMode: AttestationOptionsViewModel.IsBrowserPrivateMode,
                linkedDevice: null,
                credentialHints: credentialHints,
                windowHandle: WindowHandle.MainWindow
                );

            this.AttestationResponse = JsonSerializer.Serialize(response, _indentedJsonContext.AttestationPublicKeyCredential);
        }
        catch (Exception ex)
        {
            DialogService.ShowNotificationDialog(ex.Message);
        }
    }

    private async Task OnAuthenticate()
    {
        try
        {
            // Clear the results window first
            this.AssertionResponse = null;

            // Convert single hint to array if specified
            string[]? credentialHints = GetCredentialHints(AssertionOptionsViewModel.CredentialHint);

            var response = await _api.AuthenticatorGetAssertionAsync(
                AssertionOptionsViewModel.RelyingPartyId,
                AssertionOptionsViewModel.Challenge,
                AssertionOptionsViewModel.UserVerificationRequirement,
                AssertionOptionsViewModel.AuthenticatorAttachment,
                AssertionOptionsViewModel.Timeout,
                allowCredentials: null,
                extensions: AssertionOptionsViewModel.ClientExtensions,
                browserInPrivateMode: AssertionOptionsViewModel.IsBrowserPrivateMode,
                linkedDevice: null,
                credentialHints: credentialHints,
                windowHandle: WindowHandle.MainWindow
            );

            this.AssertionResponse = JsonSerializer.Serialize(response, _indentedJsonContext.AssertionPublicKeyCredential);
        }
        catch (Exception ex)
        {
            DialogService.ShowNotificationDialog(ex.Message);
        }
    }

    private void OnSignAttestation()
    {
        var defaultAlgorithm = AttestationOptionsViewModel.PublicKeyCredentialParameters?.Count > 0
            ? AttestationOptionsViewModel.PublicKeyCredentialParameters[0]
            : Algorithm.ES256;

        byte[]? lastCredentialId = null;
        if (!string.IsNullOrWhiteSpace(AttestationResponse))
        {
            try
            {
                var lastAttestation = JsonSerializer.Deserialize(AttestationResponse, _indentedJsonContext.AttestationPublicKeyCredential);
                if (lastAttestation?.Id is { Length: > 0 })
                    lastCredentialId = lastAttestation.Id;
            }
            catch (JsonException) { }
        }

        DialogService.ShowAttestationSigningDialog(new AttestationSigningDialogParameters
        {
            RelyingParty = AttestationOptionsViewModel.RelyingPartyEntity,
            User = AttestationOptionsViewModel.UserEntity,
            Challenge = AttestationOptionsViewModel.Challenge,
            UserVerificationRequirement = AttestationOptionsViewModel.UserVerificationRequirement,
            DefaultAlgorithm = defaultAlgorithm,
            LastCredentialId = lastCredentialId
        }, response =>
        {
            if (response != null)
                AttestationResponse = response;
        });
    }

    private void OnSignAssertion()
    {
        // Try to extract the credential ID from the last attestation response
        byte[]? lastCredentialId = null;
        if (!string.IsNullOrWhiteSpace(AttestationResponse))
        {
            try
            {
                var lastAttestation = JsonSerializer.Deserialize(AttestationResponse, _indentedJsonContext.AttestationPublicKeyCredential);
                if (lastAttestation?.Id is { Length: > 0 })
                    lastCredentialId = lastAttestation.Id;
            }
            catch (JsonException)
            {
                // Ignore parse errors; leave lastCredentialId null
            }
        }

        var defaultAlgorithm = AttestationOptionsViewModel.PublicKeyCredentialParameters?.Count > 0
            ? AttestationOptionsViewModel.PublicKeyCredentialParameters[0]
            : Algorithm.ES256;

        DialogService.ShowAssertionSigningDialog(new AssertionSigningDialogParameters
        {
            RelyingPartyId = AssertionOptionsViewModel.RelyingPartyId ?? string.Empty,
            Challenge = AssertionOptionsViewModel.Challenge ?? [],
            UserVerificationRequirement = AssertionOptionsViewModel.UserVerificationRequirement,
            DefaultAlgorithm = defaultAlgorithm,
            CredentialId = lastCredentialId
        }, response =>
        {
            if (response != null)
                AssertionResponse = response;
        });
    }

    private void OnListCredentials()
    {
        try
        {
            // Clear the results first
            this.Credentials.Clear();

            var credentials = WebAuthnApi.GetPlatformCredentialList(
                CredentialManagementViewModel.RelyingPartyId,
                CredentialManagementViewModel.IsBrowserPrivateMode);

            // Populate the collection for the grid view
            if (credentials != null)
            {
                foreach (var credential in credentials)
                {
                    this.Credentials.Add(credential);
                }
            }
        }
        catch (Exception ex)
        {
            DialogService.ShowNotificationDialog(ex.Message);
        }
    }

    private void OnLoadMicrosoftOptions()
    {
        // Reset the UI first
        ResetCommand.Execute(null);

        // Assertion:
        AssertionOptionsViewModel.RelyingPartyId = "login.microsoft.com";
        AssertionOptionsViewModel.Challenge = Base64UrlConverter.FromBase64UrlString("Ty5leUowZVhBaU9pSktWMVFpTENKaGJHY2lPaUpTVXpJMU5pSXNJbmcxZENJNklqVlBaamxRTlVZNVowTkRkME50UmpKQ1QwaEllRVJFVVMxRWF5SjkuZXlKaGRXUWlPaUoxY200NmJXbGpjbTl6YjJaME9tWnBaRzg2WTJoaGJHeGxibWRsSWl3aWFYTnpJam9pYUhSMGNITTZMeTlzYjJkcGJpNXRhV055YjNOdlpuUXVZMjl0SWl3aWFXRjBJam94TmpBNU1qYzNOVFE0TENKdVltWWlPakUyTURreU56YzFORGdzSW1WNGNDSTZNVFl3T1RJM056ZzBPSDAubDNyeF9JTnNLT0hsZTR5azdvSmk3MG0yMUNsV2lWWklJMGxRdVhJbWZ0N1RMX0ppcTRpc0Uza05vRjR6X0cyYlFhdDdaOG55dVRZamNkTmsxSm5OT0k1ZXBMMUIwNkR4N21OU05sZ3ZlWWhKR05faVB4RC1lOVJkVXJvNjlPLWx1cHRPUjVQX3B6dUpWU0dGTFEwLXBZUHE5NzlEVmI2ZF9pMHYxbjBKakd3bkxIMVE5b3ZRSEJzR1E1YzFvMUhNSDNBdEltZjI0Zk9McHBOT2s3WXM5OXlIdFM5VkFwcTNmbG5vT3VxWVpXQzBDMnJjNXpsdzAwR3p5OGV6NWZra010SDNKRGpmbWZ1aWxfb1RCc20yQUIwX1Y5NjZxRlJuMGxsWXcteXlBTDlISU1LelM1TU01aDhpb1lFakxrZlRGblpJandUVERpOHV3SG1LMkVueFpn");
        AssertionOptionsViewModel.UserVerificationRequirement = UserVerificationRequirement.Required;
        AssertionOptionsViewModel.Timeout = 120000;

        // Attestation:
        AttestationOptionsViewModel.UserEntity = new UserInformation()
        {
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

        AttestationOptionsViewModel.ClientExtensions = new AuthenticationExtensionsClientAttestationInputs()
        {
            CredProtect = UserVerification.Optional,
            HmacCreateSecret = true
        };

        AttestationOptionsViewModel.Challenge = Encoding.ASCII.GetBytes("CbWTU93Ppbgok1glyka*K9sZSWkqpK3qS1ldeLJxsI4k3jMLIi3dl8VDx10siTGd8U5SNj8yyMIbqXQH!apXGnrhWmYlg2GNdEGddIkO03cql!kKVgKi*MqEIl9aPqmJdYuRMjrEYlIyzi4*wP0YSyA$");
        AttestationOptionsViewModel.AuthenticatorAttachment = AuthenticatorAttachment.CrossPlatform;
        AttestationOptionsViewModel.UserVerificationRequirement = UserVerificationRequirement.Required;
        AttestationOptionsViewModel.ResidentKey = ResidentKeyRequirement.Required;
        AttestationOptionsViewModel.AttestationConveyancePreference = AttestationConveyancePreference.Direct;
        AttestationOptionsViewModel.EnterpriseAttestation = EnterpriseAttestationType.None;
        AttestationOptionsViewModel.PublicKeyCredentialParameters = [Algorithm.ES256, Algorithm.RS256];
        AttestationOptionsViewModel.Timeout = 120000;

        // Credential Management
        CredentialManagementViewModel.RelyingPartyId = "login.microsoft.com";
    }

    private void OnLoadFacebookOptions()
    {
        // Reset the UI first
        ResetCommand.Execute(null);

        // Assertion:
        AssertionOptionsViewModel.RelyingPartyId = "facebook.com";
        AssertionOptionsViewModel.Challenge = Base64UrlConverter.FromBase64UrlString("QmFQclh3QUFBQURTQkZoZzczeExFUVo2c0dNVndxdjI1ajc0eVhZVkx2c2JUemZtWlc4RkFSM3VDV2ZvTHpENFlXdnZSTTI4bnQzZG1nd1ZoSEg2WWhTRmgxNnpuX3NsVmJJMA");
        AssertionOptionsViewModel.UserVerificationRequirement = UserVerificationRequirement.Preferred;
        AssertionOptionsViewModel.AuthenticatorAttachment = AuthenticatorAttachment.CrossPlatform;
        AssertionOptionsViewModel.Timeout = 60000;

        AssertionOptionsViewModel.ClientExtensions = new AuthenticationExtensionsClientAssertionInputs()
        {
            AppID = "https://www.facebook.com/u2f/app_id/?uid=740592393&s=AY66Df2A7Ed9rniipaIgG81SH6QO0rlmffyygOtXMZPrBg"
        };

        // Attestation:
        AttestationOptionsViewModel.UserEntity = new UserInformation()
        {
            Name = "john.doe",
            DisplayName = "John Doe",
            Id = [0x37, 0x34, 0x37, 0x35, 0x39, 0x32, 0x33, 0x39, 0x33]
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
        AttestationOptionsViewModel.PublicKeyCredentialParameters = [Algorithm.ES256];
        AttestationOptionsViewModel.Timeout = 60000;

        // Credential Management
        CredentialManagementViewModel.RelyingPartyId = "facebook.com";
    }

    private void OnLoadGoogleOptions()
    {
        // Reset the UI first
        ResetCommand.Execute(null);

        // Assertion:
        AssertionOptionsViewModel.RelyingPartyId = "google.com";
        AssertionOptionsViewModel.Challenge = Base64UrlConverter.FromBase64UrlString("YnFQclh3QUFBQUFpRUZtN1BBSFpURkR1TFlBeFJldzVPdEtWOHY3Y24xV0JUQ0puRVhvN0FSMVN6eVktNGNrSmU3Z1h1NXZPaVJjX2lpUGU2ZzNMaDAySURaQTlQeXVSbVcwUQ");
        AssertionOptionsViewModel.UserVerificationRequirement = UserVerificationRequirement.Discouraged;
        AssertionOptionsViewModel.Timeout = 30000;

        AssertionOptionsViewModel.ClientExtensions = new AuthenticationExtensionsClientAssertionInputs()
        {
            AppID = "https://www.gstatic.com/securitykey/origins.json"
        };

        // Attestation:
        AttestationOptionsViewModel.UserEntity = new UserInformation()
        {
            Name = "john.doe",
            DisplayName = "John Doe",
            Id = [0x37, 0x34, 0x37, 0x35, 0x39, 0x32, 0x33, 0x39, 0x33]
        };

        AttestationOptionsViewModel.RelyingPartyEntity = new RelyingPartyInformation()
        {
            Name = "Google",
            Id = "https://www.gstatic.com/securitykey/origins.json"
        };

        AttestationOptionsViewModel.Challenge = Base64UrlConverter.FromBase64UrlString("YnFQclh3QUFBQUFpRUZtN1BBSFpURkR1TFlBeFJldzVPdEtWOHY3Y24xV0JUQ0puRVhvN0FSMVN6eVktNGNrSmU3Z1h1NXZPaVJjX2lpUGU2ZzNMaDAySURaQTlQeXVSbVcwUQ");
        AttestationOptionsViewModel.AuthenticatorAttachment = AuthenticatorAttachment.CrossPlatform;
        AttestationOptionsViewModel.UserVerificationRequirement = UserVerificationRequirement.Discouraged;
        AttestationOptionsViewModel.PublicKeyCredentialParameters = [Algorithm.ES256];
        AttestationOptionsViewModel.Timeout = 30000;

        // Credential Management
        CredentialManagementViewModel.RelyingPartyId = "google.com";
    }
}
