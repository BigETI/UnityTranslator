using System;

/// <summary>
/// Unity translator interface
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents a translation object
    /// </summary>
    /// <typeparam name="TSelf">Own type (inheriter)</typeparam>
    /// <typeparam name="TValue">Translated value type</typeparam>
    /// <typeparam name="TTranslationData">Translation data type</typeparam>
    /// <typeparam name="TTranslatedData">Translated data type</typeparam>
    public interface ITranslationObject<TSelf, TValue, TTranslationData, TTranslatedData> : IBaseTranslationObject, IReadOnlyTranslationData<TValue, TTranslatedData>, ITranslationDataWrapper<TValue, TTranslationData, TTranslatedData>, IComparable<TSelf> where TSelf : IBaseTranslationObject, IReadOnlyTranslationData<TValue, TTranslatedData>, IComparable<TSelf> where TTranslationData : ITranslationData<TValue, TTranslatedData> where TTranslatedData : ITranslatedData<TValue>
    {
        // ...
    }
}
