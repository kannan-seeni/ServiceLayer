using System.Text;

namespace TNCSC.Hulling.ServiceLayer.Export
{
    public static class Helper
    {
        #region XmlEncode
        /// <summary>
        /// XmlEncode
        /// </summary>
        /// <param name="text"></param>
        /// <param name="isAttribute"></param>
        /// <returns></returns>
        public static string XmlEncode(string text, bool isAttribute = false)
        {

            if (!string.IsNullOrEmpty(text))
            {
                var sb = new StringBuilder(text.Length);

                foreach (var chr in text)
                {
                    if (chr == '<')
                        sb.Append("&lt;");
                    else if (chr == '>')
                        sb.Append("&gt;");
                    else if (chr == '&')
                        sb.Append("&amp;");

                    // special handling for quotes
                    else if (isAttribute && chr == '\"')
                        sb.Append("&quot;");
                    else if (isAttribute && chr == '\'')
                        sb.Append("&apos;");

                    // Legal sub-chr32 characters
                    else if (chr == '\n')
                        sb.Append(isAttribute ? "&#xA;" : "\n");
                    else if (chr == '\r')
                        sb.Append(isAttribute ? "&#xD;" : "\r");
                    else if (chr == '\t')
                        sb.Append(isAttribute ? "&#x9;" : "\t");

                    else
                    {
                        //if (chr < 32)
                        //    throw new InvalidOperationException("Invalid character in Xml String. Chr " +
                        //                                        Convert.ToInt16(chr) + " is illegal.");
                        sb.Append(chr);
                    }
                }

                return sb.ToString();
            }
            return string.Empty;
        }
        #endregion
    }
}
