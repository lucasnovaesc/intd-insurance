

using Domain.Entities;
using Domain.Exceptions;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Infrastructure.PostgreRepositories.ProposalRepository
{
    public class ProposalPostgreRepository : IProposalRepository
    {
        private readonly ServiceContractingContext _serviceContractingContext;
        private int _codeReturnDatabase = 0;
        public ProposalPostgreRepository(ServiceContractingContext ctx) => this._serviceContractingContext = ctx;

        public async Task<List<Proposal>> FindAll()
        {
            try
            {
                List<Proposal> proposalList = await this._serviceContractingContext.Proposals
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

                List<Proposal> proposals = await this._serviceContractingContext.Proposals
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

                Proposal proposal = await this._serviceContractingContext.Proposals
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

                Proposal proposal = await this._serviceContractingContext.Proposals
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
                proposal.DateCreation = DateTime.SpecifyKind(proposal.DateCreation, DateTimeKind.Utc);
                proposal.DateModification = DateTime.SpecifyKind(proposal.DateModification, DateTimeKind.Utc);
                this._serviceContractingContext.Proposals.Add(proposal);
                int returnDbChange = await this._serviceContractingContext.SaveChangesAsync();


                if (returnDbChange <= _codeReturnDatabase)
                {
                    throw new InsertEntityException($"Error: Can not Insert {proposal.ProposalNumber}");
                }
                bool valueBoolReturn = returnDbChange > _codeReturnDatabase;
                return valueBoolReturn;

            }
            catch (DbUpdateException dbEx)
            {
                this._serviceContractingContext.Entry(proposal).State = EntityState.Detached;

                if (dbEx.InnerException is PostgresException pgEx)
                    throw new InsertEntityException($"Error: Can not Insert {pgEx.Detail}");
                Console.WriteLine(dbEx);

                throw new InsertEntityException(dbEx.Message);
            }
        }
    }
}
