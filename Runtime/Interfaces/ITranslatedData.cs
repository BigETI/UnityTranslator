using UnityEngine;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that describes translated data
    /// </summary>
    /// <typeparam name="T">Translated value type</typeparam>
    public interface ITranslatedData<T>
    {
        /// <summary>
        /// Translated value
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Language
        /// </summary>
        SystemLanguage Language { get; }
    }
}
