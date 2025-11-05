using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserAccountRepo
    {
        public readonly Sp25researchDbContext _context = new Sp25researchDbContext();
        public List<UserAccount> GetAll()
        {
            return _context.UserAccounts.ToList();
        }
        public UserAccount GetById(int id)
        {
            return _context.UserAccounts.Find(id);
        }
        public void AddNew(UserAccount userAccount)
        {
            _context.UserAccounts.Add(userAccount);
            _context.SaveChanges();
        }

        public UserAccount GetByEmail(string email)
        {
            return _context.UserAccounts.FirstOrDefault(ua => ua.Email == email);
        }
    }
}