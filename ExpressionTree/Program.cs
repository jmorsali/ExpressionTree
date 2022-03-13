// See https://aka.ms/new-console-template for more information


using ExpressionTree;

var studentmodel = new StudentFilterDTO { Id = 10, SearchName = "Ali", FromAge = 10, ToAge = 14 };

List<Student> students = new List<Student>
{
    new() { Id=10, Name="Hasan" , Age=18, Description="Test 12"},
    new() { Id=11, Name="Ali", Age=16, Description="Test 9"},
    new() { Id=10, Name="Ali", Age=13, Description="Test 8"},
    new() { Id=12, Name="Hasan", Age=13, Description="Test 13"},
};


var filter = extension.SearchExpression<Student, StudentFilterDTO>(studentmodel);

var quesry = students.AsQueryable().Where(filter);
var res = quesry.ToList();

foreach (var student in res)
{
    Console.WriteLine("Student:Id :=> " + student.Id);
    Console.WriteLine("Student:Name :=> " + student.Name);
}
