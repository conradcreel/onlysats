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
                MessageContent = $"<ul class=\"assetList\"><li><img src=\"{"https://preview.redd.it/kvqfttwq32z51.jpg?auto=webp&s=4c3067e449d5bace6a560fd8ff8acdd372a3fc11"}\">{"Schiff Sniff"}</li>" +
                $"<li><img src=\"{"https://pbs.twimg.com/media/DUwjs-RW4AAyiP6.jpg"}\">{"Fuck ETH"}</li></ul>"
            };
        
            return Task.FromResult(queuedMessage);
        }

        public Task<List<QueuedMessage>> GetQueuedMessages(int receiverUserId)
        {
            throw new NotImplementedException();
        }

        public Task<QueuedMessage> QueueMessage(QueuedMessage msg)
        {
            // TODO
            return Task.FromResult(msg);
        }
    }

    #endregion
}