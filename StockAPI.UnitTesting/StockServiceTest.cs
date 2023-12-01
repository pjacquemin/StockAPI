using Moq;
using StockAPI.Interfaces;
using StockAPI.Logic.StockService;
using System.Web.Http;

namespace StockAPI.UnitTesting
{
    public class StockServiceTest
    {
        private readonly StockService _stockService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IStockRepository> _stockRepositoryMock;

        public StockServiceTest()
        {
            _stockRepositoryMock = new Mock<IStockRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(r => r.StockRepository).Returns(_stockRepositoryMock.Object);
            _stockService = new StockService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async void Test_add_product_to_stock()
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

            MoveResult result = await _stockService.MoveProduct(stockId, productId, MoveTypeEnum.Add, quantity);

            Assert.Equal(26, result.NewQuantity);
        }

        [Fact]
        public async void Test_take_product_from_stock()
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

            MoveResult result = await _stockService.MoveProduct(stockId, productId, MoveTypeEnum.Take, quantity);

            Assert.Equal(14, result.NewQuantity);
        }

        [Fact]
        public void Test_move_quantity_less_than_0()
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

            Func<Task> act = () => _stockService.MoveProduct(stockId, productId, MoveTypeEnum.Take, quantity);

            var exception = Assert.ThrowsAsync<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public void Test_new_quantity_less_than_0()
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

            Func<Task> act = () => _stockService.MoveProduct(stockId, productId, MoveTypeEnum.Take, quantity);

            var exception = Assert.ThrowsAsync<InvalidOperationException>(act);
        }

        [Fact]
        public void Test_product_not_found()
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

            Func<Task> act = () => _stockService.MoveProduct(stockId, productId, MoveTypeEnum.Take, quantity);

            var exception = Assert.ThrowsAsync<HttpResponseException>(act);
        }

        [Fact]
        public void Test_stock_not_existing()
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

            Func<Task> act = () => _stockService.MoveProduct(stockId, productId, MoveTypeEnum.Take, quantity);

            var exception = Assert.ThrowsAsync<HttpResponseException>(act);
        }

    }
}