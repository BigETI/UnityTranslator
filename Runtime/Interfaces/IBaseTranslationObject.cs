/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// An interface that represents a base translation object
    /// </summary>
    public interface IBaseTranslationObject : IBaseTranslationData
    {
#pragma warning disable IDE1006 // Naming styles
        /// <summary>
        /// Name
        /// </summary>
        string name { get; set; }
#pragma warning restore IDE1006 // Naming styles

        /// <summary>
        /// Comment
        /// </summary>
        string Comment { get; }

#if UNITY_EDITOR
        /// <summary>
        /// Sets comment for translation
        /// </summary>
        /// <param name="comment">Comment</param>
        void SetComment(string comment);
#endif
    }
}
