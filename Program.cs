using System;
using System.Collections.Generic;

namespace AbonCalc
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("AbonCalc - October 2019");
            Console.WriteLine("Enter Expression");
            String LastInput;
            bool Quit = false;

            while (Quit == false)
            {
                LastInput = Console.ReadLine();
                if (LastInput == "Quit" | LastInput == "quit")
                {
                    Console.WriteLine("Quitting");
                    Quit = true;
                }
                else
                {
                    //Console.WriteLine(ArrayLexer(LastInput));
                    string[] Test = ArrayLexer(LastInput);
                    foreach (string Current in Test)
                    {
                        Console.WriteLine(Current);
                    }
                }
            }
        }

        static string[] ArrayLexer(string Input)
        {
            string[] LexedArray = new string[Input.Length]; //The array that lexed parts of the expression are put into. Each number and operator gets a slot.
            int Iteration = 0; //Iterations of the while loop
            int CurrentIndexInArray = 0; //The current index of the array the lexer is working at.
            string CurrentValue = string.Empty; //The current number the lexer is assembling. When and operator is found, this is put into the LexedArray at CurrentIndexInArray and cleared.
            bool Lexing = true; //Controls the while loop. Set to false when last character of Input is reached.

            while (Lexing == true)
            {
                if (Iteration == (Input.Length) - 1)
                {
                    LexedArray[CurrentIndexInArray] = CurrentValue;
                    Lexing = false;
                }
                char CurrentChar = Input[Iteration];
                if (IsOperator(CurrentChar) == true)
                {
                    LexedArray[CurrentIndexInArray] = CurrentValue;
                    CurrentValue = "";
                    CurrentIndexInArray++;
                    LexedArray[CurrentIndexInArray] = Char.ToString(CurrentChar);
                    CurrentIndexInArray++;
                }
                else
                {
                    CurrentValue += CurrentChar;
                    Console.WriteLine(CurrentValue);
                }
                Iteration++;
            }
            return (LexedArray);
        }
        
        static void LexedArrayParser(string[] Input)
        {

        }
        static float Solver(string Operator, float Val1, float Val2)
        {
            float Output;
            //Operator switchcase
            switch (Operator)
            {
                default:
                    {
                        Output = 0;
                        Console.WriteLine("Invalid Operator : " + Operator + ".");
                        break;
                    }

                case "^":
                    //Power
                    {
                        Output = MathF.Pow(Val1, Val2);
                        break;
                    }
                case "/":
                    //Divide
                    {
                        Output = Val1 / Val2;
                        break;
                    }
                case "*":
                    //Multiply
                    {
                        Output = Val1 * Val2;
                        break;
                    }
                case "+":
                    //Add
                    {
                        Output = Val1 + Val2;
                        break;
                    }
                case "-":
                    //Subtract
                    {
                        Output = Val1 - Val2;
                        break;
                    }
            }

            return Output;
        }
        static bool IsOperator(char Input)
        {
            return ("^*/+-").Contains(Input);
        }

    }
}
