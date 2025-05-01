using NUnit.Framework;
using w10d4_CICD.Models;
using w10d4_CICD.Services;
using System.Linq;

namespace ProductApi.Tests.Services
{
    public class ProductServiceTests
    {
        private ProductService _service;

        [SetUp]
        public void Setup()
        {
            _service = new ProductService();
        }

        [Test]
        public void GetAll_ShouldReturnInitialProducts()
        {
            var products = _service.GetAll();
            Assert.That(products.Count(), Is.GreaterThanOrEqualTo(2));
        }

        [Test]
        public void GetById_ShouldReturnCorrectProduct()
        {
            var product = _service.GetById(1);
            Assert.IsNotNull(product);
            Assert.That(product.Name, Is.EqualTo("Laptop"));
        }

        [Test]
        public void GetById_InvalidId_ShouldReturnNull()
        {
            var product = _service.GetById(999);
            Assert.IsNull(product);
        }

        [Test]
        public void Add_ShouldAddNewProductWithIncrementedId()
        {
            var newProduct = new Product { Name = "Tablet", Price = 300 };
            _service.Add(newProduct);

            var addedProduct = _service.GetById(newProduct.Id);
            Assert.IsNotNull(addedProduct);
            Assert.That(addedProduct.Name, Is.EqualTo("Tablet"));
        }
    }
}
