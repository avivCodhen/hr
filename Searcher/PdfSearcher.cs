﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace HR.Searcher
{
    public class PdfSearcher : ISearcher
    {
        public bool SearchText(string fileName, string[] texts)
        {
            var bytes = File.ReadAllBytes(fileName);
            var text = ConvertToText(bytes);

            texts = texts.ReverseSearchTextIfRtl();
            return texts.Any(s => text.Contains(s, StringComparison.InvariantCultureIgnoreCase)) || text.SplitFormat().TextDistance(texts, 1);
        }
        

        private static string ConvertToText(byte[] bytes)
        {
            var sb = new StringBuilder();


            var reader = new PdfReader(bytes);
            var numberOfPages = reader.NumberOfPages;

            for (var currentPageIndex = 1; currentPageIndex <= numberOfPages; currentPageIndex++)
            {
                var text = PdfTextExtractor.GetTextFromPage(reader, currentPageIndex,
                    new LocationTextExtractionStrategy());
                sb.Append(text);
            }

            return sb.ToString();
        }
    }
}