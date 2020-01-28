using System;
using System.Collections.Generic;
using UnityEngine;
using UnityTranslator.Data;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// Texture translation data interface
    /// </summary>
    public interface ITextureTranslationData : IComparable<ITextureTranslationData>
    {
        /// <summary>
        /// Translated textures
        /// </summary>
        IReadOnlyList<TranslatedTextureData> Textures { get; }

        /// <summary>
        /// Translated texture
        /// </summary>
        Texture Texture { get; }

        /// <summary>
        /// Add translated texture
        /// </summary>
        /// <param name="texture">Translated texture</param>
        void AddTexture(TranslatedTextureData texture);
    }
}
