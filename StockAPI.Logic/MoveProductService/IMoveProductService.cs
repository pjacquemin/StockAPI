
namespace StockAPI.Logic.MoveProductService
{
    public interface IMoveProductService
    {
        Task<MoveResult> MoveProduct(int stockId, int productId, MoveTypeEnum moveType, int quantity);
    }
}
