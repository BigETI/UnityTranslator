using System;
using UnityEngine;
using UnityTranslator.Data;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents a sprite translation object
    /// </summary>
    /// <typeparam name="TSelf">Own type (inheriter)</typeparam>
    public interface ISpriteTranslationObject<TSelf> : ITranslationObject<TSelf, Sprite, SpriteTranslationData, TranslatedSpriteData> where TSelf : IBaseTranslationObject, IReadOnlyTranslationData<Sprite, TranslatedSpriteData>, IComparable<TSelf>
    {
        // ...
    }
}
