using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorseApp
{
    public class ContactData
    {
        private static int id;
        private string recipientName;
        private List<string> myMessages;
        private List<string> otherMessages;

        public List<string> MyMessages
        {
            get { return myMessages; }
        }

        public List<string> OtherMessages
        {
            get { return otherMessages; }
        }

        public string RecipientName
        {
            get { return recipientName; }
            set { recipientName = value; }
        }

        public ContactData()
        {
            myMessages = new List<string>();
            otherMessages = new List<string>();
        }
    }
}
