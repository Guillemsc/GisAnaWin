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

        GMapOverlay _overlay = null;
        GMapPolygon polygon = null;
        private tblFinques _tbl = null;
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

            SetText(text);

            Draw();
        }

        public void HighlightMarker()
        {
            _overlay.Markers.Remove(text_marker);

            if (_overlay != null && !_overlay.Markers.Contains(text_marker))
            {
                text_marker = new GMarkerGoogle(center_pos, GMarkerGoogleType.red_pushpin);
                _overlay.Markers.Add(text_marker);

                SetText(text);
            }
        }

        public void DeHighlightMarker()
        {
            _overlay.Markers.Remove(text_marker);
            if (_overlay != null && !_overlay.Markers.Contains(text_marker))
            {
                text_marker = new GMarkerGoogle(center_pos, GMarkerGoogleType.blue_pushpin);
                _overlay.Markers.Add(text_marker);

                SetText(text);
            }
        }

        public void ClearDraw()
        {
            if (polygon == null || text_marker == null)
                return;

            if (_overlay != null)
            {
                _overlay.Polygons.Remove(polygon);
                _overlay.Markers.Remove(text_marker);
            }

        }

        public void Draw()
        {
            if (polygon == null || text_marker == null)
                return;

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

        public void SetText(string _text)
        { 
            text = _text;

            if(text_marker != null)
            {
                text_marker.ToolTipMode = MarkerTooltipMode.Always;
                text_marker.ToolTipText = text;
                text_marker.ToolTip.TextPadding = new Size(5, 0);
                text_marker.ToolTip.Stroke.Color = Color.FromArgb(120, 255, 255, 255);
                text_marker.ToolTip.Fill = new SolidBrush(Color.FromArgb(180, 255, 255, 255));
                text_marker.ToolTip.Foreground = new SolidBrush(Color.FromArgb(250, 32, 32, 32));
                text_marker.ToolTip.Font = new Font("Arial", 9, FontStyle.Bold);
            }
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

                _lines = lines;
                _fill = fill;
            }
        }

        public void Highlight()
        {
            if (polygon != null)
            {
                polygon.Stroke = new Pen(Color.Red, 4);
                _highlighed = true;
            }
        }

        public void DeHiglight()
        {
            if (polygon != null)
            {
                polygon.Stroke = new Pen(_lines, 3);
                polygon.Fill = new SolidBrush(Color.FromArgb(100, _fill));
                _highlighed = false;
            }
        }

        public bool IsHighlighted()
        {
            return _highlighed;
        }

        GMapPolygon polygon = null;
        GMapOverlay _overlay = null;
        GMapMarker text_marker = null;
        PointLatLng center_pos;
        tblParceles _tbl = null;
        List<tblCoordenadesFincaParcela> _coordenates_tbl = new List<tblCoordenadesFincaParcela>();
        Color _lines = Color.Blue;
        Color _fill = Color.Blue;
        bool _highlighed = false;
        string text;
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
