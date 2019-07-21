﻿using System;
using UnityEngine;
using UnityTranslator.Data;

/// <summary>
/// Unity translator objects namespace
/// </summary>
namespace UnityTranslator.Objects
{
    /// <summary>
    /// Translation object script class
    /// </summary>
    [CreateAssetMenu(fileName = "Translation", menuName = "Translator/Translation")]
    public class TranslationObjectScript : ScriptableObject, IComparable<TranslationObjectScript>
    {
        /// <summary>
        /// Translation
        /// </summary>
        [SerializeField]
        private TranslationData translation;

        /// <summary>
        /// Comment
        /// </summary>
        [TextArea]
        [SerializeField]
        private string comment;

        /// <summary>
        /// Translation
        /// </summary>
        public TranslationData Translation
        {
            get
            {
                return translation;
            }
        }

        /// <summary>
        /// Comment
        /// </summary>
        public string Comment
        {
            get
            {
                if (comment == null)
                {
                    comment = string.Empty;
                }
                return comment;
            }
        }

        /// <summary>
        /// Compare to
        /// </summary>
        /// <param name="other">Other</param>
        /// <returns>Result</returns>
        public int CompareTo(TranslationObjectScript other)
        {
            int ret = 1;
            if (other != null)
            {
                ret = translation.CompareTo(other.translation);
            }
            return ret;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString()
        {
            return translation.ToString();
        }
    }
}
