using onlysats.domain.Models;
using onlysats.domain.Services.Response;

namespace onlysats.domain.Services.Response.ContentManagement;

public class SetVaultResponse : ResponseBase
{
    public VaultModel Vault {get;set;}
}