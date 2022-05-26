using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace Doublel.Localization
{
    public class SqlTranslator : Translator
    {
        private SqlConnection _connection;
        private Dictionary<string, string> _cachedLocalizationData;

        public SqlTranslator(SqlConnection connection, CultureInfo culture) : base(culture)
        {
            _connection = connection;
            LoadLocalizationData();
        }

        private void LoadLocalizationData()
        {
            _cachedLocalizationData = new Dictionary<string,string>();

            var query = $"SELECT [Key], [Value] FROM DoublelTranslations WHERE Locale = '{LanguageTag}'";

            var command = new SqlCommand(query, _connection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                _cachedLocalizationData.Add(reader.GetString(0), reader.GetString(1));
            }
        }

        protected override string GetTranslationValue(string key) => _cachedLocalizationData.ContainsKey(key) ? _cachedLocalizationData[key] : key;
    }
}
