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

        public bool CheckServerConnection()
        {
            return servidor.DatabaseExists();
        }

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

        public List<tblLineasPartesFinca> GetLineasPartesFinca()
        {
            List<tblLineasPartesFinca> ret = new List<tblLineasPartesFinca>();

            if (servidor.tblLineasPartesFinca.Count() > 0)
            {
                foreach (var prov in servidor.tblLineasPartesFinca)
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

        public List<tblUnitatsMetriques> GetUnitatsMetriques()
        {
            List<tblUnitatsMetriques> ret = new List<tblUnitatsMetriques>();

            if(servidor.tblUnitatsMetriques.Count() > 0)
            {
                foreach (var prov in servidor.tblUnitatsMetriques)
                    ret.Add(prov);
            }

            return ret;
        }

        public List<tblPersonal> GetPersonal()
        {
            List<tblPersonal> ret = new List<tblPersonal>();

            if(servidor.tblPersonal.Count() > 0)
            {
                foreach (var prov in servidor.tblPersonal)
                    ret.Add(prov);
            }

            return ret;
        }

        public List<tblMaquinaria> GetMaquinaria()
        {
            List<tblMaquinaria> ret = new List<tblMaquinaria>();

            if (servidor.tblMaquinaria.Count() > 0)
            {
                foreach (var prov in servidor.tblMaquinaria)
                    ret.Add(prov);
            }

            return ret;
        }

        public List<tblProductesFitosanitaris> GetAdobs()
        {
            List<tblProductesFitosanitaris> ret = new List<tblProductesFitosanitaris>();

            if (servidor.tblProductesFitosanitaris.Count() > 0)
            {
                foreach (var prov in servidor.tblProductesFitosanitaris)
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

        public void AddLineaParteFinca(tblLineasPartesFinca parte_linea)
        {
            servidor.tblLineasPartesFinca.InsertOnSubmit(parte_linea);
        }

        public void AddAnalitica(tblAnaliticaFincaParcela analitica)
        {
            servidor.tblAnaliticaFincaParcela.InsertOnSubmit(analitica);
        }

        public void AddPersonal(tblPersonal personal)
        {
            servidor.tblPersonal.InsertOnSubmit(personal);
        }

        public void AddMaquinaria(tblMaquinaria maquinaria)
        {
            servidor.tblMaquinaria.InsertOnSubmit(maquinaria);
        }

        public void AddAdob(tblProductesFitosanitaris adob)
        {
            servidor.tblProductesFitosanitaris.InsertOnSubmit(adob);
        }

        public void DeleteCoordenades(tblCoordenadesFincaParcela coordenates)
        {
            servidor.tblCoordenadesFincaParcela.DeleteOnSubmit(coordenates);
        }

        public void DeleteParteFinca(tblPartesFinca parte)
        {
            servidor.tblPartesFinca.DeleteOnSubmit(parte);
        }

        public void DeleteLineaParteFinca(tblLineasPartesFinca parte_linea)
        {
            servidor.tblLineasPartesFinca.DeleteOnSubmit(parte_linea);
        }

        public void DeleteAnalitica(tblAnaliticaFincaParcela analitica)
        {
            servidor.tblAnaliticaFincaParcela.DeleteOnSubmit(analitica);
        }

        public void DeletePersonal(tblPersonal personal)
        {
            servidor.tblPersonal.DeleteOnSubmit(personal);
        }

        public void DeleteMaquinaria(tblMaquinaria maquinaria)
        {
            servidor.tblMaquinaria.DeleteOnSubmit(maquinaria);
        }

        public void DeleteAdob(tblProductesFitosanitaris adob)
        {
            servidor.tblProductesFitosanitaris.DeleteOnSubmit(adob);
        }

        public void SubmitChanges()
        {
            servidor.SubmitChanges();
        }

        private DataClasses1DataContext servidor = null;
    }
}
