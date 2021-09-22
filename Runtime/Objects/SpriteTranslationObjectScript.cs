using System;
using System.Collections.Generic;
using UnityEngine;
using UnityTranslator.Data;

/// <summary>
/// Unity translator objects namespace
/// </summary>
namespace UnityTranslator.Objects
{
    [CreateAssetMenu(fileName = "SpriteTranslation", menuName = "Translator/Sprite translation")]
    public class SpriteTranslationObjectScript : ScriptableObject, ISpriteTranslationObject<SpriteTranslationObjectScript>
    {
        /// <summary>
        /// Sprite translation
        /// </summary>
        [SerializeField]
        private SpriteTranslationData spriteTranslation = default;

        /// <summary>
        /// Comment
        /// </summary>
        [TextArea]
        [SerializeField]
        private string comment = string.Empty;

        /// <summary>
        /// Translation
        /// </summary>
#if UNITY_EDITOR
        public
#else
        private
#endif
            SpriteTranslationData Translation => spriteTranslation ??= new SpriteTranslationData();

        /// <summary>
        /// Values
        /// </summary>
        public IReadOnlyList<TranslatedSpriteData> Values => Translation.Values;

        /// <summary>
        /// Translated value
        /// </summary>
        public Sprite Value => Translation.Value;

        /// <summary>
        /// Fallback value
        /// </summary>
        public Sprite FallbackValue => Translation.FallbackValue;

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
        public bool TryGetValue(SystemLanguage language, out Sprite result) => Translation.TryGetValue(language, out result);

        /// <summary>
        /// Gets translated value from the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>Translated value</returns>
        public Sprite GetValue(SystemLanguage language) => Translation.GetValue(language);

        /// <summary>
        /// Compares this sprite translation to another sprite translation
        /// </summary>
        /// <param name="other">Other sprite translation</param>
        /// <returns>Comparison result</returns>
        public int CompareTo(SpriteTranslationObjectScript other) => (other == null) ? 1 : name.CompareTo(other.name);

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString() => Translation.ToString();
    }
}
