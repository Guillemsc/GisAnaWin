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
        List<tblCoordenadesFincaParcela> coordenades = new List<tblCoordenadesFincaParcela>();

        public Propietari propietari_actual = null;
        public Finca finca_actual = null;
        public Parcela parcela_actual = null;
        public Varietat varietat_actual = null;

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

            switch(varietat.idColor)
            {
                case 0:
                    color = Color.FromArgb(51, 0, 0);
                    break;
                case 1:
                    color = Color.FromArgb(51, 25, 0);
                    break;
                case 2:
                    color = Color.FromArgb(51, 51, 0);
                    break;
                case 3:
                    color = Color.FromArgb(25, 51, 0);
                    break;
                case 4:
                    color = Color.FromArgb(0, 51, 0);
                    break;
                case 5:
                    color = Color.FromArgb(0, 51, 25);
                    break;
                case 6:
                    color = Color.FromArgb(0, 51, 51);
                    break;
                case 7:
                    color = Color.FromArgb(0, 25, 51);
                    break;
                case 8:
                    color = Color.FromArgb(0, 0, 51);
                    break;
                case 9:
                    color = Color.FromArgb(25, 0, 51);
                    break;
                case 10:
                    color = Color.FromArgb(51, 0, 51);
                    break;
                case 11:
                    color = Color.FromArgb(51, 0, 25);
                    break;
                case 12:
                    color = Color.FromArgb(255, 51, 51);
                    break;
                case 13:
                    color = Color.FromArgb(255, 153, 51);
                    break;
                case 14:
                    color = Color.FromArgb(255, 255, 51);
                    break;
                case 15:
                    color = Color.FromArgb(153, 255, 51);
                    break;
                case 16:
                    color = Color.FromArgb(51, 255, 51);
                    break;
                case 17:
                    color = Color.FromArgb(51, 255, 153);
                    break;
                case 18:
                    color = Color.FromArgb(51, 255, 255);
                    break;
                case 19:
                    color = Color.FromArgb(51, 153, 255);
                    break;
                case 20:
                    color = Color.FromArgb(51, 51, 255);
                    break;
                case 21:
                    color = Color.FromArgb(153, 51, 255);
                    break;
                case 22:
                    color = Color.FromArgb(255, 51, 255);
                    break;
                case 23:
                    color = Color.FromArgb(255, 51, 153);
                    break;
                case 24:
                    color = Color.FromArgb(255, 255, 255);
                    break;
                case 25:
                    color = Color.FromArgb(255, 255, 255);
                    break;
                case 26:
                    color = Color.FromArgb(255, 255, 255);
                    break;
            }
        }

        public override string ToString()
        {
            return _tbl.Nombre;
        }

        public tblTipoUva GetTbl() { return _tbl; }

        private tblTipoUva _tbl;
        public Color color = Color.Blue;
    }
}
