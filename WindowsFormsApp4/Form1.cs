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
            if (seleccio_propietari_noms_combobox.IsSelected())
            {
                Propietari sele = seleccio_propietari_noms_combobox.GetSelected() as Propietari;

                propietaris_manager.propietari_actual = sele;

                propietari_nom_text.SetText(sele.GetTbl().Nombre);

                seleccio_propietari_noms_combobox.CleanSelection();

                seleccio_propietari_win.SetEnabled(false);
            }
        }

        public void SeleccioFincaGuarda(object sender, EventArgs e)
        {
            if (seleccio_finca_noms_combobox.IsSelected())
            {
                Finca sele = seleccio_finca_noms_combobox.GetSelected() as Finca;

                Propietari prop = TrobaPropietariPerFinca(sele);

                if (prop != null)
                {
                    propietaris_manager.propietari_actual = prop;
                    propietaris_manager.propietari_actual.finca_actual = sele;

                    propietari_nom_text.SetText(prop.GetTbl().Nombre);

                    finca_nom_text.SetText(sele.GetTbl().Nom1);

                    ActualitzaLlistaParceles();

                    seleccio_finca_noms_combobox.CleanSelection();

                    seleccio_finca_win.SetEnabled(false);
                }
            }
        }

        public void SeleccioVarietatGuarda(object sender, EventArgs e)
        {
            if(seleccio_varietat_noms_combobox.IsSelected())
            {
                Varietat sele = seleccio_varietat_noms_combobox.GetSelected() as Varietat;

                propietaris_manager.varietat_actual = sele;

                varietat_nom_text.SetText(sele.GetTbl().Nombre);

                ActualitzaLlistaParceles();

                seleccio_varietat_noms_combobox.CleanSelection();

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

            propietari_nom_text.SetText("No hi ha propietari seleccionat");

            finca_nom_text.SetText("No hi ha finca seleccionada");

            varietat_nom_text.SetText("No hi ha varietat seleccionada");

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

                propietari_nom_text.SetText(propietari.GetTbl().Nombre);

                finca_nom_text.SetText(finca.GetTbl().Nom1);

                propietaris_manager.can_point = true;

                editor_parceles_panel.SetEnabled(true);
                editor_parceles_crea_button.SetEnabled(false);
                editor_parceles_guarda_button.SetEnabled(true);

                if (propietaris_manager.propietari_actual.finca_actual.parcela_actual.HasPoints())
                {
                    editor_parceles_elimina_button.SetEnabled(true);
                    gmap.Position = propietaris_manager.propietari_actual.finca_actual.parcela_actual.GetCenterPos();
                }
                else
                    editor_parceles_elimina_button.SetEnabled(false);

                ActualitzaLlistaParceles();
            }
        }

        public void CreaParcela(object sender, EventArgs e)
        {
            List<tblCoordenadesFincaParcela> coor_list = new List<tblCoordenadesFincaParcela>();

            for(int i = 0; i < point_manager.GetTmpMarcadors().Count; i++)
            {
                int parcela_id = propietaris_manager.propietari_actual.finca_actual.parcela_actual.GetTbl().idParcela;
                double lat = point_manager.GetTmpMarcadors()[i].GetPos().Lat;
                double lon = point_manager.GetTmpMarcadors()[i].GetPos().Lng;
                string codigo_empresa = propietaris_manager.propietari_actual.finca_actual.parcela_actual.GetTbl().CodigoEmpresa;
                int finca_di = propietaris_manager.propietari_actual.finca_actual.GetTbl().idFinca;

                tblCoordenadesFincaParcela coor = server_manager.AddCoordenades(parcela_id, lat, lon, codigo_empresa, finca_di, i);
                coor_list.Add(coor);
            }

            propietaris_manager.propietari_actual.finca_actual.parcela_actual.AddCoordenades(coor_list);

            point_manager.NetejaTmpMarcadors();

            editor_parceles_crea_button.SetEnabled(false);

            editor_parceles_elimina_button.SetEnabled(true);

            propietaris_manager.can_point = false;
        }

        public void EliminaParcela(object sender, EventArgs e)
        {
            List<tblCoordenadesFincaParcela> c = propietaris_manager.propietari_actual.finca_actual.parcela_actual.GetCoordenades();

            for(int i = 0; i < c.Count; i++)
            {
                server_manager.DeleteCoordenades(c[i]);
            }

            propietaris_manager.propietari_actual.finca_actual.parcela_actual.ClearPoints();
            editor_parceles_elimina_button.SetEnabled(false);

            propietaris_manager.can_point = true;
        }

        public void GuardaCanvis(object sender, EventArgs e)
        {
            server_manager.SubmitChanges();
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

            ActualitzaCoordenadesDesDeServidor(propietaris_manager.GetParceles());
        }

        public void ActualitzaCoordenadesDesDeServidor(List<Parcela> parceles)
        {
            List<tblCoordenadesFincaParcela> coordenades = server_manager.GetCoordenades();

            propietaris_manager.EliminaCoordenades();

            for (int i = 0; i < coordenades.Count; i++)
            {
                propietaris_manager.AfegirCoordenada(coordenades[i]);
            }

            for(int z = 0; z < parceles.Count; z++)
            {
                Parcela p_actual = parceles[z];
                List<tblCoordenadesFincaParcela> c = GetCoordenadesPerParcela(p_actual);
                p_actual.AddCoordenades(c);
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

        public List<tblCoordenadesFincaParcela> GetCoordenadesPerParcela(Parcela pa)
        {
            List<tblCoordenadesFincaParcela> ret = new List<tblCoordenadesFincaParcela>();

            List<tblCoordenadesFincaParcela> coordenades = propietaris_manager.GetCoordenades();

            for(int i = 0; i<coordenades.Count; i++)
            {
                if(coordenades[i].idParcela == pa.GetTbl().idParcela)
                {
                    ret.Add(coordenades[i]);
                }
            }

            return ret;
        }

        public Parcela GetParcelaPolygon(GMapPolygon item)
        {
            Parcela ret = null;

            List<Parcela> parceles = propietaris_manager.GetParceles();

            for (int i = 0; i < parceles.Count; i++)
            {
                if(parceles[i].GetPolygon() == item)
                {
                    ret = parceles[i];
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

        private void gmap_PoligonEnter(GMapPolygon item)
        {
            propietaris_manager.curr_list_box = null;

            Parcela p = GetParcelaPolygon(item);

            if(p != null)
            {
                string nom = GetParcelaFinca(p).GetTbl().Nom1;
                string propietari = GetParcelaPropietari(p).GetTbl().Nombre;
                string varietat = GetVarietatParcela(p).GetTbl().Nombre;
                string vitivin = p.GetTbl().idParcelaVinicola;
                string any_plant = p.GetTbl().AnyPlantacio.ToString();
                string n_plantes = p.GetTbl().NumPlantes.ToString();
                string ha = p.GetTbl().Ha.ToString();
                string poligon = p.GetTbl().Poligon.Replace(" ", "");
                string parcela = p.GetTbl().Parcela.Replace(" ", "");

                GPoint gpos = gmap.FromLatLngToLocal(p.GetCenterPos());
                Point pos = new Point((int)gpos.X, (int)gpos.Y);

                propietaris_manager.curr_list_box = new ListBox();
                propietaris_manager.curr_list_box.Location = pos;
                propietaris_manager.curr_list_box.Width = 190;
                propietaris_manager.curr_list_box.Height = 110;
                propietaris_manager.curr_list_box.Show();

                propietaris_manager.curr_list_box.Items.Add("Finca: " + nom);
                propietaris_manager.curr_list_box.Items.Add("Propietari: " + propietari);
                propietaris_manager.curr_list_box.Items.Add("Varietat: " + varietat);
                propietaris_manager.curr_list_box.Items.Add("Parcela vitivinicola: " + vitivin);
                propietaris_manager.curr_list_box.Items.Add("Any: " + any_plant);
                propietaris_manager.curr_list_box.Items.Add("Nº Plant: " + n_plantes);
                propietaris_manager.curr_list_box.Items.Add("Ha: " + ha);
                propietaris_manager.curr_list_box.Items.Add(poligon + "/" + parcela);

                this.Controls.Add(propietaris_manager.curr_list_box);
                propietaris_manager.curr_list_box.BringToFront();

                propietaris_manager.can_point_back = propietaris_manager.can_point;
                propietaris_manager.can_point = false;
            }
        }

        private void gmap_PoligonOut(GMapPolygon item)
        {
            propietaris_manager.curr_list_box.Hide();
            this.Controls.Remove(propietaris_manager.curr_list_box);
            propietaris_manager.curr_list_box = null;

            propietaris_manager.can_point = propietaris_manager.can_point_back;
        }

        private void gmap_PoligonClick(GMapPolygon item, MouseEventArgs e)
        {
            Parcela par = GetParcelaPolygon(item);
            Finca fin = GetParcelaFinca(par);
            Propietari prop = GetParcelaPropietari(par);

            propietaris_manager.propietari_actual = prop;
            propietaris_manager.propietari_actual.finca_actual = fin;

            propietari_nom_text.SetText(prop.GetTbl().Nombre);

            finca_nom_text.SetText(fin.GetTbl().Nom1);

            ActualitzaLlistaParceles();
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

            seleccio_propietari_noms_combobox.CleanSelection();

            seleccio_propietari_noms_combobox.Clear();

            for (int i = 0; i < proveedors.Count; i++)
            {
                seleccio_propietari_noms_combobox.AddElement(proveedors[i]);
            }
        }

        public void ActualitzaLlistaFinques()
        {
            seleccio_finca_noms_combobox.CleanSelection();

            seleccio_finca_noms_combobox.Clear();

            if (propietaris_manager.propietari_actual == null)
            {
                List<Finca> finques = propietaris_manager.GetFinques();

                for (int i = 0; i < finques.Count; i++)
                {
                    seleccio_finca_noms_combobox.AddElement(finques[i]);
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
                        seleccio_finca_noms_combobox.AddElement(finques[i]);
                }
            }
        }

        public void ActualitzaLlistaVarietats()
        {
            seleccio_varietat_noms_combobox.CleanSelection();

            seleccio_varietat_noms_combobox.Clear();

            List<Varietat> varietats = propietaris_manager.GetVarietats();

            if (propietaris_manager.propietari_actual == null)
            {
                for(int i = 0; i < varietats.Count; i++)
                {
                    seleccio_varietat_noms_combobox.AddElement(varietats[i]);
                }
            }
            else
            {
                for (int i = 0; i < varietats.Count; i++)
                {
                    if(PropietariTeVarietat(propietaris_manager.propietari_actual, varietats[i]))
                    {
                        seleccio_varietat_noms_combobox.AddElement(varietats[i]);
                    }
                }
            }
        }

        public void ActualitzaLlistaParceles()
        {
            llista_finques_panel.ClearPanel();

            List<Parcela> parceles = new List<Parcela>();

            int acumulator;

            if (propietaris_manager.propietari_actual != null)
            {
                if(propietaris_manager.propietari_actual.finca_actual == null)
                {
                    UI_Text t = new UI_Text(new Point(5, 5), 100, 30, "No hi ha finca ni varietat seleccionats");
                    llista_finques_panel.AddElement(t);
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

                    UI_Text t = new UI_Text(new Point(5, acumulator), 100, 30, "- Parcela " + (i + 1) + ": " + varietat.GetTbl().Nombre, parceles[i].GetTbl().idParcela.ToString());
                    t.GetElement().Click += new System.EventHandler(this.ParcelaClick);
                    t.GetElement().Click += new System.EventHandler(this.ObreFinestraOpcionsParcela);
                    llista_finques_panel.AddElement(t);
                    acumulator += 18;
                }
                return;
            } 
            else
            {
                UI_Text t = new UI_Text(new Point(5, 5), 100, 30, "No hi ha finca ni varietat seleccionats");
                llista_finques_panel.AddElement(t);
                return;
            }

            parceles = GetParcelesFinca(propietaris_manager.propietari_actual.finca_actual);

            if (parceles.Count == 0)
            {
                UI_Text t = new UI_Text(new Point(5, 5), 100, 30, "No hi ha finques");
                llista_finques_panel.AddElement(t);
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

                UI_Text t = new UI_Text(new Point(5, acumulator), 100, 30, "- Parcela " + (i + 1) + ": " + varietat.GetTbl().Nombre, parceles[i].GetTbl().idParcela.ToString());
                t.GetElement().Click += new System.EventHandler(this.ParcelaClick);
                t.GetElement().Click += new System.EventHandler(this.ObreFinestraOpcionsParcela);

                if (propietaris_manager.propietari_actual.finca_actual.parcela_actual == parceles[i])
                    t.SetColor(Color.AliceBlue, Color.Black);

                llista_finques_panel.AddElement(t);
                acumulator += 18;
            }
        }

        // Busca una coordenada amb latitud i longitud
        public void SearchLatLon(object sender, EventArgs e)
        {
            UI_TextInput lat = text_input_lat;
            UI_TextInput lon = text_input_lon;

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
            if (mapsat_button != null)
            {
                if (gmap.MapProvider == GMap.NET.MapProviders.GoogleMapProvider.Instance)
                {
                    gmap.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
                    mapsat_button.SetText("Canvia a Mapa");
                }
                else
                {
                    gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
                    mapsat_button.SetText("Canvia a Satel.lit");
                }
            }
        }
    }
}
