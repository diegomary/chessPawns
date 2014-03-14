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
using Chess.Classes;
using Chess.Enums;

namespace Chess
{ 
    class Program
    {
        static void Main(string[] args)
        {
            ChessBoard csb = new ChessBoard();
            MessageBox.Show(csb.Report);
        }
    }
}
