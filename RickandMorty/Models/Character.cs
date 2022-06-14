using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RickandMorty.Models
{
    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Species { get; set; }

        public string Type { get; set; }

        public string Gender { get; set; }

        [JsonProperty("origin")]
        public NameUrl Origin { get; set; }

        [JsonProperty("location")]
        public NameUrl Location { get; set; }

        public string Image { get; set; }

        public List<string> Episode { get; set; }

        public string Url { get; set; }

        public DateTime Created { get; set; }
    }


    public class NameUrl
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }
}
