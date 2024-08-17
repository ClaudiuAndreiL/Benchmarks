namespace Benchmarks.MaxMessageParts.Enums
{

    public enum MessageStatusEnum
    {
        Unknown,
        CanBeSent,
        OptedOut,
        SenderIdInvalid,
        MsisdnInvalid,
        LandlineNotAllowed,
        DestinationNotAllowed,
        MessagePartCountExceeded,
        SendingLimitExceeded,
        OutsideBusinessHours,
        EdgeFailed,
        EdgeSubmitted,
        Dispatched,
        FailedToDispatch,
        Accepted,
        FailedToRoute,
        Routed,
        ReplacedOriginator,
        SentToNetwork,
        FailedToSendToNetwork,
        NetworkAccepted,
        NetworkRejected,
        NetworkFailed,
        NetworkExpired,
        MessageDropped,
        MessageExpired,
        Delivered,
        Inbound
    }
}
