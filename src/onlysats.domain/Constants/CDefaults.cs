namespace onlysats.domain.Constants;

public static class CDefaults
{
    public static readonly string NO_FOLDER_NAME = string.Empty;
    public static readonly string DEFAULT_VAULT_NAME = "Default";

    /// <summary>
    /// The name of the default Dapr pubsub configuration to use.
    /// TODO: Probably move this to configuration and create a 
    /// wrapper around DaprClient and then inject that into
    /// all the Domain services
    /// </summary>
    public static readonly string PUB_SUB_NAME = "pubsub";
}