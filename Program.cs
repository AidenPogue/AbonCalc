using System;
using System.Collections.Generic;

namespace AbonCalc
{
    class AbonCalcMain
    {
        public static bool ShowDebugMessages = false;

        static void Main()
        {
            Console.WriteLine("AbonCalc - October 2019");
            Console.WriteLine("Operators : ^, /, *, +, -");
            Console.WriteLine("Commands : quit, clear or clr.");
            InputHandeller();
        }
        static void InputHandeller()
        {
            Console.WriteLine("Enter Expression");

            string LastInput;
            bool Quit = false;

            while (Quit == false)
            {
                LastInput = Console.ReadLine();
                if (LastInput == "Quit" | LastInput == "quit")
                {
                    Console.WriteLine("Quitting");
                    Quit = true;
                }
                else if (LastInput == "Clear" | LastInput == "clear" | LastInput == "clr")
                {
                    Console.Clear();
                }
                else
                {
                    ArraySolver(ArrayLexer(LastInput));
                }
            }
        }

        static List<string> ArrayLexer(string Input)
        {
            List<string> LexedList = new List<string>(); //The array that lexed parts of the expression are put into. Each number and operator gets a slot.
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
                            Console.WriteLine("Unrecognized or invalid character at index : " +Iteration+ ". This character will be skipped, but the answer might be incorrect.");
                        }

                    }
                    else if (Input[Iteration - 1] == '-')
                    {
                        Console.WriteLine("Too many -'s. Aborting evaluation.");
                        InputHandeller();
                    }
                    else //Not negative.
                    {
                        LexedList.Add(CurrentValue); //Put the current value into the current array index.
                        DebugMessages("Added " + CurrentValue + " to solving array.");
                        CurrentIndexInArray++; //Next index for operator.
                        LexedList.Add(char.ToString(CurrentChar)); //Add operator.
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
            LexedList.Add(CurrentValue);
            DebugMessages("Added " + CurrentValue + " to solving array.");
            return (LexedList);
        }
        
        static void ArraySolver(List<string> InputList)
        {            
            string[] OperatorArray = { "^", "/*", "+-" }; //BEDMAS ordered operators.
            string SearchingForOperator = OperatorArray[0]; //The current operator(s) the lexer is searching for
            int CurrentIndexInList = 0;
            float Operand1 = 0;
            float Operand2 = 0;
            string CurrentStringInList = "";
            
            foreach (string CurrentOperatorString in OperatorArray)
            {
                CurrentIndexInList = 0;

                while (CurrentIndexInList < InputList.Count)
                {
                    //Reset equation vars.
                    CurrentStringInList = InputList[CurrentIndexInList];
                    Operand1 = 0;
                    Operand2 = 0;
                    if (CurrentOperatorString.Contains(CurrentStringInList) == true)
                    {
                        Operand1 = float.Parse(InputList[(CurrentIndexInList - 1)]); //Get value to left of operator.
                        Operand2 = float.Parse(InputList[(CurrentIndexInList + 1)]); //Get value to right of operator.
                        InputList[CurrentIndexInList] = Solver(CurrentStringInList, Operand1, Operand2).ToString(); //Set operator index to current equation result.
                        InputList.RemoveAt(CurrentIndexInList - 1); //Remove value to left of operator.
                        InputList.RemoveAt(CurrentIndexInList); //Remove value that *was* to the right of operator, which is now at CurrentIndexInList as everything was shifted to the left by one.
                        CurrentIndexInList -= 1; //Set current index 2 back to coincide with the removal of both operators.
                    }
                    else
                    {
                        CurrentIndexInList++;
                    }

                }
            }
            Console.WriteLine("Answer = " + InputList[0]);
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
