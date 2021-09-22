using System;
using UnityTranslator.Data;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents a string translation object
    /// </summary>
    /// <typeparam name="TSelf">Own type (inheriter)</typeparam>
    public interface IStringTranslationObject<TSelf> : ITranslationObject<TSelf, string, StringTranslationData, TranslatedStringData> where TSelf : IBaseTranslationObject, IReadOnlyTranslationData<string, TranslatedStringData>, IComparable<TSelf>
    {
        // ...
    }
}
