

using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.Generic;

namespace Infrastruture.PostgreRepository.ProductRepository
{
    public class ProductPostgreRepository : IProductRepository
    {
        private readonly ServiceProposalContext _serviceProposalContext;
        private int _codeReturnDatabase = 0;
        public ProductPostgreRepository(ServiceProposalContext ctx) => this._serviceProposalContext = ctx;

        public async Task<bool> Delete(Guid productId)
        {
            try
            {
                Product deleteProduct = this._serviceProposalContext.Products.FirstOrDefault(q => q.ProductId == productId);
                if (deleteProduct == null)
                    throw new EntityNotFoundException($"{deleteProduct.Name} not Found");

                this._serviceProposalContext.Products.Remove(deleteProduct);
                int returnDbChange = await this._serviceProposalContext.SaveChangesAsync();

                if (returnDbChange == 0)
                    throw new EntityNotFoundException($"Error: Can not deleted {deleteProduct.Name}");

                bool valueBoolReturn = returnDbChange > _codeReturnDatabase;
                return valueBoolReturn;
            }
            catch (DbUpdateException dbEx)
            {
                this._serviceProposalContext.Entry(productId).State = EntityState.Detached;

                if (dbEx.InnerException is PostgresException pgEx)
                    throw new EntityNotFoundException($"Erro to delete customer: {pgEx.Detail}");

                throw new EntityNotFoundException(dbEx.Message);
            }
        }

        public async Task<List<Product>> FindAll()
        {
            try
            {
                List<Product> productList = await this._serviceProposalContext.Products
                    .ToListAsync();
                return productList;
            }
            catch
            {
                throw new Exception("Can not possible to find all Products");
            }
        }

        public async Task<Product> FindById(Guid productId)
        {
            try
            {

                Product product = await this._serviceProposalContext.Products
                    .FirstOrDefaultAsync(c => c.ProductId == productId);
                return product;
            }
            catch
            {
                throw new Exception($"Can not possible to find Product by unique identifier");
            }
        }

        public async Task<List<Product>> FindByProductType(Guid productTypeId)
        {
            try
            {

                List<Product> products = await this._serviceProposalContext.Products
                    .Where(c => c.ProductTypeId == productTypeId).ToListAsync();
                return products;
            }
            catch
            {
                throw new Exception($"Can not possible to find Product by Product Type unique identifier");
            }
        }

        public async Task<bool> Insert(Product product)
        {
            try
            {
                this._serviceProposalContext.Products.Add(product);
                int returnDbChange = await this._serviceProposalContext.SaveChangesAsync();


                if (returnDbChange <= _codeReturnDatabase)
                {
                    throw new InsertEntityException($"Error: Can not Insert {product.Name}");
                }
                bool valueBoolReturn = returnDbChange > _codeReturnDatabase;
                return valueBoolReturn;

            }
            catch (DbUpdateException dbEx)
            {
                this._serviceProposalContext.Entry(product).State = EntityState.Detached;

                if (dbEx.InnerException is PostgresException pgEx)
                    throw new InsertEntityException($"Error: Can not Insert {pgEx.Detail}");

                throw new InsertEntityException(dbEx.Message);
            }
        }

        public async Task<bool> Update(Product product)
        {
            try
            {
                Product OldProduct = this._serviceProposalContext.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
                if (OldProduct == null)
                    throw new EntityNotFoundException($"{product.Name} not Found!");

                this._serviceProposalContext.Products.Entry(OldProduct).CurrentValues.SetValues(product);
                int returnDbChange = await this._serviceProposalContext.SaveChangesAsync();

                if (returnDbChange == _codeReturnDatabase)
                    throw new UpdateEntityException($"Error: Can not Update {product.Name}");

                bool valueBoolReturn = returnDbChange > _codeReturnDatabase;
                return valueBoolReturn;
            }
            catch (DbUpdateException dbEx)
            {
                this._serviceProposalContext.Entry(product).State = EntityState.Detached;

                if (dbEx.InnerException is PostgresException pgEx)
                    throw new InsertEntityException($"Error: Can not Update {pgEx.Detail}");

                throw new InsertEntityException(dbEx.Message);
            }
        }
    }
}
