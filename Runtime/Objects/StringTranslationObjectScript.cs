using System;
using System.Collections.Generic;
using UnityEngine;
using UnityTranslator.Data;

/// <summary>
/// Unity translator objects namespace
/// </summary>
namespace UnityTranslator.Objects
{
    [CreateAssetMenu(fileName = "StringTranslation", menuName = "Translator/String translation")]
    public class StringTranslationObjectScript : ScriptableObject, IStringTranslationObject<StringTranslationObjectScript>
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
        private string comment = string.Empty;

        /// <summary>
        /// Translation
        /// </summary>
        public StringTranslationData Translation => stringTranslation ??= new StringTranslationData();

        /// <summary>
        /// Values
        /// </summary>
        public IReadOnlyList<TranslatedStringData> Values => Translation.Values;

        /// <summary>
        /// Translated value
        /// </summary>
        public string Value => Translation.Value;

        /// <summary>
        /// Fallback value
        /// </summary>
        public string FallbackValue => Translation.FallbackValue;

        /// <summary>
        /// Fallback language
        /// </summary>
        public SystemLanguage FallbackLanguage => Translation.FallbackLanguage;

        /// <summary>
        /// Comment
        /// </summary>
        public string Comment => comment ?? string.Empty;

#if UNITY_EDITOR
        /// <summary>
        /// Sets comment for translation
        /// </summary>
        /// <param name="comment">Comment</param>
        public void SetComment(string comment) => this.comment = comment ?? throw new ArgumentNullException(nameof(comment));
#endif

        /// <summary>
        /// Is language contained in this translation data
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>"true" if language is contained in this translation data, otherwise "false"</returns>
        public bool IsLanguageContained(SystemLanguage language) => Translation.IsLanguageContained(language);

        /// <summary>
        /// Tries to get translated value from the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <param name="result">Result</param>
        /// <returns>"true" if translated value is available, otherwise "false"</returns>
        public bool TryGetValue(SystemLanguage language, out string result) => Translation.TryGetValue(language, out result);

        /// <summary>
        /// Gets translated value from the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>Translated value</returns>
        public string GetValue(SystemLanguage language) => Translation.GetValue(language);

        /// <summary>
        /// Compares this string translation to another string translation
        /// </summary>
        /// <param name="other">Other string translation</param>
        /// <returns>Comparison result</returns>
        public int CompareTo(StringTranslationObjectScript other) => (other == null) ? 1 : name.CompareTo(other.name);

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString() => Value;
    }
}
