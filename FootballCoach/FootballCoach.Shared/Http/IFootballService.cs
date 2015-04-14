using System.Collections.Generic;
using System.Threading.Tasks;
using FootballCoach.Model;

namespace FootballCoach.Http
{
    public interface IFootballService
    {
        Task<IEnumerable<Match>> GetAllMatches();
        Task<IEnumerable<Player>> GetAllPlayers();
        Task<Player> Add(Player expense);
        Task Delete(int id);
        Task Update(Player expense);

    }
}