using System;

namespace AbonCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AbonCalc - October 2019");
            Console.WriteLine("Enter Expression");
            String LastInput = (Console.ReadLine());

            //if (LastInput == "Quit") ;
            //{
            //    Console.WriteLine("Quitting");
            //}
            //else
            //{
            //    Lexer(LastInput);
            //}

            Console.WriteLine(Solver("+", 2, 3));

            //Console.WriteLine(Test);

            //Lexer();
        }

        static void Error(int Index, char Char)
        {
            Console.WriteLine("Error at " + Index + " (" + Char + ")");
        }
        static void Lexer(string Input)
        {

            char[] InputCharArray = Input.ToCharArray();
            //The array of the input string.
            string Operand1 = "";
            //The first operand of the current eqation.
            string Operator = "";
            //The operator of the current equation
            string Operand2 = "";
            //The second operand of the current eqation.
            float TempTotal;
            //The current result of the equation. Results from the solver method are applied to this value.
            string CurrentValue = "";
            //The current numerical value from the array.
            int InputCharArrayIndex = 0;
            //The current index that the Lexer is working at.
            char CurrentChar = 'n';
            //The current char at the index of the lexer.
            bool Lexing = true;
            //Controls the main while loop.

            while (Lexing == true) //MAIN LEXER WHILE LOOP.
            {
                CurrentChar = InputCharArray[InputCharArrayIndex];

                if (InputCharArrayIndex == InputCharArray.Length) // Check if last in array.
                {
                    Lexing = false;
                }

                
                InputCharArrayIndex++;
            }
        }
        static float Solver(string Op, float Val1, float Val2)
        {
            float Output;
        //Operator switchcase
        switch (Op)
            {
                default:
                    {
                        Output = 0;
                        Console.WriteLine("Invalid Operator : " + Op + ".");
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
            

    }
}
