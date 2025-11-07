using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ResearcherRepo
    {
        public readonly Sp25researchDbContext _context = new Sp25researchDbContext();

        //public ResearcherRepo(Sp25researchDbContext context)
        //{
        //    _context = context;
        //}

        public List<Researcher> GetAll()
        {
            return _context.Researchers.ToList();
        }

        public Researcher GetById(int id)
        {
            return _context.Researchers.Find(id);
        }

        public void AddNew(Researcher researcher)
        {
            _context.Researchers.Add(researcher);
            _context.SaveChanges();
        }
    }
}
