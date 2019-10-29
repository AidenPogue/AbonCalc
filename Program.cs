using System;
using System.Collections.Generic;

namespace AbonCalc
{
    class AbonCalcMain
    {
        public static bool ShowDebugMessages = false;

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
				ArraySolver(ArrayLexer(LastInput));
			}
			
        }

        static string[] ArrayLexer(string Input)
        {
            string[] LexedArray = new string[Input.Length]; //The array that lexed parts of the expression are put into. Each number and operator gets a slot.
            int CurrentIndexInArray = 0; //The current index of the array the lexer is working at.
            string CurrentValue = string.Empty; //The current number the lexer is assembling. When and operator is found, this is put into the LexedArray at CurrentIndexInArray and cleared.
            char CurrentChar = ' ';
            char LastChar = ' ';


            for (int Iteration = 0; Iteration < Input.Length; Iteration++)
            {
                CurrentChar = Input[Iteration];
                if (IsOperator(CurrentChar) == true)
                {
                    //Negative handling.
                    if (CurrentValue == "")
                    {
                        if (CurrentChar == '-')//Check if negative.
                        {
                            CurrentValue += "-"; // Add to beginning of current value.
                        }
                        else //Call error method if not negative.
                        {
                            Error();
                        }

                    }
                    else //Not negative.
                    {
                        LexedArray[CurrentIndexInArray] = CurrentValue; //Put the current value into the current array index.
                        DebugMessages("Added " + CurrentValue + " to solving array.");
                        CurrentIndexInArray++; //Next index for operator.
                        LexedArray[CurrentIndexInArray] = char.ToString(CurrentChar); //Add operator.
                        CurrentIndexInArray++; //Next index for next number.
                        CurrentValue = String.Empty; //Clear the current value for next number.
                    }
                }
                else if (IsNumber(CurrentChar) == true) //Check if current char is a number.
                {
                    CurrentValue += CurrentChar; //Add current char to the end of the current value.
                }
                else //It's neither an operator nor a number. Call the error method.
                {
                    Error();
                }

                LastChar = CurrentChar;
            }
            //When we reach the last char in Input and exit the for loop, we put the final number into the array.
            LexedArray[CurrentIndexInArray] = CurrentValue;
            DebugMessages("Added " + CurrentValue + " to solving array.");
            return (LexedArray);
        }
        
        static void ArraySolver(string[] Input)
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

            }
        }
        static float Solver(string Operator, float Val1, float Val2)
        {
            float Output;
            //Operator switchcase
            switch (Operator)
            {
                default: //Should never be used, as ArrayLexer should catch any invalid chars.
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

        static void DebugMessages(string Input)
        {
            if (AbonCalcMain.ShowDebugMessages == true)
            {
                Console.WriteLine("Debug : " + Input);
            }
        }

    }
}
