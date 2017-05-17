namespace ITVillage
{
    public class Player
    {
        private Position currentPosition;
        private int indexOfCurrentPosition;
        private int coins;

        private int innsOwned;

        private int[] moves;
        private int indexOfCurrentMove;

        private bool steppedOnNakov;

        public Player(int row, int col, int[] moves)
        {
            this.currentPosition = new Position(row, col);
            this.indexOfCurrentPosition = GetIndexOfCurrentPosition(currentPosition);
            this.coins = 50;

            this.innsOwned = 0;

            this.moves = moves;
            this.indexOfCurrentMove = 0;

            this.steppedOnNakov = false;
        }
        
        public int Coins
        {
            get { return this.coins; }
            set { this.coins = value; }
        }

        public int IndexOfCurrentMove
        {
            get { return this.indexOfCurrentMove; }
            set { this.indexOfCurrentMove = value; }
        }

        public int IndexOfCurrentPosition
        {
            get { return this.indexOfCurrentPosition; }
            set { this.indexOfCurrentPosition = value; }
        }

        public bool SteppedOnNakov
        {
            get { return this.steppedOnNakov; }
            set { this.steppedOnNakov = value; }
        }

        public int InnsOwned
        {
            get { return this.innsOwned; }
            set { this.innsOwned = value; }
        }

        public char MakeAMove(Board board)
        {
            int indexOfCurrentMove = this.indexOfCurrentMove;
            int movesToMake = this.moves[indexOfCurrentMove];

            this.indexOfCurrentPosition = (this.indexOfCurrentPosition + movesToMake) % Board.AllValidFields.Length;

            Position nextPlayersPosition = Board.AllValidFields[indexOfCurrentPosition];

            this.currentPosition.Row = nextPlayersPosition.Row;
            this.currentPosition.Col = nextPlayersPosition.Col;

            return board.GetField(currentPosition);
        }

        private int GetIndexOfCurrentPosition(Position toGetIndexFor)
        {
            int indexToReturn = 0;

            for (int currField = 0; currField < Board.AllValidFields.Length; currField++)
            {
                Position field = Board.AllValidFields[currField];

                if (field.Row == toGetIndexFor.Row && field.Col == toGetIndexFor.Col)
                {
                    break;
                }

                indexToReturn++;
            }

            return indexToReturn;
        }

        public string GetOutcomeForPlayerOrDefault()
        {
            string outcome = default(string);

            if (this.coins < 0)
            {
                outcome = "<p>You lost! You ran out of money!<p>";
            }
            else if (this.innsOwned == Board.NumberOfInns)
            {
                outcome = $"<p>You won! You own the village now! You have {this.coins} coins!<p>";
            }
            else if (this.indexOfCurrentMove >= this.moves.Length)
            {
                outcome = $"<p>You lost! No more moves! You have {this.coins} coins!<p>";
            }
            else if (this.steppedOnNakov)
            {
                outcome = "<p>You won! Nakov's force was with you!<p>";
            }

            return outcome;
        }

        public void TakeTheMoneyFromTheOwnedInns()
        {
            this.coins += this.innsOwned * Constants.MoneyForAnInn;
        }
    }
}
