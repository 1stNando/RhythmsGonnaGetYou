using System;

namespace RhythmsGonnaGetYou
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to C#");

            var context = new RhythmsGonnaGetYouContext();

            var keepGoing = true;

            while (keepGoing)
            {
                Console.Write("(V)iew all bands. (A)dd a new album/song. (U)pdate recording label status. (Q)uit. ");

                var option = Console.ReadLine().ToUpper();

                switch (option)
                {
                    case "Q":
                        keepGoing = false;
                        break;

                    case "V":

                        break;

                    ///////////
                    case "A":

                        break;

                    case "U":

                        break;
                }
            }
        }
    }
}
