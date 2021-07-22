using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            gameOn();
        }
        static void gameOn()
        {
            Console.WriteLine("Welcome to the best Tic Tac Toe everrrr");
            string input;
            char[,] table = new char[4, 4] { { ' ', '1', '2', '3' }, { '1', '-', '-', '-' }, { '2', '-', '-', '-' }, { '3', '-', '-', '-' } };
            int row, column;
            char win;
            bool finish = false;
            Console.WriteLine("Lets start! Please enter the numbers of the position you like, first the number of the row and then the number of the column.");
            pritsRows(table);
            while (!finish)
            {
                input = Console.ReadLine();
                input = inputValidation(input);
                row = int.Parse(input[0] + "");
                column = int.Parse(input[1] + "");
                changeToX(row, column, table);
                Console.WriteLine("your turn...");
                pritsRows(table);
                win = isWin(table);
                switch (win)
                {
                    case 'X':
                        Console.WriteLine("Mazel Tov !!! Player X Won the game");
                        Console.ReadLine();
                        finish = true;
                        break;
                    case 'O':
                        Console.WriteLine("Mazel Tov !!! Player O Won the game");
                        Console.ReadLine();
                        finish = true;
                        break;
                    case 't':
                        Console.WriteLine("This is a Tie, maybe you will win the next time");
                        Console.ReadLine();
                        finish = true;
                        break;
                }
                table = compuetrTurn(table);
                Console.WriteLine("computer turn...");
                pritsRows(table);
                win = isWin(table);
                switch (win)
                {
                    case 'X':
                        Console.WriteLine("Mazel Tov !!! Player X Won the game");
                        Console.ReadLine();
                        finish = true;
                        break;
                    case 'O':
                        Console.WriteLine("Mazel Tov !!! Player O Won the game");
                        Console.ReadLine();
                        finish = true;
                        break;
                    case 't':
                        Console.WriteLine("This is a Tie, maybe you will win next time");
                        Console.ReadLine();
                        finish = true;
                        break;
                    case 'f':
                        Console.WriteLine("your turn, Please enter the numbers of the position you like, first the number of the row and then the number of the column.");
                        break;
                }
            }
        }
        static void pritsRows(char[,] table)
        {
            Console.WriteLine();
            for (int r = 0; r < 4; r++)
            {
                for (int i = 0; i < 4; i++)
                {
                    Console.Write(table[r, i] + " ");
                }
                Console.WriteLine();
            }
        }
        static string inputValidation(string input)
        {
            bool valid = false;
            while (valid == false)
            {
                //step 1
                if (input.Length != 2)
                {
                    Console.WriteLine("your input is not good,please enter only numbers between 1-3,the first number is row and the second in column");
                    input = Console.ReadLine();
                    continue;
                }
                //step 2
                string nums = "123";
                bool onlynums = false ,num1=false,num2=false;
                valid = false;
                for (int a = 0; a < nums.Length; a++)
                {
                    if (nums[a] == input[1])
                        num1 = true;
                    if (nums[a] == input[0])
                        num2 = true;
                    if (num1 == true && num2 == true)
                    {
                        onlynums = true;
                        break;
                    }
                }
                //step 3
                for (int i = 1; i < 4 && onlynums; i++)
                {
                    if (int.Parse(input[0] + "") == i)
                    {


                        for (int y = 1; y < 4; y++)
                        {
                            if (int.Parse(input[1] + "") == y)
                            {
                                valid = true;
                                break;
                            }
                            else
                                valid = false;
                        }
                        break;
                    }


                }
                //step 4
                if (valid == false)
                {
                    Console.WriteLine("your input is not good,please enter only numbers between 1-3,the first number is row and the second in column");
                    input = Console.ReadLine();
                }
            }
            return input;
        }
        static char[,] compuetrTurn(char[,] table)
        {
            string index;
            int countTurns = 9;
            for (int r = 1; r < 4; r++)
            {
                for (int i = 1; i < 4; i++)
                {

                    if (table[r, i] == '-')
                        countTurns -= 1;
                }
            }
            //שלב 1
            if (table[2, 2] == '-' && countTurns == 1)
            {
                table[2, 2] = 'O';
                return table;
            }
            else if (table[2, 2] == 'X' && countTurns == 1)
            {
                if (table[1, 1] == '-')
                    table[1, 1] = 'O';
                else if (table[1, 3] == '-')
                    table[1, 3] = 'O';
                else if (table[3, 1] == '-')
                    table[3, 1] = 'O';
                else if (table[1, 1] == '-')
                    table[3, 3] = 'O';
                return table;
            }
            //שלב 2+3
            index = twoInRow(table);
            if (index != "00")
            {
                table[int.Parse(index[0] + ""), int.Parse(index[1] + "")] = 'O';
                return table;
            }
            //שלב 4
            if (countTurns == 3)
            {
                if ((table[1, 1] == 'X' && table[3, 3] == 'X') || (table[1, 3] == 'X' && table[3, 1] == 'X'))
                {
                    table[1, 2] = 'O';
                    countTurns++;
                    return table;
                }
            }
            //שלב 5
            if (countTurns == 3)
            {
                if (table[3, 3] == 'X' && table[2, 2] == 'X')
                {
                    table[1, 3] = 'O';
                    return table;
                }
            }
            // שלב7+6
            if (countTurns == 3)
            {

                table = stage67(table);
                return table;

            }



            //שלב סופי
            for (int r = 1; r < 4; r++)
            {
                for (int i = 1; i < 4; i++)
                {

                    if (table[r, i] == '-')
                    {
                        table[r, i] = 'O';
                        return table;
                    }
                }
            }

            return table;

        }
        static char[,] stage67(char[,] table)
        {
            bool find = false;
            if (table[2, 2] == 'O' && ((table[1, 2] == 'X' && table[3, 2] == 'X') || (table[2, 1] == 'X' && table[2, 3] == 'X')))
                if (table[1, 1] == '-')
                {
                    find = true;
                    table[1, 1] = 'O';
                }
                else if (table[1, 3] == '-')
                {
                    find = true;
                    table[1, 3] = 'O';
                }
                else if (table[3, 1] == '-')
                {
                    find = true;
                    table[3, 1] = 'O';
                }
                else if (table[3, 3] == '-')
                {
                    find = true;
                    table[3, 3] = 'O';
                }

            if (!find)
            {
                //    1 2 3
                //1   
                //2
                //3
                if ((table[1, 2] == 'X' || table[1, 3] == 'X') && (table[2, 1] == 'X' || table[3, 1] == 'X'))
                {
                    table[1, 1] = 'O';
                    return table;
                }
                else if ((table[1, 1] == 'X' || table[1, 2] == 'X') && (table[2, 3] == 'X' || table[3, 3] == 'X'))
                {
                    table[1, 3] = 'O';
                    return table;
                }
                else if ((table[3, 2] == 'X' || table[3, 3] == 'X') && (table[1, 1] == 'X' || table[2, 1] == 'X'))
                {
                    table[3, 1] = 'O';
                    return table;
                }
                else if ((table[3, 1] == 'X' || table[3, 2] == 'X') && (table[1, 3] == 'X' || table[2, 3] == 'X'))
                {
                    table[3, 3] = 'O';
                    return table;
                }



            }
            return table;
        }

        static char[,] changeToX(int column, int row, char[,] table)
        {
            string valid;
            if (table[column, row] == '-')
                table[column, row] = 'X';
            else
            {
                Console.WriteLine("the position is allredy declared");
                valid = inputValidation("");
                changeToX(int.Parse(valid[0] + ""), int.Parse(valid[1] + ""), table);
            }


            return table;
        }
        static string twoInRow(char[,] table)
        {
            bool find = false;
            char T = 'O';
            string index = "00";
            for (int j = 0; j < 2 && !find; j++)
            {

                for (int r = 1; r < 4 && !find; r++)
                {
                    int row = 0, col = 0;
                    for (int i = 1; i < 4 && !find; i++)
                    {
                        if (table[r, i] == T)
                            row++;
                        if (table[i, r] == T)
                            col++;
                    }

                    if (row == 2)
                        for (int i = 1; i < 4; i++)
                            if (table[r, i] == '-')
                            {
                                index = "" + r + i;
                                find = true;
                            }
                    if (col == 2 && !find)
                        for (int i = 1; i < 4 && !find; i++)
                            if (table[i, r] == '-')
                            {
                                index = "" + i + r;
                                find = true;
                            }

                }
                //אלכסון 1

                if ((table[1, 1] == T && table[2, 2] == T) && (table[3, 3] == '-' && !find))
                {
                    find = true;
                    index = "33";
                    break;
                }

                else if ((table[1, 1] == T && table[3, 3] == T) && (table[2, 2] == '-' && !find))
                {
                    find = true;
                    index = "22";
                    break;
                }

                else if ((table[2, 2] == T && table[3, 3] == T) && (table[1, 1] == '-' && !find))
                {
                    find = true;
                    index = "11";
                    break;
                }

                //אלכסון 2

                else if ((table[1, 3] == T && table[2, 2] == T) && (table[3, 1] == '-' && !find))
                {
                    find = true;
                    index = "31";
                    break;
                }

                else if ((table[1, 3] == T && table[3, 1] == T) && (table[2, 2] == '-' && !find))
                {
                    find = true;
                    index = "22";
                    break;
                }

                else if ((table[2, 2] == T && table[3, 1] == T) && (table[1, 3] == '-' && !find))
                {
                    find = true;
                    index = "13";
                    break;
                }

                T = 'X';
            }
            return index;
        }

        static char isWin(char[,] table)
        {
            bool win = false;
            char T = 'O';
            for (int j = 0; j < 2 && !win; j++)
            {
                int empty = 0;
                for (int r = 1; r < 4 && !win; r++)
                {
                    int row = 0, col = 0;
                    for (int i = 1; i < 4 && !win; i++)
                    {
                        if (table[r, i] == '-')
                            empty++;
                        if (table[r, i] == T)
                            row++;
                        if (table[i, r] == T)
                            col++;
                    }

                    if (row == 3)
                    {
                        win = true;
                        return T;
                    }
                    if (col == 3)
                    {
                        win = true;
                        return T;
                    }
                }



                //אלכסונים 

                if (((table[1, 3] == T && table[2, 2] == T) && table[3, 1] == T) || ((table[1, 1] == T && table[2, 2] == T) && table[3, 3] == T))
                {
                    return T;

                }


                if (empty == 0)
                    return 't';

                T = 'X';
            }

            return 'f';
        }
    }
}


