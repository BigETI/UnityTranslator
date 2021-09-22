using System;
using System.Collections.Generic;

/// <summary>
/// Unity translator editor namespace
/// </summary>
namespace UnityTranslatorEditor
{
    public static class StringUtilities
    {
        public static string GetCommonPrefix(IEnumerable<string> strings)
        {
            if (strings == null)
            {
                throw new ArgumentNullException(nameof(strings));
            }
            string ret = null;
            foreach (string string_value in strings)
            {
                if (string.IsNullOrWhiteSpace(string_value))
                {
                    ret = string.Empty;
                    break;
                }
                ret ??= string_value;
                while (!string_value.Contains(ret) && (ret.Length > 0))
                {
                    ret = ret.Substring(0, ret.Length - 1);
                }
                if (ret.Length <= 0)
                {
                    break;
                }
            }
            return ret ?? string.Empty;
        }
    }
}
