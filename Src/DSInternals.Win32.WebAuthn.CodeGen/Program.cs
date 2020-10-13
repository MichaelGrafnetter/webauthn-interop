using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CppAst;

namespace DSInternals.Win32.WebAuthn.CodeGen
{
    class Program
    {
        private const string InputFile = @"..\..\..\CodeGen.h";
        private const string OutputFile = @"..\..\..\..\DSInternals.Win32.WebAuthn\ApiConstants.cs";
        private const string MacroPrefix = "WEBAUTHN_";
        private const char WordSeparator = '_';
        private const string Header = @"
/* This file has been automatically generated. Do not modify it! */

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Contains constants from <webauthn.h>.
    /// </summary>
    internal static partial class ApiConstants
    {";
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

            var compilation = CppParser.ParseFile(InputFile, parserOptions);

            using (var writer = new StreamWriter(OutputFile))
            {
                writer.WriteLine(Header);

                // Process all macros and generate constants from #defines.
                foreach (var macro in compilation.Macros)
                {
                    if (!macro.Name.StartsWith(MacroPrefix))
                        continue;

                    switch (macro.Tokens[0].Kind)
                    {
                        case CppTokenKind.Identifier:
                            string normalizedIdentifier = NormalizeName(macro.Value);
                            ProcessConstant(writer, macro.Name, "int", normalizedIdentifier);
                            break;
                        case CppTokenKind.Literal:
                        case CppTokenKind.Punctuation:
                            bool isNumber = char.IsDigit(macro.Value[0]) || char.IsPunctuation(macro.Value[0]);
                            string type = isNumber ? "int" : "string";
                            string trimmedValue = isNumber ? macro.Value : macro.Value.TrimStart('L');
                            ProcessConstant(writer, macro.Name, type, trimmedValue);
                            break;
                    }
                }

                writer.WriteLine(Footer);
            }
        }

        static void ProcessConstant(StreamWriter writer, string name, string type, string value)
        {
            string normalizedName = NormalizeName(name);

            writer.WriteLine(@"{1}/// <remarks>
{1}/// Corresponds to {0}.
{1}/// </remarks>", name, Padding);
            writer.WriteLine("{3}public const {1} {0} = {2};\r\n", normalizedName, type, value, Padding);
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
