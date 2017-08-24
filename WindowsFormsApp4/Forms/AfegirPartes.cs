using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class AfegirPartes : Form
    {
        public AfegirPartes(PropietarisManager propietaris_manager, PointsManager points_manager, ServerManager server_manager, UIManager ui_manager)
        {
            InitializeComponent();
            Carrega(propietaris_manager, points_manager, server_manager, ui_manager);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ActualitzaLlistaTreballs();
            CarregaLListaPartes();

            List<object> unitats = new List<object>();
            List<object> maquinaria = new List<object>();
            List<object> aplicadors = new List<object>();
            List<object> adobs = new List<object>();

            for (int i = 0; i < propietaris_manager.GetUnitatsMetriques().Count; i++)
                unitats.Add(propietaris_manager.GetUnitatsMetriques()[i]);

            for (int i = 0; i < propietaris_manager.GetMaquinaria().Count; i++)
                maquinaria.Add(propietaris_manager.GetMaquinaria()[i]);

            for (int i = 0; i < propietaris_manager.GetPersonal().Count; i++)
                aplicadors.Add(propietaris_manager.GetPersonal()[i]);

            for (int i = 0; i < propietaris_manager.GetAdobs().Count; i++)
                adobs.Add(propietaris_manager.GetAdobs()[i]);

            grid.UpdateComboBoxColumn("Aplicador", aplicadors);
            grid.UpdateComboBoxColumn("Unitats", unitats);
            grid.UpdateComboBoxColumn("Maquinaria", maquinaria);
            grid.UpdateComboBoxColumn("Adobs", adobs);
        }

        // ---------------------------------------------------------------------- Botons
        // -----------------------------------------------------------------------------

        private void AfegeigParte(object sender, EventArgs e)
        {
            Treball treball = null;
            string descripcio = "";

            if (treballs_combobox.IsSelected())
                treball = treballs_combobox.GetSelected() as Treball;

            descripcio = descripcio_text_input.GetText();

            if (treball == null || data_dataselect.GetDate() == null || descripcio == "")
                return;

            List<Parcela> seleccionades = propietaris_manager.GetParcelesSeleccionades();

            for (int i = 0; i < seleccionades.Count; i++)
            {
                tblLineasPartesFinca parte_linea = new tblLineasPartesFinca();
                parte_linea.CodigoEmpresa = "0";
                parte_linea.Descripcion = descripcio;
                parte_linea.idFamiliaCoste = treball.GetTbl().idCost;
                parte_linea.idLinea = 0;
                parte_linea.idParcela = seleccionades[i].GetTbl().idParcela;
                parte_linea.Observaciones = "";
                parte_linea.Precio = 0;
                parte_linea.Total = 0;
                parte_linea.Unidades = 0;

                grid.AddRow(treball.GetTbl().Descripcio, descripcio, 0.0, parte_linea, null, seleccionades[i].GetTbl().idParcelaVinicola, seleccionades[i].GetTbl().Ha, false, null);
            }

            treballs_combobox.CleanSelection();
            descripcio_text_input.SetText("");
        }

        private void EliminaParte(object sender, EventArgs e)
        {
            if (grid.IsSelected())
            {
                grid.DeleteRow(grid.GetSelectedRowIndex());
            }
        }

        private void Accepta(object sender, EventArgs e)
        {
            if (grid.GetRows().Count == 0)
                return;

            List<Parcela> parceles = propietaris_manager.GetParcelesSeleccionades();
            List<Finca> finques = new List<Finca>();

            for (int i = 0; i < parceles.Count; i++)
            {
                Finca finca = propietaris_manager.GetFincaPerParcela(parceles[i]);

                if (!finques.Contains(finca))
                    finques.Add(finca);
            }

            for (int f = 0; f < finques.Count; f++)
            {
                Finca finca_actual = finques[f];

                tblPartesFinca parte = new tblPartesFinca();
                parte.Fecha = data_dataselect.GetDate();
                parte.CodigoEmpresa = finca_actual.GetTbl().CodigoEmpresa;
                parte.idFinca = finca_actual.GetTbl().idFinca;
                parte.idParte = propietaris_manager.GetPartesNewId();
                parte.Estat = GetEstat();

                propietaris_manager.AfegirParte(parte);
                server_manager.AddParteFinca(parte);

                for (int r = 0; r < grid.GetRows().Count; r++)
                {
                    tblLineasPartesFinca li = grid.GetRowCell(r, "tblLinea").Value as tblLineasPartesFinca;

                    for (int p = 0; p < parceles.Count; p++)
                    {
                        Parcela parcela_actual = parceles[p];

                        if (parcela_actual.GetTbl().idFinca == finca_actual.GetTbl().idFinca && parcela_actual.GetTbl().idParcela == li.idParcela)
                        {
                            tblLineasPartesFinca linea = new tblLineasPartesFinca();
                            linea.Descripcion = grid.GetRows()[r].Cells[1].Value as string;
                            linea.idFamiliaCoste = li.idFamiliaCoste;
                            linea.CodigoEmpresa = parcela_actual.GetTbl().CodigoEmpresa;
                            linea.idParcela = parcela_actual.GetTbl().idParcela;
                            linea.idLinea = propietaris_manager.GetPartesLineaNewId();
                            linea.idParte = parte.idParte;
                            linea.FertirrigacioSiNo = (bool)grid.GetRowCell(r, "Fertirrigació").Value;

                            if (grid.GetRows()[r].Cells[8].Value != null)
                                linea.EficaciaTractament = int.Parse((string)grid.GetRowCell(r, "Eficacia tractament").Value);

                            if (grid.GetRows()[r].Cells[4].Value != null)
                                linea.idUnitatMetrica = propietaris_manager.GetUnitatMetricaPerNom((string)grid.GetRowCell(r, "Unitat Metrica").Value).GetTbl().id;

                            if (grid.GetRows()[r].Cells[9].Value != null)
                                linea.idAplicador = int.Parse(propietaris_manager.GetPersonalPerNom((string)grid.GetRowCell(r, "Aplicador").Value).GetTbl().id);
                            
                            if (grid.GetRows()[r].Cells[10].Value != null)
                                linea.idMaquinaria = int.Parse(propietaris_manager.GetMaquinaPerNom((string)grid.GetRowCell(r, "Maquinaria").Value).GetTbl().id);

                            if (grid.GetRows()[r].Cells[11].Value != null)
                                linea.idProduteFito = int.Parse(propietaris_manager.GetAdobPerNom((string)grid.GetRowCell(r, "Adob").Value).GetTbl().id.ToString());

                            string dec = grid.GetRowCell(r, "Unitats").Value.ToString();
                            linea.Unidades = decimal.Parse(dec);

                            propietaris_manager.AfegirParteLinea(linea);
                            server_manager.AddLineaParteFinca(linea);
                        }
                    }
                }
            }

            server_manager.SubmitChanges();

            grid.CleanSelection();

            this.Close();
        }

        // -----------------------------------------------------------------------------
        // Servidor --------------------------------------------------------------------
        // -----------------------------------------------------------------------------

        // -------------------------------------------------------------------- Servidor
        // -----------------------------------------------------------------------------

        // -----------------------------------------------------------------------------
        // Utils -----------------------------------------------------------------------
        // -----------------------------------------------------------------------------

        public string GetEstat()
        {
            if (pendent_check.GetChecked())
                return "pendent";

            if (proces_check.GetChecked())
                return "proces";

            if (acabat_check.GetChecked())
                return "acabat";

            return "";
        }

        // ----------------------------------------------------------------------- Utils
        // -----------------------------------------------------------------------------


        // -----------------------------------------------------------------------------
        // Gmap ------------------------------------------------------------------------
        // -----------------------------------------------------------------------------

        // ------------------------------------------------------------------------ Gmap
        // -----------------------------------------------------------------------------

        // -----------------------------------------------------------------------------
        // Actualitza ------------------------------------------------------------------
        // -----------------------------------------------------------------------------

        public void ActualitzaLlistaTreballs()
        {
            treballs_combobox.Clear();

            List<Treball> treballs = propietaris_manager.GetTreballs();

            for (int i = 0; i < treballs.Count; i++)
            {
                treballs_combobox.AddElement(treballs[i]);
            }
        }

        public void CarregaLListaPartes()
        {
            grid.Clear();
        }

        // ------------------------------------------------------------------ Actualitza
        // -----------------------------------------------------------------------------

    }
}
