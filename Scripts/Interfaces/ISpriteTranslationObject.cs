using System;
using UnityEngine;
using UnityTranslator.Data;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// Sprite translation object interface
    /// </summary>
    public interface ISpriteTranslationObject : ITranslationObject, IComparable<ISpriteTranslationObject>
    {
        /// <summary>
        /// Sprite translation
        /// </summary>
        SpriteTranslationData SpriteTranslation { get; }

        /// <summary>
        /// Translated sprite
        /// </summary>
        Sprite Sprite { get; }

        /// <summary>
        /// Comment
        /// </summary>
        string Comment { get; }
    }
}
