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

        public List<tblCoordenadesFincaParcela> GetCoordenades()
        {
            List<tblCoordenadesFincaParcela> ret = new List<tblCoordenadesFincaParcela>();

            foreach (var prov in servidor.tblCoordenadesFincaParcela)
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

        public List<tblFamiliesCost> GetTreballs()
        {
            List<tblFamiliesCost> ret = new List<tblFamiliesCost>();

            foreach (var prov in servidor.tblFamiliesCost)
                ret.Add(prov);

            return ret;
        }

        public tblCoordenadesFincaParcela AddCoordenades(int id_parcela, double lat, double lon, string codigo_empresa, int id_finca, int id_punt_cor)
        {
            tblCoordenadesFincaParcela ret = new tblCoordenadesFincaParcela();

            ret.idParcela = id_parcela;
            ret.latitud = lat;
            ret.longitud = lon;
            ret.CodigoEmpresa = codigo_empresa;
            ret.idFinca = id_finca;
            ret.idPuntCor = id_punt_cor;

            servidor.tblCoordenadesFincaParcela.InsertOnSubmit(ret);

            return ret;
        }

        public void DeleteCoordenades(tblCoordenadesFincaParcela coordenates)
        {
            servidor.tblCoordenadesFincaParcela.DeleteOnSubmit(coordenates);
        }

        public void SubmitChanges()
        {
            servidor.SubmitChanges();
        }

        private DataClasses1DataContext servidor = null;
    }
}
