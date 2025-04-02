using BusinessInterfaceLayer;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using ModelLayer;
using MySqlConnector;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class TokenRepository : ITokenRepository
    {
        public DateTime LastAccessed { get; set; }
        public required string LastAccessedBy { get; set; }

        private readonly BlockChainContext _blockChainContext;

        public TokenRepository(BlockChainContext blockChainContext)
        {
            _blockChainContext = blockChainContext;
        }

        public List<TokenModel> RetrieveTokens()
        {
            return _blockChainContext.token.ToList(); 
        }

        public Task<List<TokenDataGridModel>> RetrieveTokenTable()
        {
            var tokens = RetrieveTokens();
             
            var totalOfSupply = tokens.Select(x => x.total_supply).Sum();

            var tokenDataGridList = new List<TokenDataGridModel>();

            tokens.ForEach(x =>
            {
                tokenDataGridList.Add(
                    new TokenDataGridModel { 
                        id = x.id, 
                        symbol = x.symbol,
                        name = x.name,
                        contract_address = x.contract_address,
                        total_holders = x.total_holders,
                        total_supply = x.total_supply,
                        pctg_supply = (float)x.total_supply / totalOfSupply,
                    }); 
            });

            return Task.FromResult(tokenDataGridList); 
        }

        public Task<string> RetrieveTokenChart()
        {
            var tokens = RetrieveTokens();

            var tokenChart = new List<TokenChartModel>();

            tokens.ForEach(x =>
            {
                tokenChart.Add(
                    new TokenChartModel
                    {
                        id = x.id,
                        label = x.name,
                        value = x.total_supply
                    });
            });

            return Task.FromResult(JsonSerializer.Serialize(tokenChart));
        }

        public async Task<int> AddOrUpdateTokenAsync(TokenModel token)
        {
            var tokenSearchResult = (from t in _blockChainContext.token
                                    where t.name == token.name
                                    && t.symbol == token.symbol
                                    select t)
                                    .ToList();

            var tokenModel = new TokenModel
            {
                name = token.name,
                symbol = token.symbol,
                total_supply = token.total_supply,
                contract_address = token.contract_address,
                total_holders = token.total_holders,
            };

            //if token is not found
            if (!tokenSearchResult.Any())
            {
                //add new token
                _blockChainContext.Add<TokenModel>(tokenModel);
            }
            else
            {
                // Retrieve entity by id
                // Answer for question #1
                var entity = _blockChainContext.token.FirstOrDefault(item => item.id == tokenSearchResult.First().id);
                if (entity != null)
                {
                    entity.contract_address = token.contract_address; 
                    entity.total_supply = token.total_supply;
                    entity.total_holders = token.total_holders;
                }
            }
            var result = await Task.Run(() => _blockChainContext.SaveChanges()); 

            return result; 
        }
    }
}
