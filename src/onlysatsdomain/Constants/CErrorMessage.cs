namespace onlysats.domain.Constants
{
    public static class CErrorMessage
    {
        public static readonly string SETUP_WALLET_BAD_REQUEST = "Must supply UserAccountId, Username and xPubKey";

        public static readonly string SETUP_WALLET_CANNOT_CREATE_ACCOUNT = "Unable to create account with BTC Payment Processor";

        public static readonly string SETUP_WALLET_CANNOT_UPDATE_WALLET = "Unable to update BTC payment method";

        public static readonly string SETUP_WALLET_CANNOT_CREATE_WALLET = "Unable to create Wallet in OnlySats";

        public static readonly string SETUP_CREATOR_BAD_REQUEST = "Must supply Authentication information from Identity Provider";

        public static readonly string SETUP_CREATOR_FAIL_USER_ACCOUNT = "Could not create UserAccount";

        public static readonly string SETUP_CREATOR_FAIL = "Could not create Creator";

        public static readonly string SETUP_PATRON_BAD_REQUEST = "Must supply Authentication information from Identity Provider";

        public static readonly string SETUP_PATRON_FAIL_USER_ACCOUNT = "Could not create UserAccount";

        public static readonly string SETUP_PATRON_FAIL = "Could not create Patron";

        public static readonly string UPDATE_CREATOR_SETTINGS_BAD_REQUEST = "Must supply CreatorId and include at least one property";

        public static readonly string UPDATE_PATRON_SETTINGS_BAD_REQUEST = "Must supply PatronId and include at least one property";

        public static readonly string LOAD_CREATOR_BAD_REQUEST = "Must supply CreatorId";

        public static readonly string LOAD_PATRON_BAD_REQUEST = "Must supply PatronId";

        public static readonly string UPDATE_CREATOR_SETTINGS_FAIL = "Could not retrieve settings to update";

        public static readonly string GET_ASSET_PACKAGES_BAD_REQUEST = "Must supply CreatorId and optionally a Vault or list of Asset Packages";

        public static readonly string GET_ASSETS_BAD_REQUEST = "Must supply CreatorId";

        public static readonly string GET_VAULTS_BAD_REQUEST = "Must supply CreatorId";

        public static readonly string SET_ASSET_BAD_REQUEST = "Must supply CreatorId and Asset properties";

        public static readonly string SET_ASSET_COULD_NOT_UPDATE = "Could not update Asset";

        public static readonly string SET_ASSET_COULD_NOT_CREATE = "Could not create Asset";

        public static readonly string SET_ASSET_PACKAGE_BAD_REQUEST = "Must supply CreatorId and Asset Package properties";

        public static readonly string SET_ASSET_PACKAGE_COULD_NOT_UPDATE = "Could not update Asset Package";

        public static readonly string SET_ASSET_PACKAGE_COULD_NOT_CREATE = "Could not create Asset Package";

        public static readonly string SET_VAULT_BAD_REQUEST = "Must supply CreatorId and Vault properties";

        public static readonly string SET_VAULT_COULD_NOT_UPDATE = "Could not update Vault";

        public static readonly string SET_VAULT_COULD_NOT_CREATE = "Could not create Vault";

        public static readonly string GET_PATRON_ASSETS_BAD_REQUEST = "Must supply PatronId and optional filters";

        public static readonly string SET_PATRON_ASSETS_BAD_REQUEST = "";

        public static readonly string SET_PATRON_ASSETS_NO_ASSETS_FOUND = "";

    }
}
