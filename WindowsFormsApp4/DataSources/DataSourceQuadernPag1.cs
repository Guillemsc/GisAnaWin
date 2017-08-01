using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4.DataSources
{
    class ReportDataQuadernPag1
    {
        public ReportDataQuadernPag1(string _titular_explotacio, string _titular_explotacio_nif, 
            string _titular_explotacio_ccpae, string _assessor_explotacio, string _assessor_explotacio_nif, string _assessor_explotacio_registre)
        {
            titular_explotacio = _titular_explotacio; titular_explotacio_nif = _titular_explotacio_nif;
            titular_explotacio_ccpae = _titular_explotacio_ccpae; assessor_explotacio = _assessor_explotacio;
            assessor_explotacio_nif = _assessor_explotacio_nif; assessor_explotacio_registre = _assessor_explotacio_registre;
        }

        public string titular_explotacio { get; set; }
        public string titular_explotacio_nif { get; set; }
        public string titular_explotacio_ccpae { get; set; }
        public string assessor_explotacio { get; set; }
        public string assessor_explotacio_nif { get; set; }
        public string assessor_explotacio_registre { get; set; }
    }
}
