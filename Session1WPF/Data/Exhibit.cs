using System;
using System.Collections.Generic;

namespace Session1WPF.Data;

public partial class Exhibit
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long StudiosId { get; set; }

    public virtual Studio Studios { get; set; } = null!;
}
