using UnityEngine;

/// <summary>
/// Unity audio source translator trigger interface
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// Audio source translator trigger interface
    /// </summary>
    public interface IAudioSourceTranslatorTrigger
    {
        /// <summary>
        /// Audio clip translation
        /// </summary>
        AudioClip AudioClipTranslation { get; }
    }
}
