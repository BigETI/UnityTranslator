using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

/// <summary>
/// Unity translator editor namespace
/// </summary>
namespace UnityTranslatorEditor
{
    /// <summary>
    /// An interface that represents a tokenized search field
    /// </summary>
    public interface ITokenizedSearchField
    {
        /// <summary>
        /// Search field
        /// </summary>
        SearchField SearchField { get; }

        /// <summary>
        /// Search query
        /// </summary>
        string SearchQuery { get; set; }

        /// <summary>
        /// Search tokens
        /// </summary>
        public IReadOnlyList<string> SearchTokens { get; }

        /// <summary>
        /// Is the specified input contained in search
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>"true" if the specified input is contained in search, otherwise "false"</returns>
        bool IsContainedInSearch(string input);

        /// <summary>
        /// Draws tokenized search field
        /// </summary>
        /// <param name="options">GUI layout options</param>
        void Draw(params GUILayoutOption[] options);
    }
}
