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
    /// Sprite translation data interface
    /// </summary>
    public interface ISpriteTranslationData : IComparable<ISpriteTranslationData>
    {
        /// <summary>
        /// Translated sprites
        /// </summary>
        IReadOnlyList<TranslatedSpriteData> Sprites { get; }

        /// <summary>
        /// Translated sprite
        /// </summary>
        Sprite Sprite { get; }

        /// <summary>
        /// Add translated sprite
        /// </summary>
        /// <param name="sprite">Translated sprite</param>
        void AddSprite(TranslatedSpriteData sprite);
    }
}
