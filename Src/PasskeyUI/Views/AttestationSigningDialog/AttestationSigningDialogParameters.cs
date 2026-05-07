using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Interop;
using Prism.Dialogs;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class AttestationSigningDialogParameters
{
    private const string RelyingPartyKey = "RelyingParty";
    private const string UserKey = "User";
    private const string ChallengeKey = "Challenge";
    private const string UserVerificationRequirementKey = "UserVerificationRequirement";
    private const string DefaultAlgorithmKey = "DefaultAlgorithm";
    private const string LastCredentialIdKey = "LastCredentialId";

    public required RelyingPartyInformation RelyingParty { get; init; }
    public required UserInformation User { get; init; }
    public required byte[] Challenge { get; init; }
    public UserVerificationRequirement UserVerificationRequirement { get; init; }
    public Algorithm DefaultAlgorithm { get; init; } = Algorithm.ES256;
    public byte[]? LastCredentialId { get; init; }

    public static AttestationSigningDialogParameters From(IDialogParameters parameters) => new()
    {
        RelyingParty = parameters.GetValue<RelyingPartyInformation>(RelyingPartyKey),
        User = parameters.GetValue<UserInformation>(UserKey),
        Challenge = parameters.GetValue<byte[]>(ChallengeKey),
        UserVerificationRequirement = parameters.GetValue<UserVerificationRequirement>(UserVerificationRequirementKey),
        DefaultAlgorithm = parameters.GetValue<Algorithm>(DefaultAlgorithmKey),
        LastCredentialId = parameters.ContainsKey(LastCredentialIdKey)
            ? parameters.GetValue<byte[]>(LastCredentialIdKey)
            : null
    };

    public IDialogParameters ToDialogParameters()
    {
        var p = new DialogParameters
        {
            { RelyingPartyKey, RelyingParty },
            { UserKey, User },
            { ChallengeKey, Challenge },
            { UserVerificationRequirementKey, UserVerificationRequirement },
            { DefaultAlgorithmKey, DefaultAlgorithm }
        };
        if (LastCredentialId != null)
            p.Add(LastCredentialIdKey, LastCredentialId);
        return p;
    }
}
