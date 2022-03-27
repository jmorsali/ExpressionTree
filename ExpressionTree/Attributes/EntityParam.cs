public class EntityParam : Attribute
{
    public string Name { get; }

    public EntityParam(string Name)
    {
        this.Name = Name;
    }
}
