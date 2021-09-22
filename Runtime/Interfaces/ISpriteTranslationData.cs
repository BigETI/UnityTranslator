using System;
using UnityEngine;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents sprite translation data
    /// </summary>
    /// <typeparam name="TSelf">Own type (inheriter)</typeparam>
    /// <typeparam name="TTranslatedData">Translated data type</typeparam>
    public interface ISpriteTranslationData<TSelf, TTranslatedData> : ITranslationData<Sprite, TTranslatedData>, IComparable<TSelf> where TSelf : ITranslationData<Sprite, TTranslatedData>, IComparable<TSelf> where TTranslatedData : ITranslatedSpriteData
    {
        // ...
    }
}
