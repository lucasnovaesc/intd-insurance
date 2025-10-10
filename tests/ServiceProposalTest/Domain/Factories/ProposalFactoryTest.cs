

using Domain.Entities.Enuns;
using Domain.Exceptions;
using Domain.Factories;
using FluentAssertions;

namespace ServiceProposalTest.Domain.Factories
{
    public class ProposalFactoryTest
    {
        private readonly ProposalFactory _factory;

        public ProposalFactoryTest()
        {
            _factory = new ProposalFactory();
        }

        [Fact]
        public void MakeNew_ShouldCreateProposal_WhenValidData()
        {
            // Arrange
            long proposalNumber = 1001;
            Guid productId = Guid.NewGuid();
            Guid customerId = Guid.NewGuid();

            // Act
            var proposal = _factory.MakeNew(proposalNumber, productId, customerId);

            // Assert
            proposal.Should().NotBeNull();
            proposal.ProposalId.Should().NotBe(Guid.Empty);
            proposal.ProposalNumber.Should().Be(proposalNumber);
            proposal.ProductId.Should().Be(productId);
            proposal.CustomerId.Should().Be(customerId);
            proposal.DateCreation.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
            proposal.DateModification.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
            proposal.ProposalStatusId.Should().Be(ProposalStatusEnum.Analysing);
        }

        [Fact]
        public void MakeExistent_ShouldCreateProposal_WhenValidData()
        {
            // Arrange
            Guid proposalId = Guid.NewGuid();
            long proposalNumber = 2002;
            Guid productId = Guid.NewGuid();
            Guid customerId = Guid.NewGuid();
            DateTime creationDate = DateTime.UtcNow.AddDays(-10);
            string status = "Approved";

            // Act
            var proposal = _factory.MakeExistent(proposalId, proposalNumber, productId, customerId, creationDate, status);

            // Assert
            proposal.Should().NotBeNull();
            proposal.ProposalId.Should().Be(proposalId);
            proposal.ProposalNumber.Should().Be(proposalNumber);
            proposal.ProductId.Should().Be(productId);
            proposal.CustomerId.Should().Be(customerId);
            proposal.DateCreation.Should().Be(creationDate);
            proposal.DateModification.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
            proposal.ProposalStatusId.Should().Be(ProposalStatusEnum.Approved);
        }

        [Fact]
        public void MakeExistent_ShouldThrowException_WhenStatusIsInvalid()
        {
            // Arrange
            Guid proposalId = Guid.NewGuid();
            long proposalNumber = 3003;
            Guid productId = Guid.NewGuid();
            Guid customerId = Guid.NewGuid();
            DateTime creationDate = DateTime.UtcNow.AddDays(-5);
            string invalidStatus = "NotAValidStatus";

            // Act
            Action act = () => _factory.MakeExistent(proposalId, proposalNumber, productId, customerId, creationDate, invalidStatus);

            // Assert
            act.Should().Throw<EntityPropertyIncorrect>()
                .WithMessage($"Invalid ProposalStatus: {invalidStatus}");
        }
    }
}
