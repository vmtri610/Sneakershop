using System;
using System.Collections.Generic;

namespace UserService.Models;

public partial class User
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhotoUrl { get; set; } = null!;
}
