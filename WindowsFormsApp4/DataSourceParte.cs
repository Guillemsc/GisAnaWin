using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4
{
    public class ReportDataParte
    {
        public ReportDataParte(string _finca_nom, string _parela_viti, string _estat, string _data, string _treball, string _descripcio, string _unitats)
        {
            finca_nom = _finca_nom; parcela_viti = _parela_viti; estat = _estat; data = _data; treball = _treball; descripcio = _descripcio; unitats = _unitats;
        }

        public string finca_nom { get; set; }
        public string parcela_viti { get; set; }
        public string estat { get; set; }
        public string data { get; set; }
        public string treball { get; set; }
        public string descripcio { get; set; }
        public string unitats { get; set; }
    }
}
