using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using GMap.NET.WindowsForms;

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

        public List<tblPartesFinca> GetPartes()
        {
            return partes;
        }

        public void AfegirParte(tblPartesFinca parte)
        {
            partes.Add(parte);
        }

        public void EliminaParte(tblPartesFinca parte)
        {
            partes.Remove(parte);
        }

        public void ClearPartes()
        {
            partes.Clear();
        }

        public List<tblLineasPartesFinca> GetPartesLinea()
        {
            return partes_linea;
        }

        public void AfegirParteLinea(tblLineasPartesFinca parte)
        {
            partes_linea.Add(parte);
        }

        public void EliminaParteLinea(tblLineasPartesFinca parte)
        {
            partes_linea.Remove(parte);
        }

        public void ClearPartesLinea()
        {
            partes_linea.Clear();
        }

        public List<Analitica> GetAnalitiques()
        {
            return analitiques;
        }

        public void AfegirAnalitica(Analitica analitica)
        {
            analitiques.Add(analitica);
        }

        public void EliminaAnalitiques()
        {
            analitiques.Clear();
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

        public List<UnitatMetrica> GetUnitatsMetriques()
        {
            return unitats_metriques;
        }

        public void AfegirUnitatMetrica(UnitatMetrica uni)
        {
            unitats_metriques.Add(uni);
        }

        public void EliminaUnitatsMetriques()
        {
            unitats_metriques.Clear();
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

        public List<Parcela> GetParcelesSeleccionades() { return parceles_seleccionades; }

        public void AddParcelaSeleccionada(Parcela parcela)
        {
            if(!parceles_seleccionades.Contains(parcela))
                parceles_seleccionades.Add(parcela);
        }

        public void DeleteParcelaSeleccionada(Parcela parcela)
        {
            parceles_seleccionades.Remove(parcela);
        }

        public void ClearParcelesSeleccionades() { parceles_seleccionades.Clear(); }

        public bool ParcelesSeleccionadesConte(Parcela parcela)
        {
            bool ret = false;

            if (parceles_seleccionades.Contains(parcela))
                ret = true;
            
            return ret;
        }


        public Finca GetFincaPerParcela(Parcela p)
        {
            Finca ret = null;

            List<Finca> finques = GetFinques();

            for (int i = 0; i < finques.Count; i++)
            {
                Finca finca_actual = finques[i];

                if (finca_actual.GetTbl().idFinca == p.GetTbl().idFinca)
                {
                    ret = finca_actual;
                    break;
                }
            }

            return ret;
        }

        public int GetPartesNewId()
        {
            int ret = -1;

            List<tblPartesFinca> partes = GetPartes();
            List<Analitica> analitiques = GetAnalitiques();

            for (int i = 0; i < partes.Count; i++)
            {
                if (partes[i].idParte > ret)
                    ret = partes[i].idParte;
            }

            for (int i = 0; i < analitiques.Count; i++)
            {
                if (analitiques[i].GetTbl().idParte > ret)
                    ret = (int)analitiques[i].GetTbl().idParte;
            }

            ret++;

            return ret;
        }

        public int GetPartesLineaNewId()
        {
            int ret = -1;

            List<tblLineasPartesFinca> partes_l = GetPartesLinea();

            for (int i = 0; i < partes_l.Count; i++)
            {
                if (partes_l[i].idLinea > ret)
                    ret = partes_l[i].idLinea;
            }

            ret++;

            return ret;
        }

        public string EliminaAccents(string txt)
        {
            byte[] tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(txt);
            string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);
            return asciiStr;
        }

        public Propietari GetPropietariPerFinca(Finca finca)
        {
            Propietari ret = null;

            for (int i = 0; i < GetPropietaris().Count; i++)
            {
                string id1 = GetPropietaris()[i].GetTbl().idProveedor.Replace(" ", "");
                string id2 = finca.GetTbl().idProveedor.ToString().Replace(" ", "");

                if (id1 == id2)
                {
                    ret = GetPropietaris()[i];
                    break;
                }
            }

            return ret;
        }

        public List<Finca> GetFinquesPerPropietari(Propietari prop)
        {
            List<Finca> ret = new List<Finca>();

            for (int i = 0; i < GetFinques().Count; i++)
            {
                string id1 = prop.GetTbl().idProveedor.Replace(" ", "");
                string id2 = GetFinques()[i].GetTbl().idProveedor.ToString().Replace(" ", "");

                if (id1 == id2)
                    ret.Add(GetFinques()[i]);
            }

            return ret;
        }

        public List<Parcela> GetParcelesPerFinca(Finca fin)
        {
            List<Parcela> parceles = GetParceles();

            List<Parcela> ret = new List<Parcela>();

            for (int i = 0; i < parceles.Count; i++)
            {
                if (parceles[i].GetTbl().idFinca == fin.GetTbl().idFinca)
                {
                    ret.Add(parceles[i]);
                }
            }

            return ret;
        }

        public List<Parcela> GetParcelesPerPropietari(Propietari p)
        {
            List<Parcela> parceles = GetParceles();

            List<Parcela> ret = new List<Parcela>();

            for (int i = 0; i < parceles.Count; i++)
            {
                Finca f = GetFincaPerParcela(parceles[i]);

                if (f != null && f.GetTbl().idProveedor.ToString().Replace(" ", "") == p.GetTbl().idProveedor.Replace(" ", ""))
                {
                    ret.Add(parceles[i]);
                }
            }

            return ret;
        }

        public List<Parcela> GetParcelesPerVarietat(Varietat v)
        {
            List<Parcela> parceles = GetParceles();

            List<Parcela> ret = new List<Parcela>();

            for (int i = 0; i < parceles.Count; i++)
            {
                if (parceles[i].GetTbl().idVarietat == v.GetTbl().idTipoUva)
                {
                    ret.Add(parceles[i]);
                }
            }

            return ret;
        }

        public Propietari GetPropietariPerParcela(Parcela parcela)
        {
            Propietari ret = null;

            Finca finca = GetFincaPerParcela(parcela);

            ret = GetPropietariPerFinca(finca);

            return ret;
        }

        public bool PropietariTeVarietat(Propietari prop, Varietat var)
        {
            List<Finca> finques = GetFinquesPerPropietari(prop);

            for (int i = 0; i < finques.Count; i++)
            {
                List<Parcela> parceles = GetParcelesPerFinca(finques[i]);

                for (int y = 0; y < parceles.Count; y++)
                {
                    if (parceles[y].GetTbl().idVarietat == var.GetTbl().idTipoUva)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Varietat GetVarietatPerParcela(Parcela parcela)
        {
            Varietat ret = null;

            List<Varietat> varietats = GetVarietats();

            for (int i = 0; i < varietats.Count; i++)
            {
                if (parcela.GetTbl().idVarietat == varietats[i].GetTbl().idTipoUva)
                {
                    ret = varietats[i];
                    break;
                }
            }

            return ret;
        }

        public Parcela GetParcelaPerParcelaID(string id)
        {
            Parcela ret = null;

            List<Parcela> parceles = GetParceles();

            for (int i = 0; i < parceles.Count; i++)
            {
                Parcela parcela_actual = parceles[i];
                if (parcela_actual.GetTbl().idParcela.ToString().Replace(" ", "") == id.ToString().Replace(" ", ""))
                {
                    ret = parcela_actual;
                    break;
                }
            }

            return ret;
        }

        public List<tblCoordenadesFincaParcela> GetCoordenadesPerParcela(Parcela pa)
        {
            List<tblCoordenadesFincaParcela> ret = new List<tblCoordenadesFincaParcela>();

            List<tblCoordenadesFincaParcela> coordenades = GetCoordenades();

            for (int i = 0; i < coordenades.Count; i++)
            {
                if (coordenades[i].idParcela == pa.GetTbl().idParcela)
                {
                    ret.Add(coordenades[i]);
                }
            }

            return ret;
        }

        public Parcela GetParcelaPolygon(GMapPolygon item)
        {
            Parcela ret = null;

            List<Parcela> parceles = GetParceles();

            for (int i = 0; i < parceles.Count; i++)
            {
                if (parceles[i].GetPolygon() == item)
                {
                    ret = parceles[i];
                    break;
                }
            }

            return ret;
        }

        public Finca GetFincaPerId(string id)
        {
            Finca ret = null;

            List<Finca> finques = GetFinques();

            for (int i = 0; i < finques.Count; i++)
            {
                if (finques[i].GetTbl().idFinca.ToString().Replace(" ", "") == id.Replace(" ", ""))
                {
                    ret = finques[i];
                }
            }

            return ret;
        }

        public tblPartesFinca GetPartePerParteId(int id)
        {
            List<tblPartesFinca> partes = GetPartes();

            for (int i = 0; i < partes.Count; i++)
            {
                if (partes[i].idParte == id)
                    return partes[i];
            }

            return null;
        }

        public List<tblLineasPartesFinca> GetPartesLineaPerParte(tblPartesFinca parte)
        {
            List<tblLineasPartesFinca> ret = new List<tblLineasPartesFinca>();

            List<tblLineasPartesFinca> partes_linea = GetPartesLinea();

            for (int i = 0; i < partes_linea.Count; i++)
            {
                if (partes_linea[i].idParte == parte.idParte)
                    ret.Add(partes_linea[i]);
            }

            return ret;
        }

        public tblLineasPartesFinca GetLineaPartePerLineaID(int id)
        {
            List<tblLineasPartesFinca> partes_linea = GetPartesLinea();

            for (int i = 0; i < partes_linea.Count; i++)
            {
                if (partes_linea[i].idLinea == id)
                    return partes_linea[i];
            }

            return null;
        }

        public List<tblPartesFinca> GetPartesPerFincaId(int id)
        {
            List<tblPartesFinca> ret = new List<tblPartesFinca>();

            List<tblPartesFinca> partes = GetPartes();

            for (int i = 0; i < partes.Count; i++)
            {
                if (partes[i].idFinca == id)
                {
                    ret.Add(partes[i]);
                }
            }

            return ret;
        }

        public Treball GetTreballPerTreballId(int id)
        {
            Treball ret = null;

            List<Treball> treballs = GetTreballs();

            for (int i = 0; i < treballs.Count; i++)
            {
                if (treballs[i].GetTbl().idCost == id)
                {
                    ret = treballs[i];
                    break;
                }
            }

            return ret;
        }

        public Analitica GetAnaliticaPerId(int id)
        {
            Analitica ret = null;

            List<Analitica> analitiques = GetAnalitiques();

            for (int i = 0; i < analitiques.Count; i++)
            {
                if (analitiques[i].GetTbl().idAnalitica == id)
                {
                    ret = analitiques[i];
                    break;
                }
            }

            return ret;
        }

        public Finca GetFincaPerParte(tblPartesFinca parte)
        {
            Finca ret = null;

            List<Finca> finques = GetFinques();

            for (int i = 0; i < finques.Count; i++)
            {
                Finca finca_actual = finques[i];

                if (finca_actual.GetTbl().idFinca == parte.idFinca)
                {
                    ret = finca_actual;
                    break;
                }
            }

            return ret;
        }

        public Propietari GetPropietariPerParte(tblPartesFinca parte)
        {
            Propietari ret = null;

            Finca f = GetFincaPerParte(parte);

            if (f != null)
            {
                ret = GetPropietariPerFinca(f);
            }

            return ret;
        }

        public List<tblLineasPartesFinca> GetLineasPerParteId(int id)
        {
            List<tblLineasPartesFinca> ret = new List<tblLineasPartesFinca>();

            List<tblLineasPartesFinca> partes_l = GetPartesLinea();

            for (int i = 0; i < partes_l.Count; i++)
            {
                if (partes_l[i].idParte == id)
                {
                    ret.Add(partes_l[i]);
                }
            }

            return ret;
        }

        public UnitatMetrica GetUnitatMetricaPerId(int id)
        {
            UnitatMetrica ret = null;

            List<UnitatMetrica> unitats = GetUnitatsMetriques();

            for(int i = 0; i < unitats.Count(); i++)
            {
                if(unitats[i].GetTbl().id == id)
                {
                    ret = unitats[i];
                    break;
                }
            }

            return ret;
        }

        public UnitatMetrica GetUnitatMetricaPerNom(string nom)
        {
            UnitatMetrica ret = null;

            List<UnitatMetrica> unitats = GetUnitatsMetriques();

            for (int i = 0; i < unitats.Count(); i++)
            {
                if (unitats[i].GetTbl().Unitat.ToLower().Replace(" ", "") == nom.ToLower().Replace(" ", ""))
                {
                    ret = unitats[i];
                    break;
                }
            }

            return ret;
        }


        List<Propietari>                 propietaris = new List<Propietari>();
        List<Finca>                      finques = new List<Finca>();
        List<Parcela>                    parceles = new List<Parcela>();
        List<Varietat>                   varietats = new List<Varietat>();
        List<Treball>                    treballs = new List<Treball>();
        List<UnitatMetrica>              unitats_metriques = new List<UnitatMetrica>();
        List<tblPartesFinca>             partes = new List<tblPartesFinca>();
        List<tblLineasPartesFinca>       partes_linea = new List<tblLineasPartesFinca>();
        List<Analitica>                  analitiques = new List<Analitica>();
        List<tblCoordenadesFincaParcela> coordenades = new List<tblCoordenadesFincaParcela>();

        public Propietari                propietari_actual = null;
        public Finca                     finca_actual = null;
        public Parcela                   parcela_actual = null;
        public Varietat                  varietat_actual = null;
        public Treball                   treball_actual = null;
        public tblPartesFinca            parte_actual = null;
        public tblLineasPartesFinca      parte_linea_actual = null;

        List<Parcela>                    parceles_seleccionades = new List<Parcela>();

        public bool                      can_point_back = false;
        public bool                      can_point = false;
        public ListBox                   curr_list_box = null;

        public string                    empresa_nom;
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
        public Varietat(tblTipoUva varietat, tblColorProducto color_prod)
        {
            _tbl = varietat;

            if(_tbl.ColorRGB != null)
                color = Color.FromArgb((int)_tbl.ColorRGB);

            if (color_prod != null)
                color_producto = color_prod;
        }

        public override string ToString()
        {
            string name = _tbl.Nombre;

            if (color_producto != null)
                name += (" ( " + color_producto.Color.Replace(" ", "").ToLower() + ")");

            return name;
        }

        public tblTipoUva GetTbl() { return _tbl; }

        private tblTipoUva _tbl;
        public Color color = Color.Blue;
        private tblColorProducto color_producto = null;
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

    public class Analitica
    {
        public Analitica(tblAnaliticaFincaParcela analitica)
        {
            _tbl = analitica;
        }

        public override string ToString()
        {
            return _tbl.Fecha.ToString();   
        }

        public tblAnaliticaFincaParcela GetTbl() { return _tbl; }

        private tblAnaliticaFincaParcela _tbl;
    }

    public class UnitatMetrica
    {
        public UnitatMetrica(tblUnitatsMetriques _uni)
        {
            uni = _uni;
        }

        public override string ToString()
        {
            return uni.Unitat;
        }

        public tblUnitatsMetriques GetTbl()
        {
            return uni;
        }

        private tblUnitatsMetriques uni;
    }
}
