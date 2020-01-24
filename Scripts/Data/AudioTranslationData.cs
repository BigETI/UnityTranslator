using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// Audio translation data class
    /// </summary>
    [Serializable]
    public class AudioTranslationData : IComparable<AudioTranslationData>
    {
        /// <summary>
        /// Audios
        /// </summary>
        [SerializeField]
        private TranslatedAudioData[] audios = new TranslatedAudioData[] { TranslatedAudioData.defaultTranslatedAudio };

        /// <summary>
        /// Lookup
        /// </summary>
        private Dictionary<SystemLanguage, AudioClip> lookup;

        /// <summary>
        /// Audios
        /// </summary>
        public IReadOnlyList<TranslatedAudioData> Audios
        {
            get
            {
                if (audios == null)
                {
                    audios = new TranslatedAudioData[] { TranslatedAudioData.defaultTranslatedAudio };
                }
                return audios;
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
                    foreach (TranslatedAudioData audio in Audios)
                    {
                        if (lookup.ContainsKey(audio.Language))
                        {
                            lookup[audio.Language] = audio.AudioClip;
                        }
                        else
                        {
                            lookup.Add(audio.Language, audio.AudioClip);
                        }
                    }
                }
                if (lookup.ContainsKey(Translator.SystemLanguage))
                {
                    ret = lookup[Translator.SystemLanguage];
                }
                else if (Audios.Count > 0)
                {
                    ret = Audios[0].AudioClip;
                }
                return ret;
            }
        }

        /// <summary>
        /// Add translated audio
        /// </summary>
        /// <param name="audio">Translated audio</param>
        public void AddAudioClip(TranslatedAudioData audio)
        {
            bool append = true;
            for (int i = 0; i < Audios.Count; i++)
            {
                TranslatedAudioData translated_audio = Audios[i];
                if (translated_audio.Language == audio.Language)
                {
                    audios[i] = audio;
                    append = false;
                    break;
                }
            }
            if (append)
            {
                TranslatedAudioData[] audios = new TranslatedAudioData[Audios.Count + 1];
                Array.Copy(this.audios, 0, audios, 0, this.audios.Length);
                audios[this.audios.Length] = audio;
                this.audios = audios;
            }
        }

        /// <summary>
        /// Compare to
        /// </summary>
        /// <param name="other">Other</param>
        /// <returns>Result</returns>
        public int CompareTo(AudioTranslationData other)
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
