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
    internal class EFUnitOfWork : IUnitOfWork
    {
        private DBContext db;
        private PlayerRepository playerRepository;

        public EFUnitOfWork(DbContextOptions<DBContext> connectionString)
        {
            db = new DBContext(connectionString);
        }
        public IRepository<Player> Players
        {
            get
            {
                if (playerRepository == null)
                    playerRepository = new PlayerRepository(db);
                return playerRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
