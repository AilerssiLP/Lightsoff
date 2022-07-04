using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using LightsOff.Core;
using LightsOff.Entity;
using LightsOff.Service;

namespace LightsOff
{
   internal class ConsoleUI
   {
        private readonly Field _field;

        private readonly IScoreService _scoreService = new ScoreServiceFile();
        private readonly IRatingService _ratingService = new RatingServiceFile();
        private readonly ICommentService _commentService = new CommentServiceFile();

        protected static int origRow;
        protected static int origCol;
        private int moves = 0;
        private int breaking=0;


        public ConsoleUI(Field field)
        {
           _field = field;
        }

       public void Play()
       {
            
            do
           {
               PrintField();
               ProcessInput();
           } while (!_field.IsSolved());


           _scoreService.AddScore(
                new Score { Player = Environment.UserName, Points = _field.GetScore(moves), PlayedAt = DateTime.Now });

           PrintField();

           Buttons();

        }


        private void PrintField()
       {
            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("===================================");
            Console.WriteLine("| Welcome to the game lights out! |");
            Console.WriteLine("|    Good luck solving it :)      |");
            Console.WriteLine("===================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n-----------------------------------");
            Console.WriteLine("| To win you have to switch all   |");
            Console.WriteLine("| lights off by writing the       |");
            Console.WriteLine("| positions of it's x and y.      |");
            Console.WriteLine("| X is Off and O is On.           |");
            Console.WriteLine("-----------------------------------");
            //Console.Write("\nCurrent Field:\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Current Field:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--------------");
            Console.Write("  ");
            for (var row = 0; row < _field.RowCount; row++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(row + " ");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("|\n");
            for (var row = 0; row < _field.RowCount; row++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(row+" ");
                for (var column = 0; column < _field.ColumnCount; column++)
               {
                    var tile = _field.GetTile(row, column);

                    if (_field.GetTile(row, column).Value == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;

                        Console.Write("O ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("X ");
                    }

               }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("|");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--------------");
            Console.ForegroundColor = ConsoleColor.White;
            if (breaking == 0) { 
                Console.WriteLine("Choose difficulty\n");
                breaking = 1;
                string difficulty = Console.ReadLine();
                while (Convert.ToInt32(difficulty) <= 0) {
                    Console.WriteLine("Choose difficulty above 0\n");
                    difficulty = Console.ReadLine();
                }
                _field.Map(Convert.ToInt32(difficulty));
                PrintField();
            }
            
        }

       private void ProcessInput()
       {
            Console.ForegroundColor = ConsoleColor.Gray;//console command
            Console.WriteLine("To give up game write -1 as a a position of row or column\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Input number of row: ");

            Console.ForegroundColor = ConsoleColor.Blue;//console rows
            string row_s = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.White;//console command
            Console.WriteLine("Input number of column: ");

            Console.ForegroundColor = ConsoleColor.Blue;//console rows
            string column_s = Console.ReadLine();
            int row_s1 = Convert.ToInt32(row_s);
            int column_s1 = Convert.ToInt32(column_s);

            int rows =0;
            int columns =0;
            int breaker = 1;

            try
                {
                    rows = Convert.ToInt32(row_s);

                    columns = Convert.ToInt32(column_s);
                }
                catch {
                    PrintField();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Give me the right positions from 0 to 4!!!\n");
                    breaker = 0;
                    ProcessInput();
                }
            

            if (rows >= -1 && rows <= 4 && columns >= -1 && columns <= 4)
            {
                if (rows == -1 || columns == -1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nYou gave up GAME OVER!!!!!");
                    Console.ForegroundColor = ConsoleColor.White;
                    _field.Generate();
                    Buttons();
                    Environment.Exit(0);
                }
            }
            else
            {
                PrintField();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Give me the right positions from 0 to 4!!!\n");
                breaker = 0;
                ProcessInput();
            }
            if (breaker == 1) {
                _field.Play(row_s1, column_s1);
                moves++;
            }
            
       }

        public int GetRating()
        {
            Console.WriteLine("Rate my game from 0-5!!!");
            string rate = Console.ReadLine();
            int rate1 = Convert.ToInt32(rate);
            if (rate1 > 5 || rate1 < 0) {
                Console.WriteLine("Wrong rating rate my game 0-5!!!\n");
                GetRating();
            }
            return rate1;
        }
        public string GetComment()
        {
            Console.WriteLine("Write your comment down bellow");
            string rate = Console.ReadLine();
            return rate;
        }
        private void PrintComments()
        {
            Console.WriteLine("\n--------------------------------");
            Console.WriteLine("|           Comments           |");
            Console.WriteLine("--------------------------------");

            foreach (var Comment in _commentService.GetAllComments())
            {
                Console.Write("\n");
                Console.Write(Comment.Player);
                Console.Write("'s comment:\n");
                Console.Write("---------------------\n ->");
                Console.Write(Comment.PlayerComment);
                Console.Write("\n");
            }
        }

            private void PrintRating()
        {
            Console.WriteLine("\n--------------------------------");
            Console.WriteLine("|        Avarage Rating        |");
            Console.WriteLine("--------------------------------");

            Console.Write((int)_ratingService.GetAveRating());

            Console.Write("  -  ");

            for (int i=0; i< ((int)_ratingService.GetAveRating()); i++) {
                Console.Write("*", 49+i, 13);
            }

            /*
            WriteAt("--------------------------------", 45, 15);
            WriteAt("------------- RATINGS ----------", 45, 16);
            WriteAt("--------------------------------", 45, 17);
            int count = 0;
            foreach (var rating in _ratingService.GetAllRatings())
            {
                WriteAt("[0]", 45, 18 + count);
                WriteAt("[ ]", 48, 18 + count);
                WriteAtInt(count, 49, 18 + count);
                WriteAt(rating.Player, 55, 18 + count);
                WriteAtInt(rating.PlayerRating, 68, 18 + count);
                count++;
            }
            WriteAt("--------------------------------", 45, 20 + count);*/
        }

        
            private void PrintTopScores()
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("|          TOP SCORES          |");
            Console.WriteLine("--------------------------------");
            int count = 0;
            foreach (var score in _scoreService.GetTopScores())
            {
                Console.Write("[0] ["+count+"] " );
                Console.Write(score.Player+" ");
                Console.Write(score.Points +" \n");
                count++;
            }

        }

        private void Buttons()
        {
            Console.Clear();
            PrintField();
            if (_field.IsSolved())
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\nGame solved!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ForegroundColor= ConsoleColor.White;
            Console.WriteLine("\nIf you wanna play the game again press Enter! ");
            Console.WriteLine("If you wanna rate my game press R! ");
            Console.WriteLine("If you wanna see the rating of this game press L! ");
            Console.WriteLine("If you wanna leave a comment press C! ");
            Console.WriteLine("If you wanna see new comments press D! ");
            Console.WriteLine("If you wanna see Top scores press T! ");
            Console.WriteLine("If you wanna leave the game press Esc! ");
            //PrintTopRatings();
            ConsoleKeyInfo input = Console.ReadKey(true);
            if (input.Key == ConsoleKey.Escape)
            {
                //Console.WriteLine("escape ");
                Environment.Exit(0);
            }
            else if (input.Key == ConsoleKey.R)
            {
                _ratingService.AddRating(
                new Rating { Player = Environment.UserName, PlayerRating = GetRating(), PlayedAt = DateTime.Now });
                Buttons();
            }
            else if (input.Key == ConsoleKey.Enter){
                breaking = 0;
                Play();
            }
            else if (input.Key == ConsoleKey.L)
            {
                PrintRating();
                Thread.Sleep(3000);
                Buttons();
            }else if (input.Key == ConsoleKey.C)
            {
                _commentService.AddComment(
                new Comment { Player = Environment.UserName, PlayerComment = GetComment(), PlayedAt = DateTime.Now });
                Buttons();
            }else if (input.Key == ConsoleKey.D)
            {
                PrintComments();
                Thread.Sleep(3000);
                Buttons();
            }else if (input.Key == ConsoleKey.T)
            {
                PrintTopScores();
                Thread.Sleep(3000);
                Buttons();
            }
        }

    }
}
