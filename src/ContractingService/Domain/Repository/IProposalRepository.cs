using Domain.Entities;

namespace Domain.Repository
{
    public interface IProposalRepository
    {
        Task<bool> Insert(Proposal proposal);
        Task<Proposal> FindById(Guid proposalId);
        Task<List<Proposal>> FindAll();
        Task<List<Proposal>> FindByCustomerId(Guid customerId);
        Task<Proposal> FindByProposalNumber(long proposalNumber);
    }
}﻿
