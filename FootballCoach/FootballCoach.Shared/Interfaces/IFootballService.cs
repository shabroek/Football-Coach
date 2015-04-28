using System.Collections.Generic;
using System.Threading.Tasks;
using FootballCoach.Model;

namespace FootballCoach.Interfaces
{
    public interface IFootballService
    {
        Task<IEnumerable<Match>> GetAllMatches();
        Task<IEnumerable<Player>> GetAllPlayers();
        Task<Player> AddPlayer(Player player);
        Task DeletePlayer(int playerId);
        Task UpdatePlayer(Player player);
    }
}
