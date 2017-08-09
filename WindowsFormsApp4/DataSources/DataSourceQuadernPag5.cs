using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4.DataSources
{
    public class ReportDataQuadernPag5
    {
        public ReportDataQuadernPag5() { }
        public ReportDataQuadernPag5(string _data, string _num_finca, string _cultiu, string _plaga, string _superficie, string _num_aplicador,
            string _num_maquinaria, string _kg_brou, string _productes_nom, string _productes_num_registre, string _productes_dosi, 
            string _eficacia)
        {
            data = _data;
            num_finca = _num_finca;
            cultiu = _cultiu;
            plaga = _plaga;
            superficie = _superficie;
            num_aplicador = _num_aplicador;
            num_maquinaria = _num_maquinaria;
            kg_brou = _kg_brou;
            productes_nom = _productes_nom;
            productes_num_registre = _productes_num_registre;
            productes_dosi = _productes_dosi;
            eficacia = _eficacia;
        }

        public string data { get; set; }
        public string num_finca { get; set; }
        public string cultiu { get; set; }
        public string plaga { get; set; }
        public string superficie { get; set; }
        public string num_aplicador { get; set; }
        public string num_maquinaria { get; set; }
        public string kg_brou { get; set; }
        public string productes_nom { get; set; }
        public string productes_num_registre { get; set; }
        public string productes_dosi { get; set; }
        public string eficacia { get; set; }
    }
}
