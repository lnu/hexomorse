namespace hexomorse
{
    public class HexEncoder : IEncoder
    {
        public string Encode(string input)
        {
            return string.Join(".", input.Select(c => ((int)c).ToString("X2").ToLower()));
        }
        public string Decode(string input)
        {
            return string.Join("", input.Split(".").Select(p => Convert.ToChar(System.Convert.ToUInt32(p, 16))));
        }
    }
}
