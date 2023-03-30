using System;
using System.Collections.Generic;

namespace ASPTurtelMemory.Models;

public partial class World
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Block>? Blocks { get; set; } = new List<Block>();

    public virtual ICollection<Turtle>? Turtles { get; set; } = new List<Turtle>();
}
