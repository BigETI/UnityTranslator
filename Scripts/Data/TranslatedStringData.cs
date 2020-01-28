using System;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// Translated string data class
    /// </summary>
    [Serializable]
    public struct TranslatedStringData
    {
        /// <summary>
        /// Default translated string
        /// </summary>
        public static readonly TranslatedStringData defaultTranslatedString = new TranslatedStringData(string.Empty, SystemLanguage.English);

        /// <summary>
        /// Translated string
        /// </summary>
        [TextArea]
        [SerializeField]
        private string stringValue;

        /// <summary>
        /// Language
        /// </summary>
        [SerializeField]
        private SystemLanguage language;

        /// <summary>
        /// Translated string
        /// </summary>
        public string String
        {
            get
            {
                if (stringValue == null)
                {
                    stringValue = string.Empty;
                }
                return stringValue;
            }
        }

        /// <summary>
        /// Language
        /// </summary>
        public SystemLanguage Language => language;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stringValue">Translated string</param>
        /// <param name="language">Language</param>
        public TranslatedStringData(string stringValue, SystemLanguage language)
        {
            this.stringValue = stringValue;
            this.language = language;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString() => String;
    }
}
