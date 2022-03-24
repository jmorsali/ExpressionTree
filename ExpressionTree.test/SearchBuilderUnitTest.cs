using System.Linq;
using Xunit;

namespace DTOSearchExpressionBuilder.Test
{
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
            var query = entitiesFixture.StudentsEntitiesSample.AsQueryable().Where(filter);
            var searchResult = query.ToList();

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
            var query = entitiesFixture.StudentsEntitiesSample.AsQueryable().Where(filter);
            var searchResult = query.ToList();

            //Assert 

            Assert.True(!searchResult.Any());

        }


        [Fact]
        public void ShouldBePassWithFixtureEntitiyAndSearchDTOWithNullParameterAndRightStringValue()
        {
            //Setup Test Fixture
            StudentFilterDTO studentFilterDTOSample = new StudentFilterDTO
            {
                SearchName = "Javad"
            };
            
            //Execute Test

            var filter = SearchExpressionBuilder.SearchExpression<Student, StudentFilterDTO>(studentFilterDTOSample);
            var query = entitiesFixture.StudentsEntitiesSample.AsQueryable().Where(filter);
            var searchResult = query.ToList();

            //Assert 

            Assert.True(searchResult.Any());

        }

        [Fact]
        public void ShouldBePassWithFixtureEntitiyAndSearchDTOWithNullParameterAndWrongStringValue()
        {
            //Setup Test Fixture
            StudentFilterDTO studentFilterDTOSample = new StudentFilterDTO
            {
                SearchName = "HOS"
            };
            
            //Execute Test

            var filter = SearchExpressionBuilder.SearchExpression<Student, StudentFilterDTO>(studentFilterDTOSample);
            var query = entitiesFixture.StudentsEntitiesSample.AsQueryable().Where(filter);
            var searchResult = query.ToList();

            //Assert 

            Assert.True(!searchResult.Any());

        }


        
        [Fact]
        public void ShouldBePassWithFixtureEntitiyAndSearchDTOWithNullParameterAndWrongIntValue()
        {
            //Setup Test Fixture
            StudentFilterDTO studentFilterDTOSample = new StudentFilterDTO
            {
                Id=22,
            };
            
            //Execute Test

            var filter = SearchExpressionBuilder.SearchExpression<Student, StudentFilterDTO>(studentFilterDTOSample);
            var query = entitiesFixture.StudentsEntitiesSample.AsQueryable().Where(filter);
            var searchResult = query.ToList();

            //Assert 

            Assert.True(!searchResult.Any());

        }
    }
}