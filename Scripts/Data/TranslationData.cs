using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// Translation data class
    /// </summary>
    [Serializable]
    public class TranslationData : IComparable<TranslationData>
    {
        /// <summary>
        /// Texts
        /// </summary>
        [SerializeField]
        private TranslatedTextData[] texts = new TranslatedTextData[] { TranslatedTextData.defaultTranslatedText };

        /// <summary>
        /// Lookup
        /// </summary>
        private Dictionary<SystemLanguage, string> lookup;

        /// <summary>
        /// Texts
        /// </summary>
        public IReadOnlyList<TranslatedTextData> Texts
        {
            get
            {
                if (texts == null)
                {
                    texts = new TranslatedTextData[] { TranslatedTextData.defaultTranslatedText };
                }
                return texts;
            }
        }

        /// <summary>
        /// Translated text
        /// </summary>
        public string Text
        {
            get
            {
                string ret = string.Empty;
                if (lookup == null)
                {
                    lookup = new Dictionary<SystemLanguage, string>();
                    foreach (TranslatedTextData text in Texts)
                    {
                        if (lookup.ContainsKey(text.Language))
                        {
                            lookup[text.Language] = text.Text;
                        }
                        else
                        {
                            lookup.Add(text.Language, text.Text);
                        }
                    }
                }
                if (lookup.ContainsKey(Translator.SystemLanguage))
                {
                    ret = lookup[Translator.SystemLanguage];
                }
                else if (Texts.Count > 0)
                {
                    ret = Texts[0].Text;
                }
                return ret;
            }
        }

        /// <summary>
        /// Add translated text
        /// </summary>
        /// <param name="text">Translated text</param>
        public void AddText(TranslatedTextData text)
        {
            bool append = true;
            for (int i = 0; i < Texts.Count; i++)
            {
                TranslatedTextData translated_text = Texts[i];
                if (translated_text.Language == text.Language)
                {
                    texts[i] = text;
                    append = false;
                    break;
                }
            }
            if (append)
            {
                TranslatedTextData[] texts = new TranslatedTextData[Texts.Count + 1];
                Array.Copy(this.texts, 0, texts, 0, this.texts.Length);
                texts[this.texts.Length] = text;
                this.texts = texts;
            }
        }

        /// <summary>
        /// Compare to
        /// </summary>
        /// <param name="other">Other</param>
        /// <returns>Result</returns>
        public int CompareTo(TranslationData other)
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
        public override string ToString() => Text;
    }
}
