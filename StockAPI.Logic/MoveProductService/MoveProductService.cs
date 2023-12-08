using StockAPI.Interfaces;
using System.Net;
using System.Web.Http;

namespace StockAPI.Logic.MoveProductService
{
    public sealed class MoveProductService : IMoveProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private int _stockId { get; set; }
        private int _productId { get; set; }
        private MoveTypeEnum _moveType { get; set; }
        private int _quantity { get; set; }
        private MoveResult _moveResult { get; set; }
        private Models.Stock? _stockModel { get; set; }
        private Models.ProductStock? _productStockModel { get; set; }

        public MoveProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MoveResult> MoveProduct(int stockId, int productId, MoveTypeEnum moveType, int quantity)
        {
            _initAttributes(stockId, productId, moveType, quantity);
            _checkPreConditions();
            _moveQuantity();
            await _unitOfWork.SaveChangesAsync();
            _checkPostConditions();

            return _moveResult;
        }

        private async void _initAttributes(int stockId, int productId, MoveTypeEnum moveType, int quantity)
        {
            _stockId = stockId;
            _productId = productId;
            _moveType = moveType;
            _quantity = quantity;
            _stockModel = await _unitOfWork.StockRepository.GetStockById(_stockId);
            _productStockModel = _stockModel?.ProductStocks.FirstOrDefault(ps => ps.ProductId == _productId);
            _moveResult = new MoveResult();
        }

        private void _checkPreConditions()
        {
            if (_quantity < 0)
            {
                throw new ArgumentOutOfRangeException("quantity", "Quantity should be greater or equal to 0");
            }

            if (_stockModel == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (_productStockModel == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        private MoveResult _moveQuantity()
        {
            _moveResult.OldQuantity = _productStockModel!.Quantity;

            if (_moveType == MoveTypeEnum.Add)
            {
                _productStockModel.Quantity += _quantity;
            }

            if (_moveType == MoveTypeEnum.Take)
            {
                _productStockModel.Quantity -= _quantity;
            }

            _moveResult.NewQuantity = _productStockModel.Quantity;

            return _moveResult;
        }

        private void _checkPostConditions()
        {
            if (_moveResult.NewQuantity < 0)
            {
                throw new InvalidOperationException("New quantity should be greater or equal to 0");
            }
        }
    }
}
