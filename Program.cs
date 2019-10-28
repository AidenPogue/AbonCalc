﻿using System;
using System.Collections.Generic;

namespace AbonCalc
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("AbonCalc - October 2019");
            Console.WriteLine("Enter Expression");
            string LastInput;
            bool Quit = false;

			LastInput = Console.ReadLine();
			if (LastInput == "Quit" | LastInput == "quit")
			{
				Console.WriteLine("Quitting");
				Quit = true;
			}
			else
			{
				LexedArrayParser(ArrayLexer(LastInput));
			}
			
        }

        static string[] ArrayLexer(string Input)
        {
            string[] LexedArray = new string[Input.Length]; //The array that lexed parts of the expression are put into. Each number and operator gets a slot.
            int CurrentIndexInArray = 0; //The current index of the array the lexer is working at.
            string CurrentValue = string.Empty; //The current number the lexer is assembling. When and operator is found, this is put into the LexedArray at CurrentIndexInArray and cleared.

            foreach (char CurrentChar in Input)
            {
                if (IsOperator(CurrentChar) == true)
                {
                    LexedArray[CurrentIndexInArray] = CurrentValue; //Put the current value into the current array index.
                    CurrentIndexInArray++; //Next index for operator.
                    LexedArray[CurrentIndexInArray] = char.ToString(CurrentChar); //Add operator.
                    CurrentIndexInArray++; //Next index for next number.
                    CurrentValue = String.Empty; //Clear the current value for next number.
                }
                else if (IsNumber(CurrentChar) == true) //Check if current char is a number.
                {
                    CurrentValue += CurrentChar; //Add current char to the end of the current value.
                }
                else //It's neither an operator nor a number. Call the error method.
                {
                    Error();
                }
            }
            //When we reach the last char in Input and exit the foreach loop, we put the final number into the array.
            LexedArray[CurrentIndexInArray] = CurrentValue;
            return (LexedArray);
        }
        
        static void LexedArrayParser(string[] Input)
        {
            List<string> InputList = new List<string>(Input); //The list copied from Input that the lexer works with.
            string[] OperatorArray = { "^", "/*", "+-" }; //BEDMAS ordered operators.
            string SearchingForOperator = OperatorArray[0]; //The current operator(s) the lexer is searching for
            int CurrentIndex = 0;
            float Operand1 = 0;
            float Operand2 = 0;
            float LastSolverResult = 0;
            string Operator = "";
            foreach (string CurrentOperatorString in OperatorArray)
            {
                CurrentIndex = 0;
                foreach (string CurrentString in InputList)
                {
                    if (CurrentOperatorString.Contains(CurrentString))
                    {
                        //Operator Found
                        if (CurrentIndex == (InputList.Count-1)) //Check if index is last, and throw error if so.
                        {
                            Error();
                        }
                        else
                        {
                            
                        }
                    }
                    else
                    {
                        CurrentIndex++;
                    }
                }
            }
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

        static bool IsNumber(char Input)
        {
            return ("0123456789.").Contains(Input);
        }

        static void Error()
        {
            Console.WriteLine("Error Lmao");
        }

    }
}
