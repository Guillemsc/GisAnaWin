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

        private void button1_Click(object sender, EventArgs e)
        {
            //if (e.Button == System.Windows.Forms.MouseButtons.Left)
            //{
            //    double lat = gmap.FromLocalToLatLng(e.X, e.Y).Lat;
            //    double lng = gmap.FromLocalToLatLng(e.X, e.Y).Lng;

            //    Marcador m = new Marcador(lat, lng, point_manager.overlay_markers);
            //}

            //Marcador m = new Marcador(0, 0);

            // Overlay
            //GMapOverlay overlay = new GMapOverlay("markers");

            //// Map pos
            //PointLatLng pos = new PointLatLng(48.8617774, 2.349272);

            //// Marker
            //GMapMarker marker
            //= new GMarkerGoogle(pos, GMarkerGoogleType.blue_pushpin);

            //marker.ToolTipText = "x:" + pos.Lat + " " + "y:" + pos.Lng;

            //marker.ToolTip.Fill = Brushes.Black;
            //marker.ToolTip.Foreground = Brushes.White;
            //marker.ToolTip.Stroke = Pens.Black;
            //marker.ToolTip.TextPadding = new Size(20, 20);

            //// ------
            //List<PointLatLng> points = new List<PointLatLng>();
            //points.Add(new PointLatLng(48.866383, 2.323575));
            //points.Add(new PointLatLng(48.863868, 2.321554));
            //points.Add(new PointLatLng(48.861017, 2.330030));
            //points.Add(new PointLatLng(48.863727, 2.331918));

            //// Poligon
            //GMapPolygon polygon = new GMapPolygon(points, "Jardin des Tuileries");
            //overlay.Polygons.Add(polygon);
            //// ------

            //overlay.Markers.Add(marker);
            //gmap.Overlays.Add(overlay);
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
                }
            }
        }
    }
}
