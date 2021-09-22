using UnityEngine;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents translation data
    /// </summary>
    /// <typeparam name="TValue">Translated value type</typeparam>
    /// <typeparam name="TTranslatedData"></typeparam>
    public interface ITranslationData<TValue, TTranslatedData> : IReadOnlyTranslationData<TValue, TTranslatedData> where TTranslatedData : ITranslatedData<TValue>
    {
        void Insert(TValue value, SystemLanguage language);

        void Remove(SystemLanguage language);

        void Clear();
    }
}
