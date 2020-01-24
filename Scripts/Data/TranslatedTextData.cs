using System;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// Translated text data class
    /// </summary>
    [Serializable]
    public struct TranslatedTextData
    {
        /// <summary>
        /// Default translated text
        /// </summary>
        public static readonly TranslatedTextData defaultTranslatedText = new TranslatedTextData(string.Empty, SystemLanguage.English);

        /// <summary>
        /// Text
        /// </summary>
        [TextArea]
        [SerializeField]
        private string text;

        /// <summary>
        /// Language
        /// </summary>
        [SerializeField]
        private SystemLanguage language;

        /// <summary>
        /// Text
        /// </summary>
        public string Text
        {
            get
            {
                if (text == null)
                {
                    text = string.Empty;
                }
                return text;
            }
        }

        /// <summary>
        /// Language
        /// </summary>
        public SystemLanguage Language => language;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="language">Language</param>
        public TranslatedTextData(string text, SystemLanguage language)
        {
            this.text = text;
            this.language = language;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString()
        {
            return Text;
        }
    }
}
