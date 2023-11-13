using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;
using WebApplication5.ViewModels.User;

namespace WebApplication5.Controllers.DataServices
{
    public class SOCloneContextService
    {
        private readonly StackOverflowCloneContext _context;

        public SOCloneContextService(StackOverflowCloneContext context)
        {
            _context = context;
        }

        public Profile GetById(int id)
        {
            var profile = _context.Profile
                .Include(p => p.Questions)
                .Include(p => p.Answers)
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);

            return profile;
        }

        public void Create(Profile newProfile) 
        {
            _context.Profile.Add(newProfile);
            _context.SaveChanges();
        }
    }
}
