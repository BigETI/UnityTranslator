using UnityEngine;
using UnityTranslator.Data;

/// <summary>
/// Unity translator objects namespace
/// </summary>
namespace UnityTranslator.Objects
{
    /// <summary>
    /// String translation object script class
    /// </summary>
    [CreateAssetMenu(fileName = "StringTranslation", menuName = "Translator/String translation")]
    public class StringTranslationObjectScript : ScriptableObject, IStringTranslationObject
    {
        /// <summary>
        /// String translation
        /// </summary>
        [SerializeField]
        private StringTranslationData stringTranslation;

        /// <summary>
        /// Comment
        /// </summary>
        [TextArea]
        [SerializeField]
        private string comment;

        /// <summary>
        /// String translation
        /// </summary>
        public StringTranslationData StringTranslation
        {
            get
            {
                if (stringTranslation == null)
                {
                    stringTranslation = new StringTranslationData();
                }
                return stringTranslation;
            }
        }

        /// <summary>
        /// Translated text
        /// </summary>
        public string String => StringTranslation.String;

        /// <summary>
        /// Comment
        /// </summary>
        public string Comment
        {
            get
            {
                if (comment == null)
                {
                    comment = string.Empty;
                }
                return comment;
            }
        }

        /// <summary>
        /// Contains language
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>"true" if contains language, otherwise "false"</returns>
        public bool ContainsLanguage(SystemLanguage language)
        {
            bool ret = false;
            foreach (TranslatedStringData translated_string in StringTranslation.Strings)
            {
                if (translated_string.Language == language)
                {
                    if (translated_string.String.Trim().Length > 0)
                    {
                        ret = true;
                        break;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Compare to
        /// </summary>
        /// <param name="other">Other</param>
        /// <returns>Result</returns>
        public int CompareTo(IStringTranslationObject other)
        {
            int ret = 1;
            if (other != null)
            {
                ret = StringTranslation.CompareTo(other.StringTranslation);
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
