using Session1WPF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session1WPF.Classes
{
    public static class Helper
    {
      public static SessionDbContext Db = new SessionDbContext();

      public static void ReafreshAll()
      {
            foreach (var item in Db.ChangeTracker.Entries())
            {
                item.Reload();
            }
      }
    }
}
