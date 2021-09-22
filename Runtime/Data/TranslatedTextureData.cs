using System;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// A class that describes translated texture data
    /// </summary>
    [Serializable]
    public struct TranslatedTextureData : ITranslatedTextureData
    {
        /// <summary>
        /// Default translated texture
        /// </summary>
        public static readonly TranslatedTextureData defaultTranslatedTexture = new TranslatedTextureData(null, SystemLanguage.English);

        /// <summary>
        /// Translated texture
        /// </summary>
        [SerializeField]
        private Texture texture;

        /// <summary>
        /// Language
        /// </summary>
        [SerializeField]
        private SystemLanguage language;

        /// <summary>
        /// Translated value
        /// </summary>
        public Texture Value => texture;

        /// <summary>
        /// Language
        /// </summary>
        public SystemLanguage Language => language;

        /// <summary>
        /// Constructs new translated texture data
        /// </summary>
        /// <param name="texture">Translated texture</param>
        /// <param name="language">Language</param>
        public TranslatedTextureData(Texture texture, SystemLanguage language)
        {
            this.texture = texture;
            this.language = language;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString() => texture ? texture.name : string.Empty;
    }
}
