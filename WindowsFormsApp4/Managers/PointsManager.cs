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

namespace WindowsFormsApp4
{
    public class PointsManager
    {
        public PointsManager(GMapControl gmap)
        {
            gmap.Overlays.Add(overlay_markers);
            gmap.Overlays.Add(overlay_parcela);
        }

        public void NetejaTmpMarcadors()
        {
            for (int i = 0; i < tmp_marcadors.Count; i++)
            {
                tmp_marcadors[i].ClearDraw();
            }
            tmp_marcadors.Clear();
        }

        public void NetejaTmpParceles()
        {
            for (int i = 0; i < tmp_parceles.Count; i++)
            {
                tmp_parceles[i].ClearDraw();
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

        public void EliminaTmpMarcadorSiEsTroba(GMapMarker item)
        {
            for (int i = 0; i < tmp_marcadors.Count; i++)
            {
                if (tmp_marcadors[i].GetMarcador() == item)
                {
                    tmp_marcadors[i].ClearDraw();
                    tmp_marcadors.Remove(tmp_marcadors[i]);
                    break;
                }
            }
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

        public bool deleting_marker = false;
    }

    public class Finca
    {
        public Finca(GMapOverlay overlay, string nom, int id)
        {
            _overlay = overlay;
            _nom = nom;
            _id = id;
        }

        public void AfegeixParcela(Parcela parcela)
        {
            parceles.Add(parcela);
        }

        public void ClearDraw()
        {
            for (int i = 0; i < parceles.Count; i++)
            {
                parceles[i].ClearDraw();

                if (_overlay != null)
                    _overlay.Polygons.Remove(polygon);

            }
        }

        public Parcela GetParcelaPerID(int id)
        {
            Parcela ret = null;

            for (int i = 0; i < parceles.Count; i++)
            {
                if (parceles[i].GetID() == id)
                {
                    ret = parceles[i];
                    break;
                }
            }

            return ret;
        }

        public void Draw()
        {
            for (int i = 0; i < parceles.Count; i++)
            {
                parceles[i].Draw();
            }

            if (_overlay != null && !_overlay.Polygons.Contains(polygon))
            {
                _overlay.Polygons.Add(polygon);
            }
        }

        public void EliminaParcela(Parcela parcela)
        {
            for (int i = 0; i < parceles.Count; i++)
            {
                if (parceles[i] == parcela)
                {
                    parceles[i].ClearDraw();
                    parceles.Remove(parcela);
                    break;
                }
            }
        }

        public string GetNom() { return _nom; }
        public void SetNom(string nom) { _nom = nom; }
        public int GetID() { return _id; }

        public List<Parcela> parceles = new List<Parcela>();
        string _nom;
        GMapOverlay _overlay = null;
        GMapPolygon polygon = null;
        public Parcela parcela_actual = null;
        private int _id = 0;
    }

    public class Parcela
    {
        public Parcela(List<Marcador> marcadors, GMapOverlay overlay, int id, string descripcio = "")
        {
            _descripcio = descripcio;
            _id = id;

            List<PointLatLng> points = new List<PointLatLng>();

            for (int i = 0; i < marcadors.Count; i++)
            {
                points.Add(marcadors[i].GetPos());
            }

            polygon = new GMapPolygon(points, _descripcio);

            center_pos = (CalculateCenterOfPolygon(points));
            text_marker = new GMarkerGoogle(center_pos, GMarkerGoogleType.blue_pushpin);
            text_marker.ToolTipMode = MarkerTooltipMode.Always;
            overlay.Markers.Add(text_marker);

            _overlay = overlay;
            Draw();
        }

        public string GetDescripcio() { return _descripcio; }
        public int GetID() { return _id; }
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

        public void ClearDraw()
        {
            if (_overlay != null)
                _overlay.Polygons.Remove(polygon);
        }

        public void Draw()
        {
            if (_overlay != null && !_overlay.Polygons.Contains(polygon))
            {
                _overlay.Polygons.Add(polygon);
            }
        }

        public PointLatLng GetCenterPos()
        {
            return center_pos;
        }

        public void Add()
        {
            _overlay.Polygons.Add(polygon);
        }

        public void SetText(string text)
        {
            text_marker.ToolTipText = text;
        }

        private PointLatLng CalculateCenterOfPolygon(List<PointLatLng> polypoints)
        {
            PointLatLng ret = new PointLatLng(0, 0);

            int sum = 0;
            double lat = 0;
            double lon = 0;

            for (int i = 0; i < polypoints.Count; i++)
            {
                sum++;
                lat += polypoints[i].Lat;
                lon += polypoints[i].Lng;
            }

            lat = lat / sum;
            lon = lon / sum;

            ret = new PointLatLng(lat, lon);

            return ret;
        }

        private string _descripcio;
        GMapPolygon polygon = null;
        GMapOverlay _overlay = null;
        private int _id = 0;
        GMapMarker text_marker = null;
        PointLatLng center_pos;
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
            if (marker != null)
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
            if (marker != null)
            {
                marker.ToolTip.Fill = omple;
                marker.ToolTip.Foreground = pla;
                marker.ToolTip.Stroke = linea;
            }
        }

        public void ClearDraw()
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
