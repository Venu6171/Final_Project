using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorseApp
{
    public class SaveAndLoadXML
    {

        XmlSerializer serializer;

        /// <summary>
        /// Construtor
        /// </summary>
        public SaveAndLoadXML()
        {
            serializer = new XmlSerializer(typeof(ContactsData));
        }

        public void SavecontactData(string filepath, ref ContactsData contactData)
        {
            using (TextWriter file = new StreamWriter(filepath))
            {
                try
                {
                    serializer.Serialize(file, contactData);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Unable to deserialize the {0} due to following: {1}",
                        filepath, ex.Message));
                }
            }
        }

        public ContactsData LoadcontactData(string filepath)
        {
            if (!File.Exists(filepath))
            {
                throw new Exception(string.Format("{0} does not exist", filepath));
            }

            ContactsData data = null;
            using (var file = new StreamReader(filepath))
            {
                try
                {
                    data = serializer.Deserialize(file) as ContactsData;
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Unable to deserialize the {0} due to following: {1}",
                        filepath, ex.Message));
                }
            }
            return data;
        }
    }
}
