using Document_Processing_System.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using namespace Document_Processing_System.Entities;

namespace Document_Processing_System.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<DocumentDetail>DocumentDetails { get; set; }
        
    }
}
