using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4.DataSources
{
    public class ReportDataQuadernPag4
    {
        public ReportDataQuadernPag4() { }
        public ReportDataQuadernPag4(string _data, string _num_finca, string _cultiu, string _superficie, string _treballs, 
            string _ado_nom, string _ado_compo, string _ado_quantitat, string _ado_fert)
        {
            data = _data;
            num_finca = _num_finca;
            cultiu = _cultiu;
            superficie = _superficie;
            treballs = _treballs;
            ado_nom = _ado_nom;
            ado_compo = _ado_compo;
            ado_quantitat = _ado_quantitat;
            ado_fert = _ado_fert;

        }

        public string data { get; set; }
        public string num_finca { get; set; }
        public string cultiu { get; set; }
        public string superficie { get; set; }
        public string treballs { get; set; }
        public string ado_nom { get; set; }
        public string ado_compo { get; set; }
        public string ado_quantitat { get; set; }
        public string ado_fert { get; set; }
    }
}
