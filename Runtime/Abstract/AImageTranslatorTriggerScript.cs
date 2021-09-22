using System;
using UnityEngine;
using UnityTranslator.Objects;

/// <summary>
/// Unity translator triggers namespace
/// </summary>
namespace UnityTranslator.Triggers
{
    /// <summary>
    /// An abstract class that describes an image translator trigger script
    /// </summary>
    [Serializable]
    public abstract class AImageTranslatorTriggerScript : MonoBehaviour, IBaseImageTranslatorTrigger
    {
        /// <summary>
        /// Sprite translation object
        /// </summary>
        [SerializeField]
        private SpriteTranslationObjectScript spriteTranslationObject = default;

        /// <summary>
        /// Sprite translation
        /// </summary>
        public Sprite SpriteTranslation => spriteTranslationObject ? spriteTranslationObject.Value : null;

        /// <summary>
        /// Updates sprite
        /// </summary>
        /// <param name="sprite">Sprite</param>
        protected abstract void UpdateSprite(Sprite sprite);

        /// <summary>
        /// Gets invoked when script gets started
        /// </summary>
        protected virtual void Start()
        {
            UpdateSprite(SpriteTranslation);
            Destroy(this);
        }
    }
}
