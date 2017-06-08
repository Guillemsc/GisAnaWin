using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;

namespace WindowsFormsApp1
{
    class PointsManager
    {
        List<Parcela> parceles = new List<Parcela>();
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
        public Parcela() { }

        public void AfegeixMarcador(Marcador marcador)
        {
            if(marcador != null)
                marcadors.Add(marcador);
        }

        public void EliminaMarcador(Marcador marcador)
        {
            for(int i = 0; i < marcadors.Count; i++)
            {
                if (marcadors[i] == marcador)
                {
                    marcadors.Remove(marcador);
                    break;
                }
            }
        }

        public string GetName() { return _name; }
        public void SetName(string name) { _name = name; }

        private List<Marcador> marcadors = new List<Marcador>();
        //private Console color;
        private string _name = "Parcela sense nom";
    }

    class Marcador
    {
        public Marcador(float lat, float lon)
        {
            PointLatLng pos = new PointLatLng(lat, lon);
            marker = new GMarkerGoogle(pos, GMarkerGoogleType.blue_pushpin);
        }

        public GMapMarker GetMarcador()
        {
            return marker;
        }

        //public PointLatLng GetPos()
        //{
        //    if(marker != null)
        //        PointLatLng pos = new PointLatLng(marker.Position.Lat, marker.Position.Lng);
        //}

        //public void SetPos(float lat, float lon)
        //{
        //    point.Lat = lat;
        //    point.Lng = lon;
        //}

        GMapMarker marker = null;
    }
}
