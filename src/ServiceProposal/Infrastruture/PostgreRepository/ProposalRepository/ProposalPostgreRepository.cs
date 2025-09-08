

using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Infrastruture.PostgreRepository.ProposalRepository
{
    public class ProposalPostgreRepository : IProposalRepository
    {
        private readonly ServiceProposalContext _serviceProposalContext;
        private int _codeReturnDatabase = 0;
        public ProposalPostgreRepository(ServiceProposalContext ctx) => this._serviceProposalContext = ctx;

        public async Task<bool> Delete(Guid proposalId)
        {
            try
            {
                Proposal deleteProposal = this._serviceProposalContext.Proposals.FirstOrDefault(q => q.ProposalId == proposalId);
                if (deleteProposal == null)
                    throw new EntityNotFoundException($"{deleteProposal.ProposalNumber} not Found");

                this._serviceProposalContext.Proposals.Remove(deleteProposal);
                int returnDbChange = await this._serviceProposalContext.SaveChangesAsync();

                if (returnDbChange == 0)
                    throw new EntityNotFoundException($"Error: Can not deleted {deleteProposal.ProposalNumber}");

                bool valueBoolReturn = returnDbChange > _codeReturnDatabase;
                return valueBoolReturn;
            }
            catch (DbUpdateException dbEx)
            {
                this._serviceProposalContext.Entry(proposalId).State = EntityState.Detached;

                if (dbEx.InnerException is PostgresException pgEx)
                    throw new EntityNotFoundException($"Erro to delete customer: {pgEx.Detail}");

                throw new EntityNotFoundException(dbEx.Message);
            }
        }

        public async Task<List<Proposal>> FindAll()
        {
            try
            {
                List<Proposal> proposalList = await this._serviceProposalContext.Proposals
                    .Include(p => p.ProposalStatus)
                    .Include(p => p.Customer)
                    .Include(p => p.Product)
                    .ToListAsync();
                return proposalList;
            }
            catch
            {
                throw new Exception("Can not possible to find all Proposal");
            }
        }

        public async Task<List<Proposal>> FindByCustomerId(Guid customerId)
        {
            try
            {

                List<Proposal> proposals = await this._serviceProposalContext.Proposals
                    .Include(p => p.ProposalStatus)
                    .Include(p => p.Customer)
                    .Include(p => p.Product)
                    .Where(c => c.CustomerId == customerId).ToListAsync();
                return proposals;
            }
            catch
            {
                throw new Exception($"Can not possible to find Proposal by Customer unique identifier");
            }
        }

        public async Task<Proposal> FindById(Guid proposalId)
        {
            try
            {

               Proposal proposal = await this._serviceProposalContext.Proposals
                    .Include(p => p.ProposalStatus)
                    .Include(p => p.Customer)
                    .Include(p => p.Product)
                    .FirstOrDefaultAsync(c => c.ProposalId == proposalId);
                return proposal;
            }
            catch
            {
                throw new Exception($"Can not possible to find Proposal by unique identifier");
            }
        }

        public async Task<Proposal> FindByProposalNumber(long proposalNumber)
        {
            try
            {

                Proposal proposal = await this._serviceProposalContext.Proposals
                    .Include(p => p.ProposalStatus)
                    .Include(p => p.Customer)
                    .Include(p => p.Product)
                     .FirstOrDefaultAsync(c => c.ProposalNumber == proposalNumber);
                return proposal;
            }
            catch
            {
                throw new Exception($"Can not possible to find Proposal by Proposal Number {proposalNumber}");
            }
        }

        public async Task<bool> Insert(Proposal proposal)
        {
            try
            {
                this._serviceProposalContext.Proposals.Add(proposal);
                int returnDbChange = await this._serviceProposalContext.SaveChangesAsync();


                if (returnDbChange <= _codeReturnDatabase)
                {
                    throw new InsertEntityException($"Error: Can not Insert {proposal.ProposalNumber}");
                }
                bool valueBoolReturn = returnDbChange > _codeReturnDatabase;
                return valueBoolReturn;

            }
            catch (DbUpdateException dbEx)
            {
                this._serviceProposalContext.Entry(proposal).State = EntityState.Detached;

                if (dbEx.InnerException is PostgresException pgEx)
                    throw new InsertEntityException($"Error: Can not Insert {pgEx.Detail}");

                throw new InsertEntityException(dbEx.Message);
            }
        }

        public async Task<bool> Update(Proposal proposal)
        {
            try
            {
                Proposal OldProposal = this._serviceProposalContext.Proposals.FirstOrDefault(x => x.ProposalId == proposal.ProposalId);
                if (OldProposal == null)
                    throw new EntityNotFoundException($"{OldProposal.ProposalNumber} not Found!");

                this._serviceProposalContext.Proposals.Entry(OldProposal).CurrentValues.SetValues(proposal);
                int returnDbChange = await this._serviceProposalContext.SaveChangesAsync();

                if (returnDbChange == _codeReturnDatabase)
                    throw new UpdateEntityException($"Error: Can not Update {proposal.ProposalNumber}");

                bool valueBoolReturn = returnDbChange > _codeReturnDatabase;
                return valueBoolReturn;
            }
            catch (DbUpdateException dbEx)
            {
                this._serviceProposalContext.Entry(proposal).State = EntityState.Detached;

                if (dbEx.InnerException is PostgresException pgEx)
                    throw new InsertEntityException($"Error: Can not Update {pgEx.Detail}");

                throw new InsertEntityException(dbEx.Message);
            }
        }
    }
}
