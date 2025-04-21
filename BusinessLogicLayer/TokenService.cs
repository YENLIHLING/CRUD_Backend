using BusinessInterfaceLayer;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using ModelLayer;

namespace BusinessLogicLayer
{
    public class TokenService : ITokenRepository
    {
        private readonly BlockChainContext _blockChainContext;

        public TokenService(BlockChainContext blockChainContext)
        {
            _blockChainContext = blockChainContext;
        }

        private Task<List<TokenModel>> RetrieveTokens()
        {
            return _blockChainContext.token.ToListAsync(); 
        }

        public async Task<List<TokenDataGridModel>> RetrieveTokenTable()
        {
            var tokens = await RetrieveTokens();
             
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

            return tokenDataGridList;
        }

        public async Task<int> AddOrUpdateTokenAsync(TokenModel token)
        {
            var tokenSearchResult = await (from t in _blockChainContext.token
                                    where t.name == token.name
                                    && t.symbol == token.symbol
                                    select t)
                                    .ToListAsync();

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
            
            return await _blockChainContext.SaveChangesAsync(); 
        }
    }
}
