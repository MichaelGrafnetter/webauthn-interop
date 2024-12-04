namespace DSInternals.Win32.WebAuthn
{
    public abstract class WebauthnCredentialCreationOptions
    {
        public abstract PublicKeyCredentialCreationOptions PublicKeyOptions { get; set; }
    }
}
