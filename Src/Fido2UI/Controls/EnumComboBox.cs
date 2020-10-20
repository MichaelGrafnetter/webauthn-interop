using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public class EnumComboBox : System.Windows.Controls.ComboBox
    {
        private const int NullEnumValue = int.MaxValue;

        static EnumComboBox()
        {
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            PopulateItems();
        }

        [Browsable(true), Category("Data"), Description("Sets the fully qualified type of the enum to display in the ComboBox.")]
        public string EnumTypeName { get; set; }

        [Browsable(true), Category("Data"), Description("Sets the value to be displayed for empty selection.")]
        public string NullDisplayName { get; set; }

        private void PopulateItems()
        {
            if(EnumTypeName == null)
            {
                // Do not continue
                return;
            }

            Type enumType = Type.GetType(EnumTypeName);
            if (enumType == null)
            {
                throw new ArgumentException("Cannot load the type: " + EnumTypeName);
            }

            List<KeyValuePair<int, string>> values = new List<KeyValuePair<int, string>>();

            if (NullDisplayName != null)
            {
                values.Add(new KeyValuePair<int, string>(NullEnumValue, NullDisplayName));
            }

            var enumValues = Enum.GetValues(enumType);
            for (int i = 0; i < enumValues.Length; i++)
            {
                var currentValue = enumValues.GetValue(i);
                int numericValue = (int)currentValue;
                string description = currentValue.ToString();
                values.Add(new KeyValuePair<int, string>(numericValue, description));
            }

            ItemsSource = values;
            
            DisplayMemberPath = nameof(KeyValuePair<int, string>.Value);
            SelectedValuePath = nameof(KeyValuePair<int, string>.Key);

            if (SelectedIndex == -1)
            {
                // Select the first value if none is selected
                SelectedIndex = 0;
            }
        }
    }
}
