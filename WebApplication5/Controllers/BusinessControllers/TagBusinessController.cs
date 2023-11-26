using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using WebApplication5.Controllers.DataServices;
using WebApplication5.Models;
using WebApplication5.ViewModels.QuestionAndAnswer;

namespace WebApplication5.Controllers.BusinessControllers
{
    public class TagBusinessController : Controller
    {
        private SOCloneContextService _contextService;
        public TagBusinessController(SOCloneContextService context)
        {
            _contextService = context;
        }

        public List<string> GetAllTagStrings()
        {
            IQueryable<Tag> tagObjs = _contextService.GetAllTags(false);
            List<string> tagStrings = tagObjs.Select(t => t.Title).ToList();
            return tagStrings;
        }

        
    }
}
