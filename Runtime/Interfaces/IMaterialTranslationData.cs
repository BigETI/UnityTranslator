using System;
using UnityEngine;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents material translation data
    /// </summary>
    /// <typeparam name="TSelf">Own type (inheriter)</typeparam>
    /// <typeparam name="TTranslatedData">Translated data type</typeparam>
    public interface IMaterialTranslationData<TSelf, TTranslatedData> : ITranslationData<Material, TTranslatedData>, IComparable<TSelf> where TSelf : ITranslationData<Material, TTranslatedData>, IComparable<TSelf> where TTranslatedData : ITranslatedMaterialData
    {
        // ...
    }
}
