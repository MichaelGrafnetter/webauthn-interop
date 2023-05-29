using System.Collections.Generic;
using DSInternals.Win32.WebAuthn.COSE;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public class AlgorithmSelectorViewModel : BindableBase, IAlgorithmSelectorViewModel
    {
        public AlgorithmSelectorViewModel()
        {
            // Configure default values
            SelectDefaultAlgorithms();
        }

        public List<Algorithm> SelectedAlgorithms
        {
            get
            {
                // Convert checkboxes to PubKeyCredParam

                var result = new List<Algorithm>();

                if (AlgorithmES256Enabled)
                    result.Add(Algorithm.ES256);

                if (AlgorithmES384Enabled)
                    result.Add(Algorithm.ES384);

                if (AlgorithmES512Enabled)
                    result.Add(Algorithm.ES512);

                if (AlgorithmRS256Enabled)
                    result.Add(Algorithm.RS256);

                if (AlgorithmRS384Enabled)
                    result.Add(Algorithm.RS384);

                if (AlgorithmRS512Enabled)
                    result.Add(Algorithm.RS512);

                if (AlgorithmPS256Enabled)
                    result.Add(Algorithm.PS256);

                if (AlgorithmPS384Enabled)
                    result.Add(Algorithm.PS384);

                if (AlgorithmPS512Enabled)
                    result.Add(Algorithm.PS512);

                return result;
            }
            set
            {
                // Convert PubKeyCredParam to checkboxes
                if (value == null)
                {
                    SelectDefaultAlgorithms();
                    return;
                }

                ClearSelectedAlgorithms();

                foreach (var algorithm in value)
                {
                    switch (algorithm)
                    {
                        case Algorithm.ES256:
                            AlgorithmES256Enabled = true;
                            break;
                        case Algorithm.ES384:
                            AlgorithmES384Enabled = true;
                            break;
                        case Algorithm.ES512:
                            AlgorithmES512Enabled = true;
                            break;
                        case Algorithm.RS256:
                            AlgorithmRS256Enabled = true;
                            break;
                        case Algorithm.RS384:
                            AlgorithmRS384Enabled = true;
                            break;
                        case Algorithm.RS512:
                            AlgorithmRS512Enabled = true;
                            break;
                        case Algorithm.PS256:
                            AlgorithmPS256Enabled = true;
                            break;
                        case Algorithm.PS384:
                            AlgorithmPS384Enabled = true;
                            break;
                        case Algorithm.PS512:
                            AlgorithmPS512Enabled = true;
                            break;
                    }
                }
            }
        }

        private bool _algorithmRS512Enabled;
        public bool AlgorithmRS512Enabled
        {
            get => _algorithmRS512Enabled;
            set
            {
                bool changed = SetProperty(ref _algorithmRS512Enabled, value);

                if(changed)
                {
                    RaisePropertyChanged(nameof(SelectedAlgorithms));
                }
            }
        }

        private bool _algorithmRS384Enabled;
        public bool AlgorithmRS384Enabled
        {
            get => _algorithmRS384Enabled;
            set
            {
                bool changed = SetProperty(ref _algorithmRS384Enabled, value);

                if (changed)
                {
                    RaisePropertyChanged(nameof(SelectedAlgorithms));
                }
            }
        }

        private bool _algorithmRS256Enabled;
        public bool AlgorithmRS256Enabled
        {
            get => _algorithmRS256Enabled;
            set {
                bool changed = SetProperty(ref _algorithmRS256Enabled, value);

                if (changed)
                {
                    RaisePropertyChanged(nameof(SelectedAlgorithms));
                }
            }
        }

        private bool _algorithmPS512Enabled;
        public bool AlgorithmPS512Enabled
        {
            get => _algorithmPS512Enabled;
            set {
                bool changed = SetProperty(ref _algorithmPS512Enabled, value);

                if (changed)
                {
                    RaisePropertyChanged(nameof(SelectedAlgorithms));
                }
            }
        }

        private bool _algorithmPS384Enabled;
        public bool AlgorithmPS384Enabled
        {
            get => _algorithmPS384Enabled;
            set {
                bool changed = SetProperty(ref _algorithmPS384Enabled, value);

                if (changed)
                {
                    RaisePropertyChanged(nameof(SelectedAlgorithms));
                }
            }
        }

        private bool _algorithmPS256Enabled;
        public bool AlgorithmPS256Enabled
        {
            get => _algorithmPS256Enabled;
            set {
                bool changed = SetProperty(ref _algorithmPS256Enabled, value);

                if (changed)
                {
                    RaisePropertyChanged(nameof(SelectedAlgorithms));
                }
            }
        }

        private bool _algorithmES512Enabled;
        public bool AlgorithmES512Enabled
        {
            get => _algorithmES512Enabled;
            set {
                bool changed = SetProperty(ref _algorithmES512Enabled, value);

                if (changed)
                {
                    RaisePropertyChanged(nameof(SelectedAlgorithms));
                }
            }
        }

        private bool _algorithmES384Enabled;
        public bool AlgorithmES384Enabled
        {
            get => _algorithmES384Enabled;
            set {
                bool changed = SetProperty(ref _algorithmES384Enabled, value);

                if (changed)
                {
                    RaisePropertyChanged(nameof(SelectedAlgorithms));
                }
            }
        }

        private bool _algorithmES256Enabled;
        public bool AlgorithmES256Enabled
        {
            get => _algorithmES256Enabled;
            set {
                bool changed = SetProperty(ref _algorithmES256Enabled, value);

                if (changed)
                {
                    RaisePropertyChanged(nameof(SelectedAlgorithms));
                }
            }
        }

        private void ClearSelectedAlgorithms()
        {
            AlgorithmES256Enabled = false;
            AlgorithmES384Enabled = false;
            AlgorithmES512Enabled = false;
            AlgorithmPS256Enabled = false;
            AlgorithmPS384Enabled = false;
            AlgorithmPS512Enabled = false;
            AlgorithmRS256Enabled = false;
            AlgorithmRS384Enabled = false;
            AlgorithmRS512Enabled = false;
        }

        private void SelectDefaultAlgorithms()
        {
            ClearSelectedAlgorithms();
            AlgorithmRS256Enabled = true;
            AlgorithmES256Enabled = true;
        }
    }
}
