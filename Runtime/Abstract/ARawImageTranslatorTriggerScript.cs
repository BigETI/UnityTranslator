using UnityEngine;
using UnityTranslator.Objects;

/// <summary>
/// Unity translator triggers namespace
/// </summary>
namespace UnityTranslator.Triggers
{
    /// <summary>
    /// An abstract class that describes a raw image translator trigger script
    /// </summary>
    public abstract class ARawImageTranslatorTriggerScript : MonoBehaviour, IBaseRawImageTranslatorTrigger
    {
        /// <summary>
        /// Texture translation object
        /// </summary>
        [SerializeField]
        private TextureTranslationObjectScript textureTranslationObject = default;

        /// <summary>
        /// Texture translation
        /// </summary>
        public Texture TextureTranslation => textureTranslationObject ? textureTranslationObject.Value : null;

        /// <summary>
        /// Updates texture
        /// </summary>
        /// <param name="texture">Texture</param>
        protected abstract void UpdateTexture(Texture texture);

        /// <summary>
        /// Gets invoked when script gets started
        /// </summary>
        protected virtual void Start()
        {
            UpdateTexture(TextureTranslation);
            Destroy(this);
        }
    }
}
