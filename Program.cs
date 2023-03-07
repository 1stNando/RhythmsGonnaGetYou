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

        //Method to call to display welcome greeting.
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

            Console.WriteLine(" -------------------------------------\n");
            Console.WriteLine("|DATA ON RECORD:                      |\n");
            Console.WriteLine("|( 6.) View all bands                 |");
            Console.WriteLine("|( 7.) View all albums by a band      |");
            Console.WriteLine("|( 8.) View all albums by ReleaseDate |");
            Console.WriteLine("|( 9.) View all signed bands          |");
            Console.WriteLine("|(10.) View all non-signed bands      |\n");
            Console.WriteLine("|(11.) Quit                           |\n");
            Console.WriteLine(" -------------------------------------\n");
            Console.WriteLine("Key in the desired action number and press ENTER.\n");
        }



        static void Main(string[] args)
        {
            var context = new RhythmsGonnaGetYouContext();

            var keepGoing = true;

            while (keepGoing)
            {
                DisplayGreeting();

                var menuOption = PromptForInteger("> : ");

                switch (menuOption)
                {
                    case 11:
                        keepGoing = false;
                        break;

                    case 111:
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

                    case 1: //Add band.
                        var nameToSearch = PromptForString("What is the name of the new band: ");

                        var existingBand = context.Bands.FirstOrDefault(Bands => Bands.Name == nameToSearch);

                        if (existingBand != null)
                        {
                            Console.WriteLine($"{nameToSearch} already exists in our database.\nMake sure you spelled it correctly. ");
                        }

                        else //Add the new band information.
                        {
                            Console.WriteLine($"Name of the new band: {nameToSearch} ");
                            var bandName = nameToSearch;

                            var countryOfOrigin = PromptForString("\nWhat is the country of origin: \n");

                            Console.WriteLine("How many members are in this band: \n");
                            var numberOfMembers = int.Parse(Console.ReadLine());

                            var website = PromptForString("What is the name of their website: ");

                            var genre = PromptForString("What genre of music: \n");

                            //Boolean condition isSigned t/f
                            Console.WriteLine("Is the band officially signed with our record label? Tyep in Yes or No \n");
                            var isSigned = getBoolInputValue(Console.ReadLine());

                            var contactName = PromptForString("What is the name of their contact: \n");

                            var contactPhoneNumber = PromptForInteger("Enter their phone number: \n");

                            var newBand = new Band
                            {
                                Name = bandName,
                                CountryOfOrigin = countryOfOrigin,
                                NumberOfMembers = numberOfMembers,
                                Website = website,
                                Genre = genre,
                                IsSigned = isSigned,
                                ContactName = contactName,
                                ContactPhoneNumber = contactPhoneNumber
                            };

                            context.Bands.Add(newBand);
                            context.SaveChanges();
                            Console.WriteLine($"Success! Congratulations on adding {bandName} to our record label!");
                        }
                        break;

                    case 2: //Add album.
                        Console.WriteLine("What is the name of the album you want to add? ");
                        var searchAlbums = PromptForString("> : ");

                        var existingAlbum = context.Albums.FirstOrDefault(Albums => Albums.Title == searchAlbums);

                        if (existingAlbum != null)
                        {
                            Console.WriteLine($"{searchAlbums} is already in existence. Please try again. ");
                        }
                        else
                        { //Look for the band name first.
                            var searchBands = PromptForString("Type in the name of the band who made album: \n");

                            var doesBandExist = context.Bands.FirstOrDefault(Bands => Bands.Name == searchBands);

                            if (doesBandExist == null)
                            {
                                Console.WriteLine($"\n {searchBands}\n does not exist. Please add the band first. ");
                            }

                            else
                            {
                                //Create the new album record.
                                Console.WriteLine($"Band name: {searchBands}. \n");

                                Console.WriteLine($"Album name: {searchAlbums} \n");
                                var albumTitle = searchAlbums;

                                var isItExplicit = getBoolInputValue("Is the rating explicit? Type in Yes or No: \n");

                                var dateOfRelease = PromptForDatetime("Type in the album release date: \n");

                                var newAlbum = new Album
                                {
                                    Title = albumTitle,
                                    IsExplicit = isItExplicit,
                                    ReleaseDate = dateOfRelease
                                };

                                //Save the new album to the database
                                context.Albums.Add(newAlbum);
                                context.SaveChanges();
                                Console.WriteLine($"\nYour new album titled {albumTitle} has been successfully added. ");
                            }
                        }
                        break;

                    case 3:
                        //First search the database for the song name.
                        Console.WriteLine("What is the title of the song you are adding : \n");
                        var searchSong = PromptForString("> : ");

                        var doesSongExist = context.Songs.FirstOrDefault(Songs => Songs.Title == searchSong);

                        //Then if the song already exists in the database display message.
                        if (doesSongExist != null)
                        {
                            Console.WriteLine($"We found that song titled {doesSongExist} is already in the database. Please try a different song name. \n");
                        }
                        else
                        {
                            //Now we need to search for the name of the album this song is from.
                            Console.WriteLine("Type in the name of the album this song is in: \n");

                            var lookInAlbums = PromptForString("> : ");
                            var foundExistingAlbum = context.Albums.FirstOrDefault(Albums => Albums.Title == lookInAlbums);

                            //If it turns out that the album corresponding to the song you want to add does not currently exist in the database, then display a message to the user. 
                            if (foundExistingAlbum == null)
                            {
                                Console.WriteLine($"\n{lookInAlbums} album does not exist in the database. Please add it first. \n");
                            }
                            else
                            {
                                //New song to be added.
                                Console.WriteLine($"Album Title: {lookInAlbums} \n");
                                Console.WriteLine($"Song Title: {searchSong} \n");
                                //Prompt user for the details of the song.
                                Console.WriteLine("Type in the track number: \n");
                                var trackNumber = int.Parse(Console.ReadLine());

                                var lengthOfTrack = PromptForString("Type in the length of the track - in the format: [00:00:00] \n");

                                //Create new instance of a song.
                                var newSong = new Song
                                {
                                    TrackNumber = trackNumber,
                                    Title = searchSong,
                                    Duration = lengthOfTrack
                                };

                                //Save the new song to the database.
                                context.Songs.Add(newSong);
                                context.SaveChanges();

                                Console.WriteLine($"You have successfully added song, titled {searchSong}, to the database. \n");
                            }
                        }
                        break;

                    case 4:
                        //


                        break;
                }
            }
        }


    }
}
