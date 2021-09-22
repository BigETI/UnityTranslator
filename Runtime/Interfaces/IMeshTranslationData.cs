using System;
using UnityEngine;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents mesh translation data
    /// </summary>
    /// <typeparam name="TSelf">Own type (inheriter)</typeparam>
    /// <typeparam name="TTranslatedData">Translated data type</typeparam>
    public interface IMeshTranslationData<TSelf, TTranslatedData> : ITranslationData<Mesh, TTranslatedData>, IComparable<TSelf> where TSelf : ITranslationData<Mesh, TTranslatedData>, IComparable<TSelf> where TTranslatedData : ITranslatedMeshData
    {
        // ...
    }
}
