using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using onlysats.domain.Entity;
using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories
{
    /// <summary>
    /// Encapsulates persistence of temporary Chat messages
    /// </summary>
    public interface IChatRepository
    {
        Task<QueuedMessage> QueueMessage(QueuedMessage msg);
        Task<List<QueuedMessage>> GetQueuedMessages(int receiverUserId);
        Task<QueuedMessage> GetQueuedMessage(int queuedMessageId);
        Task<QueuedMessage> GetQueuedMessageByInvoice(string invoiceId);
    }

    #region Implementation

    public class ChatRepository : IChatRepository
    {
        private readonly ISqlRepository _Repository;

        public ChatRepository(ISqlRepository sqlRepository)
        {
            _Repository = sqlRepository;
        }

        public Task<QueuedMessage> GetQueuedMessage(int queuedMessageId)
        {
            throw new NotImplementedException();
        }

        public Task<QueuedMessage> GetQueuedMessageByInvoice(string invoiceId)
        {
            var queuedMessage = new QueuedMessage
            {
                RoomId = "!KGRjPGFzoRrGDiVGSn:onlysats.matrix",
                SynapseAccessToken = "syt_dGhvdA_KoJXtIaprxjdqHYqcezz_49nIUk",
                InvoiceId = invoiceId,
                BOLT11 = String.Empty,
                CreatorId = 666,
                Id = 222,
                MessageContent = $"<ul class=\"assetList\"><li><img src=\"{"https://i.kym-cdn.com/photos/images/original/001/926/250/bd1.png"}\">{"Pic #1"}</li><li><img src=\"{"https://pbs.twimg.com/media/DWgReaDXcAAAvVQ.jpg"}\">{"Pic #2"}</li></ul>"
            };
        
            return Task.FromResult(queuedMessage);
        }

        public Task<List<QueuedMessage>> GetQueuedMessages(int receiverUserId)
        {
            throw new NotImplementedException();
        }

        public Task<QueuedMessage> QueueMessage(QueuedMessage msg)
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}