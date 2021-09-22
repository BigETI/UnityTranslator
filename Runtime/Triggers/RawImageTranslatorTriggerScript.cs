using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Unity translator triggers namespace
/// </summary>
namespace UnityTranslator.Triggers
{
    /// <summary>
    /// A class that describes a raw image translator trigger script
    /// </summary>
    public class RawImageTranslatorTriggerScript : ARawImageTranslatorTriggerScript, IRawImageTranslatorTrigger
    {
        /// <summary>
        /// Updates texture
        /// </summary>
        /// <param name="texture">Texture</param>
        protected override void UpdateTexture(Texture texture)
        {
            if (TryGetComponent(out RawImage raw_image))
            {
                raw_image.texture = texture;
            }
        }
    }
}
