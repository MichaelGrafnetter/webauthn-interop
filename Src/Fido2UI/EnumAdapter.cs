using System;
using System.Collections.Generic;
using System.Linq;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public static class EnumAdapter
    {
        public static IList<KeyValuePair<T?, string>> GetComboBoxItems<T>(string nullDisplayName = null) where T : struct, Enum 
        {
            var result = new List<KeyValuePair<T?, string>>();

            if(nullDisplayName != null)
            {
                result.Add(new KeyValuePair<T?, string>(null, nullDisplayName));
            }

            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();

            foreach(var value in values)
            {
                result.Add(new KeyValuePair<T?, string>(value, Enum.GetName(typeof(T), value)));
            }

            return result;
        }
    }
}
