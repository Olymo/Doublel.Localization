using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace Doublel.Localization.Test
{
    public class LocalizationTests
    {
        [Fact]
        public void LocalizationWorksForCodeBasedTranslator()
        {
            var culture = new CultureInfo("en-US");
            var translator = new CodeBasedTranslator(culture);

            var translated = translator.Translate("t");

            translated.Should().Be("test");

            var translatedText = translator.TranslateText("Text ~ex~ for ~t~ purposes.");
            translatedText.Should().Be("Text example for test purposes.");
        }

        [Fact]
        public void LocalizationWorksForJsonTranslator()
        {
            var culture = new CultureInfo("en-US");
            var translator = new JsonTranslator(culture);

            var translated = translator.Translate("t");

            translated.Should().Be("test");

            var translatedText = translator.TranslateText("Text ~ex~ for ~t~ purposes.");
            translatedText.Should().Be("Text example for test purposes."); 
        }

        [Fact]
        public void LocalizationWorksForSqlTranslator()
        {
            var culture = new CultureInfo("en-US");

            var connString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Boilerplate;Integrated Security=True";

            var conn = new SqlConnection(connString);

            conn.Open();

            var translator = new SqlTranslator(conn,culture);

            conn.Close();

            var translated = translator.Translate("t");

            translated.Should().Be("test");

            var translatedText = translator.TranslateText("Text ~ex~ for ~t~ purposes.");
            translatedText.Should().Be("Text example for test purposes.");
        }
    }
}
