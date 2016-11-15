//---------------------------------------------------
//Hannah Hopkins, Josh Kent, and Todd Gibson
//Project 5
//Battleship
//28 April 2016
//This program implements the game battleship.
//Key:
//0=not been hit, no ship there
//1=hit, no ship there
//2=not hit, destroyer ship there
//3=not hit, submarine ship there
//4=not hit, battleship there
//5=not hit, carrier ship there
//10=hit, ship there
//---------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
