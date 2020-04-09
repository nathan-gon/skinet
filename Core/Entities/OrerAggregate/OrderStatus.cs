using System.Runtime.Serialization;

namespace Core.Entities.OrerAggregate
{
  public enum OrderStatus
  {
    [EnumMember(Value = "Pending")]
    Pending,
    [EnumMember(Value = "Payment Recevied")]
    PaymentRecevied,
    [EnumMember(Value = "Payment Failed")]
    PaymentFailed
  }
}