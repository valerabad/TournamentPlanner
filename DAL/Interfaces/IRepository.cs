using System;
using System.Collections.Generic;

namespace TournamentPlanner.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Delete(T item);
        //IEnumerable<T> Find(Func<T, Boolean> predicate);
    }
}
