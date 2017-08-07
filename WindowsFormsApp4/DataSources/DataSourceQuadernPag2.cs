using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4.DataSources
{
    public class ReportDataQuadernPag2t1
    {
        public ReportDataQuadernPag2t1(string _num_ordre, string _nom_cognom, string _nif, string _num_carnet, string _num_quali)
        {
            num_ordre = _num_ordre;
            nom_cognom = _nom_cognom;
            nif = _nif;
            num_carnet = _num_carnet;
            num_quali = _num_quali;
        }

        public string num_ordre {get; set;}
        public string nom_cognom { get; set; }
        public string nif { get; set; }
        public string num_carnet { get; set; }
        public string num_quali { get; set; }
    }

    public class ReportDataQuadernPag2t2
    {
        public ReportDataQuadernPag2t2(string _num_ordre, string _nom_cognom, string _nif, string _num_carnet, string _num_quali)
        {
            numero_ordre_2t2 = _num_ordre;
            nom_cognom_2t2 = _nom_cognom;
            nif_2t2 = _nif;
            num_carnet_2t2 = _num_carnet;
            num_quali_2t2 = _num_quali;
        }

        public string numero_ordre_2t2 { get; set; }
        public string nom_cognom_2t2 { get; set; }
        public string nif_2t2 { get; set; }
        public string num_carnet_2t2 { get; set; }
        public string num_quali_2t2 { get; set; }
    }

    public class ReportDataQuadernPag2t3
    {
        public ReportDataQuadernPag2t3(string _num_ordre, string _nom_cognom, string _nif, string _num_registre)
        {
            num_ordre = _num_ordre;
            nom_cognom = _nom_cognom;
            nif = _nif;
            num_registre = _num_registre;
        }

        public string num_ordre { get; set; }
        public string nom_cognom { get; set; }
        public string nif { get; set; }
        public string num_registre { get; set; }
    }

    public class ReportDataQuadernPag2t4
    {
        public ReportDataQuadernPag2t4(string _num_ordre, string _tipo_maquina, string _data_compra, string _num_roma, string _data_ins)
        {
            num_ordre = _num_ordre;
            tipo_maquina = _tipo_maquina;
            data_compra = _data_compra;
            num_roma = _num_roma;
            data_ins = _data_ins;
        }

        public string num_ordre { get; set; }
        public string tipo_maquina { get; set; }
        public string data_compra { get; set; }
        public string num_roma { get; set; }
        public string data_ins { get; set; }
    }

    public class ReportDataQuadernPag2t5
    {
        public ReportDataQuadernPag2t5(string _num_ordre, string _tipo_maquina, string _data_compra, string _num_roma, string _data_ins)
        {
            num_ordre_2t5 = _num_ordre;
            tipo_maquina_2t5 = _tipo_maquina;
            data_compra_2t5 = _data_compra;
            num_roma_2t5 = _num_roma;
            data_ins_2t5 = _data_ins;
        }

        public string num_ordre_2t5 { get; set; }
        public string tipo_maquina_2t5 { get; set; }
        public string data_compra_2t5 { get; set; }
        public string num_roma_2t5 { get; set; }
        public string data_ins_2t5 { get; set; }
    }
}
