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

            if (LastInput == "Quit" | LastInput == "quit")
            {
                Console.WriteLine("Quitting");
            }
            else
            {
                Console.WriteLine(ArrayLexer(LastInput));
            }
        }

        static string[] ArrayLexer(string Input)
        {
            int Iteration = 0;
            int CurrentIndexInArray = 0;
            string[] EquationArray = new string[100];
            string CurrentValue = string.Empty;
            char CurrentChar = ' ';

            while (Iteration != Input.Length)
            {
                if (IsOperator(CurrentChar) == true)
                {
                    EquationArray[CurrentIndexInArray] = CurrentValue;
                }
            }
        }
        static bool IsOperator(char Input)
        {
            return Char.ToString(Input).Contains("^*/+-");
        }
 
    }
}
