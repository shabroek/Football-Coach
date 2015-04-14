using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FootballCoach.Model;
using Newtonsoft.Json;

namespace FootballCoach.Http
{
    public class JsonNetFootballService : IFootballService
    {
        private const string ServiceUrl = "http://localhost:8000/odata";
        private readonly HttpClient _client = new HttpClient();

        public async Task<IEnumerable<Match>> GetAllMatches()
        {
            HttpResponseMessage response = await _client.GetAsync(ServiceUrl + "/Matches");
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Match[]>(jsonString);
        }

        public async Task<IEnumerable<Player>> GetAllPlayers()
        {
            HttpResponseMessage response = await _client.GetAsync(ServiceUrl + "/Players");
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Player[]>(jsonString);
        }

        public async Task<Player> Add(Player expense)
        {
            var jsonString = Serialize(expense);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            await _client.PostAsync(ServiceUrl, content);
            return expense;
        }

        public async Task Delete(int id)
        {
            await _client.DeleteAsync(String.Format("{0}({1})", ServiceUrl, id.ToString()));
        }

        public async Task Update(Player expense)
        {
            var jsonString = Serialize(expense);
            var content = new StringContent(jsonString,
                Encoding.UTF8, "application/json");
            await _client.PutAsync(String.Format("{0}({1})", ServiceUrl, expense.PlayerId), content);
        }

        private string Serialize(Player expense)
        {
            return JsonConvert.SerializeObject(expense);
        }
    }
}