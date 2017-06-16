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

        public Propietari propietari_actual = null;
        public Varietat varietat_actual = null;

        public List<Label> propietaris_texts = new List<Label>();

        public bool can_point = false;
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

        public void AfegirFinca(Finca finca)
        {
            finques.Add(finca);
        }

        public Finca GetFincaPerID(string id)
        {
            Finca ret = null;

            for (int i = 0; i < finques.Count; i++)
            {
                if (finques[i].GetTbl().idFinca.ToString() == id)
                {
                    ret = finques[i];
                    break;
                }
            }

            return ret;
        }

        public Finca GetFincaPerIndex(int index)
        {
            Finca ret = null;

            if (index < finques.Count())
            {
                ret = finques[index];
            }

            return ret;
        }

        public void EliminarFinca(Finca finca)
        {
            for (int i = 0; i < finques.Count; i++)
            {
                if (finques[i] == finca)
                {
                    finques.Remove(finca);
                    break;
                }
            }
        }

        public tblProveedores GetTbl() { return _tbl; }

        public List<Finca> finques = new List<Finca>();
        public Finca finca_actual = null;

        public tblProveedores _tbl = null;
    }

    public class Varietat    
    {
        public Varietat(tblTipoUva varietat)
        {
            _tbl = varietat;
        }

        public override string ToString()
        {
            return _tbl.Nombre;
        }

        public tblTipoUva GetTbl() { return _tbl; }

        private tblTipoUva _tbl;
    }
}
