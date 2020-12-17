using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubic
{
    public class Game
    {
        private char[,] mapSource = new char[5, 5]
            {
                {'x', '1', '0', '0', 'x' },
                {'1', '0', '1', '1', '0' },
                {'0', '0', '0', '1', '1' },
                {'1', '0', '0', '0', '1' },
                {'x', '1', '0', '0', 'x' }
            };

        private char[,] map;

        //Х100х
        //10110
        //00011
        //10001
        //Х100х

        private int maxLength => (int)map.GetLongLength(0);

        private bool show = false;
        public bool Win { get; private set; } = false;
        private bool firstJoin = true;

        public void MoveLeft(int str)
        {
            // TODO: Проверка

            if (!CheckData(str))
                return;

            // Сдвиг

            var cashChar = map[str, 0]; // Во,берем начальную

            for (int i = 1; i < maxLength; i++)
            {
                map[str, i - 1] = map[str, i];
            }

            map[str, maxLength - 1] = cashChar; // После сдвига присваиваем
        }

        public void MoveRight(int str)
        {
            // TODO: Проверка

            if (!CheckData(str))
                return;

            // Сдвиг

            var cashChar = map[str, maxLength - 1];

            for (int i = maxLength - 2; i >= 0; i--)
            {
                map[str, i + 1] = map[str, i];
            }

            map[str, 0] = cashChar;

        }

        public void MoveUp(int str)
        {
            // TODO: Проверка

            if (!CheckData(str))
                return;

            // Сдвиг

            var cashChar = map[0, str];

            for (int i = 1; i < maxLength; i++)
            {
                map[i - 1, str] = map[i, str];
            }

            map[maxLength - 1, str] = cashChar;
        }

        public void MoveDown(int str)
        {
            // TODO: Проверка

            if (!CheckData(str))
                return;

            // Сдвиг

            var cashChar = map[maxLength - 1, str];

            for (int i = maxLength - 2; i >= 0; i--)
            {
                map[i + 1, str] = map[i, str];
            }

            map[0, str] = cashChar;

        }

        public void RotateRight(int times)
        {
            var cashMatrix = CutMatrix(map);
            var maxCashLength = (int)cashMatrix.GetLongLength(0);

            for (int t = 0; t < times; t++)
            {

                var cashChar = cashMatrix[0, 0];


                // Сдвиг вверх
                for (int i = 1; i < maxCashLength; i++)
                {
                    cashMatrix[i - 1, 0] = cashMatrix[i, 0];
                }

                // Сдвиг влево
                for (int i = 1; i < maxCashLength; i++)
                {
                    cashMatrix[2, i - 1] = cashMatrix[2, i];
                }

                // Сдвиг вниз
                for (int i = maxCashLength - 2; i >= 0; i--)
                {
                    cashMatrix[i + 1, 2] = cashMatrix[i, 2];
                }

                // Сдвиг вправо
                for (int i = maxCashLength - 2; i >= 0; i--)
                {
                    cashMatrix[0, i + 1] = cashMatrix[0, i];
                }

                cashMatrix[0, 1] = cashChar;

            }

            IncludeMatrix(cashMatrix);

        }

        public void RotateLeft(int times)
        {
            var cashMatrix = CutMatrix(map);
            var maxCashLength = (int)cashMatrix.GetLongLength(0);

            for (int t = 0; t < times; t++)
            {

                var cashChar = cashMatrix[0, 2];


                // Сдвиг вверх
                for (int i = 1; i < maxCashLength; i++)
                {
                    cashMatrix[i - 1, 2] = cashMatrix[i, 2];
                }

                // Сдвиг вправо
                for (int i = maxCashLength - 2; i >= 0; i--)
                {
                    cashMatrix[2, i + 1] = cashMatrix[2, i];
                }

                // Сдвиг вниз
                for (int i = maxCashLength - 2; i >= 0; i--)
                {
                    cashMatrix[i + 1, 0] = cashMatrix[i, 0];
                }

                // Сдвиг влево
                for (int i = 1; i < maxCashLength; i++)
                {
                    cashMatrix[0, i - 1] = cashMatrix[0, i];
                }

                cashMatrix[0, maxCashLength - 2] = cashChar;

            }

            IncludeMatrix(cashMatrix);
        }

        private bool CheckData(int data)
        {
            switch (data)
            {
                case 1:
                    return true;
                    break;
                case 2:
                    return true;
                    break;
                case 3:
                    return true;
                    break;
                default:
                    return false;
                    break;
            }
        }

        private char[,] CutMatrix(char[,] matrix)
        {

            var cashArray = new char[maxLength - 2, maxLength - 2];

            for (int i = 1; i < maxLength - 1; i++)
            {
                for (int j = 1; j < maxLength - 1; j++)
                {
                    cashArray[i - 1, j - 1] = matrix[i, j];
                }
            }

            return cashArray; // Все ебать работает, отлично.
        }

        private void IncludeMatrix(char[,] matrix)
        {
            for (int i = 1; i < maxLength - 1; i++)
            {
                for (int j = 1; j < maxLength - 1; j++)
                {
                    map[i, j] = matrix[i - 1, j - 1];
                }
            }
        }

        public void Show() => show = !show;

        public void Update()
        {
            if (firstJoin)
                Restart();

            WinHandle();

            if (show && !Win)
                DrawWithCorners();
            else if (!show && !Win)
                DrawWithoutCorners();

            if (Win)
            {
                Console.WriteLine();
                Console.WriteLine("\t Поздравляю, ты победил!");
                Console.ReadLine();
                firstJoin = true;
            }
        }

        private void DrawWithoutCorners()
        {
            DrawHelp();
            Console.SetCursorPosition(20, 1);
            Console.Write("[Show corners: off]");
            Console.SetCursorPosition(0, 2);
            for (int i = 1; i < maxLength - 1; i++)
            {
                Console.Write("    ");

                for (int j = 1; j < maxLength - 1; j++)
                {
                    Console.Write(map[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private void DrawWithCorners()
        {
            //Console.WriteLine();
            DrawHelp();
            Console.SetCursorPosition(20, 1);
            Console.Write("[Show corners: on]");
            Console.SetCursorPosition(0, 1);
            for (int i = 0; i < maxLength; i++)
            {
                Console.Write("  ");

                for (int j = 0; j < maxLength; j++)
                {
                    Console.Write(map[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void Restart()
        {
            Win = false;
            show = false;
            map = (char[,]) mapSource.Clone();
            firstJoin = false;
        }

        private void WinHandle()
        {
            var cash = 0;
            for (int i = 1; i < maxLength - 1; i++)
            {
                for (int j = 1; j < maxLength - 1; j++)
                {
                    if (map[i, j] == '1')
                        cash++;
                }
            }

            if (cash == 9)
                Win = true;
        }
        
        private void DrawHelp()
        {
            Console.SetCursorPosition(20, 3);
            Console.Write("\tПомощь:");
            Console.SetCursorPosition(20, 4);
            Console.Write("Перемещение строк/столбцов: /move [left / right / up / down] [номер стобца/строки (1, 2, 3)]");
            Console.SetCursorPosition(20, 5);
            Console.Write("Поворот квадрата: /rotate [left / right]");
            Console.SetCursorPosition(20, 6);
            Console.Write("Показать крайние элементы: /show");
        }
    }
}
