using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // -------------------------
        // Botons ------------------

        private void ObreFinestraSeleccioPropietari(object sender, EventArgs e)
        {
            seleccio_finca_win.SetEnabled(false);
            seleccio_varietat_win.SetEnabled(false);

            seleccio_propietari_win.SetEnabled(!seleccio_propietari_win.GetEnabled());

            if (seleccio_propietari_win.GetEnabled())
                ActualitzaLlistaPropietari();
        }

        private void ObreFinestraSeleccioFinca(object sender, EventArgs e)
        {
            seleccio_varietat_win.SetEnabled(false);
            seleccio_propietari_win.SetEnabled(false);

            seleccio_finca_win.SetEnabled(!seleccio_finca_win.GetEnabled());

            if (seleccio_finca_win.GetEnabled())
                ActualitzaLlistaFinques();
        }

        private void ObreFinestraSeleccioVarietat(object sender, EventArgs e)
        {
            seleccio_propietari_win.SetEnabled(false);
            seleccio_finca_win.SetEnabled(false);

            seleccio_varietat_win.SetEnabled(!seleccio_varietat_win.GetEnabled());

            if (seleccio_varietat_win.GetEnabled())
                ActualitzaLlistaVarietats();
        }

        private void ObreFinestraOpcionsParcela(object sender, EventArgs e)
        {
            MouseEventArgs m = e as MouseEventArgs;

            if(m.Button == MouseButtons.Right)
            {
                int i = 0;
            }
        }

        public void SeleccioPropietariGuarda(object sender, EventArgs e)
        {
            UI_ComboBox cb = ui_manager.GetElement("seleccio_propietari_noms_combobox") as UI_ComboBox;

            if(cb.IsSelected())
            {
                Propietari sele = cb.GetSelected() as Propietari;

                propietaris_manager.propietari_actual = sele;

                UI_Text t = ui_manager.GetElement("propietari_nom_text") as UI_Text;
                t.SetText(sele.GetTbl().Nombre);

                cb.CleanSelection();

                seleccio_propietari_win.SetEnabled(false);
            }
        }

        public void SeleccioFincaGuarda(object sender, EventArgs e)
        {
            UI_ComboBox cb = ui_manager.GetElement("seleccio_finca_noms_combobox") as UI_ComboBox;

            if (cb.IsSelected())
            {
                Finca sele = cb.GetSelected() as Finca;

                Propietari prop = TrobaPropietariPerFinca(sele);

                if (prop != null)
                {
                    propietaris_manager.propietari_actual = prop;
                    propietaris_manager.propietari_actual.finca_actual = sele;

                    UI_Text t = ui_manager.GetElement("propietari_nom_text") as UI_Text;
                    t.SetText(prop.GetTbl().Nombre);

                    UI_Text t2 = ui_manager.GetElement("finca_nom_text") as UI_Text;
                    t2.SetText(sele.GetTbl().Nom1);

                    ActualitzaLlistaParceles();

                    cb.CleanSelection();

                    seleccio_finca_win.SetEnabled(false);
                }
            }
        }

        public void SeleccioVarietatGuarda(object sender, EventArgs e)
        {
            UI_ComboBox cb = ui_manager.GetElement("seleccio_varietat_noms_combobox") as UI_ComboBox;

            if(cb.IsSelected())
            {
                Varietat sele = cb.GetSelected() as Varietat;

                propietaris_manager.varietat_actual = sele;

                UI_Text t = ui_manager.GetElement("varietat_nom_text") as UI_Text;
                t.SetText(sele.GetTbl().Nombre);

                ActualitzaLlistaParceles();

                cb.CleanSelection();

                seleccio_varietat_win.SetEnabled(false);
            }
        }

        public void NetejaSeleccions(object sender, EventArgs e)
        {
            if (propietaris_manager.propietari_actual != null)
            {
                propietaris_manager.propietari_actual.finca_actual = null;
                propietaris_manager.propietari_actual = null;
            }
            propietaris_manager.varietat_actual = null;

            UI_Text t = ui_manager.GetElement("propietari_nom_text") as UI_Text;
            t.SetText("No hi ha propietari seleccionat");

            UI_Text t1 = ui_manager.GetElement("finca_nom_text") as UI_Text;
            t1.SetText("No hi ha finca seleccionada");

            UI_Text t2 = ui_manager.GetElement("varietat_nom_text") as UI_Text;
            t2.SetText("No hi ha varietat seleccionada");

            seleccio_finca_win.SetEnabled(false);
            seleccio_varietat_win.SetEnabled(false);
            seleccio_propietari_win.SetEnabled(false);

            editor_parceles_panel.SetEnabled(false);

            propietaris_manager.can_point = false;

            ActualitzaLlistaParceles();
        }

        public void ParcelaClick(object sender, EventArgs e)
        {
            Label l = sender as Label;

            if(l != null)
            {
                Parcela parcela = GetParcelaPerParcelaID(l.Name);

                Finca finca = GetParcelaFinca(parcela);

                Propietari propietari = TrobaPropietariPerFinca(finca);

                propietaris_manager.propietari_actual = propietari;
                propietaris_manager.propietari_actual.finca_actual = finca;
                propietaris_manager.propietari_actual.finca_actual.parcela_actual = parcela;

                UI_Text t = ui_manager.GetElement("propietari_nom_text") as UI_Text;
                t.SetText(propietari.GetTbl().Nombre);

                UI_Text t2 = ui_manager.GetElement("finca_nom_text") as UI_Text;
                t2.SetText(finca.GetTbl().Nom1);

                propietaris_manager.can_point = true;

                editor_parceles_panel.SetEnabled(true);
                editor_parceles_crea_button.SetEnabled(false);

                if(propietaris_manager.propietari_actual.finca_actual.parcela_actual.HasPoints())
                    editor_parceles_elimina_button.SetEnabled(true);
                else
                    editor_parceles_elimina_button.SetEnabled(false);

                ActualitzaLlistaParceles();
            }
        }

        public void CreaParcela(object sender, EventArgs e)
        {
            propietaris_manager.propietari_actual.finca_actual.parcela_actual.AddMarcadors(point_manager.GetTmpMarcadors());

            for(int i = 0; i < point_manager.GetTmpMarcadors().Count; i++)
            {
                int parcela_id = propietaris_manager.propietari_actual.finca_actual.parcela_actual.GetTbl().idParcela;
                double lat = point_manager.GetTmpMarcadors()[i].GetPos().Lat;
                double lon = point_manager.GetTmpMarcadors()[i].GetPos().Lng;
                string codigo_empresa = propietaris_manager.propietari_actual.finca_actual.parcela_actual.GetTbl().CodigoEmpresa;
                int finca_di = propietaris_manager.propietari_actual.finca_actual.GetTbl().idFinca;

                tblCoordenadesFincaParcela coor = server_manager.AddCoordenades(parcela_id, lat, lon, codigo_empresa, finca_di, i);
            }

            point_manager.NetejaTmpMarcadors();

            editor_parceles_crea_button.SetEnabled(false);

            editor_parceles_elimina_button.SetEnabled(true);
        }

        public void EliminaParcela(object sender, EventArgs e)
        {
            propietaris_manager.propietari_actual.finca_actual.parcela_actual.ClearPoints();
            editor_parceles_elimina_button.SetEnabled(false);
        }

        // -------------------------


        // -------------------------
        // Servidor ----------------

        public void ActualitzaPropietarisDesDeServidor()
        {
            List<tblProveedores> proveedors = server_manager.GetProveedors();

            propietaris_manager.EliminaPropietaris();

            for (int i = 0; i < proveedors.Count; i++)
            {
                Propietari p = new Propietari(proveedors[i]);
                propietaris_manager.AfegirPropietari(p);
            }
        }

        public void ActualitzaFinquesDesDeServidor()
        {
            List<tblFinques> finques = server_manager.GetFinques();

            propietaris_manager.EliminaFinques();

            for(int i = 0; i < finques.Count; i++)
            {
                Finca f = new Finca(point_manager.overlay_finca, finques[i]);
                propietaris_manager.AfegirFinca(f);
            }
        }

        public void ActualitzaParcelesDesDeServidor()
        {
            List<tblParceles> parceles = server_manager.GetParceles();

            propietaris_manager.EliminaParceles();

            for (int i = 0; i < parceles.Count; i++)
            {
                Parcela p = new Parcela(point_manager.overlay_parcela, parceles[i]);
                propietaris_manager.AfegirParcela(p);
            }
        }

        public void ActualitzaVarietatsDesDeServidor()
        {
            List<tblTipoUva> varietats = server_manager.GetVarietats();

            propietaris_manager.EliminaVarietats();

            for(int i = 0; i<varietats.Count; i++)
            {
                Varietat v = new Varietat(varietats[i]);
                propietaris_manager.AfegirVarietat(v);
            }
        }

        // -------------------------


        // -------------------------
        // Utils -------------------

        public string EliminaAccents(string txt)
        {
            byte[] tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(txt);
            string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);
            return asciiStr;
        }

        public Propietari TrobaPropietariPerFinca(Finca finca)
        {
            Propietari ret = null;

            for (int i = 0; i < propietaris_manager.GetPropietaris().Count; i++)
            {
                string id1 = propietaris_manager.GetPropietaris()[i].GetTbl().idProveedor.Replace(" ", "");
                string id2 = finca.GetTbl().idProveedor.ToString().Replace(" ", "");

                if (id1 == id2)
                {
                    ret = propietaris_manager.GetPropietaris()[i];
                    break;
                }
            }

            return ret;
        }

        public List<Finca> GetFinquesPropietari(Propietari prop)
        {
            List<Finca> ret = new List<Finca>();

            for (int i = 0; i < propietaris_manager.GetFinques().Count; i++)
            {
                string id1 = prop.GetTbl().idProveedor.Replace(" ", "");
                string id2 = propietaris_manager.GetFinques()[i].GetTbl().idProveedor.ToString().Replace(" ", "");

                if (id1 == id2)
                    ret.Add(propietaris_manager.GetFinques()[i]);
            }

            return ret;
        }

        public List<Parcela> GetParcelesFinca(Finca fin)
        {
            List<Parcela> parceles = propietaris_manager.GetParceles();

            List<Parcela> ret = new List<Parcela>();

            for(int i = 0; i<parceles.Count; i++)
            {
                if(parceles[i].GetTbl().idFinca == fin.GetTbl().idFinca)
                {
                    ret.Add(parceles[i]);
                }
            }

            return ret;
        }

        public Finca GetParcelaFinca(Parcela p)
        {
            Finca ret = null;

            List<Finca> finques = propietaris_manager.GetFinques();

            for(int i = 0; i<finques.Count; i++)
            {
                Finca finca_actual = finques[i];

                if(finca_actual.GetTbl().idFinca == p.GetTbl().idFinca)
                {
                    ret = finca_actual;
                    break;
                }
            }

            return ret;
        }

        public Propietari GetParcelaPropietari(Parcela parcela)
        {
            Propietari ret = null;

            Finca finca = GetParcelaFinca(parcela);

            ret = TrobaPropietariPerFinca(finca);

            return ret;
        }

        public bool PropietariTeVarietat(Propietari prop, Varietat var)
        {
            List<Finca> finques = GetFinquesPropietari(prop);

            for(int i = 0; i < finques.Count ; i++)
            {
                List<Parcela> parceles = GetParcelesFinca(finques[i]);

                for(int y = 0; y < parceles.Count; y++)
                {
                    if(parceles[y].GetTbl().idVarietat == var.GetTbl().idTipoUva)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Varietat GetVarietatParcela(Parcela parcela)
        {
            Varietat ret = null;

            List<Varietat> varietats = propietaris_manager.GetVarietats();

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

            List<Parcela> parceles = propietaris_manager.GetParceles();

            for(int i = 0; i<parceles.Count; i++)
            {
                Parcela parcela_actual = parceles[i];
                if (parcela_actual.GetTbl().idParcela.ToString().Replace(" ", "") == id)
                {
                    ret = parcela_actual;
                    break;
                }
            }

            return ret;
        }

        // -------------------------

        // -------------------------
        // GMap --------------------

        // Click a un marcador per a eliminar-lo
        public void gmap_MarkerClick(GMapMarker item, MouseEventArgs e)
        {
            point_manager.EliminaTmpMarcadorSiEsTroba(item);

            if (point_manager.GetTmpMarcadors().Count >= 3 && !propietaris_manager.propietari_actual.finca_actual.parcela_actual.HasPoints())
                editor_parceles_crea_button.SetEnabled(true);

            else
                editor_parceles_crea_button.SetEnabled(false);

            point_manager.deleting_marker = true;
        }

        // Click a la pantalla per crear punts
        private void gmap_MouseClick(object sender, MouseEventArgs e)
        {
            if (!propietaris_manager.can_point)
                return;

            if (gmap.IsMouseOverMarker)
                return;

            if (point_manager.deleting_marker)
            {
                point_manager.deleting_marker = false;
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (propietaris_manager.propietari_actual.finca_actual != null)
                {
                    double lat = gmap.FromLocalToLatLng(e.X, e.Y).Lat;
                    double lng = gmap.FromLocalToLatLng(e.X, e.Y).Lng;

                    Marcador m = new Marcador(lat, lng, point_manager.overlay_markers);
                    point_manager.AfegeixTmpMarcadors(m);

                    if (point_manager.GetTmpMarcadors().Count >= 3 && !propietaris_manager.propietari_actual.finca_actual.parcela_actual.HasPoints())
                        editor_parceles_crea_button.SetEnabled(true);

                    else
                        editor_parceles_crea_button.SetEnabled(false);
                }
            }
        }

        // Actualitza la latitud i la longitud quan es mou la posicio del mapa
        public void UpdateLatLon()
        {
            if (text_input_lat != null && text_input_lon != null)
            {
                string lat_s = Math.Round(gmap.Position.Lat, 6).ToString();
                string lon_s = Math.Round(gmap.Position.Lng, 6).ToString();

                text_input_lat.SetText(lat_s.Replace(',', '.'));
                text_input_lon.SetText(lon_s.Replace(',', '.'));
            }
        }

        // -------------------------

        // Actualitza la llista de propietaris en la UI
        public void ActualitzaLlistaPropietari()
        {
            List<Propietari> proveedors = propietaris_manager.GetPropietaris();

            UI_ComboBox p = ui_manager.GetElement("seleccio_propietari_noms_combobox") as UI_ComboBox;

            p.CleanSelection();

            p.Clear();

            for (int i = 0; i < proveedors.Count; i++)
            {
                p.AddElement(proveedors[i]);
            }
        }

        public void ActualitzaLlistaFinques()
        {
            UI_ComboBox p = ui_manager.GetElement("seleccio_finca_noms_combobox") as UI_ComboBox;

            p.CleanSelection();

            p.Clear();

            if (propietaris_manager.propietari_actual == null)
            {
                List<Finca> finques = propietaris_manager.GetFinques();

                for (int i = 0; i < finques.Count; i++)
                {
                    p.AddElement(finques[i]);
                }
            }
            else
            {
                List<Finca> finques = GetFinquesPropietari(propietaris_manager.propietari_actual);

                for (int i = 0; i < finques.Count; i++)
                {
                    string id1 = propietaris_manager.propietari_actual.GetTbl().idProveedor.Replace(" ", "");
                    string id2 = finques[i].GetTbl().idProveedor.ToString().Replace(" ", "");

                    if(id1 == id2)
                        p.AddElement(finques[i]);
                }
            }
        }

        public void ActualitzaLlistaVarietats()
        {
            UI_ComboBox p = ui_manager.GetElement("seleccio_varietat_noms_combobox") as UI_ComboBox;

            p.CleanSelection();

            p.Clear();

            List<Varietat> varietats = propietaris_manager.GetVarietats();

            if (propietaris_manager.propietari_actual == null)
            {
                for(int i = 0; i < varietats.Count; i++)
                {
                    p.AddElement(varietats[i]);
                }
            }
            else
            {
                for (int i = 0; i < varietats.Count; i++)
                {
                    if(PropietariTeVarietat(propietaris_manager.propietari_actual, varietats[i]))
                    {
                        p.AddElement(varietats[i]);
                    }
                }
            }
        }

        public void ActualitzaLlistaParceles()
        {
            UI_Panel p = ui_manager.GetElement("llista_finques_panel") as UI_Panel;

            p.ClearPanel();

            List<Parcela> parceles = new List<Parcela>();

            int acumulator;

            if (propietaris_manager.propietari_actual != null)
            {
                if(propietaris_manager.propietari_actual.finca_actual == null)
                {
                    UI_Text t = new UI_Text("", new Point(5, 5), 100, 30, "No hi ha finca ni varietat seleccionats");
                    p.AddElement(t);
                    return;
                }
            }
            else if(propietaris_manager.varietat_actual != null)
            {
                parceles = propietaris_manager.GetParceles();

                acumulator = 5;
                for (int i = 0; i < parceles.Count; i++)
                {
                    Varietat varietat = GetVarietatParcela(parceles[i]);

                    if (propietaris_manager.varietat_actual != null)
                    {
                        if (varietat != propietaris_manager.varietat_actual)
                            continue;
                    }

                    UI_Text t = new UI_Text(parceles[i].GetTbl().idParcela.ToString(), new Point(5, acumulator), 100, 30, "- Parcela " + (i + 1) + ": " + varietat.GetTbl().Nombre);
                    t.GetElement().Click += new System.EventHandler(this.ParcelaClick);
                    t.GetElement().Click += new System.EventHandler(this.ObreFinestraOpcionsParcela);
                    p.AddElement(t);
                    acumulator += 18;
                }
                return;
            } 
            else
            {
                UI_Text t = new UI_Text("", new Point(5, 5), 100, 30, "No hi ha finca ni varietat seleccionats");
                p.AddElement(t);
                return;
            }

            parceles = GetParcelesFinca(propietaris_manager.propietari_actual.finca_actual);

            if (parceles.Count == 0)
            {
                UI_Text t = new UI_Text("", new Point(5, 5), 100, 30, "No hi ha finques");
                p.AddElement(t);
                return;
            }

            acumulator = 5;
            for (int i = 0; i < parceles.Count; i++)
            {
                Varietat varietat = GetVarietatParcela(parceles[i]);

                if (propietaris_manager.varietat_actual != null)
                {
                    if (varietat != propietaris_manager.varietat_actual)
                        continue;
                }

                UI_Text t = new UI_Text(parceles[i].GetTbl().idParcela.ToString(), new Point(5, acumulator), 100, 30, "- Parcela " + (i + 1) + ": " + varietat.GetTbl().Nombre);
                t.GetElement().Click += new System.EventHandler(this.ParcelaClick);
                t.GetElement().Click += new System.EventHandler(this.ObreFinestraOpcionsParcela);

                if (propietaris_manager.propietari_actual.finca_actual.parcela_actual == parceles[i])
                    t.SetColor(Color.AliceBlue, Color.Black);

                p.AddElement(t);
                acumulator += 18;
            }
        }

        // Busca una coordenada amb latitud i longitud
        public void SearchLatLon(object sender, EventArgs e)
        {
            UI_TextInput lat = ui_manager.GetElement("cordenates_lat") as UI_TextInput;
            UI_TextInput lon = ui_manager.GetElement("cordenates_lon") as UI_TextInput;

            if (lat == null || lon == null)
                return;

            double num_lat = 0;
            bool is_lat = false;
            double num_lon = 0;
            bool is_lon = false;

            if (double.TryParse(lat.GetText(), System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out num_lat))
            {
                if (num_lat <= 360)
                    is_lat = true;
            }
            if (double.TryParse(lon.GetText(), System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out num_lon))
            {
                if (num_lon <= 360)
                    is_lon = true;
            }

            if (is_lat && is_lon)
                gmap.Position = new PointLatLng(num_lat, num_lon);
        }

        // Canvia el mapa a satelit o mapa normal
        public void SwitchMapSat(object sender, EventArgs e)
        {
            UI_Button b = ui_manager.GetElement("mapsat_button") as UI_Button;

            if (b != null)
            {
                if (gmap.MapProvider == GMap.NET.MapProviders.GoogleMapProvider.Instance)
                {
                    gmap.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
                    b.SetText("Canvia a Mapa");
                }
                else
                {
                    gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
                    b.SetText("Canvia a Satel.lit");
                }
            }
        }
    }
}
