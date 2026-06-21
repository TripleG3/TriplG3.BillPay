namespace TripleG3.BillPay.Models;

public record User(
    string Username,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime? DOB,
    List<BillOrPay> BillOrPays,
    List<string> LinkedUsers)
{
    public User() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, null, new List<BillOrPay>(), new List<string>()) { }
}
