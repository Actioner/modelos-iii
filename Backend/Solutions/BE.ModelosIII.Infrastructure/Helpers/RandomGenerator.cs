using System;
using System.Text;

namespace BE.ModelosIII.Infrastructure.Helpers
{
    public static class RandomGenerator
    {
        private static readonly Random Random = new Random((int)DateTime.Now.Ticks);

        public static string RandomString(int size)
        {
            var builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}
