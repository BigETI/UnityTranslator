using System;
using UnityEngine;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents audio clip translation data
    /// </summary>
    /// <typeparam name="TSelf">Own type (inheriter)</typeparam>
    /// <typeparam name="TTranslatedData">Translated data type</typeparam>
    public interface IAudioClipTranslationData<TSelf, TTranslatedData> : ITranslationData<AudioClip, TTranslatedData>, IComparable<TSelf> where TSelf : ITranslationData<AudioClip, TTranslatedData>, IComparable<TSelf> where TTranslatedData : ITranslatedAudioClipData
    {
        // ...
    }
}
