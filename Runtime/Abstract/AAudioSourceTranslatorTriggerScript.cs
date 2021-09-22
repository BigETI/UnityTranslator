using System;
using UnityEngine;
using UnityTranslator.Objects;

/// <summary>
/// Unity translator triggers namespace
/// </summary>
namespace UnityTranslator.Triggers
{
    /// <summary>
    /// An abstract class that describes an audio source translator trigger script
    /// </summary>
    [Serializable]
    public abstract class AAudioSourceTranslatorTriggerScript : MonoBehaviour, IBaseAudioSourceTranslatorTrigger
    {
        /// <summary>
        /// Audio translation object
        /// </summary>
        [SerializeField]
        private AudioClipTranslationObjectScript audioTranslationObject = default;

        /// <summary>
        /// Audio clip translation
        /// </summary>
        public AudioClip AudioClipTranslation => audioTranslationObject ? audioTranslationObject.Value : null;

        /// <summary>
        /// Updates audio clip
        /// </summary>
        /// <param name="audioClip">Audio clip</param>
        protected abstract void UpdateAudioClip(AudioClip audioClip);

        /// <summary>
        /// Gets invoked when script gets started
        /// </summary>
        protected virtual void Start()
        {
            UpdateAudioClip(AudioClipTranslation);
            Destroy(this);
        }
    }
}
