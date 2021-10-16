using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Doublel.Localization
{
    public class CodeBasedTranslator : Translator
    {
        public CodeBasedTranslator()
        {

        }

        public CodeBasedTranslator(CultureInfo culture) : base(culture)
        {

        }

        protected virtual Dictionary<string, Dictionary<string, string>> Translations { get; } = new Dictionary<string, Dictionary<string, string>>
        {
            { "en-US", new Dictionary<string, string> 
                { 
                    { "ex", "example" },
                    { "t", "test" }
                }
            
            }
        };
        protected override string GetTranslationValue(string key)
        {
            var translation = Translations[LanguageTag][key];

            return translation == null ? $"#{key}#" : translation;
        }
    }
}
