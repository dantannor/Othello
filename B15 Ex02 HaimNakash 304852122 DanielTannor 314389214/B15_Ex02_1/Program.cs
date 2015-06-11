using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using B15_Ex02_1;
using B15_Ex02_1.Control;

namespace GUI
{
    public class program
    {
        public static void Main()
        {
            //Controller controller = new Controller();

            
            FormGame form = new FormGame();

            form.ShowDialog();
             
            /*
            Controller controller = new Controller();
            Console.WriteLine("Press enter to exit");
             */
        }
    }
}
