using System;
using UnityEngine;
using UnityTranslator.Objects;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// Audio source translator trigger script abstract class
    /// </summary>
    [Serializable]
    public abstract class AAudioSourceTranslatorTriggerScript : MonoBehaviour, IAudioSourceTranslatorTrigger
    {
        /// <summary>
        /// Translation object
        /// </summary>
        [SerializeField]
        private AudioClipTranslationObjectScript audioTranslationObject = default;

        /// <summary>
        /// Audio clip translation
        /// </summary>
        public AudioClip AudioClipTranslation => ((audioTranslationObject == null) ? null : audioTranslationObject.AudioClip);

        /// <summary>
        /// Update audio clip
        /// </summary>
        /// <param name="audioClip">Audio clip</param>
        protected abstract void UpdateAudioClip(AudioClip audioClip);

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            UpdateAudioClip(AudioClipTranslation);
            Destroy(this);
        }
    }
}
