using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorseApp
{
    public class LocalData
    {
        private List<string> contactNames;
        public List<string> ContactNames
        {
            get { return contactNames; }
        }
        public LocalData()
        {
            contactNames = new List<string>();
        }

    }
}
