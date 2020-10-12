using System;
using System.Globalization;
using System.Linq;
using System.Text;
using CppAst;

namespace CodeGen
{
    class Program
    {
        private const string MacroPrefix = "WEBAUTHN_";
        private const char WordSeparator = '_';
        private const string Header = @"
/* This file has been automatically generated. Do not modify it! */

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Contains constants from <webauthn.h>.
    /// </summary>
    internal static class ApiConstants
    {
";
        private const string Footer = @"
    }
}
";
        private const string Padding = "        ";

        static void Main(string[] args)
        {
            var parserOptions = new CppParserOptions()
            {
                ParseMacros = true,
                ParseSystemIncludes = false
            };

            var compilation = CppParser.ParseFile(@"C:\Users\MichaelGrafnetter\source\repos\MichaelGrafnetter\WebAuthN.Interop\Src\WebAuthN\webauthn.h", parserOptions);

            Console.WriteLine(Header);

            foreach (var macro in compilation.Macros)
            {
                if (!macro.Name.StartsWith(MacroPrefix))
                    continue;

                switch(macro.Tokens[0].Kind)
                {
                    case CppTokenKind.Identifier:
                        string normalizedIdentifier = NormalizeName(macro.Value);
                        ProcessConstant(macro.Name, "int", normalizedIdentifier);
                        break;
                    case CppTokenKind.Literal:
                        bool isNumber = char.IsDigit(macro.Value[0]);
                        string type = isNumber ? "int" : "string";
                        string trimmedValue = isNumber ? macro.Value : macro.Value.TrimStart('L');
                        ProcessConstant(macro.Name, type, trimmedValue);
                        break;
                }
            }

            Console.WriteLine(Footer);
        }

        static void ProcessConstant(string name, string type, string value)
        {
            string normalizedName = NormalizeName(name);

            Console.WriteLine("{1}/// <remarks>Corresponds to {0}.</remarks>", name, Padding);
            Console.WriteLine("{3}public const {1} {0} = {2};", normalizedName, type, value, Padding);
            Console.WriteLine();
        }

        static string NormalizeName(string name)
        {
            var textInfo = CultureInfo.InvariantCulture.TextInfo;
            return name.Split(WordSeparator)
                .Skip(1)
                .Select(word => textInfo.ToTitleCase(word.ToLowerInvariant()))
                .Aggregate(new StringBuilder(), (sb, word) => sb.Append(word))
                .ToString();
        }
    }
}
