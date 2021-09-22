using System;
using UnityEngine;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents texture translation data
    /// </summary>
    /// <typeparam name="TSelf">Own type (inheriter)</typeparam>
    /// <typeparam name="TTranslatedData">Translated data type</typeparam>
    public interface ITextureTranslationData<TSelf, TTranslatedData> : ITranslationData<Texture, TTranslatedData>, IComparable<TSelf> where TSelf : ITranslationData<Texture, TTranslatedData>, IComparable<TSelf> where TTranslatedData : ITranslatedTextureData
    {
        // ...
    }
}
