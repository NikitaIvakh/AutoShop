using System.Text;

namespace AutoShop.Domain.Extensions
{
    public static class StringExtension
    {
        public static string Join(this List<string> words)
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < words.Count; i++)
                stringBuilder.Append($"{i + 1}: {words[i]} ");

            return stringBuilder.ToString();
        }
    }
}