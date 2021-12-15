namespace hexomorse
{
    public static class Encoder
    {
        public static IEncoder GetEncoder(string encoder)
        {
            switch (encoder)
            {
                case "hex":
                    return new HexEncoder();
                case "morse":
                    return new MorseEncoder();
            }
            throw new Exception($"Encoder {encoder} not found, only hex or morse supported");
        }
    }
}