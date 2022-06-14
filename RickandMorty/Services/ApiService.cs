using Newtonsoft.Json;
using RickandMorty.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RickandMorty.Services
{
    public class ApiService
    {
        private readonly IHttpServices httpServices;

        public ApiService()
        {
            httpServices = new HttpServices();
        }


        public async Task<CharactersList> GetAllCharacters(string url)
        {
            CharactersList characters = new CharactersList();
            try
            {
                string response = await httpServices.GetAsync(url);

                characters = JsonConvert.DeserializeObject<CharactersList>(response);

            }
            catch (Exception ex)
            {
            }

            return characters;
        }

        public async Task<Character> GetCharacterById(int characterId)
        {
            Character character = new Character();
            try
            {
                string response = await httpServices.GetAsync($"{Utility.BaseUrl}{characterId}");

                character = JsonConvert.DeserializeObject<Character>(response);

            }
            catch (Exception ex)
            {
            }

            return character;
        }
    }
}
