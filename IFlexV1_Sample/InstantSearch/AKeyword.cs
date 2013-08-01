namespace OpenOptions.dnaFusion.Flex.V1.InstantSearchKeywords
{
    public abstract class AKeyword
    {
        public override string ToString()
        {
            return Keyword;
        }

        public abstract string Keyword
        {
            get;
        }
    }
}
