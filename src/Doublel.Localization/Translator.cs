using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Doublel.Localization
{
    public abstract class Translator : ITranslator
    {
        public CultureInfo Culture { get; set; }

        public Translator()
        {

        }

        public Translator(CultureInfo culture)
        {
            Culture = culture;
        }

        public virtual string Translate(string key)
        {
            if(string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("Translation key must not be null or empty.");
            }
            
            if(!IsKeyValid(key))
            {
                throw new InvalidOperationException("Invalid translation key.");
            }

            return GetTranslationValue(key);
        }

        protected virtual bool IsKeyValid(string key)
        {
            return Regex.IsMatch(key, "^[a-zA-Z]*$");
        }

        protected string LanguageTag => Culture.IetfLanguageTag;

        public string TranslateText(string text)
        {
            return Regex.Replace(text, @"\~[^~]*\~", EvaluateMatch); ;
        }

        private string EvaluateMatch(Match match)
        {
            var key = match.Value.Substring(1, match.Value.Length - 2);

            return Translate(key);
        }

        protected abstract string GetTranslationValue(string key);
    }
}
