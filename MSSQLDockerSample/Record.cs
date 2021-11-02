namespace MSSQLDockerSample
{
    internal class Record
    {
        public Record(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public int Id { get; }

        public string Text { get; }

        public override string ToString() => $"{Id}    {Text}";
    }
}
