using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// Audio clip translation data class
    /// </summary>
    [Serializable]
    public class AudioClipTranslationData : IAudioClipTranslationData
    {
        /// <summary>
        /// Translated audio clips
        /// </summary>
        [SerializeField]
        private TranslatedAudioClipData[] audioClips = new TranslatedAudioClipData[] { TranslatedAudioClipData.defaultTranslatedAudioClip };

        /// <summary>
        /// Lookup
        /// </summary>
        private Dictionary<SystemLanguage, AudioClip> lookup;

        /// <summary>
        /// Translated audio clips
        /// </summary>
        public IReadOnlyList<TranslatedAudioClipData> AudioClips
        {
            get
            {
                if (audioClips == null)
                {
                    audioClips = new TranslatedAudioClipData[] { TranslatedAudioClipData.defaultTranslatedAudioClip };
                }
                return audioClips;
            }
        }

        /// <summary>
        /// Translated audio clip
        /// </summary>
        public AudioClip AudioClip
        {
            get
            {
                AudioClip ret = null;
                if (lookup == null)
                {
                    lookup = new Dictionary<SystemLanguage, AudioClip>();
                    foreach (TranslatedAudioClipData audio_clip in AudioClips)
                    {
                        if (lookup.ContainsKey(audio_clip.Language))
                        {
                            lookup[audio_clip.Language] = audio_clip.AudioClip;
                        }
                        else
                        {
                            lookup.Add(audio_clip.Language, audio_clip.AudioClip);
                        }
                    }
                }
                if (lookup.ContainsKey(Translator.SystemLanguage))
                {
                    ret = lookup[Translator.SystemLanguage];
                }
                else if (AudioClips.Count > 0)
                {
                    ret = AudioClips[0].AudioClip;
                }
                return ret;
            }
        }

        /// <summary>
        /// Add translated audio clip
        /// </summary>
        /// <param name="audioClip">Translated audio clip</param>
        public void AddAudioClip(TranslatedAudioClipData audioClip)
        {
            bool append = true;
            for (int i = 0; i < AudioClips.Count; i++)
            {
                ref TranslatedAudioClipData translated_audio_clip = ref audioClips[i];
                if (translated_audio_clip.Language == audioClip.Language)
                {
                    translated_audio_clip = audioClip;
                    append = false;
                    break;
                }
            }
            if (append)
            {
                TranslatedAudioClipData[] audio_clips = new TranslatedAudioClipData[AudioClips.Count + 1];
                Array.Copy(audioClips, 0, audio_clips, 0, audioClips.Length);
                audio_clips[audioClips.Length] = audioClip;
                audioClips = audio_clips;
            }
        }

        /// <summary>
        /// Compare to
        /// </summary>
        /// <param name="other">Other</param>
        /// <returns>Result</returns>
        public int CompareTo(IAudioClipTranslationData other)
        {
            int ret = 1;
            if (other != null)
            {
                ret = ((AudioClip == null ? string.Empty : AudioClip.name).CompareTo((other.AudioClip == null) ? string.Empty : other.AudioClip.name));
            }
            return ret;
        }
    }
}
