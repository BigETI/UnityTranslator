﻿using System;
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
    public class TranslatedTextData
    {
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
        private SystemLanguage language = SystemLanguage.English;

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
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString()
        {
            return Text;
        }
    }
}
