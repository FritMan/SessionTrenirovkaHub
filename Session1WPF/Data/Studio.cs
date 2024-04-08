using System;
using System.Collections.Generic;

namespace Session1WPF.Data;

public partial class Studio
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Exhibit> Exhibits { get; } = new List<Exhibit>();
}
