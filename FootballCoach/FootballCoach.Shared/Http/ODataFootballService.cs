using System.Collections.Generic;
using System.Threading.Tasks;
using FootballCoach.Interfaces;
using FootballCoach.Model;
using Simple.OData.Client;

namespace FootballCoach.Http
{
    public class ODataFootballService : IFootballService
    {
        private const string ServiceUrl = "http://localhost:8000/odata";
        private readonly ODataClient _client = new ODataClient(ServiceUrl);

        public async Task<IEnumerable<Match>> GetAllMatches()
        {
            return await _client.For<Match>().FindEntriesAsync();
        }

        public async Task<IEnumerable<Player>> GetAllPlayers()
        {
            return await _client.For<Player>().FindEntriesAsync();
        }

        public async Task<Player> AddPlayer(Player player)
        {
            return await _client.For<Player>().Set(player).InsertEntryAsync();
        }

        public async Task DeletePlayer(int playerId)
        {
            await _client.For<Player>().Key(playerId).DeleteEntryAsync();
        }

        public async Task UpdatePlayer(Player player)
        {
            await _client.For<Player>().Key(player.PlayerId).Set(player).UpdateEntryAsync();
        }
    }
}