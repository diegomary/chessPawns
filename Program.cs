//The problem:
//White and Black pawns are spread on the chessboard. The following study spreads pawns randomly and then iteratordetermine vertiacal
//and diagonal interceptions that are reported indexer abstract dialog box.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Chess
{
    public enum ChessSet
    {
        WHITE,
        BLACK,
        EMPTY
    }

  public  class Position
    {
        public int row { get; set; }
        public string column {get; set;}
        public ChessSet isFilled { get; set; }        
    }
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder wr = new StringBuilder();
            List<Position> allChessPos = new List<Position>();
            for(int i=1; i < 9; i++)
            {
                for(int j = 0;j <8; j++)
                {
                    Position temp = new Position() { row = i, column = ((char)(65 + j)).ToString(), isFilled = ChessSet.EMPTY };
                    allChessPos.Add(temp);
                }                
            }
            int u = allChessPos.Count();
            Position[] posarray = allChessPos.ToArray();
            Random rand = new Random();
            // Randomly spread WHITE Pawns
            while (true)
            {               
                for (int i = 0; i < 8; ++i)
                {
                    int temprand = rand.Next(0, 64);
                    if (posarray[temprand].isFilled.Equals(ChessSet.EMPTY)) posarray[temprand].isFilled = ChessSet.WHITE;
                    if (posarray.Where(m => m.isFilled.Equals(ChessSet.WHITE)).Count() == 8) break;
                }
                if (posarray.Where(m => m.isFilled.Equals(ChessSet.WHITE)).Count() == 8) break;
            }
            // Randomly spread BLACK Pawns
            while (true)
            {                
                for (int i = 0; i < 8; ++i)
                {
                    int temprand = rand.Next(0, 64);
                    if ((posarray[temprand].isFilled.Equals(ChessSet.EMPTY))) posarray[temprand].isFilled = ChessSet.BLACK;
                    if (posarray.Where(m => m.isFilled.Equals(ChessSet.BLACK)).Count() == 8) break;

                }
                if (posarray.Where(m => m.isFilled.Equals(ChessSet.BLACK)).Count() == 8) break;
            }

            // Let's determine the Collection of White Pawn
            var whitePawns = posarray.Where(m => m.isFilled.Equals(ChessSet.WHITE));
            wr.AppendLine("White pawns Spread on the Chessboard.");         
            foreach(var white in whitePawns)
            {
                wr.AppendLine("A white Pawn is at " + white.column + white.row);
            }
            wr.AppendLine("");
            wr.AppendLine("Black pawns Spread on the Chessboard.");
            // Let's determine the Collection of Black Pawn
            var blackPawns = posarray.Where(m => m.isFilled.Equals(ChessSet.BLACK));
            foreach (var black in blackPawns)
            {
                wr.AppendLine("A black Pawn is at " + black.column + black.row);
            }
            wr.AppendLine("");           
           // TDD Assertions  using Nunit for example
           int WHITE= posarray.Where(m => m.isFilled.Equals(ChessSet.WHITE)).Count(); // Must be 8
           int BLACK = posarray.Where(m => m.isFilled.Equals(ChessSet.BLACK)).Count();// Must be 8
           int EMPTY = posarray.Where(m => m.isFilled.Equals(ChessSet.EMPTY)).Count();// Must be 48         
           // Let's see white possibility to proceed in vertical
           wr.AppendLine("");
           wr.AppendLine("The following Report Gives information on White pawns that are blocked by other pawn and can't proceed ahead.");
           wr.AppendLine("");
           foreach (var pos in posarray)
            {
                if(pos.isFilled.Equals(ChessSet.WHITE))
                {
                    int nextRow = pos.row + 1;
                    if (nextRow == 9) continue; // We can't evaluate eyond chesstable  boundary
                    Position ahead = posarray.Where(m => m.row.Equals(nextRow) && m.column.Equals(pos.column)).FirstOrDefault();
                    if(ahead.isFilled.Equals(ChessSet.BLACK) || ahead.isFilled.Equals(ChessSet.WHITE))
                    wr.AppendLine("The White Pawn at Position " + pos.column + pos.row + " is not allowed because of the presence of a " + ahead.isFilled.ToString() + " Pawn in front at " + ahead.column + ahead.row );
                }
            }
            wr.AppendLine("");
            wr.AppendLine("The following Report Gives information on white pawn that can go diagonally and eat a black pawn");
            wr.AppendLine("");
            foreach (var pos in posarray)
            {
                if (pos.isFilled.Equals(ChessSet.WHITE))
                {
                    int nextRow = pos.row + 1;
                    if (nextRow == 9) continue; // We can't evaluate beyond chesstable boundary
                    string rightcolahead = (((char)((int)pos.column.ToArray()[0] + 1)).ToString()).Equals("I") ? string.Empty : ((char)((int)pos.column.ToArray()[0] + 1)).ToString();
                    string leftcolahead = ((char)((int)pos.column.ToArray()[0] - 1)).ToString().Equals("@") ? string.Empty : ((char)((int)pos.column.ToArray()[0] - 1)).ToString();
                    if (!String.IsNullOrEmpty(rightcolahead))
                    {
                        Position lateralrightAhead = posarray.Where(m => m.row.Equals(nextRow) && m.column.Equals(rightcolahead)).FirstOrDefault();
                        if (lateralrightAhead.isFilled.Equals(ChessSet.BLACK))
                        {
                            wr.AppendLine("The White Pawn at Position " + pos.column + pos.row + " can eat the black pawn at position " + lateralrightAhead.column + lateralrightAhead.row);
                        }
                    }

                    if (!String.IsNullOrEmpty(leftcolahead))
                    {
                        Position lateralleftAhead = posarray.Where(m => m.row.Equals(nextRow) && m.column.Equals(leftcolahead)).FirstOrDefault();
                        if (lateralleftAhead.isFilled.Equals(ChessSet.BLACK))
                        {
                            wr.AppendLine("The White Pawn at Position " + pos.column + pos.row + " can eat the black pawn at position " + lateralleftAhead.column + lateralleftAhead.row);
                        }
                    }
                }
            }
            MessageBox.Show(wr.ToString());
        }
    }
}
