using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// A class that describes string translation data
    /// </summary>
    [Serializable]
    public class StringTranslationData : IStringTranslationData<StringTranslationData, TranslatedStringData>
    {
        /// <summary>
        /// Translated strings
        /// </summary>
        [SerializeField]
        private TranslatedStringData[] strings = new TranslatedStringData[] { TranslatedStringData.defaultTranslatedString };

#if !UNITY_EDITOR
        /// <summary>
        /// System language to string lookup
        /// </summary>
        private Dictionary<SystemLanguage, string> systemLanguageToStringLookup;
#endif

        /// <summary>
        /// Translated values
        /// </summary>
        public IReadOnlyList<TranslatedStringData> Values => strings ??= new TranslatedStringData[] { TranslatedStringData.defaultTranslatedString };

        /// <summary>
        /// Translated value
        /// </summary>
        public string Value
        {
            get
            {
#if UNITY_EDITOR
                string ret = null;
                foreach (TranslatedStringData translated_string in Values)
                {
                    if (translated_string.Language == Translator.CurrentLanguage)
                    {
                        ret = translated_string.Value;
                        break;
                    }
                }
                return ret ?? FallbackValue;
#else
                UpdateSystemLanguageToStringLookup();
                return systemLanguageToStringLookup.TryGetValue(Translator.CurrentLanguage, out string ret) ? ret : FallbackValue;
#endif
            }
        }

        /// <summary>
        /// Fallback value
        /// </summary>
        public string FallbackValue => (Values.Count > 0) ? Values[0].Value : string.Empty;

        /// <summary>
        /// Fallback language
        /// </summary>
        public SystemLanguage FallbackLanguage => (Values.Count > 0) ? Values[0].Language : SystemLanguage.English;

#if !UNITY_EDITOR
        /// <summary>
        /// Updates system language to string lookup
        /// </summary>
        private void UpdateSystemLanguageToStringLookup()
        {
            if (systemLanguageToStringLookup == null)
            {
                systemLanguageToStringLookup = new Dictionary<SystemLanguage, string>();
                foreach (TranslatedStringData translated_string in Values)
                {
                    if (systemLanguageToStringLookup.ContainsKey(translated_string.Language))
                    {
                        systemLanguageToStringLookup[translated_string.Language] = translated_string.Value;
                    }
                    else
                    {
                        systemLanguageToStringLookup.Add(translated_string.Language, translated_string.Value);
                    }
                }
            }
        }
#endif

        /// <summary>
        /// Inserts translated string
        /// </summary>
        /// <param name="value">Translated value</param>
        /// <param name="language">Language</param>
        public void Insert(string value, SystemLanguage language)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            bool is_appending = true;
            for (int i = 0; i < Values.Count; i++)
            {
                ref TranslatedStringData translated_string = ref strings[i];
                if (translated_string.Language == language)
                {
                    translated_string = new TranslatedStringData(value, language);
                    is_appending = false;
                    break;
                }
            }
            if (is_appending)
            {
                TranslatedStringData[] strings = new TranslatedStringData[Values.Count + 1];
                Array.Copy(this.strings, 0, strings, 0, this.strings.Length);
                strings[this.strings.Length] = new TranslatedStringData(value, language);
                this.strings = strings;
            }
#if !UNITY_EDITOR
            systemLanguageToStringLookup?.Clear();
            systemLanguageToStringLookup = null;
#endif
        }

        /// <summary>
        /// Removes translated value with the specified language
        /// </summary>
        /// <param name="language">Language</param>
        public void Remove(SystemLanguage language)
        {
            if (strings != null)
            {
                int found_index = Array.FindIndex(strings, (element) => element.Language == language);
                if (found_index >= 0)
                {
                    for (int index = found_index + 1; index < strings.Length; index++)
                    {
                        strings[index - 1] = strings[index];
                    }
                    Array.Resize(ref strings, strings.Length - 1);
#if !UNITY_EDITOR
                    systemLanguageToStringLookup?.Remove(language);
#endif
                }
            }
        }

        /// <summary>
        /// Clears this translation data
        /// </summary>
        public void Clear()
        {
            if (strings != null)
            {
                strings = Array.Empty<TranslatedStringData>();
#if !UNITY_EDITOR
                systemLanguageToStringLookup?.Clear();
#endif
            }
        }

        /// <summary>
        /// Is language contained in this translation data
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>"true" if language is contained in this translation data, otherwise "false"</returns>
        public bool IsLanguageContained(SystemLanguage language)
        {
#if UNITY_EDITOR
            bool ret = false;
            foreach (TranslatedStringData translated_string in Values)
            {
                if (translated_string.Language == language)
                {
                    ret = !string.IsNullOrWhiteSpace(translated_string.Value);
                    break;
                }
            }
            return ret;
#else
            UpdateSystemLanguageToStringLookup();
            return systemLanguageToStringLookup.ContainsKey(language);
#endif
        }

        /// <summary>
        /// Tries to get translated value from the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <param name="result">Result</param>
        /// <returns>"true" if translated value is available, otherwise "false"</returns>
        public bool TryGetValue(SystemLanguage language, out string result)
        {
#if UNITY_EDITOR
            bool ret = false;
            result = string.Empty;
            foreach (TranslatedStringData translated_string in Values)
            {
                if (translated_string.Language == language)
                {
                    result = translated_string.Value;
                    ret = true;
                    break;
                }
            }
            return ret;
#else
            UpdateSystemLanguageToStringLookup();
            return systemLanguageToStringLookup.TryGetValue(language, out result);
#endif
        }

        /// <summary>
        /// Gets translated value from the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>Translated value</returns>
        public string GetValue(SystemLanguage language) => TryGetValue(language, out string ret) ? ret : string.Empty;

        /// <summary>
        /// Compares this string translation data to another string translation data
        /// </summary>
        /// <param name="other">Other string translation data</param>
        /// <returns>Comparison result</returns>
        public int CompareTo(StringTranslationData other) => (other == null) ? 1 : Value.CompareTo(other.Value);

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString() => Value;
    }
}
