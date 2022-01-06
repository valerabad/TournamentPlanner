using System;
using TournamentPlanner.DAL.Entities;

namespace TournamentPlanner.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Player> Players { get; }
        void Save();
    }
}
