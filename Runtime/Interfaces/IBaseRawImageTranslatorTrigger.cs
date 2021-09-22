using UnityEngine;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents a base raw image translator trigger
    /// </summary>
    public interface IBaseRawImageTranslatorTrigger
    {
        /// <summary>
        /// Texture translation
        /// </summary>
        Texture TextureTranslation { get; }
    }
}
