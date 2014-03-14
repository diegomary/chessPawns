using Chess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Classes
{
    public  class ChessBoard
    {
        private List<Position> _allchesspos;
        private string _report;
        public string Report
        {
            get
            {
                return _report;
            }
            set
            {
                _report = value;
            }
        }
        public  List<Position> AllChesPositions
        {
            get
            {
                return _allchesspos;
            }
            set
            {
                _allchesspos = value;
            }
        }
        public ChessBoard()
        {
            StringBuilder _strreport = new StringBuilder();
            _allchesspos = new List<Position>();
            for (int i = 1; i < 9; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Position temp = new Position() { row = i, column = ((char)(65 + j)).ToString(), isFilled = ChessSet.EMPTY };
                    _allchesspos.Add(temp);
                }
            }
            Position[] posarray = _allchesspos.ToArray();
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
            _strreport.AppendLine("White pawns Spread on the Chessboard.");
            foreach (var white in whitePawns)
            {
                _strreport.AppendLine("A white Pawn is at " + white.column + white.row);
            }
            _strreport.AppendLine("");
            _strreport.AppendLine("Black pawns Spread on the Chessboard.");
            // Let's determine the Collection of Black Pawn
            var blackPawns = posarray.Where(m => m.isFilled.Equals(ChessSet.BLACK));
            foreach (var black in blackPawns)
            {
                _strreport.AppendLine("A black Pawn is at " + black.column + black.row);
            }
            _strreport.AppendLine("");
            _strreport.AppendLine("");
            _strreport.AppendLine("The following Report Gives information on White pawns that are blocked by other pawn and can't proceed ahead.");
            _strreport.AppendLine("");
            foreach (var pos in posarray)
            {
                if (pos.isFilled.Equals(ChessSet.WHITE))
                {
                    int nextRow = pos.row + 1;
                    if (nextRow == 9) continue; // We can't evaluate eyond chesstable  boundary
                    Position ahead = posarray.Where(m => m.row.Equals(nextRow) && m.column.Equals(pos.column)).FirstOrDefault();
                    if (ahead.isFilled.Equals(ChessSet.BLACK) || ahead.isFilled.Equals(ChessSet.WHITE))
                    _strreport.AppendLine("The White Pawn at Position " + pos.column + pos.row + " is not allowed because of the presence of a " + ahead.isFilled.ToString() + " Pawn in front at " + ahead.column + ahead.row);
                }
            }
            _strreport.AppendLine("");
            _strreport.AppendLine("The following Report Gives information on white pawn that can go diagonally and eat a black pawn");
            _strreport.AppendLine("");
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
                            _strreport.AppendLine("The White Pawn at Position " + pos.column + pos.row + " can eat the black pawn at position " + lateralrightAhead.column + lateralrightAhead.row);
                        }
                    }

                    if (!String.IsNullOrEmpty(leftcolahead))
                    {
                        Position lateralleftAhead = posarray.Where(m => m.row.Equals(nextRow) && m.column.Equals(leftcolahead)).FirstOrDefault();
                        if (lateralleftAhead.isFilled.Equals(ChessSet.BLACK))
                        {
                            _strreport.AppendLine("The White Pawn at Position " + pos.column + pos.row + " can eat the black pawn at position " + lateralleftAhead.column + lateralleftAhead.row);
                        }
                    }
                }
            }
            _report = _strreport.ToString();
        }
    }
}
