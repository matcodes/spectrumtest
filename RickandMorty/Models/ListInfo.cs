using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RickandMorty.Models
{
    public class ListInfo
    {
        public int Count { get; set; }

        public int Pages { get; set; }

        public string Next { get; set; }

        public string Prev { get; set; }
    }

    public class CharactersList
    {
        [JsonProperty("info")]
        public ListInfo Info { get; set; }

        [JsonProperty("results")]
        public List<Character> Results { get; set; }
    }
}
