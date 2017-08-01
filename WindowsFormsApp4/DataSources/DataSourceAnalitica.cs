using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4
{
    public class ReportDataAnalitica
    {
        public ReportDataAnalitica(string _parcela, string _data, string _ic, string _estat, string _ph, string _grau, string _densitat, string _observacions)
        {
            parcela = _parcela; data = _data; ic = _ic; estat = _estat; ph = _ph; grau = _grau; densitat = _densitat; observacions = _observacions;
        }

        public string parcela { get; set; }
        public string data { get; set; }
        public string ic { get; set; }
        public string ph { get; set; }
        public string grau { get; set; }
        public string densitat { get; set; }
        public string estat { get; set; }
        public string observacions { get; set; }
    }
}
