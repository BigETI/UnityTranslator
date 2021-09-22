using UnityEngine;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents a base audio source translator trigger
    /// </summary>
    public interface IBaseAudioSourceTranslatorTrigger
    {
        /// <summary>
        /// Audio clip translation
        /// </summary>
        AudioClip AudioClipTranslation { get; }
    }
}
