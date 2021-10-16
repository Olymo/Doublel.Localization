using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Doublel.Localization
{
    public class JsonTranslator : Translator
    {
        public JsonTranslator()
        {

        }

        public JsonTranslator(CultureInfo culture): base(culture)
        {

        }

        protected override bool IsKeyValid(string key)
        {
            return Regex.IsMatch(key, "^[a-zA-Z]*(.[a-zA-Z]{1,})*$");
        }

        protected override string GetTranslationValue(string key)
        {
            var translationObject = GetTranslationObject();

            if (!IsKeyNested(key))
            {
                return translationObject[key];
            }

            var nestedKeys = key.Split(new string[] {"."}, StringSplitOptions.None);

            return GetNestedTranslation(nestedKeys, translationObject);
        }

        private static string GetNestedTranslation(string[] nestedKeys, dynamic translationObject)
        {
            object translationMessage = null;

            foreach (var key in nestedKeys)
            {
                translationMessage = translationObject != null ? translationObject[key] : new { };
                translationObject = translationObject != null ? translationObject[key] : new { };
            }

            return translationMessage == null ? null : translationMessage.ToString();
        }

        private static bool IsKeyNested(string key) => key.Contains(".");

        private dynamic GetTranslationObject()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Localization", LanguageTag + ".json");
            var file = new FileInfo(filePath);

            if (!file.Exists)
            {
                return null;
            }
            
            using(var reader = new StreamReader(file.FullName, Encoding.UTF8))
            {
                var content = reader.ReadToEnd();

                return JObject.Parse(content);
            }
        }
    }
}
