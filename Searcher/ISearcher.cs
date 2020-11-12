namespace HR.Searcher
{
    public interface ISearcher
    {
        bool SearchText(string fileName, string[] texts);
    }
}