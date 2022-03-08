using System;

namespace FirstProject
{
    class Program
    {
        // Prints out game plan into the console
        static void printGamePlan(char[,] GamePlan)
        {
            Console.WriteLine();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(GamePlan[i, j]);
                }
                Console.WriteLine();
            }
        }

        // Changes the marker which is supposed to be placed
        static char ChangePlayers(char playerMarker)
        {
            char newPlayerMarker;
            if (playerMarker == 'X')
            {
                newPlayerMarker = 'O';
            }
            else
            {
                newPlayerMarker = 'X';
            }
            return newPlayerMarker;
        }

        // Winning condition function
        static bool WinningCondition(char[,] GamePlan)
        {
            string WinVarVerti = "", WinVarHori = "";
            string DiagLR = "", DiagRL = "";

            DiagLR = DiagLR + GamePlan[1, 1] + GamePlan[2, 2] + GamePlan[3, 3];
            DiagRL = DiagRL + GamePlan[1, 3] + GamePlan[2, 2] + GamePlan[3, 1];

            // Checking for diagonal win condition
            if (DiagLR == "XXX" || DiagRL == "XXX")
            {
                Console.WriteLine("Player X wins!");
                return true;
            }
            else if (DiagLR == "OOO" || DiagRL == "OOO")
            {
                Console.WriteLine("Player O wins!");
                return true;
            }

            // Checking for horizontal and vertical  win condition
            for (int i = 1; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    WinVarVerti += GamePlan[i, j];
                    WinVarHori += GamePlan[j, i];
                }
                if (WinVarVerti == "XXX" || WinVarHori == "XXX")
                {
                    
                    Console.WriteLine("Player X wins!");
                    return true;
                }
                else if (WinVarVerti == "OOO" || WinVarHori == "OOO")
                {
                    Console.WriteLine("Player O wins!");
                    return true;
                }
                WinVarVerti = "";
                WinVarHori = "";
            }

            return false;
        }

        // Checks input to see if it's in correct format
        static bool InputConditions(string coordinates, char[,] GamePlan)
        {
            int x, y;

            if (coordinates.Length != 3)
            {
                Console.WriteLine("You typed in wrong format, remember it must be \"x y\"!");
                return false;
            }

            try
            {
                string[] splitedCatch = coordinates.Split(null);
                x = Convert.ToInt32(splitedCatch[0]);
                y = Convert.ToInt32(splitedCatch[1]);
            }
            catch
            {
                Console.WriteLine("You typed in wrong format, remember it must be \"x y\"!");
                return false;
            }

            string[] splited = coordinates.Split(null);
            x = Convert.ToInt32(splited[0]);
            y = Convert.ToInt32(splited[1]);

            if (x > 3 || x < 1 || y > 3 || y < 1)
            {
                Console.WriteLine("Try to stay inside game field! You need to use numbers 1-3!");
                return false;
            }

            if (GamePlan[y, x] != '-')
            {
                Console.WriteLine("Someone already has a mark here!");
                return false;
            }

            return true;
        }

        static void Main(string[] args)
        {
            // Create play area which should be 3x3
            char[,] GamePlan = new char[,] { { ' ', '1', '2', '3' }, 
                                             { '1', '-', '-', '-' }, 
                                             { '2', '-', '-', '-' }, 
                                             { '3', '-', '-', '-' } };
            ConsoleKeyInfo play;
            string coordinates;
            int x, y;
            char playerMarker = 'X';
            bool isWon = false;
            int timer = 0;

            Console.Write("To begin press y... ");
            play = Console.ReadKey();
            Console.Clear();
            if (play.KeyChar == 'y')
            {
                Console.WriteLine("Welcome to the game of Tic Tac Toe.");
                Console.WriteLine("Here you can see the game field marked with coordinates.");
                printGamePlan(GamePlan);
                Console.WriteLine();
                Console.WriteLine("To play you have to type in coordinates in this format \"x y\"");
            }

            // Game Loop
            while (play.KeyChar == 'y' && !isWon && timer < 9)
            {
                Console.WriteLine("Right now it's {0}'s turn!", playerMarker);

                coordinates = Console.ReadLine();
                while (!InputConditions(coordinates, GamePlan))
                {
                    coordinates = Console.ReadLine();
                }

                string[] splited = coordinates.Split(null);
                x = Convert.ToInt32(splited[0]);
                y = Convert.ToInt32(splited[1]);
                GamePlan[y, x] = playerMarker;

                // Make players take turns
                playerMarker = ChangePlayers(playerMarker);

                printGamePlan(GamePlan);

                // Winning Condition
                isWon = WinningCondition(GamePlan);

                Console.WriteLine();
                timer++;
                if (timer == 9 && !isWon)
                {
                    Console.WriteLine("Tie!");
                }
            }
            Console.ReadKey();
        }
    }
}
