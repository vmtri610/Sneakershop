using System;
using System.Collections.Generic;

namespace AuthService.Models;

public partial class Auth
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Hpassword { get; set; } = null!;
}
