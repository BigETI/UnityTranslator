using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Unity translator triggers namespace
/// </summary>
namespace UnityTranslator.Triggers
{
    /// <summary>
    /// A class that describes an image translator trigger script
    /// </summary>
    public class ImageTranslatorTriggerScript : AImageTranslatorTriggerScript, IImageTranslatorTrigger
    {
        /// <summary>
        /// Updates sprite
        /// </summary>
        /// <param name="sprite">Sprite</param>
        protected override void UpdateSprite(Sprite sprite)
        {
            if (TryGetComponent(out Image image))
            {
                image.sprite = sprite;
            }
        }
    }
}
