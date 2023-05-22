using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public static class MenuDisplay
    {
        private static string menuOptionsDisplay = @"
        ##############################################
        #                                            #
        #      1. Start Game                         #
        #      2. View Scoreboard                    #
        #                                            #
        ##############################################
        ";

        private static string menuSnakeDisplay = @"
                   ---_ ......._-_--.
                  (|\ /      / /| \  \
                  /  /     .'  -=-'   `.
                 /  /    .'             )
               _/  /   .'        _.)   /
              / o   o        _.-' /  .'
              \          _.-'    / .'*|
               \______.-'//    .'.' \*|
                \|  \ | //   .'.' _ |*|
                 `   \|//  .'.'_ _ _|*|
                  .  .// .'.' | _ _ \*|
                  \`-|\_/ /    \ _ _ \*\
                   `/'\__/      \ _ _ \*\
                  /^|            \ _ _ \*
                 '  `             \ _ _ \    
                                   \_
            ";


        public static void DisplayMenu()
        {
            Console.WriteLine(menuOptionsDisplay);
            Console.Write(":");
        }


        public static void DisplayStartGameMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(menuSnakeDisplay);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t \tChoose a colour for the snake! \n\t \t1-Green    2-Red\n\t \t3-Yellow   4-Blue");
            Console.Write("\t \t:");
        }
    }
}
