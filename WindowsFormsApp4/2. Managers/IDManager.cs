using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4
{
    public class IDManager
    {
        public IDManager()
        {
            AddIDType("propietari");
            AddIDType("finca");
            AddIDType("parcela");
        }

        public int GetNewID(string name)
        {
            int ret = 0;

            for (int i = 0; i < ids.Count; i++)
            {
                if (ids[i].GetName() == name)
                {
                    ret = ids[i].GetNewId();
                    break;
                }
            }

            return ret;
        }

        void AddIDType(string name)
        {
            IDType t = new IDType(name);
            ids.Add(t);
        }

        private List<IDType> ids = new List<IDType>();
    }

    class IDType
    {
        public IDType(string name)
        {
            _name = name;
        }
        public string GetName() { return _name; }

        public int GetNewId()
        {
            int ret = id_counter;

            id_counter++;

            return ret;
        }

        private string _name;
        private int id_counter = 0;
    }
}
