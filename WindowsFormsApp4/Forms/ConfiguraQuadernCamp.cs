using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4.Forms
{
    public partial class ConfiguraQuadernCamp : Form
    {
        public ConfiguraQuadernCamp(PropietarisManager _propietaris_manager, PointsManager _points_manager, 
            ServerManager _server_manager, UIManager _ui_manager, Forms.QuadernCamp q_form)
        {
            InitializeComponent(_propietaris_manager, _points_manager, _server_manager, _ui_manager, q_form);
        }

        public void Form_Load(object sender, EventArgs e)
        {
            propietaris_combobox.Clear();

            List<Propietari> propietaris = propietaris_manager.GetPropietaris();

            for(int i = 0; i < propietaris.Count; i++)
            {
                propietaris_combobox.AddElement(propietaris[i]);
            }
        }

        public void Accepta(object sender, EventArgs e)
        {
            this.Enabled = false;

            if (!propietaris_combobox.IsSelected())
                return;

            Propietari propi = propietaris_combobox.GetSelected() as Propietari;

            DataSources.ReportDataQuadernPag1         pag1 = new DataSources.ReportDataQuadernPag1();
            List<DataSources.ReportDataQuadernPag2t1> pag2t1 = new List<DataSources.ReportDataQuadernPag2t1>();
            List<DataSources.ReportDataQuadernPag2t2> pag2t2 = new List<DataSources.ReportDataQuadernPag2t2>();
            List<DataSources.ReportDataQuadernPag2t3> pag2t3 = new List<DataSources.ReportDataQuadernPag2t3>();
            List<DataSources.ReportDataQuadernPag2t4> pag2t4 = new List<DataSources.ReportDataQuadernPag2t4>();
            List<DataSources.ReportDataQuadernPag2t5> pag2t5 = new List<DataSources.ReportDataQuadernPag2t5>();
            List<DataSources.ReportDataQuadernPag3>   pag3 = new List<DataSources.ReportDataQuadernPag3>();
            List<DataSources.ReportDataQuadernPag4>   pag4 = new List<DataSources.ReportDataQuadernPag4>();
            List<DataSources.ReportDataQuadernPag5>   pag5 = new List<DataSources.ReportDataQuadernPag5>();
            List<DataSources.ReportDataQuadernPag6>   pag6 = new List<DataSources.ReportDataQuadernPag6>();

            // Pag 1
            pag1.titular_explotacio = propi.GetTbl().Nombre;
            pag1.titular_explotacio_nif = propi.GetTbl().Cif;
            pag1.titular_explotacio_ccpae = propi.GetTbl().idCAE;
            //pag1.assessor_explotacio = 
            //pag1.assessor_explotacio_nif = 
            //pag1.assessor_explotacio_registre = 

            // Pag2
            List<Personal> personal = propietaris_manager.GetPersonal();
            List<Maquina> maquines = propietaris_manager.GetMaquinaria();

            // Pag2t1  // Pag2t2  // Pag2t3
            for (int i = 0; i < personal.Count; i++)
            {
                Personal personal_act = personal[i];
                
                if((bool)personal_act.GetTbl().personal)
                {
                    DataSources.ReportDataQuadernPag2t1 p = new DataSources.ReportDataQuadernPag2t1();
                    p.nom_cognom = personal_act.GetTbl().nom;
                    p.nif = personal_act.GetTbl().nif;
                    p.num_carnet = personal_act.GetTbl().numCarnet;
                    p.num_quali = personal_act.GetTbl().nivell;

                    pag2t1.Add(p);
                }
                else if ((bool)personal_act.GetTbl().contractat)
                {
                    DataSources.ReportDataQuadernPag2t2 p = new DataSources.ReportDataQuadernPag2t2();
                    p.nom_cognom_2t2 = personal_act.GetTbl().nom;
                    p.nif_2t2 = personal_act.GetTbl().nif;
                    p.num_carnet_2t2 = personal_act.GetTbl().numCarnet;
                    p.num_quali_2t2 = personal_act.GetTbl().nivell;

                    pag2t2.Add(p);
                }
                else if((bool)personal_act.GetTbl().empresa)
                {
                    DataSources.ReportDataQuadernPag2t3 p = new DataSources.ReportDataQuadernPag2t3();
                    p.nom_cognom = personal_act.GetTbl().nom;
                    p.nif = personal_act.GetTbl().nif;
                    p.num_registre = personal_act.GetTbl().numCarnet;

                    pag2t3.Add(p);
                }
            }

            // Pag2t4  // Pag2t5

            for(int i = 0; i < maquines.Count; i++)
            {
                Maquina maquina_act = maquines[i];

                if((bool)maquina_act.GetTbl().enPropietat)
                {
                    DataSources.ReportDataQuadernPag2t4 p = new DataSources.ReportDataQuadernPag2t4();
                    p.tipo_maquina = maquina_act.GetTbl().nomMaquina;
                    p.data_compra = maquina_act.GetTbl().dataCompra.ToString();
                    p.num_roma = maquina_act.GetTbl().numRoma;
                    p.data_ins = maquina_act.GetTbl().darreraInspeccio.ToString();

                    pag2t4.Add(p);
                }
                else if ((bool)maquina_act.GetTbl().llogada)
                {
                    DataSources.ReportDataQuadernPag2t5 p = new DataSources.ReportDataQuadernPag2t5();
                    p.tipo_maquina_2t5 = maquina_act.GetTbl().nomMaquina;
                    p.data_compra_2t5 = maquina_act.GetTbl().dataCompra.ToString();
                    p.num_roma_2t5 = maquina_act.GetTbl().numRoma;
                    p.data_ins_2t5 = maquina_act.GetTbl().darreraInspeccio.ToString();

                    pag2t5.Add(p);
                }
            }

            // Pag3
            List<Parcela> parceles = propietaris_manager.GetParcelesPerPropietari(propi);

            for(int i = 0; i < parceles.Count; i++)
            {
                Parcela parcela_act = parceles[i];
                Finca finca_act = propietaris_manager.GetFincaPerParcela(parcela_act);
                Varietat varietat = propietaris_manager.GetVarietatPerParcela(parcela_act);

                DataSources.ReportDataQuadernPag3 p = new DataSources.ReportDataQuadernPag3();
                p.num_finca_p3 = finca_act.GetTbl().idFinca.ToString();
                p.nom_finca_p3 = finca_act.GetTbl().Nom1;
                p.municipi_p3 = finca_act.GetTbl().CodiMunicipi;
                p.poligon_p3 = parcela_act.GetTbl().Poligon;
                p.parcela_p3 = parcela_act.GetTbl().Parcela;
                p.recinte_p3 = "";
                p.us_p3 = "Vi";
                p.cultiu_p3 = varietat.GetTbl().Nombre;
                p.superficie_p3 = parcela_act.GetTbl().Ha.ToString();
                p.sistema_conreu_p3 = "3.1: S / 3.2: L";
                p.num_rcv_p3 = parcela_act.GetTbl().idParcelaVinicola;

                pag3.Add(p);
            }

            // Pag4   // Pag5
            List<tblLineasPartesFinca> linies = propietaris_manager.GetLineasPartePerPropietari(propi);

            for(int i = 0; i < linies.Count; i++)
            {
                tblLineasPartesFinca linea_act = linies[i];

                tblPartesFinca parte_act = propietaris_manager.GetPartePerParteId(linea_act.idParte);
                Parcela parcela_act = propietaris_manager.GetParcelaPerParcelaID(linea_act.idParcela.ToString());
                Finca finca_act = propietaris_manager.GetFincaPerParcela(parcela_act);
                Varietat varietat_act = propietaris_manager.GetVarietatPerParcela(parcela_act);
                Treball treball_act = propietaris_manager.GetTreballPerTreballId(linea_act.idFamiliaCoste);
                Adob adob_act = propietaris_manager.GetAdobPerId(linea_act.idProduteFito.ToString());
                UnitatMetrica unitat_act = propietaris_manager.GetUnitatMetricaPerId(linea_act.idUnitatMetrica != null ? (int)linea_act.idUnitatMetrica : -1);

                if (linea_act.idProduteFito == null)
                {
                    DataSources.ReportDataQuadernPag4 p = new DataSources.ReportDataQuadernPag4();
                    p.data = parte_act.Fecha.ToString();
                    p.num_finca = finca_act.GetTbl().idFinca.ToString();
                    p.cultiu = varietat_act.GetTbl().Nombre;
                    p.superficie = parcela_act.GetTbl().Ha.ToString();
                    p.treballs = treball_act.GetTbl().Descripcio;

                    pag4.Add(p);
                }
                else
                {
                    DataSources.ReportDataQuadernPag5 p = new DataSources.ReportDataQuadernPag5();
                    p.data = parte_act.Fecha.ToString();
                    p.num_finca = finca_act.GetTbl().idFinca.ToString();
                    p.cultiu = varietat_act.GetTbl().Nombre;
                    p.plaga = parte_act.Descripcion;
                    p.superficie = parcela_act.GetTbl().Ha.ToString();
                    p.num_aplicador = linea_act.idAplicador.ToString();
                    p.num_maquinaria = linea_act.idMaquinaria.ToString();
                    p.kg_brou = linea_act.Unidades.ToString();
                    if (unitat_act != null)
                        p.kg_brou += unitat_act.GetTbl().Unitat;

                    if (adob_act != null)
                    {
                        p.productes_nom = adob_act.GetTbl().NomComercial;
                        p.productes_num_registre = adob_act.GetTbl().NumRegistre.ToString();
                    }

                    p.productes_dosi = linea_act.Unidades.ToString();
                    p.eficacia = linea_act.EficaciaTractament.ToString();

                    pag5.Add(p);
                }
            }



            quadern_form.SetInfo(pag1, pag2t1, pag2t2, pag2t3, pag2t4, pag2t5, pag3, pag4, pag5, pag6);

            this.Hide();
            this.Enabled = true;
            quadern_form.ShowDialog();
        }
    }
}
