using DataAccessLayer;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ResearchProjectService
    {
        private readonly ResearchProjectRepo _researchProjectRepo = new ResearchProjectRepo();
        //public ResearchProjectService()
        //{
        //    _researchProjectRepo = new ResearchProjectRepo();
        //}
        public List<ResearchProject> GetAll()
        {
            return _researchProjectRepo.GetAll();
        }
        public ResearchProject GetById(int id)
        {
            return _researchProjectRepo.GetById(id);
        }
        public void AddNew(ResearchProject researchProject)
        {
            _researchProjectRepo.AddNew(researchProject);
        }
        public void Update(ResearchProject researchProject)
        {
            _researchProjectRepo.Update(researchProject);
        }
        public void DeleteResearchProject(int id)
        {
            _researchProjectRepo.Delete(id);
        }
    }
}
