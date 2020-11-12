using System;
using System.IO;

namespace HR.Searcher
{
    public class SearcherFactory
    {
        public static ISearcher GetSearcherByExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            if (extension.Contains("doc", StringComparison.InvariantCultureIgnoreCase)) return new WordSearcher();
            if (extension.Contains("odt", StringComparison.InvariantCultureIgnoreCase)) return new WordSearcher();
            if (extension.Contains("pdf", StringComparison.InvariantCultureIgnoreCase)) return new PdfSearcher();
            throw new Exception($"קובץ לא קריא: {fileName}");
        }
    }
}