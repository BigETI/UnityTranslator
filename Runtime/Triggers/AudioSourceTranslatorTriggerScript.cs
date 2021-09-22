using UnityEngine;

/// <summary>
/// Unity translator triggers namespace
/// </summary>
namespace UnityTranslator.Triggers
{
    /// <summary>
    /// A class that describes an audio source translator trigger script
    /// </summary>
    public class AudioSourceTranslatorTriggerScript : AAudioSourceTranslatorTriggerScript, IAudioSourceTranslatorTrigger
    {
        /// <summary>
        /// Updates audio clip
        /// </summary>
        /// <param name="audioClip">Audio clip</param>
        protected override void UpdateAudioClip(AudioClip audioClip)
        {
            if (TryGetComponent(out AudioSource audio_source))
            {
                audio_source.clip = audioClip;
            }
        }
    }
}
