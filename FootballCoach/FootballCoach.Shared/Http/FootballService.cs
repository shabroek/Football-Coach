using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using FootballCoach.Model;

namespace FootballCoach.Http
{
    public class FootballService : IFootballService
    {
        private const string ServiceUrl = "http://localhost:8000/odata";
        private readonly HttpClient _client = new HttpClient();

        public async Task<IEnumerable<Match>> GetAllMatches()
        {
            HttpResponseMessage response = await _client.GetAsync(ServiceUrl + "/Matches");
            var jsonSerializer = CreateDataContractJsonSerializer(typeof(Match[]));
            var stream = await response.Content.ReadAsStreamAsync();
            return (Match[])jsonSerializer.ReadObject(stream);
        }

        public async Task<IEnumerable<Player>> GetAllPlayers()
        {
            HttpResponseMessage response = await _client.GetAsync(ServiceUrl + "/Players");
            var jsonSerializer = CreateDataContractJsonSerializer(typeof(Player[]));
            var stream = await response.Content.ReadAsStreamAsync();
            return (Player[])jsonSerializer.ReadObject(stream);
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
            await _client.DeleteAsync(String.Format("{0}({1})"
                    , ServiceUrl, id));
        }

        public async Task Update(Player expense)
        {
            var jsonString = Serialize(expense);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var result = await _client.PutAsync(String
                .Format("{0}/{1}", ServiceUrl, expense.PlayerId), content);
        }

        private static DataContractJsonSerializer CreateDataContractJsonSerializer(Type type)
        {
            const string dateFormat = "yyyy-MM-ddTHH:mm:ss.fffffffZ";
            var settings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat(dateFormat),
                EmitTypeInformation = EmitTypeInformation.AsNeeded,
            };
            var serializer = new DataContractJsonSerializer(type, settings);
            return serializer;
        }

        private string Serialize(Player expense)
        {
            var jsonSerializer = CreateDataContractJsonSerializer(typeof(Player));
            byte[] streamArray = null;
            using (var memoryStream = new MemoryStream())
            {
                jsonSerializer.WriteObject(memoryStream, expense);
                streamArray = memoryStream.ToArray();
            }
            string json = Encoding.UTF8.GetString(streamArray, 0, streamArray.Length);
            return json;
        }
    }
}
