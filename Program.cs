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
                    //Console.WriteLine(CurrentValue);
                }
                Iteration++;
            }
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

            foreach (string CurrentOperators in OperatorArray)
            {
                while (CurrentIndex != InputList.Count)
                {
                    string CurrentString = InputList[CurrentIndex];

					if (CurrentOperators.Contains(CurrentString)) //Check for operators.
					{
                        //Get slot left of operator for operand 1.
                        Operand1 = float.Parse(InputList[(CurrentIndex-1)]);

                        //Get slot right of operator for operand 2.
                        if ((CurrentIndex+1) > InputList.Count)
                        {
                            break;
                        }
                        else
                        {
                            Operand2 = float.Parse(InputList[(CurrentIndex + 1)]);
                        }
                        InputList[CurrentIndex] = (Solver(CurrentString, Operand1, Operand2)).ToString(); //Solve into the current operator's index
                        InputList.RemoveAt(CurrentIndex - 1); InputList.RemoveAt(CurrentIndex + 1); //Remove the two adjacent operands.
                        foreach (String UMom in InputList)
                        {
                            Console.WriteLine(UMom);
                        }
                    }
					else
					{
						continue;
					}
                    CurrentIndex++;
                }
            }
            Console.WriteLine(InputList[0]);
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
