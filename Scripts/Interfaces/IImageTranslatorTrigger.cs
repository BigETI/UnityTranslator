using UnityEngine;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// Image translator trigger interface
    /// </summary>
    public interface IImageTranslatorTrigger
    {
        /// <summary>
        /// Sprite translation
        /// </summary>
        Sprite SpriteTranslation { get; }
    }
}
