using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;
using WebApplication5.ViewModels.User;

namespace WebApplication5.Controllers.DataServices
{
    public class SOCloneContextService
    {
        public readonly StackOverflowCloneContext context;

        public SOCloneContextService(StackOverflowCloneContext contextParam)
        {
            context = contextParam;
        }

        // profile methods
        public Profile GetProfileById(int id)
        {
            var profile = context.Profile
                .Include(p => p.Questions)
                .Include(p => p.Answers)
                .SingleOrDefault(p => p.Id == id);

            return profile;
        }

        public Profile? GetProfileWithQA(int? id, bool deepInclude)
        {
            if (id == null)
                return null;

            Profile? profile;
            if (deepInclude)
            {
                profile = context.Profile
                    .Include(p => p.Questions)
                    .ThenInclude(q => q.Tags)
                    .Include(p => p.Answers)
                    .ThenInclude(q => q.AssociatedQuestion)
                    .SingleOrDefault(p => p.Id == id);
            }
            else
            {
                profile = context.Profile
                    .Include(p => p.Questions)
                    .Include(p => p.Answers)
                    .SingleOrDefault(p => p.Id == id);
            }

            return profile;
        }

        public void CreateProfile(Profile newProfile) 
        {
            context.Profile.Add(newProfile);
            context.SaveChanges();
        }

        public void UpdateProfile(Profile newProfile) { }

        public void DeleteProfile(int id) { }

        // question methods
        public IQueryable<Question> GetAllQuestions(bool include) 
        {
            IQueryable<Question> questions;
            if (include)
            {
                questions = context.Question
                    .Include(p => p.Tags)
                    .Include(p => p.Author);
            }
            else
            {
                questions = context.Question;
            }

            return questions;
        }

        public Question GetQuestionById(int id)
        {
            var question = context.Question
                .Include(p => p.Tags)
                .Include(p => p.Author)
                .SingleOrDefault(p => p.Id == id);

            return question;
        }

        public void CreateQuestion(Question newQuestion)
        {
            context.Question.Add(newQuestion);
            context.SaveChanges();
        }

        public void UpdateQuestion(Profile newProfile) { }

        public void DeleteQuestion(int id) { }

        // answer methods
        public IQueryable<Answer> GetAllAnswers()
        {
            IQueryable<Answer> answers = context.Answer
                .Include(a => a.Author);
            
            return answers;
        }

        public Answer GetAnswerWithQuestion(int id)
        {
            Answer answer = context.Answer
                .Include(a => a.AssociatedQuestion)
                .SingleOrDefault(p => p.Id == id);

            return answer;
        }

        // tag methods
        public IQueryable<Tag> GetAllTags(bool include)
        {
            IQueryable<Tag> tags;
            if (include)
            {
                tags = context.Tag
                    .Include(p => p.Questions);
            }
            else
            {
                tags = context.Tag;
            }

            return tags;
        }
    }
}
