namespace onlysats.domain.Constants;

public static class CErrorMessage
{
    public static readonly string SETUP_WALLET_BAD_REQUEST = "Must supply UserAccountId, Username and xPubKey";

    public static readonly string SETUP_CREATOR_BAD_REQUEST = "";

    public static readonly string SETUP_CREATOR_FAIL_USER_ACCOUNT = "Could not create UserAccount";

    public static readonly string SETUP_CREATOR_FAIL = "Could not create Creator";

    public static readonly string SETUP_PATRON_BAD_REQUEST = "";

    public static readonly string UPDATE_CREATOR_SETTINGS_BAD_REQUEST = "Must supply CreatorId and include at least one property";

    public static readonly string UPDATE_PATRON_SETTINGS_BAD_REQUEST = "";

    public static readonly string LOAD_CREATOR_BAD_REQUEST = "Must supply CreatorId";

    public static readonly string LOAD_PATRON_BAD_REQUEST = "Must supply PatronId";
}
