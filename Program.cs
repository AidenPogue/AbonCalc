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

            if (LastInput == "Quit")
            {
                Console.WriteLine("Quitting");
            }
            else
            {
                Console.WriteLine(Lexer(LastInput));
            }
        }

        static string Lexer(string Input)
        {
            //Var Declarations
            {
                string WorkingString = Input; //The string the lexer works with.
                float Operand1;
                string Operator;
                float Operand2;
                bool Lexing = true;
                int Iteration; //The index the lexer is currently working at.
                string CurrentChar; //The char at the current index.
            }
            //Var Declarations

            //MAIN WHILE LOOP
            while (Lexing == true)
            {

            }
        }
    }
}
