using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RhythmsGonnaGetYou
{
    class Program
    {
        //Create a a method to prompt for string
        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine();

            return userInput;
        }

        //Create a method to prompt for integer.
        static int PromptForInteger(string prompt)
        {
            var isThisGoodInput = false;
            do
            {
                var stringInput = PromptForString(prompt);

                int numberInput;
                isThisGoodInput = Int32.TryParse(stringInput, out numberInput);

                if (isThisGoodInput)
                {   //So far so good, store the number in numberInput for use.
                    return numberInput;
                }
                else
                {   //Ooh ooh not a valid input from the user.
                    Console.WriteLine("Sorry, but that is not a valid input - try again.");
                }
            } while (!isThisGoodInput);
            //C# demands we have a return present.
            return 0;
        }

        //Create a method to manage DateTime prompt
        static DateTime PromptForDatetime(string prompt)
        {
            Console.Write(prompt);
            DateTime userInput;
            var isThisGoodInput = DateTime.TryParse(Console.ReadLine(), out userInput);

            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, but that is not a valid input - will use default date.");
                return default(DateTime);
            }
        }

        //Create a method to manage a bool for status of band. isSigned true/false.
        public static bool getBoolInputValue(string IsSigned)
        {
            var IsSignedToLower = IsSigned.ToLower();

            if (IsSigned.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (IsSigned.Equals("n", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else
            {
                return false;
            }
        }



        static void DisplayGreeting()
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("     - - - - - - - - - - - - - - - - - - - - - - - - - - - - \n");
            Console.WriteLine("    |                 You are now logged into                |\n");
            Console.WriteLine("    |                The Rhythm's Gonna Get You              |\n");
            Console.WriteLine("    |              Recording Label and Studio LLC            |\n");
            Console.WriteLine("    |                Artist Management Services              |\n");
            Console.WriteLine("     - - - - - - - - - - - - - MENU - - - - - - - - - - - - -\n");

            Console.WriteLine(" ------------------------ ");
            Console.WriteLine("|BAND SETTINGS:          |");
            Console.WriteLine("|( 1.) Add band          |");
            Console.WriteLine("|( 2.) Add album         |");
            Console.WriteLine("|( 3.) Add song          |");
            Console.WriteLine("|( 4.) Un-sign a band    |"); //(update isSigned to false)
            Console.WriteLine("|( 5.) Re-sign a band    |"); //(update isSigned to true)
            Console.WriteLine(" ------------------------\n");

            Console.WriteLine(" -------------------------------------------------------\n");
            Console.WriteLine("|DATA ON RECORD:                                        |\n");
            Console.WriteLine("|( 6.) View all bands                                   |");
            Console.WriteLine("|( 7.) View all albums by a band                        |");
            Console.WriteLine("|( 8.) View all albums by ReleaseDate                   |");
            Console.WriteLine("|( 9.) View all signed bands                            |");
            Console.WriteLine("|(10.) View all non-signed bands                        |\n");
            Console.WriteLine("|(11.) Quit                                             |\n");
            Console.WriteLine(" -------------------------------------------------------\n");
            Console.WriteLine("Key in the desired action number and press ENTER.\n");
        }



        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to C#");

            var context = new RhythmsGonnaGetYouContext();

            var keepGoing = true;

            while (keepGoing)
            {
                DisplayGreeting();

                Console.Write("(V)iew all bands. (A)dd a new album/song. (U)pdate recording label status. (Q)uit. ");
                var option = Console.ReadLine().ToUpper();

                switch (option)
                {
                    case "Q":
                        keepGoing = false;
                        break;

                    case "V":
                        var bands = context.Bands.Include(band => band.Album);

                        var bandCount = bands.Count();
                        Console.WriteLine($"The database contains this number of bands: {bandCount}");

                        foreach (var band in bands)
                        {
                            if (band.Album == null)
                            {
                                Console.WriteLine($"The band {band.Name} does not have any albums in our database. ");
                            }
                            else
                            {
                                Console.WriteLine($"There is a band named {band.Name} with the following albums: {band.Album}");
                            }

                            //foreach (var song in band.Song)

                        }

                        break;

                    ///////////
                    case "A":
                        Console.Write("What is the name of the new band: ");
                        var name = Console.ReadLine();

                        Console.Write("What is the country of origin: ");
                        var countryOfOrigin = Console.ReadLine();

                        Console.Write("How many members are in this band: ");
                        var numberOfMembers = int.Parse(Console.ReadLine());

                        Console.Write("What is the name of their website: ");
                        var website = Console.ReadLine();

                        Console.Write("What is their genre: ");
                        var genre = Console.ReadLine();

                        //Console.WriteLine("Are they signed to our record label? (T)rue or (F)alse: ");
                        //var answer = Console.ReadLine().ToLower();

                        var newBand = new Band
                        {
                            Name = name,
                            CountryOfOrigin = countryOfOrigin,
                            NumberOfMembers = numberOfMembers,
                            Website = website,
                            Genre = genre
                        };

                        context.Bands.Add(newBand);
                        context.SaveChanges();

                        // if (answer == "T")
                        // {
                        //     IsSigned bool = true;
                        // }
                        // else (answer == "F")
                        // {
                        //     IsSigned bool = false;
                        // }
                        break;

                    case "U":

                        break;
                }
            }
        }


    }
}
