namespace onlysats.domain.Events
{

    /// <summary>
    /// Base class for all Events raised by OnlySats
    /// </summary>
    public abstract class EventBase
    {
        public abstract string Topic { get; }
    }
}