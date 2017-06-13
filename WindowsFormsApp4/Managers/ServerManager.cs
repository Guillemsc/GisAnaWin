using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4
{
    public class ServerManager
    {
        public ServerManager()
        {
            servidor = new DataClasses1DataContext();
        }

        public DataClasses1DataContext GetServidor() { return servidor; }

        public List<tblProveedores> GetProveedors()
        {
            List<tblProveedores> ret = new List<tblProveedores>();

            foreach (var prov in servidor.tblProveedores)
                ret.Add(prov);

            return ret;
        }

        private DataClasses1DataContext servidor = null;
    }
}
