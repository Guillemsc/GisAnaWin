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
            panelAfegeixPropietari.Visible = true;
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            PerformAutoScale();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonAfegeix_Click(object sender, EventArgs e)
        {
            if(labelAfegeix.Text != "")
            {
                Propietari p = new Propietari(labelAfegeix.Text);
                propietaris_manager.AfegirPropietari(p);
                panelAfegeixPropietari.Visible = false;
                labelAfegeix.Text = "";
    

                List<Propietari> propietaris = propietaris_manager.GetPropietaris();

                for(int i = 0; i < propietaris_manager.propietaris_texts.Count; i++)
                {
                    //panelPropietaris.Controls.Remove(propietaris_manager.propietaris_texts[i]);
                    propietaris_manager.propietaris_texts[i] = null;
                }

                propietaris_manager.propietaris_texts.Clear();

                for (int i = 0; i < propietaris.Count; i++)
                {
                    Label l = new Label();
                    l.Height = 13;
                    l.Width = 193;


                    l.Text = "- " + propietaris[i].GetNom();
                    l.Name = "" + propietaris[i].GetNom();

                    l.Click += new EventHandler(propietari_Click);
                    //panelPropietaris.Controls.Add(l);
                    propietaris_manager.propietaris_texts.Add(l);

                    //panelPropietaris.AutoScroll = false;
                    //panelPropietaris.HorizontalScroll.Enabled = false;
                    //panelPropietaris.HorizontalScroll.Visible = false;
                    //panelPropietaris.HorizontalScroll.Maximum = 0;
                    //panelPropietaris.AutoScroll = true;
                }
            }
        }

        private void propietari_Click(object sender, EventArgs e)
        {
            Label b = sender as Label;
            Propietari p = propietaris_manager.GetPropietariPerNom(b.Name);

            if(p != null)
            {
                if (propietaris_manager.propietari_actual != null)
                    propietaris_manager.propietari_actual.UnloadInfo();

                propietaris_manager.propietari_actual = p;
                propietaris_manager.propietari_actual.LoadInfo();
                //propietariActual.Text = propietaris_manager.propietari_actual.GetNom();


            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
