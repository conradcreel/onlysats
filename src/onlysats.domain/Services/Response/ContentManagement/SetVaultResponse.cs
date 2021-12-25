using onlysats.domain.Models;
using onlysats.domain.Services.Response;

namespace onlysats.domain.Services.ContentManagement;

public class SetVaultResponse : ResponseBase
{
    public VaultModel Vault {get;set;}
}