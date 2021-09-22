using System;

/// <summary>
/// Unity translator interface
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents string translation data
    /// </summary>
    /// <typeparam name="TSelf">Own type (inheriter)</typeparam>
    /// <typeparam name="TTranslatedData">Translated data type</typeparam>
    public interface IStringTranslationData<TSelf, TTranslatedData> : ITranslationData<string, TTranslatedData>, IComparable<TSelf> where TSelf : ITranslationData<string, TTranslatedData>, IComparable<TSelf> where TTranslatedData : ITranslatedStringData
    {
        // ...
    }
}
