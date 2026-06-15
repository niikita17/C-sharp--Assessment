using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{

    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        public IAuthRepository Auth { get; }
        Task<int> SaveChangesAsync();
    }
}
