
using Domain.Exceptions;
using Domain.Factories;

namespace ServiceProposalTest.Domain.Factories
{
    public class ConstumerFactoryTest
    {
        private readonly CustomerFactory _factory;

        public ConstumerFactoryTest()
        {
            _factory = new CustomerFactory();
        }

        [Fact]
        public void MakeNew_ShouldCreateCustomer_WithValidData()
        {
            // Arrange
            var name = "Carlos";
            var email = "carlos@email.com";
            var phone = "11999999999";
            var address = "Rua Nova";

            // Act
            var customer = _factory.MakeNew(name, email, phone, address);

            // Assert
            Assert.NotNull(customer);
            Assert.Equal(name, customer.Name);
            Assert.Equal(email, customer.Email);
            Assert.Equal(phone, customer.PhoneNumber);
            Assert.Equal(address, customer.Address);
            Assert.NotEqual(Guid.Empty, customer.CustomerId);
        }

        [Fact]
        public void MakeNew_ShouldThrow_WhenEmailIsInvalid()
        {
            // Arrange
            var name = "João";
            var email = "invalid-email";
            var phone = "11987654321";
            var address = "Rua XPTO";

            // Act & Assert
            Assert.Throws<EntityPropertyIncorrect>(() =>
                _factory.MakeNew(name, email, phone, address)
            );
        }

        [Fact]
        public void MakeNew_ShouldThrow_WhenPhoneNumberIsInvalid()
        {
            // Arrange
            var name = "Maria";
            var email = "maria@email.com";
            var phone = "123"; // muito curto
            var address = "Rua ABC";

            // Act & Assert
            Assert.Throws<EntityPropertyIncorrect>(() =>
                _factory.MakeNew(name, email, phone, address)
            );
        }

        [Fact]
        public void MakeExistent_ShouldCreateCustomer_WithValidData()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "José";
            var email = "jose@email.com";
            var phone = "11912345678";
            var address = "Rua Testada";
            var creationDate = DateTime.Now.AddDays(-1);

            // Act
            var customer = _factory.MakeExistent(id, name, email, phone, address, creationDate);

            // Assert
            Assert.NotNull(customer);
            Assert.Equal(id, customer.CustomerId);
            Assert.Equal(name, customer.Name);
            Assert.Equal(email, customer.Email);
            Assert.Equal(phone, customer.PhoneNumber);
            Assert.Equal(address, customer.Address);
            Assert.Equal(creationDate, customer.DateCreation);
        }

        [Fact]
        public void MakeExistent_ShouldThrow_WhenIdIsEmpty()
        {
            // Arrange
            var name = "Carlos";
            var email = "carlos@email.com";
            var phone = "11999999999";
            var address = "Rua Nova";
            var creationDate = DateTime.Now.AddDays(-5);

            // Act & Assert
            Assert.Throws<EntityPropertyIncorrect>(() =>
                _factory.MakeExistent(Guid.Empty, name, email, phone, address, creationDate)
            );
        }
    }
}
