/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents a translation data wrapper
    /// </summary>
    /// <typeparam name="TValue">Translated value type</typeparam>
    /// <typeparam name="TTranslationData">Translation data type</typeparam>
    /// <typeparam name="TTranslatedData">Translated data type</typeparam>
    public interface ITranslationDataWrapper<TValue, TTranslationData, TTranslatedData> where TTranslationData : ITranslationData<TValue, TTranslatedData> where TTranslatedData : ITranslatedData<TValue>
    {
#if UNITY_EDITOR
        /// <summary>
        /// Translation
        /// </summary>
        TTranslationData Translation { get; }
#endif
    }
}
