namespace ITVillage
{
    using System;
    using System.Text;

    public static class Methods
    {
        public static readonly Func<string, int> ParseFunc = toParse => int.Parse(toParse);

        public static readonly Func<string, string> GetOnlyLettersAndDigits = toTrim =>
        {
            StringBuilder result = new StringBuilder();

            foreach (char symbol in toTrim)
            {
                if (char.IsDigit(symbol) || char.IsLetter(symbol))
                {
                    result.Append(symbol);
                }
            }

            return result.ToString();
        };

        public static int[] DecrementAllElements(this int[] array)
        {
            for (int currElement = 0; currElement < array.Length; currElement++)
            {
                array[currElement]--;
            }

            return array;
        }
    }
}
