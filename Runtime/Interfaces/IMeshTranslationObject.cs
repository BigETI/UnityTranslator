using System;
using UnityEngine;
using UnityTranslator.Data;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents a mesh translation object
    /// </summary>
    /// <typeparam name="TSelf">Own type (inheriter)</typeparam>
    public interface IMeshTranslationObject<TSelf> : ITranslationObject<TSelf, Mesh, MeshTranslationData, TranslatedMeshData> where TSelf : IBaseTranslationObject, IReadOnlyTranslationData<Mesh, TranslatedMeshData>, IComparable<TSelf>
    {
        // ...
    }
}
