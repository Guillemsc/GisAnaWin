using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using System.Drawing;

namespace WindowsFormsApp1
{
    public class PointsManager
    {
        public PointsManager(GMapControl gmap)
        {
            gmap.Overlays.Add(overlay_markers);
            gmap.Overlays.Add(overlay_parcela);
        }

        List<Parcela> parceles = new List<Parcela>();

        public GMapOverlay overlay_markers = new GMapOverlay("markers");
        public GMapOverlay overlay_parcela = new GMapOverlay("parceles");
    }

    class Finca
    {
        Finca() { }

        public void AfegeixParcela(Parcela parcela)
        {
            parceles.Add(parcela);
        }

        public void EliminaParcela(Parcela parcela)
        {
            for (int i = 0; i < parceles.Count; i++)
            {
                if (parceles[i] == parcela)
                {
                    parceles.Remove(parcela);
                    break;
                }
            }
        }

        private List<Parcela> parceles = new List<Parcela>();
    }

    class Parcela
    {
        public Parcela(string name, List<Marcador> marcadors, GMapOverlay overlay)
        {
            _name = name;

            List<PointLatLng> points = new List<PointLatLng>();

            for (int i = 0; i < marcadors.Count; i++)
            {
                points.Add(marcadors[i].GetPos());
            }

            polygon = new GMapPolygon(points, _name);

            overlay.Polygons.Add(polygon);
        }

        public string GetName() { return _name; }
        public void SetName(string name) { _name = name; }

        public void SetColor(Brush fons, Pen linea)
        {
            polygon.Fill = fons;
            polygon.Stroke = linea;
        }

        private string _name;
        GMapPolygon polygon = null;
    }

    class Marcador
    {
        public Marcador(double lat, double lon, GMapOverlay overlay)
        {
            PointLatLng pos = new PointLatLng(lat, lon);
            marker = new GMarkerGoogle(pos, GMarkerGoogleType.blue_pushpin);
            overlay.Markers.Add(marker);
        }

        public GMapMarker GetMarcador()
        {
            return marker;
        }

        public void SetMarcadorText(string text)
        {
            if(marker != null)
                marker.ToolTipText = text;
            
        }

        public PointLatLng GetPos()
        {
            PointLatLng pos = new PointLatLng(0, 0);

            if (marker != null)
                pos = new PointLatLng(marker.Position.Lat, marker.Position.Lng);
       
            return pos;
        }

        public void SetPos(double lat, double lon)
        {
            PointLatLng pos = new PointLatLng(0, 0);

            if (marker != null)
            {
                marker.Position = new PointLatLng(lat, lon);
            }
        }

        public void SetColor(Brush omple, Brush pla, Pen linea)
        {
            if(marker != null)
            {
                marker.ToolTip.Fill = omple;
                marker.ToolTip.Foreground = pla;
                marker.ToolTip.Stroke = linea;
            }
        }

        GMapMarker marker = null;
    }
}
