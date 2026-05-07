using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Interop;
using Prism.Dialogs;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class AssertionSigningDialogParameters
{
    private const string RelyingPartyIdKey = "RelyingPartyId";
    private const string ChallengeKey = "Challenge";
    private const string UserVerificationRequirementKey = "UserVerificationRequirement";
    private const string DefaultAlgorithmKey = "DefaultAlgorithm";
    private const string CredentialIdKey = "CredentialId";

    public required string RelyingPartyId { get; init; }
    public required byte[] Challenge { get; init; }
    public UserVerificationRequirement UserVerificationRequirement { get; init; }
    public Algorithm DefaultAlgorithm { get; init; } = Algorithm.ES256;
    public byte[]? CredentialId { get; init; }

    public static AssertionSigningDialogParameters From(IDialogParameters parameters) => new()
    {
        RelyingPartyId = parameters.GetValue<string>(RelyingPartyIdKey),
        Challenge = parameters.GetValue<byte[]>(ChallengeKey),
        UserVerificationRequirement = parameters.GetValue<UserVerificationRequirement>(UserVerificationRequirementKey),
        DefaultAlgorithm = parameters.GetValue<Algorithm>(DefaultAlgorithmKey),
        CredentialId = parameters.ContainsKey(CredentialIdKey)
            ? parameters.GetValue<byte[]>(CredentialIdKey)
            : null
    };

    public IDialogParameters ToDialogParameters()
    {
        var p = new DialogParameters
        {
            { RelyingPartyIdKey, RelyingPartyId },
            { ChallengeKey, Challenge },
            { UserVerificationRequirementKey, UserVerificationRequirement },
            { DefaultAlgorithmKey, DefaultAlgorithm }
        };
        if (CredentialId != null)
            p.Add(CredentialIdKey, CredentialId);
        return p;
    }
}
