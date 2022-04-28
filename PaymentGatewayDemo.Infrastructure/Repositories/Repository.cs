using Microsoft.Extensions.Logging;
using PaymentGatewayDemo.Core.Exceptions;
using PaymentGatewayDemo.Core.Repositories.Interfaces;
using PaymentGatewayDemo.Infrastructure.Data.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayDemo.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly PaymentGatewayDemoContext _paymentContext;
        private readonly ILogger<Repository<T>> _logger;
        public Repository(PaymentGatewayDemoContext paymentContext,ILogger<Repository<T>> logger)
        {
            _paymentContext = paymentContext;
            _logger = logger;
        }
        public async Task<T> AddAsync(T entity)
        {
   
                await _paymentContext.Set<T>().AddAsync(entity);
                await _paymentContext.SaveChangesAsync();
                return entity;   
            
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
