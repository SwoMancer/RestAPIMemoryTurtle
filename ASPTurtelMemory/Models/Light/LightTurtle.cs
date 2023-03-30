namespace ASPTurtelMemory.Models.Light
{
    public partial class LightTurtle
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public int? WorldId { get; set; }

        public int? X { get; set; }

        public int? Y { get; set; }

        public int? Z { get; set; }
        public Turtle EditTurtle(Turtle turtle) 
        {
            if (Id != null)
                turtle.Id = Id;

            if (Name != null)
                turtle.Name = Name;

            if (WorldId != null)
                turtle.WorldId = WorldId;

            if (X != null)
                turtle.X = X;

            if (Y != null)
                turtle.Y = Y;

            if (Z != null)
                turtle.Z = Z;
            
            return turtle;
        }
    }
}
