using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Shared.Helpers
{
    /// <summary>
    /// Extensions for dealing with the color codes in minecraft text.
    /// </summary>
    public static class MinecraftColorCodeExtensions
    {
        /// <summary>
        /// Strips the color code from text. Including hex codes.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string StripColorCodes(this string text)
        {
            return Regex.Replace(Regex.Replace(text, @"(\u00A7\w)", string.Empty), @"(\u00A7#\w{6})", string.Empty); //First internal regex removes simple color codes, like §6 or $c, the outer regex removes hex color codes.
        }
    }
}
