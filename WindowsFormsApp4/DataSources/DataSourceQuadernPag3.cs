using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4.DataSources
{
    public class ReportDataQuadernPag3
    {
        public ReportDataQuadernPag3(string _num_finca, string _nom_finca, string _municipi, string _poligon, string _parcela,
            string _recinte, string _us, string _cultiu, string _superficie, string _sistema_conreu, string _num_rcv)
        {
            num_finca = _num_finca;
            nom_finca = _nom_finca;
            municipi = _municipi;
            poligon = _poligon;
            parcela = _parcela;
            recinte = _recinte;
            us = _us;
            cultiu = _cultiu;
            superficie = _superficie;
            sistema_conreu = _sistema_conreu;
            num_rcv = _num_rcv;
        }

        string num_finca { get; set; }
        string nom_finca { get; set; }
        string municipi { get; set; }
        string poligon { get; set; }
        string parcela { get; set; }
        string recinte { get; set; }
        string us { get; set; }
        string cultiu { get; set; }
        string superficie { get; set; }
        string sistema_conreu { get; set; }
        string num_rcv { get; set; }
    }
}
