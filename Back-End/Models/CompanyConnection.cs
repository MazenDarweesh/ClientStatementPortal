using System;
using System.Collections.Generic;

namespace ClientStatementPortal.Models;

public partial class CompanyConnection
{
    public string CompanyKey { get; set; } = null!;

    public string ServerIp { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string? UserId { get; set; }

    public string? Password { get; set; }

    public bool UseWindowsAuth { get; set; }

    public string ServerIPWithoutPort =>
        string.IsNullOrWhiteSpace(ServerIp) ? string.Empty : ServerIp.Split(':')[0];
    public string ConnectionString =>
        UseWindowsAuth
        ? $"Server={ServerIp};Database={DatabaseName};Integrated Security=True;TrustServerCertificate=True;"
        : $"Server={ServerIp};Database={DatabaseName};User Id={UserId};Password={Password};TrustServerCertificate=True;MultipleActiveResultSets=True;";
}
