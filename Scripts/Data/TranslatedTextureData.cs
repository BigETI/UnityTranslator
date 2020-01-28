using System;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// Translated texture data structure
    /// </summary>
    [Serializable]
    public struct TranslatedTextureData
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
        /// Translated texture
        /// </summary>
        public Texture Texture => texture;

        /// <summary>
        /// Language
        /// </summary>
        public SystemLanguage Language => language;

        /// <summary>
        /// Constructor
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
        public override string ToString() => ((texture == null) ? string.Empty : texture.name);
    }
}
