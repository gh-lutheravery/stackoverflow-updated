using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication5.Controllers.DataServices;
using WebApplication5.Models;

namespace WebApplication5.Controllers.BusinessControllers
{
    public class UserAuthorizer
    {
        private SOCloneContextService _contextService;

        public UserAuthorizer(SOCloneContextService context)
        {
            _contextService = context;
        }

        public bool IsUserTheAuthor(int authorId, ClaimsPrincipal claimsP)
        {
            bool result = claimsP.Claims.Where(c => c.Value == authorId.ToString()).Any();
            return result;
        }

        public bool IsUserTheAuthorByResourceId(int resourceId, dynamic model, ClaimsPrincipal claimsP)
        {
            bool result = false;
            switch (model)
            {
                case Profile:
                    var prof = _contextService.context.Profile.Find(resourceId);

                    result = claimsP.Claims.Where(c => c.Value == prof.Id.ToString()).Any();
                    break;

                case Answer:
                    var ans = _contextService.context.Answer
                    .Include(p => p.Author)
                    .SingleOrDefault(p => p.Id == resourceId);

                    result = claimsP.Claims.Where(c => c.Value == ans.Author.Id.ToString()).Any();
                    break;

                case Question:
                    var ques = _contextService.context.Question
                    .Include(p => p.Author)
                    .SingleOrDefault(p => p.Id == resourceId);

                    result = claimsP.Claims.Where(c => c.Value == ques.Author.Id.ToString()).Any();
                    break;

                default:
                    throw new ArgumentException("\"dynamic\" model argument is not a valid resource type.");
            }

            return result;
        }

    }
}
