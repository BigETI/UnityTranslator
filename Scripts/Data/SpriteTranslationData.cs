using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// Sprite translation data class
    /// </summary>
    [Serializable]
    public class SpriteTranslationData : ISpriteTranslationData
    {
        /// <summary>
        /// Translated sprites
        /// </summary>
        [SerializeField]
        private TranslatedSpriteData[] sprites = new TranslatedSpriteData[] { TranslatedSpriteData.defaultTranslatedSprite };

        /// <summary>
        /// Lookup
        /// </summary>
        private Dictionary<SystemLanguage, Sprite> lookup;

        /// <summary>
        /// Translated sprites
        /// </summary>
        public IReadOnlyList<TranslatedSpriteData> Sprites
        {
            get
            {
                if (sprites == null)
                {
                    sprites = new TranslatedSpriteData[] { TranslatedSpriteData.defaultTranslatedSprite };
                }
                return sprites;
            }
        }

        /// <summary>
        /// Translated sprite
        /// </summary>
        public Sprite Sprite
        {
            get
            {
                Sprite ret = null;
                if (lookup == null)
                {
                    lookup = new Dictionary<SystemLanguage, Sprite>();
                    foreach (TranslatedSpriteData sprite in Sprites)
                    {
                        if (lookup.ContainsKey(sprite.Language))
                        {
                            lookup[sprite.Language] = sprite.Sprite;
                        }
                        else
                        {
                            lookup.Add(sprite.Language, sprite.Sprite);
                        }
                    }
                }
                if (lookup.ContainsKey(Translator.SystemLanguage))
                {
                    ret = lookup[Translator.SystemLanguage];
                }
                else if (Sprites.Count > 0)
                {
                    ret = Sprites[0].Sprite;
                }
                return ret;
            }
        }

        /// <summary>
        /// Add translated sprite
        /// </summary>
        /// <param name="sprite">Translated sprite</param>
        public void AddSprite(TranslatedSpriteData sprite)
        {
            bool append = true;
            for (int i = 0; i < Sprites.Count; i++)
            {
                ref TranslatedSpriteData translated_sprite = ref sprites[i];
                if (translated_sprite.Language == sprite.Language)
                {
                    translated_sprite = sprite;
                    append = false;
                    break;
                }
            }
            if (append)
            {
                TranslatedSpriteData[] sprites = new TranslatedSpriteData[Sprites.Count + 1];
                Array.Copy(this.sprites, 0, sprites, 0, this.sprites.Length);
                sprites[this.sprites.Length] = sprite;
                this.sprites = sprites;
            }
        }

        /// <summary>
        /// Compare to
        /// </summary>
        /// <param name="other">Other</param>
        /// <returns>Result</returns>
        public int CompareTo(ISpriteTranslationData other)
        {
            int ret = 1;
            if (other != null)
            {
                ret = ((Sprite == null ? string.Empty : Sprite.name).CompareTo((other.Sprite == null) ? string.Empty : other.Sprite.name));
            }
            return ret;
        }
    }
}
