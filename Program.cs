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
                Console.WriteLine(Lexer(LastInput));
            }
        }

        static string Lexer(string Input)
        {
            //Var Declarations
            System.Text.StringBuilder WorkingString = new System.Text.StringBuilder(Input); //The stringbuilder string the lexer works with.
            float Operand1;
            char Operator;
            float Operand2;
            bool Lexing = true;
            int Iteration = 0; //The index the lexer is currently working at.
            char CurrentChar = ' '; //The char at the current index.
            string CurrenrVal;
            bool Finished = false; //Set to true when no valid operators are found. 
            //Var Declarations

            //MAIN WHILE LOOP
            while (Lexing == true)
            {
                CurrentChar = WorkingString[Iteration];
                
                //Check for / and *
                if ((Char.ToString(CurrentChar).Contains("*/+-^")))
                {
                   
                }
                //Check for end
                if (Iteration == WorkingString.Length) { Iteration = 0; };

                //Check if space
                if (CurrentChar == ' ') ;
                {
                    //Iteration++;
                    break;
                }
                if (Finished == true) ;
                {
                    return "San";
                }
                Iteration++;
            }
            return "Sans";
        }
    }
}
