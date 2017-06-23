using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApp4
{
    public class PropietarisManager
    {
        public List<Propietari> GetPropietaris()
        {
            return propietaris;
        }

        public void AfegirPropietari(Propietari propietari)
        {
            propietaris.Add(propietari);
        }

        public void EliminaPropietaris()
        {
            propietaris.Clear();
        }

        public List<Finca> GetFinques()
        {
            return finques;
        }

        public void AfegirFinca(Finca finca)
        {
            finques.Add(finca);
        }

        public void EliminaFinques()
        {
            finques.Clear();
        }

        public List<Parcela> GetParceles()
        {
            return parceles;
        }

        public void AfegirParcela(Parcela par)
        {
            parceles.Add(par);
        }

        public void EliminaParceles()
        {
            parceles.Clear();
        }

        public List<Varietat> GetVarietats()
        {
            return varietats;
        }

        public void AfegirVarietat(Varietat varietat)
        {
            varietats.Add(varietat);
        }

        public void EliminaVarietats()
        {
            varietats.Clear();
        }

        public List<Treball> GetTreballs()
        {
            return treballs;
        }

        public void AfegirTreball(Treball treball)
        {
            treballs.Add(treball);
        }

        public void EliminaTreballs()
        {
            treballs.Clear();
        }

        public List<tblCoordenadesFincaParcela> GetCoordenades()
        {
            return coordenades;
        }

        public void AfegirCoordenada(tblCoordenadesFincaParcela coor)
        {
            coordenades.Add(coor);
        }

        public void EliminaCoordenades()
        {
            coordenades.Clear();
        }
             
        public Propietari TrobaPropietariPerID(string id)
        {
            Propietari ret = null;

            for (int i = 0; i < propietaris.Count; i++)
            {
                if (propietaris[i].GetTbl().idProveedor == id)
                {
                    ret = propietaris[i];
                    break;
                }
            }

            return ret;
        }

        List<Propietari> propietaris = new List<Propietari>();
        List<Finca> finques = new List<Finca>();
        List<Parcela> parceles = new List<Parcela>();
        List<Varietat> varietats = new List<Varietat>();
        List<Treball> treballs = new List<Treball>();
        List<tblCoordenadesFincaParcela> coordenades = new List<tblCoordenadesFincaParcela>();

        public Propietari propietari_actual = null;
        public Finca finca_actual = null;
        public Parcela parcela_actual = null;
        public Varietat varietat_actual = null;
        public Treball treball_actual = null;

        public List<Label> propietaris_texts = new List<Label>();

        public bool can_point_back = false;
        public bool can_point = false;
        public ListBox curr_list_box = null;
    }

    public class Propietari
    {
        public Propietari(tblProveedores proveedor)
        {
            _tbl = proveedor;
        }

        public override string ToString()
        {
            return _tbl.Nombre;
        }

        public tblProveedores GetTbl() { return _tbl; }

        public tblProveedores _tbl = null;
    }

    public class Varietat    
    {
        public Varietat(tblTipoUva varietat)
        {
            _tbl = varietat;

            if(_tbl.ColorRGB != null)
                color = Color.FromArgb((int)_tbl.ColorRGB);
        }

        public override string ToString()
        {
            return _tbl.Nombre;
        }

        public tblTipoUva GetTbl() { return _tbl; }

        private tblTipoUva _tbl;
        public Color color = Color.Blue;
    }

    public class Treball
    {
        public Treball(tblFamiliesCost treball)
        {
            _tbl = treball;
        }

        public override string ToString()
        {
            return _tbl.Descripcio;
        }

        public tblFamiliesCost GetTbl() { return _tbl; }

        private tblFamiliesCost _tbl;
    }
}
