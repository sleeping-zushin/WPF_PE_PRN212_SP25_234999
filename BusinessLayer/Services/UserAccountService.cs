using DataAccessLayer;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserAccountService
    {
        private readonly UserAccountRepo _userAccountRepo;
        public UserAccountService()
        {
            _userAccountRepo = new UserAccountRepo();
        }
        public List<UserAccount> GetAll()
        {
            return _userAccountRepo.GetAll();
        }
        public UserAccount GetById(int id)
        {
            return _userAccountRepo.GetById(id);
        }
        public void AddNew(UserAccount userAccount)
        {
            _userAccountRepo.AddNew(userAccount);
        }

        public UserAccount GetByEmail(string email)
        {
            return _userAccountRepo.GetByEmail(email);
        }
    }
}
