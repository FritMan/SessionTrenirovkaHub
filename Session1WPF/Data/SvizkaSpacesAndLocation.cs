using System;
using System.Collections.Generic;

namespace Session1WPF.Data;

public partial class SvizkaSpacesAndLocation
{
    public long Id { get; set; }

    public long LocationId { get; set; }

    public long SpacesId { get; set; }

    public virtual Location Location { get; set; } = null!;

    public virtual Space Spaces { get; set; } = null!;
}
