namespace TripleG3.BillPay.Models;

public record BillOrPay(
    string UserId,
    string To,
    string From,
    BillOrPayType Type,
    decimal Amount,
    Schedule Schedule,
    DateTime? FirstPayDate,
    string Note)
{
    public string Id { get; init; } = Guid.NewGuid().ToString();

    [JsonIgnore]
    public DateTime? NextPayDate => Schedule switch
    {
        Schedule.OneTime => FirstPayDate,
        Schedule.Weekly => (FirstPayDate ?? DateTime.Today).AddDays(7),
        Schedule.Monthly => (FirstPayDate ?? DateTime.Today).AddMonths(1),
        Schedule.BiMonthly => (FirstPayDate ?? DateTime.Today).AddMonths(2),
        Schedule.EveryTwoWeeks => (FirstPayDate ?? DateTime.Today).AddDays(14),
        Schedule.Quarterly => (FirstPayDate ?? DateTime.Today).AddMonths(3),
        Schedule.Yearly => (FirstPayDate ?? DateTime.Today).AddYears(1),
        _ => null
    };

    public override string ToString() => $"{Type} {Amount:C} (Schedule={Schedule}, FirstPayDate={(FirstPayDate.HasValue ? FirstPayDate.Value.ToShortDateString() : "TBD")})";
}
