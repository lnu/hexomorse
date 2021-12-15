namespace hexomorse
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("At least 3 arguments needed");
                return;
            }
            try
            {
                string? result = null;
                var encoder = Encoder.GetEncoder(args[1]);
                if (string.Compare(args[0], "encode", true) == 0)
                {
                    Console.WriteLine(args[2]);
                    result = encoder.Encode(args[2]);
                }
                if (string.Compare(args[0], "decode", true) == 0)
                {
                    Console.WriteLine(args[2]);
                    result = encoder.Decode(args[2]);
                }
                if (result == null)
                {
                    Console.WriteLine("only encode or decode supported");
                }
                //success case
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
