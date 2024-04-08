using System;
using System.Collections.Generic;

namespace Session1WPF.Data;

public partial class Teacher
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Patronymic { get; set; }
}
