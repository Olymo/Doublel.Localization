using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Doublel.Localization
{
    public interface ITranslator
    {
        CultureInfo Culture { get; set; }
        /// <summary>
        /// Translates single key. Throws invalid operation exception if non alpha characters are passed.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Translate(string key);
        /// <summary>
        /// Translates text. Each key requiring translation should be enveloped using # character. Throws invalid operation if non alpha characters are used for key.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string TranslateText(string text);
    }
}
