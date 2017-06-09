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

            finca = new Finca(overlay_finca);
        }

        public void NetejaTmpMarcadors()
        {
            for(int i = 0; i< tmp_marcadors.Count; i++)
            {
                tmp_marcadors[i].Remove(); 
            }
            tmp_marcadors.Clear();
        }

        public void NetejaTmpParceles()
        {
            for (int i = 0; i < tmp_parceles.Count; i++)
            {
                tmp_parceles[i].Remove();
            }
            tmp_parceles.Clear();
        }

        public void AfegeixTmpMarcadors(Marcador marcador)
        {
            tmp_marcadors.Add(marcador);
        }

        public void AfegeixTmpParcela(Parcela parcela)
        {
            tmp_parceles.Add(parcela);
        }

        public List<Marcador> GetTmpMarcadors()
        {
            return tmp_marcadors;
        }

        public GMapOverlay overlay_markers = new GMapOverlay("markers");
        public GMapOverlay overlay_parcela = new GMapOverlay("parceles");
        public GMapOverlay overlay_finca = new GMapOverlay("finca");

        Finca finca = null;

        List<Marcador> tmp_marcadors = new List<Marcador>();
        List<Parcela> tmp_parceles = new List<Parcela>();
    }

    public class Finca
    {
        public Finca(GMapOverlay overlay)
        {
            _overlay = overlay;
        }

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
                    parceles[i].Remove();
                    parceles.Remove(parcela);
                    break;
                }
            }
        }

        public string GetNom() { return _nom; }
        public void SetNom(string nom) { _nom = nom; }

        private List<Parcela> parceles = new List<Parcela>();
        string _nom;
        GMapOverlay _overlay = null;
        Parcela parcela_actual = null;
    }

    public class Parcela
    {
        public Parcela(List<Marcador> marcadors, GMapOverlay overlay, string descripcio = "")
        {
            _descripcio = descripcio;

            List<PointLatLng> points = new List<PointLatLng>();

            for (int i = 0; i < marcadors.Count; i++)
            {
                points.Add(marcadors[i].GetPos());
            }

            polygon = new GMapPolygon(points, _descripcio);

            _overlay = overlay;
            _overlay.Polygons.Add(polygon);
        }

        public string GetDescripcio() { return _descripcio; }
        public void SetDescripcio(string descripcio)
        {
            _descripcio = descripcio;
            polygon.Name = descripcio;
        }

        public void SetColor(Brush fons, Pen linea)
        {
            polygon.Fill = fons;
            polygon.Stroke = linea;
        }

        public void Remove()
        {
            if (_overlay != null)
                _overlay.Polygons.Remove(polygon);
        }

        public void Add()
        {
            _overlay.Polygons.Add(polygon);
        }

        private string _descripcio;
        GMapPolygon polygon = null;
        GMapOverlay _overlay = null;
    }

    public class Marcador
    {
        public Marcador(double lat, double lon, GMapOverlay overlay)
        {
            PointLatLng pos = new PointLatLng(lat, lon);
            marker = new GMarkerGoogle(pos, GMarkerGoogleType.blue_pushpin);
            _overlay = overlay;
            _overlay.Markers.Add(marker);
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

        public void Remove()
        {
            if (_overlay != null)
                _overlay.Markers.Remove(marker);
        }

        public void Add()
        {
            _overlay.Markers.Add(marker);
        }

        GMapMarker marker = null;
        GMapOverlay _overlay = null;
    }
}
