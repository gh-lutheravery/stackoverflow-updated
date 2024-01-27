using Bogus;
using Bogus.DataSets;
using WebApplication5.Controllers.DataServices;
using WebApplication5.Models;

namespace TestApp
{
    public class HomeBusinessControllerTests
    {
        [Fact]
        public async Task PopulateHomeViewModel_ReturnsAHomeViewModel_WithValidQuestions()
        {
            // Arrange
            var fakeContext = new Faker<SOCloneContextService>();
            fakeContext.
            var controller = new HomeController(mockRepo.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        private List<Question> GetTestQuestions()
        {
            var faker = new Faker();
            var questions = new List<Question>();
            for (int i = 1; i < 100; i++)
            {
                var q = new Question();
                q.Id = i;
                q.Title = faker.Lorem.Text();
                q.AnswerCount = Random.Shared.Next(0, 30);
                q.DateCreated = faker.Date.PastOffset().Date;
                q.AcceptedAnswerId = 0;

                questions.Add(q);
            }

            return questions;
        }
    }
}