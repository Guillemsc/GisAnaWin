using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4.DataSources
{
    public class ReportDataQuadernPag6
    {
        public ReportDataQuadernPag6(string _data, string _producte, string _quali_1, string _quali_2, string _quali_3, string _quantitat, 
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

        public string data { get; set; }
        public string producte { get; set; }
        public string quali_1 { get; set; }
        public string quali_2 { get; set; }
        public string quali_3 { get; set; }
        public string quantitat { get; set; }
        public string proveidor { get; set; }
        public string num_albara { get; set; }
    }
}
