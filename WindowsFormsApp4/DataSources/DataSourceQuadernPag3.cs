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
            num_finca_p3 = _num_finca;
            nom_finca_p3 = _nom_finca;
            municipi_p3 = _municipi;
            poligon_p3 = _poligon;
            parcela_p3 = _parcela;
            recinte_p3 = _recinte;
            us_p3 = _us;
            cultiu_p3 = _cultiu;
            superficie_p3 = _superficie;
            sistema_conreu_p3 = _sistema_conreu;
            num_rcv_p3 = _num_rcv;
        }

        public string num_finca_p3 { get; set; }
        public string nom_finca_p3 { get; set; }
        public string municipi_p3 { get; set; }
        public string poligon_p3 { get; set; }
        public string parcela_p3 { get; set; }
        public string recinte_p3 { get; set; }
        public string us_p3 { get; set; }
        public string cultiu_p3 { get; set; }
        public string superficie_p3 { get; set; }
        public string sistema_conreu_p3 { get; set; }
        public string num_rcv_p3 { get; set; }
    }
}
