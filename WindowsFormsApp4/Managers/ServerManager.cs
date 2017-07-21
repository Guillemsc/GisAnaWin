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

            if (servidor.tblProveedores.Count() > 0)
            {
                foreach (var prov in servidor.tblProveedores)
                    ret.Add(prov);
            }

            return ret;
        }

        public List<tblFinques> GetFinques()
        {
            List<tblFinques> ret = new List<tblFinques>();

            if (servidor.tblFinques.Count() > 0)
            {
                foreach (var prov in servidor.tblFinques)
                    ret.Add(prov);
            }

            return ret;    
        }

        public List<tblParceles> GetParceles()
        {
            List<tblParceles> ret = new List<tblParceles>();

            if (servidor.tblParceles.Count() > 0)
            {
                foreach (var prov in servidor.tblParceles)
                    ret.Add(prov);
            }

            return ret;
        }

        public List<tblCoordenadesFincaParcela> GetCoordenades()
        {
            List<tblCoordenadesFincaParcela> ret = new List<tblCoordenadesFincaParcela>();

            if (servidor.tblCoordenadesFincaParcela.Count() > 0)
            {
                foreach (var prov in servidor.tblCoordenadesFincaParcela)
                    ret.Add(prov);
            }

            return ret;
        }

        public List<tblTipoUva> GetVarietats()
        {
            List<tblTipoUva> ret = new List<tblTipoUva>();

            if (servidor.tblTipoUva.Count() > 0)
            {
                foreach (var prov in servidor.tblTipoUva)
                    ret.Add(prov);
            }

            return ret;
        }

        public List<tblFamiliesCost> GetTreballs()
        {
            List<tblFamiliesCost> ret = new List<tblFamiliesCost>();

            if (servidor.tblFamiliesCost.Count() > 0)
            {
                foreach (var prov in servidor.tblFamiliesCost)
                    ret.Add(prov);
            }

            return ret;
        }

        public List<tblPartesFinca> GetPartesFinca()
        {
            List<tblPartesFinca> ret = new List<tblPartesFinca>();

            if (servidor.tblPartesFinca.Count() > 0)
            {
                foreach (var prov in servidor.tblPartesFinca)
                    ret.Add(prov);
            }

            return ret;
        }

        public List<tblLineasPartesFinca1> GetLineasPartesFinca()
        {
            List<tblLineasPartesFinca1> ret = new List<tblLineasPartesFinca1>();

            if (servidor.tblLineasPartesFinca1.Count() > 0)
            {
                foreach (var prov in servidor.tblLineasPartesFinca1)
                    ret.Add(prov);
            }

            return ret;
        }

        public List<tblColorProducto> GetColorsProducte()
        {
            List<tblColorProducto> ret = new List<tblColorProducto>();

            if(servidor.tblColorProducto.Count() > 0)
            {
                foreach (var prov in servidor.tblColorProducto)
                    ret.Add(prov);
            }

            return ret;
        }

        public List<tblAnaliticaFincaParcela> GetAnalitiques()
        {
            List<tblAnaliticaFincaParcela> ret = new List<tblAnaliticaFincaParcela>();

            if(servidor.tblAnaliticaFincaParcela.Count() > 0)
            {
                foreach (var prov in servidor.tblAnaliticaFincaParcela)
                    ret.Add(prov);
            }

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

        public void AddParteFinca(tblPartesFinca parte)
        {
            servidor.tblPartesFinca.InsertOnSubmit(parte);
        }

        public void AddLineaParteFinca(tblLineasPartesFinca1 parte_linea)
        {
            servidor.tblLineasPartesFinca1.InsertOnSubmit(parte_linea);
        }

        public void AddAnalitica(tblAnaliticaFincaParcela analitica)
        {
            servidor.tblAnaliticaFincaParcela.InsertOnSubmit(analitica);
        }

        public void DeleteCoordenades(tblCoordenadesFincaParcela coordenates)
        {
            servidor.tblCoordenadesFincaParcela.DeleteOnSubmit(coordenates);
        }

        public void DeleteParteFinca(tblPartesFinca parte)
        {
            servidor.tblPartesFinca.DeleteOnSubmit(parte);
        }

        public void DeleteLineaParteFinca(tblLineasPartesFinca1 parte_linea)
        {
            servidor.tblLineasPartesFinca1.DeleteOnSubmit(parte_linea);
        }

        public void DeleteAnalitica(tblAnaliticaFincaParcela analitica)
        {
            servidor.tblAnaliticaFincaParcela.DeleteOnSubmit(analitica);
        }

        public void SubmitChanges()
        {
            servidor.SubmitChanges();
        }

        private DataClasses1DataContext servidor = null;
    }
}
