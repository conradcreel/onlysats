using System.Reflection.Metadata;
using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates the flavor of Blob storage so the Domain repositories
/// can remain (mostly) ignorant. This initial implementation will 
/// be deployed to Azure but one should be able to swap out Blob storages
/// with relative ease so long as it supports chunking, shared access signatures
/// and client-side uploads
/// </summary>
public interface IBlobRepository
{
    Task<string> GenerateSharedAccessSignature(string blobId);
}

public class BlobRepository : IBlobRepository
{
    public BlobRepository(OnlySatsConfiguration configuration)
    {

    }

    public Task<string> GenerateSharedAccessSignature(string blobId)
    {
        throw new NotImplementedException();
    }
}