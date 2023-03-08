using System;
using System.Collections.Generic;
namespace RhythmsGonnaGetYou
{
    public class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
        public int NumberOfMembers { get; set; }
        public string Website { get; set; }
        public string Genre { get; set; }
        public bool IsSigned { get; set; }
        public string ContactName { get; set; }

        //One-to-Many relation. Album belongs to one band. A band has many albums. In the form of a List<Object> Objects. 
        public List<Album> Albums { get; set; }


    }
}

