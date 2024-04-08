using System;
using System.Collections.Generic;

namespace Session1WPF.Data;

public partial class TypeOfEvent
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Event> Events { get; } = new List<Event>();
}
