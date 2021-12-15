namespace hexomorse.Tests
{
    using Xunit;
    using System;

    public class MorseEncoder_Tests
    {
        [Fact]
        public void encode()
        {
            var morseEncoder = Encoder.GetEncoder("morse");
            var result = morseEncoder.Encode("hello world");
            Assert.Equal(".... . .-.. .-.. ---    .-- --- .-. .-.. -..", result);
        }

        [Fact]
        public void encode_digit()
        {
            var morseEncoder = Encoder.GetEncoder("morse");
            var result = morseEncoder.Encode("5 9 hello world");
            Assert.Equal(".....    ----.    .... . .-.. .-.. ---    .-- --- .-. .-.. -..", result);
        }

        [Fact]
        public void encode_non_ascii_char()
        {
            var morseEncoder = Encoder.GetEncoder("morse");
            Assert.Throws<Exception>(() => morseEncoder.Encode("hello w$rld"));
        }

        [Fact]
        public void decode()
        {
            var morseEncoder = Encoder.GetEncoder("morse");
            var result = morseEncoder.Decode(".... . .-.. .-.. ---    .-- --- .-. .-.. -..");
            Assert.Equal("HELLO WORLD", result);
        }

        [Fact]
        public void decode_digit()
        {
            var morseEncoder = Encoder.GetEncoder("morse");
            var result = morseEncoder.Decode(".....    ----.    .... . .-.. .-.. ---    .-- --- .-. .-.. -..");
            Assert.Equal("5 9 HELLO WORLD", result);
        }

        [Fact]
        public void decode_non_morse_char()
        {
            var morseEncoder = Encoder.GetEncoder("morse");
            Assert.Throws<Exception>(() => morseEncoder.Decode(".... x ."));
        }
    }
}
