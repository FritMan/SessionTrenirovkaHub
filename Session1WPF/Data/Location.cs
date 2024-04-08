using System;
using System.Collections.Generic;

namespace Session1WPF.Data;

public partial class Location
{
    public long Id { get; set; }

    public long KolRidov { get; set; }

    public long MestaVridu { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SvizkaSpacesAndLocation> SvizkaSpacesAndLocations { get; } = new List<SvizkaSpacesAndLocation>();
}
