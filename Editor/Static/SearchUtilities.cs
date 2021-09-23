using System;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

/// <summary>
/// Unity translator editor namespace
/// </summary>
namespace UnityTranslatorEditor
{
    /// <summary>
    /// A class that describes search utilities
    /// </summary>
    public static class SearchUtilities
    {
        /// <summary>
        /// Is the specified input contained in search
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>"true" if the specified input is contained in search, otherwise "false"</returns>
        public static bool IsContainedInSearch(string input, IReadOnlyList<string> searchTokens)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (searchTokens == null)
            {
                throw new ArgumentNullException(nameof(searchTokens));
            }
            bool ret = searchTokens.Count <= 0;
            if (!ret)
            {
                string lower_case_input = input.ToLower();
                foreach (string search_token in searchTokens)
                {
                    if (lower_case_input.Contains(search_token))
                    {
                        ret = true;
                        break;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Tokenizes the specified search query
        /// </summary>
        /// <param name="searchQuery">Search query</param>
        /// <param name="searchTokens">Search tokens</param>
        public static void TokenizeSearch(string searchQuery, List<string> searchTokens)
        {
            if (searchQuery == null)
            {
                throw new ArgumentNullException(nameof(searchQuery));
            }
            if (searchTokens == null)
            {
                throw new ArgumentNullException(nameof(searchTokens));
            }
            searchTokens.Clear();
            if (searchQuery.StartsWith("\"") && searchQuery.EndsWith("\"") && (searchQuery.Length > 1))
            {
                searchTokens.Add(searchQuery.Substring(1, searchQuery.Length - 2).ToLower());
            }
            else
            {
                foreach (string translation_search_token in searchQuery.Split(' ', '\t', '\u00a0', '\u1680', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005', '\u2006', '\u2007', '\u2008', '\u2009', '\u200a', '\u202f', '\u205f', '\u3000'))
                {
                    if (!string.IsNullOrWhiteSpace(translation_search_token))
                    {
                        searchTokens.Add(translation_search_token.ToLower());
                    }
                }
            }
        }

        /// <summary>
        /// Draws a search field
        /// </summary>
        /// <param name="searchField">Search field</param>
        /// <param name="searchQuery">Search query</param>
        /// <param name="searchTokens">Search tokens</param>
        /// <param name="options">GUI layout options</param>
        public static void DrawSearchField(SearchField searchField, ref string searchQuery, List<string> searchTokens, params GUILayoutOption[] options)
        {
            if (searchField == null)
            {
                throw new ArgumentNullException(nameof(searchField));
            }
            if (searchQuery == null)
            {
                throw new ArgumentNullException(nameof(searchQuery));
            }
            if (searchTokens == null)
            {
                throw new ArgumentNullException(nameof(searchTokens));
            }
            searchQuery = searchField.OnGUI(searchQuery, options).Trim();
            TokenizeSearch(searchQuery, searchTokens);
        }
    }
}
