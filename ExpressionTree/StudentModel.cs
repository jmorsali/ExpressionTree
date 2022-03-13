namespace ExpressionTree;

public class StudentFilterDTO
{
    [Equal]
    public int Id { get; set; }

    [EntityParam("Name")]
    [Contain]
    public string SearchName { get; set; }

    [EntityParam("Age")]
    [LessThan]
    public long ToAge { get; set; }

    [EntityParam("Age")]
    [GreaterThan]
    public long FromAge { get; set; }
}