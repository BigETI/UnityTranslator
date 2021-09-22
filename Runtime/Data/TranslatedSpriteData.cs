using System;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// A class that describes translated sprite data
    /// </summary>
    [Serializable]
    public struct TranslatedSpriteData : ITranslatedSpriteData
    {
        /// <summary>
        /// Default translated texture
        /// </summary>
        public static readonly TranslatedSpriteData defaultTranslatedSprite = new TranslatedSpriteData(null, SystemLanguage.English);

        /// <summary>
        /// Translated sprite
        /// </summary>
        [SerializeField]
        private Sprite sprite;

        /// <summary>
        /// Language
        /// </summary>
        [SerializeField]
        private SystemLanguage language;

        /// <summary>
        /// Translated value
        /// </summary>
        public Sprite Value => sprite;

        /// <summary>
        /// Language
        /// </summary>
        public SystemLanguage Language => language;

        /// <summary>
        /// Constructs new translated sprite data
        /// </summary>
        /// <param name="sprite">Translated sprite</param>
        /// <param name="language">Language</param>
        public TranslatedSpriteData(Sprite sprite, SystemLanguage language)
        {
            this.sprite = sprite;
            this.language = language;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString() => sprite ? sprite.name : string.Empty;
    }
}
