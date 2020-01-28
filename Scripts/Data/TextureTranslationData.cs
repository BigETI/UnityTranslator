using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// Texture translation data class
    /// </summary>
    [Serializable]
    public class TextureTranslationData : ITextureTranslationData
    {
        /// <summary>
        /// Translated textures
        /// </summary>
        [SerializeField]
        private TranslatedTextureData[] textures = new TranslatedTextureData[] { TranslatedTextureData.defaultTranslatedTexture };

        /// <summary>
        /// Lookup
        /// </summary>
        private Dictionary<SystemLanguage, Texture> lookup;

        /// <summary>
        /// Translated textures
        /// </summary>
        public IReadOnlyList<TranslatedTextureData> Textures
        {
            get
            {
                if (textures == null)
                {
                    textures = new TranslatedTextureData[] { TranslatedTextureData.defaultTranslatedTexture };
                }
                return textures;
            }
        }

        /// <summary>
        /// Translated texture
        /// </summary>
        public Texture Texture
        {
            get
            {
                Texture ret = null;
                if (lookup == null)
                {
                    lookup = new Dictionary<SystemLanguage, Texture>();
                    foreach (TranslatedTextureData texture in Textures)
                    {
                        if (lookup.ContainsKey(texture.Language))
                        {
                            lookup[texture.Language] = texture.Texture;
                        }
                        else
                        {
                            lookup.Add(texture.Language, texture.Texture);
                        }
                    }
                }
                if (lookup.ContainsKey(Translator.SystemLanguage))
                {
                    ret = lookup[Translator.SystemLanguage];
                }
                else if (Textures.Count > 0)
                {
                    ret = Textures[0].Texture;
                }
                return ret;
            }
        }

        /// <summary>
        /// Add translated texture
        /// </summary>
        /// <param name="texture">Translated texture</param>
        public void AddTexture(TranslatedTextureData texture)
        {
            bool append = true;
            for (int i = 0; i < Textures.Count; i++)
            {
                ref TranslatedTextureData translated_texture = ref textures[i];
                if (translated_texture.Language == texture.Language)
                {
                    translated_texture = texture;
                    append = false;
                    break;
                }
            }
            if (append)
            {
                TranslatedTextureData[] textures = new TranslatedTextureData[Textures.Count + 1];
                Array.Copy(this.textures, 0, textures, 0, this.textures.Length);
                textures[this.textures.Length] = texture;
                this.textures = textures;
            }
        }

        /// <summary>
        /// Compare to
        /// </summary>
        /// <param name="other">Other</param>
        /// <returns>Result</returns>
        public int CompareTo(ITextureTranslationData other)
        {
            int ret = 1;
            if (other != null)
            {
                ret = ((Texture == null ? string.Empty : Texture.name).CompareTo((other.Texture == null) ? string.Empty : other.Texture.name));
            }
            return ret;
        }
    }
}
