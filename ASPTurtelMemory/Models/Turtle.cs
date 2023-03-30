using System;
using System.Collections.Generic;

namespace ASPTurtelMemory.Models;

public partial class Turtle
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public int? WorldId { get; set; }

    public int? X { get; set; }

    public int? Y { get; set; }

    public int? Z { get; set; }
    public virtual World? World { get; set; }
}
