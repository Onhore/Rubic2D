using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic
{
    class Program
    {
        public static Game Game = new Game();

        private static void Main(string[] args)
        {

            while (true)
            {
                Console.Clear();
                Game.Update();
                if (!Game.Win)
                {
                    Console.WriteLine("Введите команду:");
                    CommandHandle(Console.ReadLine());
                }
            }
        }

        private static void CommandHandle(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
                return;

            var com = command.ToLower();
            var args = com.Split(' ');

            if (com[0] != '/')
                return;

            if (args[0] == "/move" && args.Length == 3)
            {
                switch (args[1])
                {
                    case "left":
                        Game.MoveLeft(int.Parse(args[2]));
                        break;
                    case "right":
                        Game.MoveRight(int.Parse(args[2]));
                        break;
                    case "up":
                        Game.MoveUp(int.Parse(args[2]));
                        break;
                    case "down":
                        Game.MoveDown(int.Parse(args[2]));
                        break;
                    default:
                        return;
                        break;
                }
            }
            else if (args[0] == "/rotate" && args.Length == 2)
            {
                switch (args[1])
                {
                    case "left":
                        Game.RotateLeft(2);
                        break;
                    case "right":
                        Game.RotateRight(2);
                        break;
                    default:
                        return;
                        break;
                }
            }
            else if (args[0] == "/show" && args.Length == 1)
                Game.Show();
        }
    }
}
