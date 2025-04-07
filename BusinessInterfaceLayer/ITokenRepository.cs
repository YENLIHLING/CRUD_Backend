using ModelLayer;

namespace BusinessInterfaceLayer
{
    public interface ITokenRepository
    {
        Task<int> AddOrUpdateTokenAsync(TokenModel token);
        Task<List<TokenDataGridModel>> RetrieveTokenTable();
    }
}
