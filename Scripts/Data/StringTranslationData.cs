using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// String translation data class
    /// </summary>
    [Serializable]
    public class StringTranslationData : IStringTranslationData
    {
        /// <summary>
        /// Translated strings
        /// </summary>
        [SerializeField]
        private TranslatedStringData[] strings = new TranslatedStringData[] { TranslatedStringData.defaultTranslatedString };

        /// <summary>
        /// Lookup
        /// </summary>
        private Dictionary<SystemLanguage, string> lookup;

        /// <summary>
        /// Translated strings
        /// </summary>
        public IReadOnlyList<TranslatedStringData> Strings
        {
            get
            {
                if (strings == null)
                {
                    strings = new TranslatedStringData[] { TranslatedStringData.defaultTranslatedString };
                }
                return strings;
            }
        }

        /// <summary>
        /// Translated string
        /// </summary>
        public string String
        {
            get
            {
                string ret = string.Empty;
                if (lookup == null)
                {
                    lookup = new Dictionary<SystemLanguage, string>();
                    foreach (TranslatedStringData str in Strings)
                    {
                        if (lookup.ContainsKey(str.Language))
                        {
                            lookup[str.Language] = str.String;
                        }
                        else
                        {
                            lookup.Add(str.Language, str.String);
                        }
                    }
                }
                if (lookup.ContainsKey(Translator.SystemLanguage))
                {
                    ret = lookup[Translator.SystemLanguage];
                }
                else if (Strings.Count > 0)
                {
                    ret = Strings[0].String;
                }
                return ret;
            }
        }

        /// <summary>
        /// Add translated string
        /// </summary>
        /// <param name="stringValue">Translated string</param>
        public void AddString(TranslatedStringData stringValue)
        {
            bool append = true;
            for (int i = 0; i < Strings.Count; i++)
            {
                ref TranslatedStringData translated_string = ref strings[i];
                if (translated_string.Language == stringValue.Language)
                {
                    translated_string = stringValue;
                    append = false;
                    break;
                }
            }
            if (append)
            {
                TranslatedStringData[] strings = new TranslatedStringData[Strings.Count + 1];
                Array.Copy(this.strings, 0, strings, 0, this.strings.Length);
                strings[this.strings.Length] = stringValue;
                this.strings = strings;
            }
        }

        /// <summary>
        /// Compare to
        /// </summary>
        /// <param name="other">Other</param>
        /// <returns>Result</returns>
        public int CompareTo(IStringTranslationData other)
        {
            int ret = 1;
            if (other != null)
            {
                ret = ToString().CompareTo(other.ToString());
            }
            return ret;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString() => String;
    }
}
