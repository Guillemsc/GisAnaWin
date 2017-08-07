using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4.DataSources
{
    class ReportDataQuadernPag3
    {
        ReportDataQuadernPag3(string _data, string _num_finca, string _cultiu, string _superficie, string _treballs, 
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

        string data;
        string num_finca;
        string cultiu;
        string superficie;
        string treballs;
        string ado_nom;
        string ado_compo;
        string ado_quantitat;
        string ado_fert;
    }
}
