using System;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// A class that describes translated material data
    /// </summary>
    [Serializable]
    public struct TranslatedMaterialData : ITranslatedMaterialData
    {
        /// <summary>
        /// Default translated material
        /// </summary>
        public static readonly TranslatedMaterialData defaultTranslatedMaterial = new TranslatedMaterialData(null, SystemLanguage.English);

        /// <summary>
        /// Translated material
        /// </summary>
        [SerializeField]
        private Material material;

        /// <summary>
        /// Language
        /// </summary>
        [SerializeField]
        private SystemLanguage language;

        /// <summary>
        /// Translated value
        /// </summary>
        public Material Value => material;

        /// <summary>
        /// Language
        /// </summary>
        public SystemLanguage Language => language;

        /// <summary>
        /// Constructs new translated material data
        /// </summary>
        /// <param name="material">Translated material</param>
        /// <param name="language">Language</param>
        public TranslatedMaterialData(Material material, SystemLanguage language)
        {
            this.material = material;
            this.language = language;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString() => material ? material.name : string.Empty;
    }
}
