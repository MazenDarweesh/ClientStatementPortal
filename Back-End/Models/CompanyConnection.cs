using System;
using System.Collections.Generic;

namespace ClientStatementPortal.Models;

public partial class CompanyConnection
{
    public string CompanyKey { get; set; }
    public string ServerIP { get; set; }
    public string DatabaseName { get; set; }
    public string? UserId { get; set; }
    public string? Password { get; set; }
    public bool UseWindowsAuth { get; set; }

    public string ServerIPWithoutPort =>
        string.IsNullOrWhiteSpace(ServerIP) ? string.Empty : ServerIP.Split(':')[0];
    public string ConnectionString =>
        UseWindowsAuth
        ? $"Server={ServerIP};Database={DatabaseName};Integrated Security=True;TrustServerCertificate=True;"
        : $"Server={ServerIP};Database={DatabaseName};User Id={UserId};Password={Password};TrustServerCertificate=True;MultipleActiveResultSets=True;";
}
