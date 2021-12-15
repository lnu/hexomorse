namespace hexomorse.Tests
{
    using Xunit;

    public class HexEncoder_Tests
    {
        [Fact]
        public void encode()
        {
            var hexencoder = Encoder.GetEncoder("hex");
            var result = hexencoder.Encode("hello world");
            Assert.Equal("68.65.6c.6c.6f.20.77.6f.72.6c.64", result);
        }

        [Fact]
        public void decode()
        {
            var hexencoder = Encoder.GetEncoder("hex");
            var result = hexencoder.Decode("68.65.6c.6c.6f.20.77.6f.72.6c.64");
            Assert.Equal("hello world", result);
        }
    }
}