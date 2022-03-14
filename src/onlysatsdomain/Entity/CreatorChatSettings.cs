namespace onlysats.domain.Entity
{

    /// <summary>
    /// Various chat settings for the Creator
    /// </summary>
    public class CreatorChatSettings : BaseEntity
    {
        /// <summary>
        /// A reference to the Creator
        /// </summary>
        public int CreatorId { get; set; }

        /// <summary>
        /// Upon receiving a new subscriber, the subscriber 
        /// can be sent a welcome chat message
        /// </summary>
        public bool ShowWelcomeMessage { get; set; }

        // TODO: A property for the Welcome message. Look into how 
        // chat messages in Matrix are formatted 

        /// <summary>
        /// Outgoing mass messages will not show in the chat list
        /// until the subscriber responds if this property is set
        /// </summary>
        public bool HideOutgoingMassMessages { get; set; }
    }
}