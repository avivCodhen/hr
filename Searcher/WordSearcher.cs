using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SautinSoft.Document;

namespace HR.Searcher
{
    public class WordSearcher : ISearcher
    {
        public bool SearchText(string fileName, string[] texts)
        {
            DocumentCore dc = DocumentCore.Load(fileName);

            var str = dc.Content.ToString().SplitFormat();
            var count = 0;
            foreach (var text in texts)
            {
                Regex regex = new Regex($"\\b({text})\\b", RegexOptions.IgnoreCase);
                count += dc.Content.Find(regex).Count();
            }

            return count >= texts.Length || str.TextDistance(texts, 1);
        }

       
    }
}