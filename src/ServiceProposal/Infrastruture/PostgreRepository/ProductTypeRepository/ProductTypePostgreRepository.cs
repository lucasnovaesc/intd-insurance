

using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Infrastruture.PostgreRepository.ProductTypeRepository
{
    public class ProductTypePostgreRepository : IProductTypeRepository
    {
        private readonly ServiceProposalContext _serviceProposalContext;
        private int _codeReturnDatabase = 0;
        public ProductTypePostgreRepository(ServiceProposalContext ctx) => this._serviceProposalContext = ctx;

        public async Task<bool> Delete(Guid productTypeId)
        {
            try
            {
                ProductType deleteProductType = this._serviceProposalContext.ProductTypes.FirstOrDefault(q => q.ProductTypeId == productTypeId);
                if (deleteProductType == null)
                    throw new EntityNotFoundException($"{deleteProductType.Name} not Found");

                this._serviceProposalContext.ProductTypes.Remove(deleteProductType);
                int returnDbChange = await this._serviceProposalContext.SaveChangesAsync();

                if (returnDbChange == 0)
                    throw new EntityNotFoundException($"Error: Can not deleted {deleteProductType.Name}");

                bool valueBoolReturn = returnDbChange > _codeReturnDatabase;
                return valueBoolReturn;
            }
            catch (DbUpdateException dbEx)
            {
                this._serviceProposalContext.Entry(productTypeId).State = EntityState.Detached;

                if (dbEx.InnerException is PostgresException pgEx)
                    throw new EntityNotFoundException($"Erro to delete customer: {pgEx.Detail}");

                throw new EntityNotFoundException(dbEx.Message);
            }
        }

        public async Task<List<ProductType>> FindAll()
        {
            try
            {
                List<ProductType> productTypeList = await this._serviceProposalContext.ProductTypes
                    .ToListAsync();
                return productTypeList;
            }
            catch
            {
                throw new Exception("Can not possible to find all Product Types");
            }
        }

        public async Task<ProductType> FindById(Guid productTypeId)
        {
            try
            {

                ProductType productType = await this._serviceProposalContext.ProductTypes
                    .FirstOrDefaultAsync(c => c.ProductTypeId == productTypeId);
                return productType;
            }
            catch
            {
                throw new Exception($"Can not possible to find Product Type by unique identifier");
            }
        }

        public async Task<bool> Insert(ProductType productType)
        {
            try
            {
                this._serviceProposalContext.ProductTypes.Add(productType);
                int returnDbChange = await this._serviceProposalContext.SaveChangesAsync();


                if (returnDbChange <= _codeReturnDatabase)
                {
                    throw new InsertEntityException($"Error: Can not Insert {productType.Name}");
                }
                bool valueBoolReturn = returnDbChange > _codeReturnDatabase;
                return valueBoolReturn;

            }
            catch (DbUpdateException dbEx)
            {
                this._serviceProposalContext.Entry(productType).State = EntityState.Detached;

                if (dbEx.InnerException is PostgresException pgEx)
                    throw new InsertEntityException($"Error: Can not Insert {pgEx.Detail}");

                throw new InsertEntityException(dbEx.Message);
            }
        }

        public async Task<bool> Update(ProductType productType)
        {
            try
            {
                ProductType OldProductType = this._serviceProposalContext.ProductTypes.FirstOrDefault(x => x.ProductTypeId == productType.ProductTypeId);
                if (OldProductType == null)
                    throw new EntityNotFoundException($"{productType.Name} not Found!");

                this._serviceProposalContext.ProductTypes.Entry(OldProductType).CurrentValues.SetValues(productType);
                int returnDbChange = await this._serviceProposalContext.SaveChangesAsync();

                if (returnDbChange == _codeReturnDatabase)
                    throw new UpdateEntityException($"Error: Can not Update {productType.Name}");

                bool valueBoolReturn = returnDbChange > _codeReturnDatabase;
                return valueBoolReturn;
            }
            catch (DbUpdateException dbEx)
            {
                this._serviceProposalContext.Entry(productType).State = EntityState.Detached;

                if (dbEx.InnerException is PostgresException pgEx)
                    throw new InsertEntityException($"Error: Can not Update {pgEx.Detail}");

                throw new InsertEntityException(dbEx.Message);
            }
        }
    }
}
