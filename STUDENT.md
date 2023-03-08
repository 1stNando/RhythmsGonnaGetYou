# RhythmsGonnaGetYou

## To push to github

If you selected `Y` during `dotnet new` you can add your changes and push to github with:

1. `git add .`
1. `git commit -m "Here I describe my changes"`
1. `git push`

Otherwise do the following _ONCE_ before using the steps above.

1. `git init`
1. `git add .`
1. `git commit -m "Initial Commit"`
1. `sdg hubCreate`
1. `git push -u origin HEAD`

## PROTIP:

When you are complete with the project and have turned it in to your instructor, update README.md with details about the assignment.

Notes:

This was how I approached my first time attempt.
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
                        }
                        break;
