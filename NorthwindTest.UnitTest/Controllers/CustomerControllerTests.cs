using Microsoft.Extensions.Logging;
using Moq;
using NorthwindTest.Controllers;
using NorthwindTest.DbSet;
using NorthwindTest.Services;

namespace NorthwindTest.UnitTest.Controllers
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private MockRepository mockRepository;

        private Mock<ILogger<CustomerController>> mockLogger;
        private Mock<ICRUDService<Customers>> mockCRUDService;

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockLogger = this.mockRepository.Create<ILogger<CustomerController>>();
            this.mockCRUDService = this.mockRepository.Create<ICRUDService<Customers>>();
        }

        private CustomerController CreateCustomerController()
        {
            return new CustomerController(
                this.mockLogger.Object,
                this.mockCRUDService.Object);
        }

        [Test]
        public async Task Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            mockCRUDService.Setup(x => x.GetAll())
                .ReturnsAsync(new List<Customers>());
            var customerController = this.CreateCustomerController();

            // Act
            var result = await customerController.Get();

            // Assert
            Assert.That(result.Count,Is.EqualTo(0));
            mockCRUDService.Verify(x => x.GetAll());
        }

        [Test]
        public async Task GetCustomerById_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            mockCRUDService.Setup(x=>x.GetById(It.IsAny<string>()))
                .ReturnsAsync(new Customers());
            var customerController = this.CreateCustomerController();
            string id = "test get customer id";

            // Act
            var result = await customerController.GetCustomerById(
                id);

            // Assert
            Assert.That(result.Value, Is.Not.Null);
            mockCRUDService.Verify(x => x.GetById(It.IsAny<string>()));
        }

        [Test]
        public async Task PostCustomer_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            mockCRUDService.Setup(x => x.Create(It.IsAny<Customers>()))
                .ReturnsAsync(true);
            var customerController = this.CreateCustomerController();
            Customers customer = new();

            // Act
            var result = await customerController.PostCustomer(
                customer);

            // Assert
            Assert.That(result, Is.Not.Null);
            mockCRUDService.Verify(x => x.Create(It.IsAny<Customers>()));
        }

        [Test]
        public async Task PutCustomer_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            mockCRUDService.Setup(x => x.Update(It.IsAny<Customers>()))
                .ReturnsAsync(true);
            var customerController = this.CreateCustomerController();
            string id = "want to test id";
            Customers customer = new()
            {
                CustomerID = id,
            };

            // Act
            var result = await customerController.PutCustomer(
                id,
                customer);

            // Assert
            Assert.That(result,Is.Not.Null);
            mockCRUDService.Verify(x => x.Update(It.IsAny<Customers>()));
        }

        [Test]
        public async Task DeleteCustomer_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            mockCRUDService.Setup(x => x.GetById(It.IsAny<string>()))
                .ReturnsAsync(new Customers());
            mockCRUDService.Setup(x=>x.DeleteById(It.IsAny<string>()))
                .ReturnsAsync(true);
            var customerController = this.CreateCustomerController();
            string id = "want to test id";

            // Act
            var result = await customerController.DeleteCustomer(
                id);

            // Assert
            Assert.That(result, Is.Not.Null);
            mockCRUDService.Verify(x => x.GetById(It.IsAny<string>()));
            mockCRUDService.Verify(x => x.DeleteById(It.IsAny<string>()));
        }
    }
}
