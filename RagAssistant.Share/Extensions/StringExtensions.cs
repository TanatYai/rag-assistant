using System.Text;
using System.Text.RegularExpressions;

namespace RagAssistant.Share.Extensions
{
    public static class StringExtensions
    {
        public static string CleanText(this string text)
        {
            text = text.Replace("\r\n", "\n").Replace("\r", "\n"); // normalize line break
            text = Regex.Replace(text, @"\t+", " "); // replace one or multiple tabs with one space
            text = Regex.Replace(text, @"[ ]{2,}", " "); // replace multiple spaces with one space
            text = Regex.Replace(text, @"^\s+|\s+$", "", RegexOptions.Multiline); // trim each line
            text = text.Trim(); // trim string

            return text;
        }

        /*
         * Small paragraphs are grouped into the same chunk.
         * A large paragraph that would exceed the chunk size starts a new chunk.
         * Tthis prevents mixing content and preserves contextual clarity
         */
        public static List<string> ChunkByParagraph(this string text, int maxChars = 1000, int overlap = 50)
        {
            string[] paragraphs = text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries); // remove empty entry
            List<string> chunks = [];
            StringBuilder buffer = new();
            int step = maxChars - overlap;

            foreach (string paragraph in paragraphs)
            {
                // single paragraph >= maxChars, then split into multiple chunks
                if (paragraph.Length > maxChars)
                {
                    // have a content in buffer, then flush into chunk
                    if (buffer.Length > 0)
                    {
                        chunks.Add(buffer.ToString().Trim());
                        buffer.Clear();
                    }

                    // split chunk with overlap
                    for (int i = 0; i < paragraph.Length; i += step)
                    {
                        string chunk = paragraph.Substring(i, Math.Min(maxChars, paragraph.Length - i));
                        chunks.Add(chunk.Trim());
                    }

                    continue;
                }

                // adding a paragraph > maxChars, then flush into chunk
                if (buffer.Length + paragraph.Length > maxChars)
                {
                    chunks.Add(buffer.ToString().Trim());
                    buffer.Clear();
                }

                // assign paragraph into buffer
                buffer.AppendLine(paragraph);
            }

            // flush last buffer into chunk
            if (buffer.Length > 0)
            {
                chunks.Add(buffer.ToString().Trim());
            }

            return chunks;
        }
    }
}
