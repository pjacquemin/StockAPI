using StockAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StockAPI.Logic.StockService
{
    public class StockService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StockService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MoveResult> MoveProduct(int stockId, int productId, MoveTypeEnum moveType, int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException("quantity", "Quantity should be greater or equal to 0");
            }

            MoveResult result = new MoveResult { 
                OldQuantity = quantity,
                NewQuantity = quantity,
            };

            var stockModel = await _unitOfWork.StockRepository.GetStockById(stockId);
            var productStockModel = stockModel.ProductStocks.First(ps => ps.ProductId == productId);

            if (productStockModel != null)
            {
                if(moveType == MoveTypeEnum.Add)
                {
                    productStockModel.Quantity += quantity;
                }

                if (moveType == MoveTypeEnum.Take)
                {
                    productStockModel.Quantity -= quantity;
                }

                result.NewQuantity = productStockModel.Quantity;
            } else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (result.NewQuantity < 0)
            {
                throw new InvalidOperationException("New quantity should be greater or equal to 0");
            }

            return result;
        }
    }
}
