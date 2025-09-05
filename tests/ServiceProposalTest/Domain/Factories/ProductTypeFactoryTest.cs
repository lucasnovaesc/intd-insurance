

using Domain.Exceptions;
using Domain.Factories;

namespace ServiceProposalTest.Domain.Factories
{
    public class ProductTypeFactoryTest
    {
        private readonly ProductTypeFactory _factory;

        public ProductTypeFactoryTest()
        {
            _factory = new ProductTypeFactory();
        }

        [Fact]
        public void MakeNew_ShouldCreateProductType_WithValidData()
        {
            // Arrange
            var name = "Eletrônicos";
            var description = "Produtos eletrônicos em geral";

            // Act
            var productType = _factory.MakeNew(name, description);

            // Assert
            Assert.NotNull(productType);
            Assert.Equal(name, productType.Name);
            Assert.Equal(description, productType.Description);
            Assert.NotEqual(Guid.Empty, productType.ProductTypeId);
        }

        [Fact]
        public void MakeNew_ShouldThrow_WhenNameIsEmpty()
        {
            var description = "Categoria sem nome";

            Assert.Throws<EntityPropertyIncorrect>(() =>
                _factory.MakeNew("", description)
            );
        }

        [Fact]
        public void MakeNew_ShouldThrow_WhenDescriptionIsEmpty()
        {
            var name = "Categoria sem descrição";

            Assert.Throws<EntityPropertyIncorrect>(() =>
                _factory.MakeNew(name, "")
            );
        }

        [Fact]
        public void MakeExistent_ShouldCreateProductType_WithValidData()
        {
            var id = Guid.NewGuid();
            var name = "Móveis";
            var description = "Cadeiras, mesas, armários";
            var creationDate = DateTime.Now.AddDays(-10);

            var productType = _factory.MakeExistent(id, name, description, creationDate);

            Assert.NotNull(productType);
            Assert.Equal(id, productType.ProductTypeId);
            Assert.Equal(name, productType.Name);
            Assert.Equal(description, productType.Description);
            Assert.Equal(creationDate, productType.DateCreation);
        }

        [Fact]
        public void MakeExistent_ShouldThrow_WhenIdIsEmpty()
        {
            var name = "Categoria inválida";
            var description = "Teste";
            var creationDate = DateTime.Now;

            Assert.Throws<EntityPropertyIncorrect>(() =>
                _factory.MakeExistent(Guid.Empty, name, description, creationDate)
            );
        }
    }
}
