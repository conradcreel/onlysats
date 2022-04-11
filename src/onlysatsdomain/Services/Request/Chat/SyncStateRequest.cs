using System;
using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Request.Chat
{
    public class SyncStateRequest : SynapseRequestBase
    {
        [JsonPropertyName("filter")]
        public string Filter { get; set; } = "0";

        [JsonPropertyName("timeout")]
        public int TimeoutMillis { get; set; } = 0;

        [JsonPropertyName("since")]
        public string Since { get; set; }

        public override string GenerateUrl()
        {
            string url = $"_matrix/client/v3/sync?filter={Filter}&timeout={TimeoutMillis}&_cacheBuster={DateTimeOffset.Now.ToUnixTimeSeconds()}";

            if (!string.IsNullOrWhiteSpace(Since))
            {
                url = $"{url}&since={Since}";
            }

            return url;
        }

        public override bool IsValid()
        {
            return TimeoutMillis <= 30000 && 
                    TimeoutMillis >= 0;
        }
    }
}