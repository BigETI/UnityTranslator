using System;
using UnityEngine;
using UnityTranslator.Data;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents a texture translation object
    /// </summary>
    /// <typeparam name="TSelf">Own type (inheriter)</typeparam>
    public interface ITextureTranslationObject<TSelf> : ITranslationObject<TSelf, Texture, TextureTranslationData, TranslatedTextureData> where TSelf : IBaseTranslationObject, IReadOnlyTranslationData<Texture, TranslatedTextureData>, IComparable<TSelf>
    {
        // ...
    }
}
