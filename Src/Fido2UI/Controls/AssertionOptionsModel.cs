using System;
using System.Collections.ObjectModel;
using Fido2NetLib;
using Fido2NetLib.Objects;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public class AssertionOptionsModel : BindableBase
    {
        public AssertionOptions Options
        {
            get
            {
                return new AssertionOptions()
                {
                    Challenge = _challenge,
                    RpId = _rpId,
                    Timeout = _timeout,
                    UserVerification = _userVerification
                };
            }
            set
            {
                _rpId = value.RpId;
                _challenge = value.Challenge;
                _userVerification = value.UserVerification;
                _timeout = value.Timeout;
                // TODO: Implement AllowCredentials in UI

                RaisePropertyChanged(nameof(Options));
                RaisePropertyChanged(nameof(RpId));
                RaisePropertyChanged(nameof(Challenge));
                RaisePropertyChanged(nameof(UserVerification));
                RaisePropertyChanged(nameof(Timeout));
            }
        }

        private byte[] _challenge;
        public byte[] Challenge
        {
            get { return _challenge; }
            set
            {
                SetProperty(ref _challenge, value);
                RaisePropertyChanged(nameof(Options));
            }
        }

        private UserVerificationRequirement? _userVerification;

        public string UserVerification
        {
            get
            {
                return null; // _userVerification?.ToString() ?? NoValue;
            }
            set
            {
                //UserVerificationRequirement? convertedValue = value != NoValue ? (UserVerificationRequirement?)Enum.Parse(typeof(UserVerificationRequirement), value) : null;
                //SetProperty(ref _userVerification, convertedValue);
                //RaisePropertyChanged(nameof(Options));
            }
        }

        private uint _timeout;
        public uint Timeout
        {
            get { return _timeout; }
            set
            {
                SetProperty(ref _timeout, value);
                RaisePropertyChanged(nameof(Options));
            }
        }

        private string _rpId;
        public string RpId
        {
            get { return _rpId; }
            set
            {
                SetProperty(ref _rpId, value);
                RaisePropertyChanged(nameof(Options));
            }
        }
    }
}
