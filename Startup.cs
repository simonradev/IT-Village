namespace ITVillage
{
    using System;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            string[] rowsOfFieldsOfTheBoard = SplitStringAndApplyFunc(Console.ReadLine(),
                                                                      Constants.Pipe,
                                                                      Methods.GetOnlyLettersAndDigits);
            int[] initialPositionOfPlayer = SplitStringAndApplyFunc(Console.ReadLine(),
                                                                    Constants.WhiteSpace,
                                                                    Methods.ParseFunc)
                                                                    .DecrementAllElements()
                                                                    .ToArray();
            int[] moves = SplitStringAndApplyFunc(Console.ReadLine(), 
                                                  Constants.WhiteSpace,
                                                  Methods.ParseFunc);

            Board board = new Board();
            board.FillTheBoard(rowsOfFieldsOfTheBoard);

            Player player = new Player(initialPositionOfPlayer[0], initialPositionOfPlayer[1], moves);

            string result = default(string);
            while (result == default(string))
            {
                player.TakeTheMoneyFromTheOwnedInns();

                char fieldType = player.MakeAMove(board);

                Board.FieldConsequences[fieldType](player);

                player.IndexOfCurrentMove++;
                result = player.GetOutcomeForPlayerOrDefault();
            }

            Console.WriteLine(result);
        }
        
        private static TType[] SplitStringAndApplyFunc<TType>(string toSplit, char toSplitBy, Func<string, TType> func)
        {
            return toSplit.Split(new[] { toSplitBy }, StringSplitOptions.RemoveEmptyEntries).Select(func).ToArray();
        }
    }
}
