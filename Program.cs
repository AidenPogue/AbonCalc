﻿using System;
using System.Collections.Generic;

namespace AbonCalc
{
    class AbonCalcMain
    {
        //Globals
        public static bool ShowDebugMessages = false; //Controls a few debug messages.

        static void Main()
        {
            Console.WriteLine("AbonCalc - October 2019");
            Console.WriteLine("Operators : ^, /, *, +, -");
            Console.WriteLine("Commands : quit, clear or clr.");
            InputHandeller();
        }
        static void InputHandeller()
        {
            string LastInput;

            for(; ;)
            {
                Console.WriteLine("Enter Expression");
                LastInput = Console.ReadLine();

                //Command switchcase
                switch(LastInput)
                {

                    case ("Quit"): //Quit
                    case ("quit"):
                        {
                            Environment.Exit(0);
                            break;
                        }

                    case ("Clear"): //Clear
                    case ("clear"):
                    case ("clr"):
                        {
                            Console.Clear();
                            break;
                        }
                    case (""):
                        {
                            InputHandeller();
                            break;
                        }

                    default: //Assume that no command means a number. Try to solve.
                        {
                            Console.WriteLine("Answer = " + ArraySolver(InputLexer(LastInput)));
                            break;
                        }

                }
            }
        }

        static string BracketLexer(string Input) //Creates a List where each bracketed expression has it's own index.
        {
            char CurrentChar; //The char at the current index the lexer is working at.
            string CurrentEquation; //New chars are added to this.

            for (int Index = 0; Index < Input.Length; Index++)
            {
                CurrentChar = Input[Index];


            }
        }

        static List<string> InputLexer(string Input) //Lexes a string into a List where numbers and operators are separated.
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
                            InvalidChar(Iteration);
                        }

                    }
                    else if (Input[Iteration - 1] == '-')
                    {
                        Console.WriteLine("Too many -'s. Aborting evaluation.");
                        InputHandeller();
                    }
                    else if (Iteration == Input.Length-1)
                    {
                        Console.WriteLine("Unexpected operator at end. Ignoring.");
                        LexedList.Add(CurrentValue);
                        return (LexedList);
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
                    InvalidChar(Iteration);
                }

                LastChar = CurrentChar;
            }
            //When we reach the last char in Input and exit the for loop, we put the final number into the array.
            LexedList.Add(CurrentValue);
            DebugMessages("Added " + CurrentValue + " to solving array.");
            return (LexedList);
        }
        
        static string ArraySolver(List<string> InputList) //Solves a List created by InputLexer.
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
                        try  
                        {
                            Operand1 = float.Parse(InputList[(CurrentIndexInList - 1)]); //Get value to left of operator.
                            Operand2 = float.Parse(InputList[(CurrentIndexInList + 1)]); //Get value to right of operator.
                            InputList[CurrentIndexInList] = Solver(CurrentStringInList, Operand1, Operand2).ToString(); //Set operator index to current equation result.
                            InputList.RemoveAt(CurrentIndexInList - 1); //Remove value to left of operator.
                            InputList.RemoveAt(CurrentIndexInList); //Remove value that *was* to the right of operator, which is now at CurrentIndexInList as everything was shifted to the left by one.
                            CurrentIndexInList -= 1; //Set current index 2 back to coincide with the removal of both operators.
                        }
                        catch(ArgumentOutOfRangeException)//The above section will throw an argument out of range if something is wrong with the InputList.
                        {
                            Console.WriteLine("All characters in expression were invalid. Aborting operation.");
                            InputHandeller();
                        }
                    }
                    else
                    {
                        CurrentIndexInList++;
                    }

                }
            }
            return (InputList[0]);
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

        static void InvalidChar(int Index)
        {
            Console.WriteLine("Unrecognized or invalid character at index : " + Index + ". This character will be skipped, but the answer might be undesired.");
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
