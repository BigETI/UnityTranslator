using System;
using UnityEngine;
using UnityTranslator.Objects;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// Image translator trigger script abstract class
    /// </summary>
    [Serializable]
    public abstract class AImageTranslatorTriggerScript : MonoBehaviour, IImageTranslatorTrigger
    {
        /// <summary>
        /// Sprite translation object
        /// </summary>
        [SerializeField]
        private SpriteTranslationObjectScript spriteTranslationObject = default;

        /// <summary>
        /// Sprite translation
        /// </summary>
        public Sprite SpriteTranslation => ((spriteTranslationObject == null) ? null : spriteTranslationObject.Sprite);

        /// <summary>
        /// Update sprite
        /// </summary>
        /// <param name="sprite">Sprite</param>
        protected abstract void UpdateSprite(Sprite sprite);

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            UpdateSprite(SpriteTranslation);
            Destroy(this);
        }
    }
}
