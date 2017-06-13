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

        // Debug -------
        private void crea_Click(object sender, EventArgs e)
        {
            if (point_manager.GetTmpMarcadors().Count > 2)
            {
                Parcela p = new Parcela(point_manager.GetTmpMarcadors(), point_manager.overlay_parcela, id_manager.GetNewID("parcela"));
                point_manager.AfegeixTmpParcela(p);
            }
        }

        private void neteja_Click(object sender, EventArgs e)
        {
            point_manager.NetejaTmpMarcadors();
            point_manager.NetejaTmpParceles();
        }
        // --------------

        // Click a un marcador per a eliminar-lo
        public void gmap_MarkerClick(GMapMarker item, MouseEventArgs e)
        {
            point_manager.EliminaTmpMarcadorSiEsTroba(item);

            if (point_manager.GetTmpMarcadors().Count >= 3)
                ui_manager.GetElement("crea_parcela").SetEnabled(true);

            else
                ui_manager.GetElement("crea_parcela").SetEnabled(false);

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
                if (propietaris_manager.propietari_actual != null && propietaris_manager.propietari_actual.finca_actual != null)
                {
                    double lat = gmap.FromLocalToLatLng(e.X, e.Y).Lat;
                    double lng = gmap.FromLocalToLatLng(e.X, e.Y).Lng;

                    Marcador m = new Marcador(lat, lng, point_manager.overlay_markers);
                    point_manager.AfegeixTmpMarcadors(m);

                    if (point_manager.GetTmpMarcadors().Count >= 3)
                        ui_manager.GetElement("crea_parcela").SetEnabled(true);

                    else
                        ui_manager.GetElement("crea_parcela").SetEnabled(false);
                }
            }
        }

        // Obra la finestra per a crear un nou propietari
        private void AfegeixPropietari(object sender, EventArgs e)
        {
            ui_manager.GetUIWindow("afegir_propietari_window").SetEnabled(true);
        }

        // Tanca la finestra per a crear un nou propietari
        private void TancarAfegeixPropietari(object sender, EventArgs e)
        {
            ui_manager.GetUIWindow("afegir_propietari_window").SetEnabled(false);
        }

        // Afegeix un nou propietari
        private void AfegirPropietari(object sender, EventArgs e)
        {
            MaskedTextBox mt = ui_manager.GetElement("nom_propietari_text_input").GetElement() as MaskedTextBox;

            if (mt != null)
            {
                if (mt.Text != "" && mt.Text.Length > 2)
                {
                    Propietari pro = new Propietari(mt.Text);
                    propietaris_manager.AfegirPropietari(pro);

                    ActualitzaUIPropietari();

                    afegir_propietari_win.SetEnabled(false);
                }
            }
        }

        // Click a un text dels propietaris
        private void PropietariClick(object sender, EventArgs e)
        {
            Label b = sender as Label;
            Propietari p = propietaris_manager.GetPropietariPerNom(b.Name);

            if (p != null)
            {
                if (propietaris_manager.propietari_actual != null)
                    propietaris_manager.propietari_actual.ClearDraw();

                propietaris_manager.propietari_actual = p;

                main_win.SetEnabled(false);
                propietari_info_win.SetEnabled(true);

                UI_Text t = ui_manager.GetElement("nom_propietari") as UI_Text;
                t.SetText(propietaris_manager.propietari_actual.GetNom());

                ActualitzaUIFinca();

                ui_manager.GetElement("crea_parcela").SetEnabled(false);

                propietaris_manager.propietari_actual.Draw();
            }
        }

        // Canvia de propietari
        public void CanviaPropietari(object sender, EventArgs e)
        {
            if (propietaris_manager.propietari_actual != null)
            {
                if (propietaris_manager.propietari_actual.finca_actual != null)
                {
                    propietaris_manager.propietari_actual.finca_actual = null;
                }
                propietaris_manager.propietari_actual.ClearDraw();
                propietaris_manager.propietari_actual = null;
            }

            propietari_info_win.SetEnabled(false);
            main_win.SetEnabled(true);
        }

        // Obra la finestra per a crear una nova finca
        public void AfegeixFinca(object sender, EventArgs e)
        {
            afegir_finca_win.SetEnabled(true);
            opcions_finca_win.SetEnabled(false);

            propietaris_manager.propietari_actual.finca_actual = null;
        }

        // Afegeix una nova finca o modifica una actual
        public void AfegirFinca(object sender, EventArgs e)
        {
            afegir_finca_win.SetEnabled(false);

            MaskedTextBox mt = ui_manager.GetElement("nom_finca_text_input").GetElement() as MaskedTextBox;

            if (mt.Text != "")
            {
                Finca f = new Finca(point_manager.overlay_finca, mt.Text, id_manager.GetNewID("finca"));
                propietaris_manager.propietari_actual.AfegirFinca(f);

                propietaris_manager.propietari_actual.finca_actual = f;

                propietaris_manager.can_point = true;

                ActualitzaUIFinca();
            }
        }

        public void ActualitzaFinca(object sender, EventArgs e)
        {
            MaskedTextBox mt = ui_manager.GetElement("opcions_nom_finca_text_input").GetElement() as MaskedTextBox;

            propietaris_manager.propietari_actual.finca_actual.SetNom(mt.Text);

            opcions_finca_win.SetEnabled(false);

            ActualitzaUIFinca();
        }

        // Selecciona una finca
        public void FinquesClick(object sender, EventArgs e)
        {
            Label l = sender as Label;
            Finca f = propietaris_manager.propietari_actual.GetFincaPerID(Int32.Parse(l.Name));

            if (f != null)
            {
                propietaris_manager.propietari_actual.finca_actual = f;

                propietaris_manager.can_point = true;

                ActualitzaUIFinca();

                // Obre finestra per actualitzar finca
                opcions_finca_win.SetEnabled(true);
                afegir_finca_win.SetEnabled(false);

                MaskedTextBox mt = ui_manager.GetElement("opcions_nom_finca_text_input").GetElement() as MaskedTextBox;
                mt.Text = propietaris_manager.propietari_actual.finca_actual.GetNom();
            }
        }

        // Selecciona una finca i obre les opcions
        public void FinquesDobleClick(object sender, EventArgs e)
        {
            Label l = sender as Label;
            Finca f = propietaris_manager.propietari_actual.GetFincaPerID(Int32.Parse(l.Name));

            if (f != null)
            {
                propietaris_manager.propietari_actual.finca_actual = f;

                propietaris_manager.can_point = true;

                ActualitzaUIFinca();
            }

            afegir_finca_win.SetEnabled(true);
        }

        // Selecciona una parcela
        public void ParcelaClick(object sender, EventArgs e)
        {
            Label l = sender as Label;

            Finca f = TrobaFincaPerParcelaID(Int32.Parse(l.Name));

            if (f != null)
            {
                propietaris_manager.propietari_actual.finca_actual = f;

                Parcela p = TrobaParcelaAFincaActual(Int32.Parse(l.Name));

                if (p != null)
                {
                    propietaris_manager.propietari_actual.finca_actual.parcela_actual = p;
                    ActualitzaUIFinca();

                    gmap.Position = propietaris_manager.propietari_actual.finca_actual.parcela_actual.GetCenterPos();
                    UpdateLatLon();
                }
            }
            //Parcela p = propietaris_manager.propietari_actual.finca_actual
        }

        // Afegeix una parcela si hi ha 3> punts en pantalla
        public void AfegeixParcela(object sender, EventArgs e)
        {
            if (point_manager.GetTmpMarcadors().Count >= 3)
            {
                Parcela p = new Parcela(point_manager.GetTmpMarcadors(), point_manager.overlay_parcela, id_manager.GetNewID("parcela"));
                propietaris_manager.propietari_actual.finca_actual.AfegeixParcela(p);

                propietaris_manager.propietari_actual.finca_actual.parcela_actual = p;
                p.SetText(propietaris_manager.propietari_actual.finca_actual.parcela_actual.GetDescripcio());

                point_manager.NetejaTmpMarcadors();

                ActualitzaUIFinca();

                ui_manager.GetElement("crea_parcela").SetEnabled(false);
            }
        }

        // Actualitza la UI que mostra tots els propietaris
        public void ActualitzaUIPropietari()
        {
            UI_Panel p = ui_manager.GetElement("propietaris_panel") as UI_Panel;

            if (p != null)
            {
                p.ClearPanel();

                int acumulation = 0;
                for (int i = 0; i < propietaris_manager.GetPropietaris().Count; i++)
                {
                    UI_Text t = new UI_Text(propietaris_manager.GetPropietaris()[i].GetNom(), new Point(5, 10 + acumulation), 20, 40, "- " + propietaris_manager.GetPropietaris()[i].GetNom());
                    t.GetElement().Click += new EventHandler(PropietariClick);
                    p.AddElement(t);
                    acumulation += 18;
                }
            }
        }

        // Actualitza la UI que mostra les finques i les parceles
        public void ActualitzaUIFinca()
        {
            UI_Panel pan = ui_manager.GetElement("finques_panel") as UI_Panel;
            int scroll_value = 0;

            if (pan != null)
            {
                Panel p = pan.GetElement() as Panel;
                scroll_value = p.VerticalScroll.Value;

                pan.ClearPanel();

                // Finques
                int acumulation = 0;
                for (int i = 0; i < propietaris_manager.propietari_actual.finques.Count(); i++)
                {
                    Finca curr_finca = propietaris_manager.propietari_actual.finques[i];

                    UI_Text t2 = new UI_Text(curr_finca.GetID().ToString(), new Point(5, 0 + acumulation), 20, 40, "- " + curr_finca.GetNom());
                    t2.GetElement().Click += new EventHandler(FinquesClick);
                    t2.GetElement().DoubleClick += new EventHandler(FinquesDobleClick);
                    pan.AddElement(t2);
                    acumulation += 18;

                    // Finca seleccionada
                    if (curr_finca == propietaris_manager.propietari_actual.finca_actual)
                    {
                        t2.SetColor(Color.Crimson, Color.White);
                    }

                    // Parceles
                    for (int y = 0; y < curr_finca.parceles.Count(); y++)
                    {
                        Parcela curr_parcela = curr_finca.parceles[y];

                        UI_Text t3 = new UI_Text(curr_parcela.GetID().ToString(), new Point(20, 0 + acumulation), 20, 40, "o Parcela:" + (y + 1).ToString());
                        t3.GetElement().Click += new EventHandler(ParcelaClick);
                        pan.AddElement(t3);
                        acumulation += 18;

                        // Parcela seleccionada
                        if (curr_parcela == propietaris_manager.propietari_actual.finca_actual.parcela_actual)
                        {
                            t3.SetColor(Color.Crimson, Color.White);
                        }
                    }
                }

                p.VerticalScroll.Value = scroll_value;
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

        public void EliminaParcela(object sender, EventArgs e)
        {

        }

        // Troba la finca que conte la parcel.la
        public Finca TrobaFincaPerParcelaID(int id)
        {
            Finca actual = null;

            for (int i = 0; i < propietaris_manager.propietari_actual.finques.Count; i++)
            {
                actual = propietaris_manager.propietari_actual.finques[i];

                for (int y = 0; y < actual.parceles.Count(); y++)
                {
                    if (id == actual.parceles[y].GetID())
                    {
                        return actual;
                    }
                }
            }

            return null;
        }

        // Busca una parcela en la finca actual
        public Parcela TrobaParcelaAFincaActual(int id)
        {
            Parcela ret = null;

            for (int i = 0; i < propietaris_manager.propietari_actual.finca_actual.parceles.Count; i++)
            {
                Parcela parcela_actual = propietaris_manager.propietari_actual.finca_actual.parceles[i];

                if (id == parcela_actual.GetID())
                {
                    ret = parcela_actual;
                    break;
                }
            }

            return ret;
        }

        public bool FincaJaExisteix(int id)
        {
            Finca actual = null;

            for (int i = 0; i < propietaris_manager.propietari_actual.finques.Count; i++)
            {
                actual = propietaris_manager.propietari_actual.finques[i];

                for (int y = 0; y < actual.parceles.Count(); y++)
                {
                    if (id == actual.parceles[y].GetID())
                    {
                        return true;
                    }
                }
            }


            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext dt = new DataClasses1DataContext();
                      
            foreach (var prov in dt.tblProveedores)
            {
                Console.WriteLine(prov.Nombre);
            }
            
        }
    }
}
