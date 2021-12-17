using onlysats.domain.Entity;
using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates persistence of Creators and their Settings
/// </summary>
public interface ICreatorRepository
{
    /// <summary>
    /// Convenience method that will execute a performant query to get 
    /// all Creator data including settings. Retrieve only
    /// </summary>
    Task<CreatorModel?> GetCreatorDetail(int id);

    /// <summary>
    /// Retrieves base Creator entity from its Id
    /// </summary>
    Task<Creator?> GetCreator(int id);

    /// <summary>
    /// Updates or inserts a base Creator entity. On insert, it 
    /// will insert default settings in a single transaction
    /// </summary>
    Task<Creator> UpsertCreator(Creator creator);

    /// <summary>
    /// Retrieves a Creator's account settings
    /// </summary>
    Task<CreatorAccountSettings?> GetCreatorAccountSettings(int creatorId);

    /// <summary>
    /// Updates or inserts a Creator's account settings
    /// </summary>
    Task<CreatorAccountSettings> UpsertCreatorAccountSettings(CreatorAccountSettings creatorAccountSettings);

    /// <summary>
    /// Retrieves a Creator's chat settings
    /// </summary>
    Task<CreatorChatSettings?> GetCreatorChatSettings(int creatorId);

    /// <summary>
    /// Updates or inserts a Creator's chat settings
    /// </summary>
    Task<CreatorChatSettings> UpsertCreatorChatSettings(CreatorChatSettings creatorChatSettings);

    /// <summary>
    /// Retrieves a Creator's notification settings
    /// </summary>
    Task<CreatorNotificationSettings?> GetCreatorNotificationSettings(int creatorId);

    /// <summary>
    ///  Updates or inserts a Creator's notification settings
    /// </summary>
    Task<CreatorNotificationSettings> UpsertCreatorNotificationSettings(CreatorNotificationSettings creatorNotificationSettings);

    /// <summary>
    /// Retrieves a Creator's profile settings
    /// </summary>
    Task<CreatorProfileSettings?> GetCreatorProfileSettings(int creatorId);

    /// <summary>
    ///  Updates or inserts a Creator's profile settings
    /// </summary>
    Task<CreatorProfileSettings> UpsertCreatorProfileSettings(CreatorProfileSettings creatorProfileSettings);

    /// <summary>
    /// Retrieves a Creator's profile settings
    /// </summary>
    Task<CreatorSecuritySettings?> GetCreatorSecuritySettings(int creatorId);

    /// <summary>
    ///  Updates or inserts a Creator's security settings
    /// </summary>
    Task<CreatorSecuritySettings> UpsertCreatorSecuritySettings(CreatorSecuritySettings creatorSecuritySettings);
}

#region Implementation

public class CreatorRepository : ICreatorRepository
{
    private readonly ISqlRepository _Repository;

    public CreatorRepository(ISqlRepository sqlRepository)
    {
        _Repository = sqlRepository;
    }

    public Task<CreatorModel?> GetCreatorDetail(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Creator?> GetCreator(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CreatorAccountSettings?> GetCreatorAccountSettings(int creatorId)
    {
        throw new NotImplementedException();
    }

    public Task<CreatorChatSettings?> GetCreatorChatSettings(int creatorId)
    {
        throw new NotImplementedException();
    }

    public Task<CreatorNotificationSettings?> GetCreatorNotificationSettings(int creatorId)
    {
        throw new NotImplementedException();
    }

    public Task<CreatorProfileSettings?> GetCreatorProfileSettings(int creatorId)
    {
        throw new NotImplementedException();
    }

    public Task<CreatorSecuritySettings?> GetCreatorSecuritySettings(int creatorId)
    {
        throw new NotImplementedException();
    }

    public Task<Creator> UpsertCreator(Creator creator)
    {
        throw new NotImplementedException();
    }

    public Task<CreatorAccountSettings> UpsertCreatorAccountSettings(CreatorAccountSettings creatorAccountSettings)
    {
        throw new NotImplementedException();
    }

    public Task<CreatorChatSettings> UpsertCreatorChatSettings(CreatorChatSettings creatorChatSettings)
    {
        throw new NotImplementedException();
    }

    public Task<CreatorNotificationSettings> UpsertCreatorNotificationSettings(CreatorNotificationSettings creatorNotificationSettings)
    {
        throw new NotImplementedException();
    }

    public Task<CreatorProfileSettings> UpsertCreatorProfileSettings(CreatorProfileSettings creatorProfileSettings)
    {
        throw new NotImplementedException();
    }

    public Task<CreatorSecuritySettings> UpsertCreatorSecuritySettings(CreatorSecuritySettings creatorSecuritySettings)
    {
        throw new NotImplementedException();
    }
}

#endregion