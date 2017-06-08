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

        private void afegeix_button_Click(object sender, EventArgs e)
        {
            main_panel.Visible = false;
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            PerformAutoScale();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
