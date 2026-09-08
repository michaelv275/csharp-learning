namespace MadlibBot.Utilities
{
    public static class StringUtility
    {
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            // Find the index of the first occurrence
            int pos = text.IndexOf(search);

            // If the search string isn't found, return the original text
            if (pos < 0)
            {
                return text;
            }

            // Remove the old substring and insert the new one
            return text.Remove(pos, search.Length).Insert(pos, replace);
        }
    }
}
