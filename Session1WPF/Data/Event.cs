using System;
using System.Collections.Generic;

namespace Session1WPF.Data;

public partial class Event
{
    public long Id { get; set; }

    public string DateStart { get; set; } = null!;

    public long TypeOfEventsId { get; set; }

    public string TimeStart { get; set; } = null!;

    public string TimeEnd { get; set; } = null!;

    public long NumberOfVisitors { get; set; }

    public long SpacesId { get; set; }

    public long? Paid { get; set; }

    public string Description { get; set; } = null!;

    public virtual Space Spaces { get; set; } = null!;

    public virtual TypeOfEvent TypeOfEvents { get; set; } = null!;
}
