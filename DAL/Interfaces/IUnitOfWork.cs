using DAL.Interfaces;
using System;
using TournamentPlanner.DAL.Entities;

namespace TournamentPlanner.DAL.Interfaces
{
    public interface IUnitOfWork //: IDisposable
    {
        IPlayerRepository PlayersRepository { get; }
        //void Save();
    }
}
