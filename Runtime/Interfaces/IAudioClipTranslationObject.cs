using System;
using UnityEngine;
using UnityTranslator.Data;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents an audio clip translation object
    /// </summary>
    /// <typeparam name="TSelf">Own type (inheriter)</typeparam>
    public interface IAudioClipTranslationObject<TSelf> : ITranslationObject<TSelf, AudioClip, AudioClipTranslationData, TranslatedAudioClipData> where TSelf : IBaseTranslationObject, IReadOnlyTranslationData<AudioClip, TranslatedAudioClipData>, IComparable<TSelf>
    {
        // ...
    }
}
