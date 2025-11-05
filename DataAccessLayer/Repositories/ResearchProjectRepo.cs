using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ResearchProjectRepo
    {
        public readonly Sp25researchDbContext _context = new Sp25researchDbContext();

        public List<ResearchProject> GetAll()
        {
            return _context
                .ResearchProjects
                .Include(rp => rp.LeadResearcher)
                .ToList();
        }

        public ResearchProject GetById(int id)
        {
            return _context
                .ResearchProjects
                //.Include(rp => rp.LeadResearcher)
                .Find(id);
        }

        public void AddNew(ResearchProject researchProject)
        {
            _context.ResearchProjects.Add(researchProject);
            _context.SaveChanges();
        }
        
        public void Update(ResearchProject researchProject)
        {
            _context.ResearchProjects.Update(researchProject);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var researchProject = _context.ResearchProjects.Find(id);
            if (researchProject != null)
            {
                _context.ResearchProjects.Remove(researchProject);
                _context.SaveChanges();
            }
        }
    }
}
