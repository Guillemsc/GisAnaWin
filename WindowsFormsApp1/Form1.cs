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

        private void crea_Click(object sender, EventArgs e)
        {
            if (point_manager.GetTmpMarcadors().Count > 2)
            {
                Parcela p = new Parcela(point_manager.GetTmpMarcadors(), point_manager.overlay_parcela);
                point_manager.AfegeixTmpParcela(p);
            } 
        }

        private void neteja_Click(object sender, EventArgs e)
        {
            point_manager.NetejaTmpMarcadors();
            point_manager.NetejaTmpParceles();
        }

        private void gmap_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Button == MouseButtons.Left)
                {
                    double lat = gmap.FromLocalToLatLng(e.X, e.Y).Lat;
                    double lng = gmap.FromLocalToLatLng(e.X, e.Y).Lng;

                    Marcador m = new Marcador(lat, lng, point_manager.overlay_markers);
                    point_manager.AfegeixTmpMarcadors(m);
                }
            }
        }

        private void AfegeixPropietari(object sender, EventArgs e)
        {
            ui_manager.GetUIWindow("afegir_propietari_window").SetEnabled(true);
        }

        private void AfegirPropietari(object sender, EventArgs e)
        {
            MaskedTextBox mt = ui_manager.GetElement("nom_propietari_text_input").GetElement() as MaskedTextBox;
            UI_Panel p = ui_manager.GetElement("propietaris_panel") as UI_Panel;

            if(mt.Text != "" && mt.Text.Length > 2)
            {
                Propietari pro = new Propietari(mt.Text);
                propietaris_manager.AfegirPropietari(pro);
                p.ClearPanel();

                int acumulation = 0;
                for(int i = 0; i < propietaris_manager.GetPropietaris().Count; i++)
                {
                    UI_Text t = new UI_Text(propietaris_manager.GetPropietaris()[i].GetNom(), new Point(5, 10 + acumulation), 20, 40, "- " + propietaris_manager.GetPropietaris()[i].GetNom());
                    t.GetElement().Click += new EventHandler(PropietariClick);
                    p.AddElement(t);
                    acumulation += 18;
                }

                afegir_propietari_win.SetEnabled(false);
            }
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            PerformAutoScale();
        }

        private void PropietariClick(object sender, EventArgs e)
        {
            Label b = sender as Label;
            Propietari p = propietaris_manager.GetPropietariPerNom(b.Name);

            if(p != null)
            {
                if (propietaris_manager.propietari_actual != null)
                    propietaris_manager.propietari_actual.UnloadInfo();

                propietaris_manager.propietari_actual = p;
                propietaris_manager.propietari_actual.LoadInfo();

                main_win.SetEnabled(false);
                propietari_info_win.SetEnabled(true);

                UI_Text t = ui_manager.GetElement("nom_propietari") as UI_Text;
                t.SetText(propietaris_manager.propietari_actual.GetNom());

                UI_Panel pan = ui_manager.GetElement("parceles_panel") as UI_Panel;

                int acumulation = 0;
                for (int i = 0; i < propietaris_manager.propietari_actual.parceles.Count(); i++)
                {
                    UI_Text t2 = new UI_Text(propietaris_manager.propietari_actual.parceles[i].GetDescripcio(), new Point(5, 10 + acumulation), 20, 40, "- " + propietaris_manager.propietari_actual.parceles[i].GetDescripcio());
                    t2.GetElement().Click += new EventHandler(ParcelesClick);
                    pan.AddElement(t2);
                    acumulation += 18;
                }
            }
        }

        public void AfegeixParcela(object sender, EventArgs e)
        {
            afegir_parcela_win.SetEnabled(true);
        }

        public void ParcelesClick(object sender, EventArgs e)
        {
 
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
