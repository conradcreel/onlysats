using System.Threading;
using System.Threading.Tasks;
using onlysats.domain.Models;

namespace onlysats.domain.Services
{

    public interface IMessagePublisher
    {
        Task PublishEvent<T>(string topicName, T data, CancellationToken cancellationToken = default);
    }

    public class MessagePublisherProxy : IMessagePublisher
    {
        //private readonly DaprClient _DaprClient;
        private readonly OnlySatsConfiguration _Configuration;

        public MessagePublisherProxy(/*DaprClient daprClient,*/ OnlySatsConfiguration configuration)
        {
            //_DaprClient = daprClient;
            _Configuration = configuration;
        }

        public async Task PublishEvent<T>(string topicName, T data, CancellationToken cancellationToken = default)
        {
            //await _DaprClient.PublishEventAsync(_Configuration.PubSubName, topicName, data, cancellationToken);
        }
    }
}