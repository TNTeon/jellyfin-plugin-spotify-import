using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viperinius.Plugin.SpotifyImport.Matchers
{
    /// <summary>
    /// All configurable matchers to check matching tracks.
    /// </summary>
    public enum ItemMatchLevel
    {
        /// <summary>
        /// Simple check for equality.
        /// </summary>
        Default,

        /// <summary>
        /// Case insensitive equality check.
        /// </summary>
        IgnoreCase,

        /// <summary>
        /// Case insensitive equality check minus any punctuation.
        /// </summary>
        IgnorePunctuationAndCase,

        /// <summary>
        /// Case insensitive equality check minus any punctuation and anything in parentheses.
        /// </summary>
        IgnoreParensPunctuationAndCase,

        /// <summary>
        /// Case insensitive equality check minus any punctuation and anything in parentheses.
        /// Additionally, use album name found in title (if any found and configured to check against).
        /// </summary>
        IgnoreParensPunctuationAndCaseUseAlbumFromTrack,

        /// <summary>
        /// Check if difference between strings within acceptable range.
        /// </summary>
        Fuzzy,
    }
}
