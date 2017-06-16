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

        public List<tblFinques> GetFinques()
        {
            List<tblFinques> ret = new List<tblFinques>();

            foreach (var prov in servidor.tblFinques)
                ret.Add(prov);

            return ret;    
        }

        public List<tblParceles> GetParceles()
        {
            List<tblParceles> ret = new List<tblParceles>();

            foreach (var prov in servidor.tblParceles)
                ret.Add(prov);

            return ret;
        }

        public List<tblTipoUva> GetVarietats()
        {
            List<tblTipoUva> ret = new List<tblTipoUva>();

            foreach (var prov in servidor.tblTipoUva)
                ret.Add(prov);

            return ret;
        }

        private DataClasses1DataContext servidor = null;
    }
}
