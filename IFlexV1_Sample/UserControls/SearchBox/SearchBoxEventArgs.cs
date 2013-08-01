namespace IFlexV1_Sample
{
    public class SearchBoxEventArgs
    {
        public SearchBoxEventArgs(string AText) { Text = AText; }
        public string Text { get; private set; } // readonly
    }
}
