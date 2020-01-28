using UnityEngine;
using UnityTranslator.Data;

/// <summary>
/// Unity translator objects namespace
/// </summary>
namespace UnityTranslator.Objects
{
    [CreateAssetMenu(fileName = "TextureTranslation", menuName = "Translator/Texture translation")]
    public class TextureTranslationObjectScript : ScriptableObject, ITextureTranslationObject
    {
        /// <summary>
        /// Texture translation
        /// </summary>
        [SerializeField]
        private TextureTranslationData textureTranslation = default;

        /// <summary>
        /// Comment
        /// </summary>
        [TextArea]
        [SerializeField]
        private string comment = default;

        /// <summary>
        /// Texture translation
        /// </summary>
        public TextureTranslationData TextureTranslation
        {
            get
            {
                if (textureTranslation == null)
                {
                    textureTranslation = new TextureTranslationData();
                }
                return textureTranslation;
            }
        }

        /// <summary>
        /// Translated texture
        /// </summary>
        public Texture Texture => TextureTranslation.Texture;

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
            foreach (TranslatedTextureData translated_sprite in TextureTranslation.Textures)
            {
                if (translated_sprite.Language == language)
                {
                    if (translated_sprite.Texture != null)
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
        public int CompareTo(ITextureTranslationObject other)
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
        public override string ToString() => TextureTranslation.ToString();
    }
}
