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
    /// A class that describes a tokenized search field
    /// </summary>
    public class TokenizedSearchField : ITokenizedSearchField
    {
        /// <summary>
        /// Whitespaces
        /// </summary>
        private static readonly char[] whitespaces = new char[] { ' ', '\t', '\u00a0', '\u1680', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005', '\u2006', '\u2007', '\u2008', '\u2009', '\u200a', '\u202f', '\u205f', '\u3000' };

        /// <summary>
        /// Search tokens
        /// </summary>
        private readonly List<string> searchTokens = new List<string>();

        /// <summary>
        /// Search query
        /// </summary>
        private string searchQuery = string.Empty;

        /// <summary>
        /// Search field
        /// </summary>
        public SearchField SearchField { get; } = new SearchField();

        /// <summary>
        /// Search query
        /// </summary>
        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                string search_query = value ?? throw new ArgumentNullException(nameof(value));
                if (searchQuery != search_query)
                {
                    searchQuery = search_query;
                    searchTokens.Clear();
                    if (searchQuery.StartsWith("\"") && searchQuery.EndsWith("\"") && (searchQuery.Length > 1))
                    {
                        searchTokens.Add(searchQuery.Substring(1, searchQuery.Length - 2).ToLower());
                    }
                    else
                    {
                        foreach (string search_token in searchQuery.Split(whitespaces))
                        {
                            if (!string.IsNullOrWhiteSpace(search_token))
                            {
                                searchTokens.Add(search_token.ToLower());
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Search tokens
        /// </summary>
        public IReadOnlyList<string> SearchTokens => searchTokens;

        /// <summary>
        /// Constructs a new tokenized search field
        /// </summary>
        public TokenizedSearchField()
        {
            // ...
        }

        /// <summary>
        /// Constructs a new tokenized search field
        /// </summary>
        /// <param name="searchQuery"></param>
        public TokenizedSearchField(string searchQuery) => SearchQuery = searchQuery;

        /// <summary>
        /// Is the specified input contained in search
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>"true" if the specified input is contained in search, otherwise "false"</returns>
        public bool IsContainedInSearch(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
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
        /// Draws tokenized search field
        /// </summary>
        /// <param name="options">GUI layout options</param>
        public void Draw(params GUILayoutOption[] options) => SearchQuery = SearchField.OnGUI(searchQuery, options).Trim();
    }
}
