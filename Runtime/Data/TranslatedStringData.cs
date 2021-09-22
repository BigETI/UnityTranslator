using System;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// A class that describes translated string data
    /// </summary>
    [Serializable]
    public struct TranslatedStringData : ITranslatedStringData
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
        /// Translated value
        /// </summary>
        public string Value => stringValue ?? string.Empty;

        /// <summary>
        /// Language
        /// </summary>
        public SystemLanguage Language => language;

        /// <summary>
        /// Constructs new translated string data
        /// </summary>
        /// <param name="stringValue">Translated string</param>
        /// <param name="language">Language</param>
        public TranslatedStringData(string stringValue, SystemLanguage language)
        {
            this.stringValue = stringValue ?? throw new ArgumentNullException(nameof(stringValue));
            this.language = language;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString() => Value;
    }
}
