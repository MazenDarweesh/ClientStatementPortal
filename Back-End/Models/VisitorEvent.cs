
namespace ClientStatementPortal.Models;

public partial class VisitorEvent
{
    public int Id { get; set; }

    public string? EventType { get; set; }

    public string? CompanyKey { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public DateTime? EventDate { get; set; }

    public string? AccountName { get; set; }

    public string? AccountType { get; set; }
}
