using ComputerListing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerListing.Repository
{
    public interface IUnitOfWork : IDisposable
    {

        public IGenericRepository<Computer> Computers { get; }

        public IGenericRepository<Accessory> Accessories { get; }

        Task Save();

    }
}
