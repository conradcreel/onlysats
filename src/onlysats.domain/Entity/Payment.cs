using onlysats.domain.Enums;

namespace onlysats.domain.Entity;

/// <summary>
/// Records information related to a payment made to a Creator by a Patron
/// </summary>
public class Payment
{
    /// <summary>
    /// The global unique identifier of this Payment
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The type of payment. E.g. subscription, content/asset purchase, tip, etc.
    /// </summary>
    public EPaymentType Type { get; set; }

    // TODO: more
}
