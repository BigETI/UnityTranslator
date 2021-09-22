using UnityEngine;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents a base image translator trigger
    /// </summary>
    public interface IBaseImageTranslatorTrigger
    {
        /// <summary>
        /// Sprite translation
        /// </summary>
        Sprite SpriteTranslation { get; }
    }
}
