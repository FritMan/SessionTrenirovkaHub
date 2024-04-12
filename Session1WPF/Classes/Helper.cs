using Session1WPF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows;

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
      
        public static string CheckErrors(UIElementCollection children)
        {
            string msg = "";
            foreach (ContentControl item in children)
            {
                if (!(item.Content is UIElement))
                {
                    continue;
                }

                var res = Validation.GetErrors(item.Content as Control);
                foreach (var error in res)
                {
                    msg += error.ErrorContent.ToString() + "\n";
                }
            }
            
            return msg;
        }
    }
}
