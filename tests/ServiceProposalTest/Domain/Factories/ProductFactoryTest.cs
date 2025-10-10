
using Domain.Exceptions;
using Domain.Factories;

namespace ServiceProposalTest.Domain.Factories
{
    public class ProductFactoryTest
    {
        public class ProductFactoryTests
        {
            private readonly ProductFactory _factory;

            public ProductFactoryTests()
            {
                _factory = new ProductFactory();
            }

            [Fact]
            public void MakeNew_ShouldCreateProduct_WithValidData()
            {
                // Arrange
                var name = "Notebook Dell";
                var description = "Notebook para desenvolvimento";
                var productTypeId = Guid.NewGuid();
                var price = 4500.00m;

                // Act
                var product = _factory.MakeNew(name, description, productTypeId, price);

                // Assert
                Assert.NotNull(product);
                Assert.Equal(name, product.Name);
                Assert.Equal(description, product.Description);
                Assert.Equal(productTypeId, product.ProductTypeId);
                Assert.Equal(price, product.Price);
                Assert.NotEqual(Guid.Empty, product.ProductId);
            }

            [Fact]
            public void MakeNew_ShouldThrow_WhenNameIsEmpty()
            {
                var description = "Produto sem nome";
                var productTypeId = Guid.NewGuid();
                var price = 100.00m;

                Assert.Throws<EntityPropertyIncorrect>(() =>
                    _factory.MakeNew("", description, productTypeId, price)
                );
            }

            [Fact]
            public void MakeNew_ShouldThrow_WhenDescriptionIsEmpty()
            {
                var name = "Produto sem descrição";
                var productTypeId = Guid.NewGuid();
                var price = 100.00m;

                Assert.Throws<EntityPropertyIncorrect>(() =>
                    _factory.MakeNew(name, "", productTypeId, price)
                );
            }

            [Fact]
            public void MakeNew_ShouldThrow_WhenProductTypeIdIsEmpty()
            {
                var name = "Produto teste";
                var description = "Teste";
                var price = 500.00m;

                Assert.Throws<EntityPropertyIncorrect>(() =>
                    _factory.MakeNew(name, description, Guid.Empty, price)
                );
            }

            [Theory]
            [InlineData(0)]
            [InlineData(-10)]
            [InlineData(1000001)]
            public void MakeNew_ShouldThrow_WhenPriceIsOutOfRange(decimal invalidPrice)
            {
                var name = "Produto inválido";
                var description = "Teste preço inválido";
                var productTypeId = Guid.NewGuid();

                Assert.Throws<EntityPropertyIncorrect>(() =>
                    _factory.MakeNew(name, description, productTypeId, invalidPrice)
                );
            }

            [Fact]
            public void MakeExistent_ShouldCreateProduct_WithValidData()
            {
                var id = Guid.NewGuid();
                var name = "Mouse Gamer";
                var description = "Mouse com RGB";
                var productTypeId = Guid.NewGuid();
                var price = 250.00m;
                var creationDate = DateTime.Now.AddDays(-2);

                var product = _factory.MakeExistent(id, name, description, productTypeId, price, creationDate);

                Assert.NotNull(product);
                Assert.Equal(id, product.ProductId);
                Assert.Equal(name, product.Name);
                Assert.Equal(description, product.Description);
                Assert.Equal(productTypeId, product.ProductTypeId);
                Assert.Equal(price, product.Price);
                Assert.Equal(creationDate, product.DateCreation);
            }

            [Fact]
            public void MakeExistent_ShouldThrow_WhenIdIsEmpty()
            {
                var name = "Produto inválido";
                var description = "Teste";
                var productTypeId = Guid.NewGuid();
                var price = 150.00m;
                var creationDate = DateTime.Now;

                Assert.Throws<EntityPropertyIncorrect>(() =>
                    _factory.MakeExistent(Guid.Empty, name, description, productTypeId, price, creationDate)
                );
            }
        }
    }
}
