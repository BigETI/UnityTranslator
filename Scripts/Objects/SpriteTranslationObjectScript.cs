using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityTranslator.Data;

/// <summary>
/// Unity translator objects namespace
/// </summary>
namespace UnityTranslator.Objects
{
    /// <summary>
    /// Sprite translation object script class
    /// </summary>
    [CreateAssetMenu(fileName = "SpriteTranslation", menuName = "Translator/Sprite translation")]
    public class SpriteTranslationObjectScript : ScriptableObject, ISpriteTranslationObject
    {
        /// <summary>
        /// Sprite translation
        /// </summary>
        [SerializeField]
        private SpriteTranslationData spriteTranslation = default;

        /// <summary>
        /// Comment
        /// </summary>
        [TextArea]
        [SerializeField]
        private string comment = default;

        /// <summary>
        /// Sprite translation
        /// </summary>
        public SpriteTranslationData SpriteTranslation
        {
            get
            {
                if (spriteTranslation == null)
                {
                    spriteTranslation = new SpriteTranslationData();
                }
                return spriteTranslation;
            }
        }

        /// <summary>
        /// Translated sprite
        /// </summary>
        public Sprite Sprite => SpriteTranslation.Sprite;

        /// <summary>
        /// Comment
        /// </summary>
        public string Comment
        {
            get
            {
                if (comment == null)
                {
                    comment = string.Empty;
                }
                return comment;
            }
        }

        /// <summary>
        /// Contains language
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>"true" if contains language, otherwise "false"</returns>
        public bool ContainsLanguage(SystemLanguage language)
        {
            bool ret = false;
            foreach (TranslatedSpriteData translated_sprite in SpriteTranslation.Sprites)
            {
                if (translated_sprite.Language == language)
                {
                    if (translated_sprite.Sprite != null)
                    {
                        ret = true;
                        break;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Compare to
        /// </summary>
        /// <param name="other">Other</param>
        /// <returns>Result</returns>
        public int CompareTo(ISpriteTranslationObject other)
        {
            int ret = 1;
            if (other != null)
            {
                ret = name.CompareTo(other.name);
            }
            return ret;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString() => SpriteTranslation.ToString();
    }
}
