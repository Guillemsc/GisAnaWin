using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4.DataSources
{
    class ReportDataQuadernPag5
    {
        ReportDataQuadernPag5(string _data, string _producte, string _quali_1, string _quali_2, string _quali_3, string _quantitat, 
            string _proveidor, string _num_albara)
        {
            data = _data;
            producte = _producte;
            quali_1 = _quali_1;
            quali_2 = _quali_2;
            quali_3 = _quali_3;
            quantitat = _quantitat;
            proveidor = _proveidor;
            num_albara = _num_albara;
        }

        string data;
        string producte;
        string quali_1;
        string quali_2;
        string quali_3;
        string quantitat;
        string proveidor;
        string num_albara;
    }
}
