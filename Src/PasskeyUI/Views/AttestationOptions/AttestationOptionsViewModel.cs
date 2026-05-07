using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Input;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Interop;
using Prism.Commands;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class AttestationOptionsViewModel : BindableBase, IAttestationOptionsViewModel
{
    private const int RandomUserIdLength = 32;

    public AttestationOptionsViewModel(IAlgorithmSelectorViewModel algorithmSelectorViewModel)
    {
        // Save dependencies
        AlgorithmSelectorViewModel = algorithmSelectorViewModel;

        // Configure default values
        Timeout = ApiConstants.DefaultTimeoutMilliseconds;

        // Initialize commands
        ResetOptionsCommand = new DelegateCommand(OnResetOptions);
        GenerateChallengeCommand = new DelegateCommand(OnGenerateChallenge);
        GenerateUserIdCommand = new DelegateCommand(OnGenerateUserId);
    }

    public IAlgorithmSelectorViewModel AlgorithmSelectorViewModel { get; private set; }

    public ICommand ResetOptionsCommand { get; private set; }
    public ICommand GenerateChallengeCommand { get; private set; }
    public ICommand GenerateUserIdCommand { get; private set; }

    private void OnGenerateChallenge()
    {
        Challenge = GetRandomBytes(ApiConstants.DefaultChallengeLength);
    }

    private void OnGenerateUserId()
    {
        UserId = GetRandomBytes(RandomUserIdLength);
    }

    private void OnResetOptions()
    {
        RpId = null;
        RpName = null;
        UserName = null;
        UserDisplayName = null;
        UserId = null;
        Challenge = null;
        ResidentKey = ResidentKeyRequirement.Discouraged;
        AuthenticatorAttachment = AuthenticatorAttachment.Any;
        UserVerificationRequirement = UserVerificationRequirement.Preferred;
        PublicKeyCredentialParameters = [Algorithm.ES256];
        AttestationConveyancePreference = AttestationConveyancePreference.None;
        EnterpriseAttestation = EnterpriseAttestationType.None;
        Timeout = ApiConstants.DefaultTimeoutMilliseconds;
        CredProtectPolicy = UserVerification.Any;
        EnforceCredProtect = false;
        MinPinLength = false;
        HmacSecret = false;
        EnablePseudoRandomFunction = false;
        LargeBlobSupport = LargeBlobSupport.None;
        CredentialBlob = null;
        IsBrowserPrivateMode = false;
        CredentialHint = PublicKeyCredentialHint.None;
        ThirdPartyPayment = false;
        RemoteWebOrigin = null;
    }

    public string RpId
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
                RaisePropertyChanged(nameof(IsFormValid));
        }
    }

    public string RpName
    {
        get;
        set => SetProperty(ref field, value);
    }

    public string UserName
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
                RaisePropertyChanged(nameof(IsFormValid));
        }
    }

    public string UserDisplayName
    {
        get;
        set => SetProperty(ref field, value);
    }

    public byte[] UserId
    {
        get;
        set => SetProperty(ref field, value);
    }

    public byte[]? Challenge
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
                RaisePropertyChanged(nameof(IsFormValid));
        }
    }

    public ResidentKeyRequirement ResidentKey
    {
        get;
        set => SetProperty(ref field, value);
    }

    public IList<KeyValuePair<ResidentKeyRequirement?, string>> ResidentKeyRequirements
    => EnumAdapter.GetComboBoxItems<ResidentKeyRequirement>();

    public AuthenticatorAttachment AuthenticatorAttachment
    {
        get;
        set => SetProperty(ref field, value);
    }

    public IList<KeyValuePair<AuthenticatorAttachment?, string>> AuthenticatorAttachments
     => EnumAdapter.GetComboBoxItems<AuthenticatorAttachment>();

    public UserVerificationRequirement UserVerificationRequirement
    {
        get;
        set => SetProperty(ref field, value);
    }

    public IList<KeyValuePair<UserVerificationRequirement?, string>> UserVerificationRequirements
    => EnumAdapter.GetComboBoxItems<UserVerificationRequirement>();

    public AttestationConveyancePreference AttestationConveyancePreference
    {
        get;
        set => SetProperty(ref field, value);
    }

    public IList<KeyValuePair<AttestationConveyancePreference?, string>> AttestationTypes
    => EnumAdapter.GetComboBoxItems<AttestationConveyancePreference>();

    public EnterpriseAttestationType EnterpriseAttestation
    {
        get;
        set => SetProperty(ref field, value);
    }

    public IList<KeyValuePair<EnterpriseAttestationType?, string>> EnterpriseAttestationTypes
    => EnumAdapter.GetComboBoxItems<EnterpriseAttestationType>();

    public uint Timeout
    {
        get;
        set => SetProperty(ref field, value);
    }

    public IList<KeyValuePair<UserVerification?, string>> CredProtectPolicies
    => EnumAdapter.GetComboBoxItems<UserVerification>();

    public IList<KeyValuePair<LargeBlobSupport?, string>> LargeBlobSupportPolicies
    => EnumAdapter.GetComboBoxItems<LargeBlobSupport>();

    public RelyingPartyInformation RelyingPartyEntity
    {
        get
        {
            return new RelyingPartyInformation()
            {
                Id = RpId,
                Name = RpName
            };
        }
        set
        {
            if (value != null)
            {
                RpId = value.Id;
                RpName = value.Name;
            }
            else
            {
                // Load default values
                RpId = null;
                RpName = null;
            }
        }
    }

    public UserInformation UserEntity
    {
        get
        {
            return new UserInformation()
            {
                Id = UserId,
                DisplayName = UserDisplayName,
                Name = UserName
            };
        }
        set
        {
            if (value != null)
            {
                UserId = value.Id;
                UserName = value.Name;
                UserDisplayName = value.DisplayName;
            }
            else
            {
                // Load default values
                UserId = null;
                UserName = null;
                UserDisplayName = null;
            }
        }
    }

    public List<Algorithm> PublicKeyCredentialParameters
    {
        get => AlgorithmSelectorViewModel.SelectedAlgorithms;
        set => AlgorithmSelectorViewModel.SelectedAlgorithms = value;
    }

    public AuthenticationExtensionsClientAttestationInputs? ClientExtensions
    {
        get
        {
            string? remoteWebOrigin = string.IsNullOrWhiteSpace(RemoteWebOrigin) ? null : RemoteWebOrigin.Trim();
            var remoteDesktopClientOverride = remoteWebOrigin != null
                ? new RemoteDesktopClientOverride { Origin = remoteWebOrigin, SameOriginWithAncestors = false }
                : null;

            if (CredProtectPolicy == UserVerification.Any &&
                HmacSecret == false &&
                MinPinLength == false &&
                CredentialBlob == null &&
                LargeBlobSupport == LargeBlobSupport.None &&
                EnablePseudoRandomFunction == false &&
                ThirdPartyPayment == false &&
                remoteDesktopClientOverride == null)
            {
                // No extensions are set
                return null;
            }

            return new AuthenticationExtensionsClientAttestationInputs()
            {
                CredProtect = this.CredProtectPolicy,
                EnforceCredProtect = this.EnforceCredProtect,
                HmacCreateSecret = this.HmacSecret,
                MinimumPinLength = this.MinPinLength,
                CredentialBlob = this.CredentialBlob?.Length > 0 ? this.CredentialBlob : null,
                LargeBlob = this.LargeBlobSupport != LargeBlobSupport.None ? new LargeBlobAttestationInputs(this.LargeBlobSupport) : null,
                Prf = this.EnablePseudoRandomFunction ? new PRFAttestationInputs() : null,
                Payment = this.ThirdPartyPayment ? new PaymentAttestationInputs { IsPayment = true } : null,
                RemoteDesktopClientOverride = remoteDesktopClientOverride
            };
        }
        set
        {
            if (value != null)
            {
                HmacSecret = value.HmacCreateSecret == true;
                CredProtectPolicy = value.CredProtect;
                EnforceCredProtect = value.EnforceCredProtect == true;
                MinPinLength = value.MinimumPinLength == true;
                CredentialBlob = value.CredentialBlob;
                LargeBlobSupport = value.LargeBlob?.Support ?? LargeBlobSupport.None;
                EnablePseudoRandomFunction = value.Prf != null;
                ThirdPartyPayment = value.Payment?.IsPayment == true;
                RemoteWebOrigin = value.RemoteDesktopClientOverride?.Origin;
            }
            else
            {
                // Load default values
                CredProtectPolicy = UserVerification.Any;
                HmacSecret = false;
                MinPinLength = false;
                CredentialBlob = null;
                LargeBlobSupport = LargeBlobSupport.None;
                EnablePseudoRandomFunction = false;
                ThirdPartyPayment = false;
                RemoteWebOrigin = null;
            }
        }
    }

    private UserVerification _credProtect;
    public UserVerification CredProtectPolicy
    {
        get => _credProtect;
        set
        {
            SetProperty(ref _credProtect, value);

            if (EnforceCredProtect && value == UserVerification.Any)
            {
                // Uncheck Enforce CredProtect
                this.EnforceCredProtect = false;
            }

            RaisePropertyChanged(nameof(EnforceCredProtectEnabled));
        }
    }

    public bool EnforceCredProtect
    {
        get;
        set => SetProperty(ref field, value);
    }

    // Do not allow enforcement of credProtect if it is not enabled.
    public bool EnforceCredProtectEnabled => CredProtectPolicy != UserVerification.Any;

    public bool MinPinLength
    {
        get;
        set => SetProperty(ref field, value);
    }

    public bool HmacSecret
    {
        get;
        set => SetProperty(ref field, value);
    }

    public bool EnablePseudoRandomFunction
    {
        get;
        set => SetProperty(ref field, value);
    }

    public LargeBlobSupport LargeBlobSupport
    {
        get;
        set => SetProperty(ref field, value);
    }

    public byte[] CredentialBlob
    {
        get;
        set => SetProperty(ref field, value);
    }

    public bool IsBrowserPrivateMode
    {
        get;
        set => SetProperty(ref field, value);
    }

    public PublicKeyCredentialHint CredentialHint
    {
        get;
        set => SetProperty(ref field, value);
    }

    public IList<KeyValuePair<PublicKeyCredentialHint?, string>> CredentialHints
    => EnumAdapter.GetComboBoxItems<PublicKeyCredentialHint>();

    public bool ThirdPartyPayment
    {
        get;
        set => SetProperty(ref field, value);
    }

    public string? RemoteWebOrigin
    {
        get;
        set => SetProperty(ref field, value);
    }

    public bool IsFormValid =>
        !string.IsNullOrWhiteSpace(RpId) &&
        Challenge is { Length: > 0 } &&
        !string.IsNullOrWhiteSpace(UserName);

    private static byte[] GetRandomBytes(int count)
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] randomBytes = new byte[count];
        rng.GetBytes(randomBytes);
        return randomBytes;
    }
}
