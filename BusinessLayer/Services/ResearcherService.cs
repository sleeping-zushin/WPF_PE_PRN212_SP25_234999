using DataAccessLayer;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ResearcherService
    {
        private readonly ResearcherRepo _researcherRepo;
        public ResearcherService()
        {
            _researcherRepo = new ResearcherRepo();
        }
        public List<Researcher> GetAll()
        {
            return _researcherRepo.GetAll();
        }
        public Researcher GetById(int id)
        {
            return _researcherRepo.GetById(id);
        }
        public void AddNew(Researcher researcher)
        {
            _researcherRepo.AddNew(researcher);
        }
    }
}
