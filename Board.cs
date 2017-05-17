namespace ITVillage
{
    using System;
    using System.Collections.Generic;

    public class Board
    {
        public static readonly Position[] AllValidFields = new Position[]
        {
            new Position(0, 0),
            new Position(0, 1),
            new Position(0, 2),
            new Position(0, 3),
            new Position(1, 3),
            new Position(2, 3),
            new Position(3, 3),
            new Position(3, 2),
            new Position(3, 1),
            new Position(3, 0),
            new Position(2, 0),
            new Position(1, 0),
        };

        public static readonly Dictionary<char, Action<Player>> FieldConsequences = new Dictionary<char, Action<Player>>
        {
            [Constants.WifiPub] = player =>
            {
                player.Coins -= Constants.WifiPubCoctailCost;
            },
            [Constants.WifiInn] = player =>
            {
                if (player.Coins >= Constants.InnCostToBuy)
                {
                    player.Coins -= Constants.InnCostToBuy;

                    player.InnsOwned++;
                }
                else
                {
                    player.Coins -= Constants.InnCostToStay;
                }
            },
            [Constants.FreelanceProject] = player =>
            {
                player.Coins += Constants.FreelanceProjectPayment;
            },
            [Constants.Storm] = player =>
            {
                player.IndexOfCurrentMove += Constants.TurnsSkippedIntoStorm;
            },
            [Constants.SuperVlado] = player =>
            {
                player.Coins *= Constants.SuperVladoMultiplyPower;
            },
            [Constants.Nakov] = player =>
            {
                player.SteppedOnNakov = true;
            }
        };

        private char[,] board;
        public static int NumberOfInns = 0;

        public Board()
        {
            this.board = new char[Constants.BoardDimensions, Constants.BoardDimensions];
        }

        public char GetField(Position toGetFieldFrom)
        {
            return this.board[toGetFieldFrom.Row, toGetFieldFrom.Col];
        }

        public void FillTheBoard(string[] rowsOfFieldsOfTheBoard)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    char currentField = rowsOfFieldsOfTheBoard[row][col];
                    this.board[row, col] = currentField;

                    if (currentField == Constants.WifiInn)
                    {
                        NumberOfInns++;
                    }
                }
            }
        }
    }
}
