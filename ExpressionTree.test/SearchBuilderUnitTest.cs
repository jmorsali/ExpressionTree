using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ExpressionTree.test
{
    public class EntitiesFixture : IDisposable
    {
        public List<Student> StudentsEntitiesSample { get; private set; }

        public EntitiesFixture()
        {
            StudentsEntitiesSample = new List<Student>
            {
                new() { Id=10, Name="Hasan" , Age=18, Description="Test 12"},
                new() { Id=11, Name="Ali", Age=16, Description="Test 9"},
                new() { Id=10, Name="Ali", Age=13, Description="Test 8"},
                new() { Id=12, Name="Hasan", Age=13, Description="Test 13"},
            };
        }

        public void Dispose()
        {
            StudentsEntitiesSample.Clear();
        }

    }


    public class SearchBuilderUnitTest : IClassFixture<EntitiesFixture>
    {
        private readonly EntitiesFixture entitiesFixture;

        public SearchBuilderUnitTest(EntitiesFixture entitiesFixture)
        {
            this.entitiesFixture = entitiesFixture;
        }

        [Fact]
        public void ShouldBePassWithFixtureEntitiyAndSearchDTOWithoutNullParameterAndRightValue()
        {
            //Setup Test Fixture
            StudentFilterDTO studentFilterDTOSample = new StudentFilterDTO
            {
                Id = 10,
                SearchName = "Ali",
                FromAge = 10,
                ToAge = 14
            };


            //Execute Test

            var filter = SearchExpressionBuilder.SearchExpression<Student, StudentFilterDTO>(studentFilterDTOSample);
            var quesry = entitiesFixture.StudentsEntitiesSample.AsQueryable().Where(filter);
            var searchResult = quesry.ToList();

            //Assert 

            Assert.True(searchResult.Any());

        }

        [Fact]
        public void ShouldBePassWithFixtureEntitiyAndSearchDTOWithoutNullParameterAndWrongValue()
        {
            //Setup Test Fixture
            StudentFilterDTO studentFilterDTOSample = new StudentFilterDTO
            {
                Id = 10,
                SearchName = "Mehdi",
                FromAge = 10,
                ToAge = 14
            };
            
            //Execute Test

            var filter = SearchExpressionBuilder.SearchExpression<Student, StudentFilterDTO>(studentFilterDTOSample);
            var quesry = entitiesFixture.StudentsEntitiesSample.AsQueryable().Where(filter);
            var searchResult = quesry.ToList();

            //Assert 

            Assert.True(!searchResult.Any());

        }


        [Fact]
        public void ShouldBePassWithFixtureEntitiyAndSearchDTOWithNullParameterAndRightValue()
        {
            //Setup Test Fixture
            StudentFilterDTO studentFilterDTOSample = new StudentFilterDTO
            {
                SearchName = "Ali",
            };
            
            //Execute Test

            var filter = SearchExpressionBuilder.SearchExpression<Student, StudentFilterDTO>(studentFilterDTOSample);
            var quesry = entitiesFixture.StudentsEntitiesSample.AsQueryable().Where(filter);
            var searchResult = quesry.ToList();

            //Assert 

            Assert.True(searchResult.Any());

        }
    }
}