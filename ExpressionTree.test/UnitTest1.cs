using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ExpressionTree.test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Setup Test Fixture
            List<Student> studentsEntitiesSample = new List<Student>
            {
                new() { Id=10, Name="Hasan" , Age=18, Description="Test 12"},
                new() { Id=11, Name="Ali", Age=16, Description="Test 9"},
                new() { Id=10, Name="Ali", Age=13, Description="Test 8"},
                new() { Id=12, Name="Hasan", Age=13, Description="Test 13"},
            };
            var studentFilterDTOSample = new StudentFilterDTO { Id = 10, SearchName = "Ali", FromAge = 10, ToAge = 14 };


            //Execute Test
            
            var filter = SearchExpressionBuilder.SearchExpression<Student, StudentFilterDTO>(studentFilterDTOSample);
            var quesry = studentsEntitiesSample.AsQueryable().Where(filter);
            var searchResult = quesry.ToList();
        }
    }
}