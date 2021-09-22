using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents read-only translation data
    /// </summary>
    /// <typeparam name="TValue">Translated value type</typeparam>
    /// <typeparam name="TTranslatedData">Translated data type</typeparam>
    public interface IReadOnlyTranslationData<TValue, TTranslatedData> : IBaseTranslationData where TTranslatedData : ITranslatedData<TValue>
    {
        /// <summary>
        /// Translated values
        /// </summary>
        IReadOnlyList<TTranslatedData> Values { get; }

        /// <summary>
        /// Translated value
        /// </summary>
        TValue Value { get; }

        /// <summary>
        /// Fallback value
        /// </summary>
        TValue FallbackValue { get; }

        /// <summary>
        /// Tries to get translated value from the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <param name="result">Result</param>
        /// <returns>"true" if translated value is available, otherwise "false"</returns>
        bool TryGetValue(SystemLanguage language, out TValue result);

        /// <summary>
        /// Gets translated value from the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>Translated value</returns>
        TValue GetValue(SystemLanguage language);
    }
}
