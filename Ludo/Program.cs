using Ludo.UI.Class;
using Ludo.UI.Class.Controls;
using Ludo.UI.Enum;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameBoardForm());
        }

        //static void Main(string[] args)
        //{
        //    Player player1 = new Player(Color.Red);
        //    Player player2 = new Player(Color.Yellow);

        //    Game Game = new Game();
        //    //Game.Start();
        //    Game.AddPlayer(player1);
        //    Game.AddPlayer(player2);

        //    //Quadrant Red = Game.GameBoard.Quadrants[0];
        //    //GameBoardPosition gameBoardPosition = Game.GameBoard.GetNextGhor(new GameBoardPosition(Red, Red.GhorPath[12]), player1);
        //    //GameBoardPosition gameBoardPosition = Game.GameBoard.GetNextGhor(
        //    //    new GameBoardPosition(Red, Red.QuadrantHome.GhorPositions[0]));

        //    bool flag = true;
        //    //player1.Pieces[0].GameBoardPosition.Quadrant = Game.GameBoard.Quadrants[0];
        //    //player1.Pieces[0].GameBoardPosition.Ghor = Game.GameBoard.Quadrants[0].GhorPath[15];

        //    while (flag)
        //    {
        //        Console.WriteLine("Type e to Exit: ");
        //        string input = Console.ReadLine();
        //        if (input == "e")
        //        {
        //            flag = false;
        //            continue;
        //        }

        //        switch (input)
        //        {
        //            //case "1":
        //            //    Console.WriteLine(player1.Pieces[0].GameBoardPosition.Quadrant.Color);
        //            //    Console.WriteLine(player1.Pieces[0].GameBoardPosition.Ghor.Position);
        //            //    break;
        //            //case "2":
        //            //    Console.WriteLine(Game.GameBoard.Quadrants[0].Color);
        //            //    break;
        //            //case "3":
        //            //    string count = Console.ReadLine();
        //            //    player1.MovePiece(player1.Pieces[0], Convert.ToInt32(count));
        //            //    break;
        //            case "4":
        //                Game.CurrentPlayer = player1;
        //                Game.Start();
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}
    }
}
