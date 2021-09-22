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
        /// <summary>
        /// Inserts translated value
        /// </summary>
        /// <param name="value">Translated value</param>
        /// <param name="language">Language</param>
        void Insert(TValue value, SystemLanguage language);

        /// <summary>
        /// Removes translated value with the specified language
        /// </summary>
        /// <param name="language">Language</param>
        void Remove(SystemLanguage language);

        /// <summary>
        /// Clears this translation data
        /// </summary>
        void Clear();
    }
}
