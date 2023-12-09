using Moq;
using StockAPI.Interfaces;
using StockAPI.Logic.MoveProductService;
using System.Net;
using System.Web.Http;

namespace StockAPI.UnitTesting
{
    public class MoveProductServiceTest
    {
        private readonly MoveProductService _moveProductService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IStockRepository> _stockRepositoryMock;

        public MoveProductServiceTest()
        {
            _stockRepositoryMock = new Mock<IStockRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(r => r.StockRepository).Returns(_stockRepositoryMock.Object);
            _moveProductService = new MoveProductService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async void Add_product_to_stock()
        {
            _stockRepositoryMock.Setup(s => s.GetStockById(8))
                .ReturnsAsync(() => new Models.Stock {
                    Id = 8,
                    Place = "3rd shelf",
                    ProductStocks = new List<Models.ProductStock>
                    {
                        new Models.ProductStock
                        {
                            ProductId = 15,
                            StockId = 8,
                            Quantity = 20,
                        }
                    }
                });
            int stockId = 8;
            int productId = 15;
            int quantity = 6;

            MoveResult result = await _moveProductService.MoveProduct(stockId, productId, MoveTypeEnum.Add, quantity);

            Assert.Equal(26, result.NewQuantity);
            Assert.Equal(20, result.OldQuantity);
        }

        [Fact]
        public async void Take_product_from_stock()
        {
            _stockRepositoryMock.Setup(s => s.GetStockById(8))
                .ReturnsAsync(() => new Models.Stock
                {
                    Id = 8,
                    Place = "3rd shelf",
                    ProductStocks = new List<Models.ProductStock>
                    {
                        new Models.ProductStock
                        {
                            ProductId = 15,
                            StockId = 8,
                            Quantity = 20,
                        }
                    }
                });
            int stockId = 8;
            int productId = 15;
            int quantity = 6;

            MoveResult result = await _moveProductService.MoveProduct(stockId, productId, MoveTypeEnum.Take, quantity);

            Assert.Equal(14, result.NewQuantity);
        }

        [Fact]
        public async void Take_quantity_less_than_0()
        {
            _stockRepositoryMock.Setup(s => s.GetStockById(8))
                .ReturnsAsync(() => new Models.Stock
                {
                    Id = 8,
                    Place = "3rd shelf",
                    ProductStocks = new List<Models.ProductStock>
                    {
                        new Models.ProductStock
                        {
                            ProductId = 15,
                            StockId = 8,
                            Quantity = 20,
                        }
                    }
                });
            int stockId = 8;
            int productId = 15;
            int quantity = -1;

            Func<Task> act = () => _moveProductService.MoveProduct(stockId, productId, MoveTypeEnum.Take, quantity);

            var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(act);
            Assert.Contains("quantity", exception.ParamName);
            Assert.Contains("Quantity should be greater or equal to 0", exception.Message);
        }

        [Fact]
        public async void Take_quantity_0()
        {
            _stockRepositoryMock.Setup(s => s.GetStockById(8))
                .ReturnsAsync(() => new Models.Stock
                {
                    Id = 8,
                    Place = "3rd shelf",
                    ProductStocks = new List<Models.ProductStock>
                    {
                        new Models.ProductStock
                        {
                            ProductId = 15,
                            StockId = 8,
                            Quantity = 20,
                        }
                    }
                });
            int stockId = 8;
            int productId = 15;
            int quantity = 0;

            MoveResult result = await _moveProductService.MoveProduct(stockId, productId, MoveTypeEnum.Take, quantity);

            Assert.Equal(20, result.NewQuantity);
        }

        [Fact]
        public async void New_stock_quantity_less_than_0()
        {
            _stockRepositoryMock.Setup(s => s.GetStockById(8))
                .ReturnsAsync(() => new Models.Stock
                {
                    Id = 8,
                    Place = "3rd shelf",
                    ProductStocks = new List<Models.ProductStock>
                    {
                        new Models.ProductStock
                        {
                            ProductId = 15,
                            StockId = 8,
                            Quantity = 20,
                        }
                    }
                });
            int stockId = 8;
            int productId = 15;
            int quantity = 21;

            Func<Task> act = () => _moveProductService.MoveProduct(stockId, productId, MoveTypeEnum.Take, quantity);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(act);
            Assert.Contains("New quantity should be greater or equal to 0", exception.Message);
        }

        [Fact]
        public async void New_stock_quantity_equals_0()
        {
            _stockRepositoryMock.Setup(s => s.GetStockById(8))
                .ReturnsAsync(() => new Models.Stock
                {
                    Id = 8,
                    Place = "3rd shelf",
                    ProductStocks = new List<Models.ProductStock>
                    {
                        new Models.ProductStock
                        {
                            ProductId = 15,
                            StockId = 8,
                            Quantity = 20,
                        }
                    }
                });
            int stockId = 8;
            int productId = 15;
            int quantity = 20;

            MoveResult result = await _moveProductService.MoveProduct(stockId, productId, MoveTypeEnum.Take, quantity);

            Assert.Equal(0, result.NewQuantity);
        }

        [Fact]
        public async void Product_not_found()
        {
            _stockRepositoryMock.Setup(s => s.GetStockById(8))
                .ReturnsAsync(() => new Models.Stock
                {
                    Id = 8,
                    Place = "3rd shelf",
                    ProductStocks = new List<Models.ProductStock>
                    {
                        new Models.ProductStock
                        {
                            ProductId = 15,
                            StockId = 8,
                            Quantity = 20,
                        }
                    }
                });
            int stockId = 8;
            int productId = 14;
            int quantity = 21;

            Func<Task> act = () => _moveProductService.MoveProduct(stockId, productId, MoveTypeEnum.Take, quantity);

            var exception = await Assert.ThrowsAsync<HttpResponseException>(act);
            Assert.Equal(HttpStatusCode.NotFound, exception.Response.StatusCode);
        }

        [Fact]
        public async void Stock_not_existing()
        {
            _stockRepositoryMock.Setup(s => s.GetStockById(8))
                .ReturnsAsync(() => new Models.Stock
                {
                    Id = 8,
                    Place = "3rd shelf",
                    ProductStocks = new List<Models.ProductStock>
                    {
                        new Models.ProductStock
                        {
                            ProductId = 15,
                            StockId = 8,
                            Quantity = 20,
                        }
                    }
                });
            int stockId = 7;
            int productId = 14;
            int quantity = 15;

            Func<Task> act = () => _moveProductService.MoveProduct(stockId, productId, MoveTypeEnum.Take, quantity);

            var exception = await Assert.ThrowsAsync<HttpResponseException>(act);
            Assert.Equal(HttpStatusCode.NotFound, exception.Response.StatusCode);
        }

    }
}