namespace onlysats.domain.Models
{
    public class OnlySatsConfiguration
    {
        public string SqlConnectionString { get; set; } = string.Empty;
        public string PubSubName { get; set; } = string.Empty;

        // TODO: Blob (File Browser)  properties
        public string BtcPayUri { get; set; } = string.Empty;
        public string BtcPayAdminUser { get; set; }
        public string BtcPayAdminPass { get; set; }
        public string BtcPayStoreId { get; set; }

        public string SynapseUri { get; set; }
        public string SynapseAdminUser { get; set; }
        public string SynapseAdminPassword { get; set; }
    }
}