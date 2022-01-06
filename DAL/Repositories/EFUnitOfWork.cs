using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentPlanner.DAL.EF;
using TournamentPlanner.DAL.Entities;
using TournamentPlanner.DAL.Interfaces;
using TournamentPlanner.DAL.Repositories;


namespace DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public IPlayerRepository PlayersRepository { get; }
        public EFUnitOfWork(IPlayerRepository context)
        {
            PlayersRepository = context;
        }

        private bool disposed = false;



    }
}
