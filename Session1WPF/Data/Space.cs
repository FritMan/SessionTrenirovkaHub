using System;
using System.Collections.Generic;

namespace Session1WPF.Data;

public partial class Space
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long Capacity { get; set; }

    public string Description { get; set; } = null!;

    public long? Location { get; set; }

    public virtual ICollection<Event> Events { get; } = new List<Event>();

    public virtual ICollection<SvizkaSpacesAndLocation> SvizkaSpacesAndLocations { get; } = new List<SvizkaSpacesAndLocation>();
}
