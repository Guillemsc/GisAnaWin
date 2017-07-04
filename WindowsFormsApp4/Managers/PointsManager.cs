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
            gmap.Overlays.Add(overlay_finca);
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

        List<Marcador> tmp_marcadors = new List<Marcador>();
        List<Parcela> tmp_parceles = new List<Parcela>();

        public bool deleting_marker = false;
    }

    public class Finca
    {
        public Finca(GMapOverlay overlay, tblFinques finca)
        {
            _overlay = overlay;
            _tbl = finca;
        }

        public override string ToString()
        {
            return _tbl.Nom1;
        }

        public tblFinques GetTbl() { return _tbl; }

        public void AddParte(tblPartesFinca parte)
        {
            partes.Add(parte);
        }

        public List<tblPartesFinca> GetPartes()
        {
            return partes;
        }

        public void AddPartesLinea(tblLineasPartesFinca1 parte)
        {
            partes_linea.Add(parte);
        }

        public List<tblLineasPartesFinca1> GetPartesLinea()
        {
            return partes_linea;
        }

        GMapOverlay _overlay = null;
        GMapPolygon polygon = null;
        private tblFinques _tbl = null;
        private List<tblPartesFinca> partes = new List<tblPartesFinca>();
        private List<tblLineasPartesFinca1> partes_linea = new List<tblLineasPartesFinca1>();
    }

    public class Parcela
    {
        public Parcela(GMapOverlay overlay, tblParceles parcela)
        {
            _overlay = overlay;
            _tbl = parcela;
        }

        public override string ToString()
        {
            return _tbl.Nom;
        }

        public void AddCoordenades(List<tblCoordenadesFincaParcela> marcadors)
        {
            if (marcadors.Count() <= 0)
                return;

            ClearDraw();

            List<PointLatLng> points = new List<PointLatLng>();

            for (int i = 0; i < marcadors.Count; i++)
            {
                AddCoordenatesTlb(marcadors[i]);

                PointLatLng point = new PointLatLng((double)marcadors[i].latitud, (double)marcadors[i].longitud);
                points.Add(point);
            }

            polygon = new GMapPolygon(points, _tbl.Nom);
            polygon.IsHitTestVisible = true;

            center_pos = (CalculateCenterOfPolygon(points));
            text_marker = new GMarkerGoogle(center_pos, GMarkerGoogleType.blue_pushpin);
            text_marker.ToolTipMode = MarkerTooltipMode.Always;
            _overlay.Markers.Add(text_marker);

            Draw();
        }

        public void ClearDraw()
        {
            if (_overlay != null)
            {
                _overlay.Polygons.Remove(polygon);
                _overlay.Markers.Remove(text_marker);
            }

        }

        public void Draw()
        {
            if (_overlay != null && !_overlay.Polygons.Contains(polygon))
            {
                _overlay.Polygons.Add(polygon);
            }

            if(_overlay != null && !_overlay.Markers.Contains(text_marker))
            {
                _overlay.Markers.Add(text_marker);
            }
        }

        public void ClearPoints()
        {
            ClearDraw();
            polygon = null;
            text_marker = null;
            ClearCoordenatesTlb();
        }

        public bool HasPoints()
        {
            if (polygon != null)
                return true;
            return false;
        }

        public PointLatLng GetCenterPos()
        {
            return center_pos;
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

        public tblParceles GetTbl() { return _tbl; }

        private void AddCoordenatesTlb(tblCoordenadesFincaParcela coor)
        {
            _coordenates_tbl.Add(coor);
        }

        public void ClearCoordenatesTlb()
        {
            _coordenates_tbl.Clear();
        }

        public List<tblCoordenadesFincaParcela> GetCoordenades()
        {
            return _coordenates_tbl;
        }

        public List<tblCoordenadesFincaParcela> GetCoordenatesTbl() { return _coordenates_tbl; }

        public GMapPolygon GetPolygon()
        {
            return polygon;
        }

        public void SetColor(Color lines, Color fill)
        {
            if(polygon != null)
            {
                polygon.Stroke = new Pen(lines, 3);
                polygon.Fill = new SolidBrush(Color.FromArgb(100, fill));
            }
        }

        public void Highlight()
        {
            if (polygon != null)
            {
                polygon.Stroke = new Pen(polygon.Stroke.Color, 10);
            }
        }

        public void DeHiglight()
        {
            if (polygon != null)
            {
                polygon.Stroke = new Pen(polygon.Stroke.Color, 3);
            }
        }

        public void AddLineaParte(tblLineasPartesFinca1 lpartes)
        {
            lineas_parte.Add(lpartes);
        }

        public List<tblLineasPartesFinca1> GetLineasParte()
        {
            return lineas_parte;
        }

        GMapPolygon polygon = null;
        GMapOverlay _overlay = null;
        GMapMarker text_marker = null;
        PointLatLng center_pos;
        tblParceles _tbl = null;
        List<tblCoordenadesFincaParcela> _coordenates_tbl = new List<tblCoordenadesFincaParcela>();
        List<tblLineasPartesFinca1> lineas_parte = new List<tblLineasPartesFinca1>();
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
