using System;
using UnityEngine;
using UnityTranslator.Data;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// Texture translation object interface
    /// </summary>
    public interface ITextureTranslationObject : ITranslationObject, IComparable<ITextureTranslationObject>
    {
        /// <summary>
        /// Texture translation
        /// </summary>
        TextureTranslationData TextureTranslation { get; }

        /// <summary>
        /// Translated texture
        /// </summary>
        Texture Texture { get; }

        /// <summary>
        /// Comment
        /// </summary>
        string Comment { get; }
    }
}
