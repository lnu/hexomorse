namespace hexomorse
{
    public interface IEncoder
    {
        string Encode(string input);
        string Decode(string output);
    }
}