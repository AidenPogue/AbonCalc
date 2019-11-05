using System;
using System.Collections.Generic;

namespace AbonCalc
{
    class AbonCalcMain
    {
        //Globals
        public static bool ShowDebugMessages = false; //Controls a few debug messages.
        public static decimal LastAnswer = 0;

        static void PrintNews()
        {
            Console.WriteLine("Nov 1st 2019 - Changed internal value handling from float to decimal, meaning more precision.");
            Console.WriteLine("Nov 4th 2019 - Full bracket support. Still some issues with error handling");
        }
        static void Main()
        {
            Console.WriteLine("AbonCalc - October - November 2019");
            Console.WriteLine("Operators : ^, /, *, +, -,");
            Console.WriteLine("BEDMAS compliant, expressions with multiple operands are supported.");
            Console.WriteLine("Commands : quit, clear / clr, news. \n");
            InputHandeller();
        }
        static void InputHandeller()
        {
            string LastInput;

            for (; ; )
            {
                Console.WriteLine("Enter Expression");
                LastInput = Console.ReadLine();

                //Command switchcase
                switch (LastInput.ToUpper())
                {

                    case ("QUIT"): //Quit
                        {
                            Environment.Exit(0);
                            break;
                        }

                    case ("CLR"): //Clear
                    case ("CLEAR"):
                        {
                            Console.Clear();
                            break;
                        }

                    case ("NEWS"):
                        {
                            PrintNews();
                            break;
                        }

                    case (""):
                        {
                            InputHandeller();
                            break;
                        }

                    default: //Assume that no command means a number. Try to solve.
                        {
                            try
                            {
                                AbonCalcMain.LastAnswer = decimal.Parse(ArraySolver(InputLexer(BracketLexer(LastInput))));
                            }
                            catch(System.OverflowException)
                            {
                                Console.WriteLine("A number in the input or the answer was too big. Aborting");
                                break;
                            }
                            catch (System.FormatException)
                            {
                                Console.WriteLine("Invalid number found, most likley too many decimals.");
                                break;  
                            }
                            Console.WriteLine("Answer = " + LastAnswer);
                            break;
                        }

                }
            }
        }

        
        static string BracketLexer(string Input) //Lexes the input string for brackets and returns the answer to the whole equation.
        {
            string Output = Input;
            string CurrentValue = string.Empty; //New chars are added to this. Cleared at "("
            char CurrentChar;
            int OpenBracketIndex = -1; //The last open bracket. -1 means that none have been incountered or the last one has been lexed.
            int CurrentIndex = 0;

            while(CurrentIndex < Output.Length)
            {
                CurrentChar = Output[CurrentIndex];

                if (CurrentChar == '(')
                {
                    OpenBracketIndex = CurrentIndex;
                    CurrentValue = string.Empty;
                    if (CurrentIndex != 0) //Implied multiplication handling.
                    {
                        if (IsNumber(Output[CurrentIndex - 1]) == true)
                        {
                            Output = Output.Insert(CurrentIndex, "*");
                        }
                    }
                    CurrentIndex++;

                }
                else if (CurrentChar == ')')
                {
                    if (OpenBracketIndex == -1) //If we find a closing bracket but we haven't found an opening one, we know it was misplaced.
                    {
                        Console.WriteLine("Misplaced closing bracket. Aborting");
                        InputHandeller();
                        
                    }
                    else
                    {
                        Output = Output.Remove(OpenBracketIndex, (CurrentIndex - OpenBracketIndex + 1)); //Remove the current bracketed equation.
                        Output = Output.Insert(OpenBracketIndex, (string)ArraySolver(InputLexer(CurrentValue))); //Replace it with its result.
                        CurrentValue = string.Empty;
                        OpenBracketIndex = -1;
                        CurrentIndex = 0;
                    }
                }
                else
                {
                    CurrentValue += CurrentChar;
                    CurrentIndex++;
                }
                
                
            }
            return Output;
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
                            InvalidChar(Iteration, CurrentChar);
                        }

                    }
                    else if (Input[Iteration - 1] == '-')
                    {
                        Console.WriteLine("Too many -'s. Aborting evaluation.");
                        InputHandeller();
                    }
                    else if (Iteration == Input.Length - 1)
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
                    InvalidChar(Iteration, CurrentChar);
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
            decimal Operand1 = 0;
            decimal Operand2 = 0;
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
                            Operand1 = decimal.Parse(InputList[(CurrentIndexInList - 1)]); //Get value to left of operator.
                            Operand2 = decimal.Parse(InputList[(CurrentIndexInList + 1)]); //Get value to right of operator.
                            InputList[CurrentIndexInList] = Solver(CurrentStringInList, Operand1, Operand2).ToString(); //Set operator index to current equation result.
                            InputList.RemoveAt(CurrentIndexInList - 1); //Remove value to left of operator.
                            InputList.RemoveAt(CurrentIndexInList); //Remove value that *was* to the right of operator, which is now at CurrentIndexInList as everything was shifted to the left by one.
                            CurrentIndexInList--; //Move index back 1 so we're back on the answer we just got.
                        }
                        catch (ArgumentOutOfRangeException)//The try block will throw an argument out of range if there are no valid chars in InputList.
                        {
                            Console.WriteLine("All characters in expression were invalid. Aborting operation.");
                            InputHandeller();
                        }
                        catch (System.FormatException) //This only gets thrown when there is multiple decimals, which will get passed to this method because they don't get skipped by the lexer.
                        {
                            Console.WriteLine("Invalid number found, most likley too many decimals.");
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
        static decimal Solver(string Operator, decimal Val1, decimal Val2)
        {
            decimal Output = 0;
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
                        Output = Val1;
                        if (Val2 % 1 == 0) //Check if int
                        {
                            for (int Iteration = 1; Iteration != Val2; Iteration++) //Math.Pow is fo kidz
                            {
                                Output *= Val1;
                            }
                        }
                        else //Not int. Use double for power.
                        {
                            Console.WriteLine("Non integer exponent detected. We will need to use a different variable type to do this, so some precision will be lost.");
                            Output = (decimal)Math.Pow(decimal.ToDouble(Val1), decimal.ToDouble(Val2));
                        }
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

        static void InvalidChar(int Index, char Char)
        {
            Console.WriteLine("Unrecognized or invalid character " + Char + " at index : " + Index + ". This character will be skipped, but the answer might be undesired.");
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
