using System;
using UnityEngine;
using UnityTranslator.Data;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents a material translation object
    /// </summary>
    /// <typeparam name="TSelf">Own type (inheriter)</typeparam>
    public interface IMaterialTranslationObject<TSelf> : ITranslationObject<TSelf, Material, MaterialTranslationData, TranslatedMaterialData> where TSelf : IBaseTranslationObject, IReadOnlyTranslationData<Material, TranslatedMaterialData>, IComparable<TSelf>
    {
        // ...
    }
}
