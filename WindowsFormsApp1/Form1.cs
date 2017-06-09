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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            PerformAutoScale();
        }

        // Debug
        private void crea_Click(object sender, EventArgs e)
        {
            if (point_manager.GetTmpMarcadors().Count > 2)
            {
                Parcela p = new Parcela(point_manager.GetTmpMarcadors(), point_manager.overlay_parcela);
                point_manager.AfegeixTmpParcela(p);
            } 
        }

        // Debug
        private void neteja_Click(object sender, EventArgs e)
        {
            point_manager.NetejaTmpMarcadors();
            point_manager.NetejaTmpParceles();
        }

        // Click a la pantalla per crear punts
        private void gmap_MouseClick(object sender, MouseEventArgs e)
        {
            if (!propietaris_manager.can_point)
                return;

            if(point_manager.eliminant_marcador)
            {
                point_manager.eliminant_marcador = false;
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

                   if(point_manager.GetTmpMarcadors().Count >= 3)
                   {
                       ui_manager.GetElement("crea_parcela").SetEnabled(true);
                   }
                   else
                   {
                       ui_manager.GetElement("crea_parcela").SetEnabled(false);
                   }

               }
            }
        }

        // Click a un marcador per a eliminar-lo
        public void gmap_MouseClick(GMapMarker item, MouseEventArgs e)
        {
            point_manager.EliminaTmpMarcadorSiEsTroba(item);
            point_manager.eliminant_marcador = true;

            if (point_manager.GetTmpMarcadors().Count >= 3)
            {
                ui_manager.GetElement("crea_parcela").SetEnabled(true);
            }
            else
            {
                ui_manager.GetElement("crea_parcela").SetEnabled(false);
            }
        }

        // Obra la finestra per a crear un nou propietari
        private void AfegeixPropietari(object sender, EventArgs e)
        {
            ui_manager.GetUIWindow("afegir_propietari_window").SetEnabled(true);
        }

        // Afegeix un nou propietari
        private void AfegirPropietari(object sender, EventArgs e)
        {
            MaskedTextBox mt = ui_manager.GetElement("nom_propietari_text_input").GetElement() as MaskedTextBox;

            if(mt.Text != "" && mt.Text.Length > 2)
            {
                Propietari pro = new Propietari(mt.Text);
                propietaris_manager.AfegirPropietari(pro);

                ActualitzaUIPropietari();

                afegir_propietari_win.SetEnabled(false);
            }
        }

        // Click a un text dels propietaris
        private void PropietariClick(object sender, EventArgs e)
        {
            Label b = sender as Label;
            Propietari p = propietaris_manager.GetPropietariPerNom(b.Name);

            if(p != null)
            {
                if (propietaris_manager.propietari_actual != null)
                    propietaris_manager.propietari_actual.ClearDraw();

                propietaris_manager.propietari_actual = p;
                propietaris_manager.propietari_actual.LoadInfo();

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
        }

        // Afegeix una nova finca
        public void AfegirFinca(object sender, EventArgs e)
        {
            afegir_finca_win.SetEnabled(false);

            MaskedTextBox mt = ui_manager.GetElement("nom_finca_text_input").GetElement() as MaskedTextBox;

            if (mt.Text != "")
            {
                Finca f = new Finca(point_manager.overlay_finca, mt.Text);
                propietaris_manager.propietari_actual.AfegirFinca(f);

                ActualitzaUIFinca();
            }
        }

        // Selecciona una finca pera poder crear parceles
        public void FinquesClick(object sender, EventArgs e)
        {
            Label l = sender as Label;
            Finca f = propietaris_manager.propietari_actual.GetFincaPerNom(l.Name);

            if (f != null)
            {
                propietaris_manager.propietari_actual.finca_actual = f;

                propietaris_manager.can_point = true;

                ActualitzaUIFinca();
            }
        }

        // Afegeix una parcela si hi ha 3> punts en pantalla
        public void AfegeixParcela(object sender, EventArgs e)
        {
            if(point_manager.GetTmpMarcadors().Count >= 3)
            {
                Parcela p = new Parcela(point_manager.GetTmpMarcadors(), point_manager.overlay_parcela);
                propietaris_manager.propietari_actual.finca_actual.AfegeixParcela(p);

                point_manager.NetejaTmpMarcadors(); 

                ActualitzaUIFinca();

                ui_manager.GetElement("crea_parcela").SetEnabled(false);
            }
        }

        // Actualitza la UI que mostra tots els propietaris
        public void ActualitzaUIPropietari()
        {
            UI_Panel p = ui_manager.GetElement("propietaris_panel") as UI_Panel;
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

        // Actualitza la UI que mostra les finques i les parceles
        public void ActualitzaUIFinca()
        {
            UI_Panel pan = ui_manager.GetElement("finques_panel") as UI_Panel;
            pan.ClearPanel();

            // Finques
            int acumulation = 0;
            for (int i = 0; i < propietaris_manager.propietari_actual.finques.Count(); i++)
            {
                Finca curr_finca = propietaris_manager.propietari_actual.finques[i];

                UI_Text t2 = new UI_Text(curr_finca.GetNom(), new Point(5, 0 + acumulation), 20, 40, "- " + curr_finca.GetNom());
                t2.GetElement().Click += new EventHandler(FinquesClick);
                pan.AddElement(t2);
                acumulation += 18;

                // Finca seleccionada
                if (curr_finca == propietaris_manager.propietari_actual.finca_actual)
                {
                    t2.SetColor(Color.Crimson, Color.White);
                }

                // Parceles
                for(int y = 0; y < curr_finca.parceles.Count(); y++)
                {
                    Parcela curr_parcela = curr_finca.parceles[y];

                    UI_Text t3 = new UI_Text(y.ToString(), new Point(20, 0 + acumulation), 20, 40, "o Parcela:" + (y+1).ToString());
                    t3.GetElement().Click += new EventHandler(FinquesClick);
                    pan.AddElement(t3);
                    acumulation += 18;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
