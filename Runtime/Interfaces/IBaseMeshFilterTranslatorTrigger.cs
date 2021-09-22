using UnityEngine;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents a base mesh filter translator trigger
    /// </summary>
    public interface IBaseMeshFilterTranslatorTrigger
    {
        /// <summary>
        /// Mesh translation
        /// </summary>
        Mesh MeshTranslation { get; }
    }
}
