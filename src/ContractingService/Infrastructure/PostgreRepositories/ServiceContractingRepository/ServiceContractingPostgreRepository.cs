

using Domain.Entities;
using Domain.Exceptions;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Infrastructure.PostgreRepositories.ServiceContractingRepository
{
    public class ServiceContractingPostgreRepository : IServiceContractingRepository
    {
        private readonly ServiceContractingContext _serviceContractingContext;
        private int _codeReturnDatabase = 0;
        public ServiceContractingPostgreRepository(ServiceContractingContext ctx) => this._serviceContractingContext = ctx;

        public async Task<bool> Delete(Guid serviceContractingId)
        {
            try
            {
                ServiceContracting deleteServiceContracting = this._serviceContractingContext.ServiceContractings.FirstOrDefault(q => q.ServiceContractingId == serviceContractingId);
                if (deleteServiceContracting == null)
                    throw new EntityNotFoundException($"{deleteServiceContracting.ServiceContractingId} not Found");

                this._serviceContractingContext.ServiceContractings.Remove(deleteServiceContracting);
                int returnDbChange = await this._serviceContractingContext.SaveChangesAsync();

                if (returnDbChange == 0)
                    throw new EntityNotFoundException($"Error: Can not deleted {deleteServiceContracting.ServiceContractingId}");

                bool valueBoolReturn = returnDbChange > _codeReturnDatabase;
                return valueBoolReturn;
            }
            catch (DbUpdateException dbEx)
            {
                this._serviceContractingContext.Entry(serviceContractingId).State = EntityState.Detached;

                if (dbEx.InnerException is PostgresException pgEx)
                    throw new EntityNotFoundException($"Erro to delete Service Contracting: {pgEx.Detail}");

                throw new EntityNotFoundException(dbEx.Message);
            }
        }

        public async Task<List<ServiceContracting>> FindAll()
        {
            try
            {
                List<ServiceContracting> serviceContractingList = await this._serviceContractingContext.ServiceContractings
                    .ToListAsync();
                return serviceContractingList;
            }
            catch
            {
                throw new Exception("Can not possible to find all Service Contracting");
            }
        }

        public async Task<ServiceContracting> FindById(Guid serviceContractingId)
        {
            try
            {

                ServiceContracting serviceContracting = await this._serviceContractingContext.ServiceContractings
                    .FirstOrDefaultAsync(c => c.ServiceContractingId == serviceContractingId);
                return serviceContracting;
            }
            catch
            {
                throw new Exception($"Can not possible to find Service Contracting by unique identifier");
            }
        }

        public async Task<ServiceContracting> FindByProposalId(Guid proposalId)
        {
            try
            {

                ServiceContracting serviceContracting = await this._serviceContractingContext.ServiceContractings
                    .FirstOrDefaultAsync(c => c.ProposalId == proposalId);
                return serviceContracting;
            }
            catch
            {
                throw new Exception($"Can not possible to find Service Contracting by Proposal unique identifier");
            }
        }

        public async Task<bool> Insert(ServiceContracting serviceContracting)
        {
            try
            {
                this._serviceContractingContext.ServiceContractings.Add(serviceContracting);
                int returnDbChange = await this._serviceContractingContext.SaveChangesAsync();


                if (returnDbChange <= _codeReturnDatabase)
                {
                    throw new InsertEntityException($"Error: Can not Insert {serviceContracting.ServiceContractingId}");
                }
                bool valueBoolReturn = returnDbChange > _codeReturnDatabase;
                return valueBoolReturn;

            }
            catch (DbUpdateException dbEx)
            {
                this._serviceContractingContext.Entry(serviceContracting).State = EntityState.Detached;

                if (dbEx.InnerException is PostgresException pgEx)
                    throw new InsertEntityException($"Error: Can not Insert {pgEx.Detail}");

                throw new InsertEntityException(dbEx.Message);
            }
        }

        public async Task<bool> Update(ServiceContracting serviceContracting)
        {
            try
            {
                ServiceContracting OldServiceContracting = this._serviceContractingContext.ServiceContractings.FirstOrDefault(x => x.ServiceContractingId == serviceContracting.ServiceContractingId);
                if (OldServiceContracting == null)
                    throw new EntityNotFoundException($"{serviceContracting.ServiceContractingId} not Found!");

                this._serviceContractingContext.ServiceContractings.Entry(OldServiceContracting).CurrentValues.SetValues(serviceContracting);
                int returnDbChange = await this._serviceContractingContext.SaveChangesAsync();

                if (returnDbChange == _codeReturnDatabase)
                    throw new UpdateEntityException($"Error: Can not Update {serviceContracting.ServiceContractingId}");

                bool valueBoolReturn = returnDbChange > _codeReturnDatabase;
                return valueBoolReturn;
            }
            catch (DbUpdateException dbEx)
            {
                this._serviceContractingContext.Entry(serviceContracting).State = EntityState.Detached;

                if (dbEx.InnerException is PostgresException pgEx)
                    throw new UpdateEntityException($"Error: Can not Update {pgEx.Detail}");

                throw new UpdateEntityException(dbEx.Message);
            }
        }
    }
}
