using System;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// A class that describes translated mesh data
    /// </summary>
    [Serializable]
    public struct TranslatedMeshData : ITranslatedMeshData
    {
        /// <summary>
        /// Default translated mesh
        /// </summary>
        public static readonly TranslatedMeshData defaultTranslatedMesh = new TranslatedMeshData(null, SystemLanguage.English);

        /// <summary>
        /// Translated mesh
        /// </summary>
        [SerializeField]
        private Mesh mesh;

        /// <summary>
        /// Language
        /// </summary>
        [SerializeField]
        private SystemLanguage language;

        /// <summary>
        /// Translated value
        /// </summary>
        public Mesh Value => mesh;

        /// <summary>
        /// Language
        /// </summary>
        public SystemLanguage Language => language;

        /// <summary>
        /// Constructs new translated mesh data
        /// </summary>
        /// <param name="mesh">Translated mesh</param>
        /// <param name="language">Language</param>
        public TranslatedMeshData(Mesh mesh, SystemLanguage language)
        {
            this.mesh = mesh;
            this.language = language;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString() => mesh ? mesh.name : string.Empty;
    }
}
