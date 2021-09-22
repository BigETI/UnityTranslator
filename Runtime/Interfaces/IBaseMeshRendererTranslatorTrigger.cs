using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents a base mesh renderer translator trigger
    /// </summary>
    public interface IBaseMeshRendererTranslatorTrigger
    {
        /// <summary>
        /// Material translations
        /// </summary>
        IReadOnlyList<Material> MaterialTranslations { get; }
    }
}
