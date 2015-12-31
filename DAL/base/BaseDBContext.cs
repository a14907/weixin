using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BaseDBContext : DbContext
    {
        public BaseDBContext()
            : base("name=TestEntities")
        {

        }
    }
}
