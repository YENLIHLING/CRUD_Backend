using ModelLayer;

namespace BusinessInterfaceLayer
{
    public interface ITokenRepository
    {
        DateTime LastAccessed { get; set; }
        string LastAccessedBy { get; set; }
        List<TokenModel> RetrieveTokens();
        Task<int> AddOrUpdateTokenAsync(TokenModel token);
        Task<List<TokenDataGridModel>> RetrieveTokenTable();
        Task<string> RetrieveTokenChart();
    }
}
