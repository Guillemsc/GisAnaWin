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
    public partial class Form2 : Form
    {
        public Form2(PropietarisManager propietaris_manager, PointsManager points_manager, ServerManager server_manager, UIManager ui_manager)
        {
            InitializeComponent(propietaris_manager, points_manager, server_manager, ui_manager);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ActualitzaLlistaTreballs();
            CarregaLListaPartes();
        }

        // ---------------------------------------------------------------------- Botons
        // -----------------------------------------------------------------------------

        private void AfegeigParte(object sender, EventArgs e)
        {
            Treball treball = null;
            DateTime data = new DateTime();
            string descripcio = "";

            if (treballs_combobox.IsSelected())
                treball = treballs_combobox.GetSelected() as Treball;

            data = data_dataselect.GetDate();
            descripcio = descripcio_text_input.GetText();

            if (treball == null || data == null || descripcio == "")
                return;

            List<Parcela> parceles = propietaris_manager.GetParcelesSeleccionades();
            List<Finca> finques = new List<Finca>();

            // --------

            for (int i = 0; i < parceles.Count; i++)
            {
                Finca finca = GetFincaPerParcela(parceles[i]);

                if (!finques.Contains(finca))
                    finques.Add(finca);
            }

            for (int f = 0; f < finques.Count; f++)
            {
                Finca finca_actual = finques[f];

                tblPartesFinca parte = new tblPartesFinca();
                parte.CodigoEmpresa = finca_actual.GetTbl().CodigoEmpresa;
                parte.Descripcion = descripcio;
                parte.Estat = "";
                parte.Fecha = data;
                parte.idFinca = finca_actual.GetTbl().idFinca;
                parte.idParte = 0;

                finca_actual.AddParte(parte);
                server_manager.AddParteFinca(parte);
            }

            for (int p = 0; p < parceles.Count; p++)
            {
                Parcela parcela_actual = parceles[p];

                tblLineasPartesFinca1 parte_linea = new tblLineasPartesFinca1();
                parte_linea.CodigoEmpresa = parcela_actual.GetTbl().CodigoEmpresa;
                parte_linea.Descripcion = descripcio;
                parte_linea.idFamiliaCoste = treball.GetTbl().idCost;
                parte_linea.idLinea = 0;
                parte_linea.idParcela = parcela_actual.GetTbl().idParcela;
                parte_linea.Observaciones = "";
                parte_linea.Precio = 0;
                parte_linea.Total = 0;
                parte_linea.Unidades = 0;

                parcela_actual.AddLineaParte(parte_linea);
                server_manager.AddLineaParteFinca(parte_linea);

                Finca finca_actual = GetFincaPerParcela(parcela_actual);
                finca_actual.AddPartesLinea(parte_linea);

                //server_manager.SubmitChanges();
            }

            // --------

            grid.AddRow(treball.GetTbl().Descripcio, data.ToShortDateString(), descripcio);

            treballs_combobox.CleanSelection();
            descripcio_text_input.SetText("");
        }

        private void EliminaParte(object sender, EventArgs e)
        {
            if(grid.IsSelected())
                grid.DeleteRow(grid.GetSelectedRowIndex());

            //server_manager.SubmitChanges();
        }

        // -----------------------------------------------------------------------------
        // Servidor --------------------------------------------------------------------
        // -----------------------------------------------------------------------------

        // -------------------------------------------------------------------- Servidor
        // -----------------------------------------------------------------------------

        // -----------------------------------------------------------------------------
        // Utils -----------------------------------------------------------------------
        // -----------------------------------------------------------------------------

        // ----------------------------------------------------------------------- Utils
        // -----------------------------------------------------------------------------

        public Finca GetFincaPerParcela(Parcela p)
        {
            Finca ret = null;

            List<Finca> finques = propietaris_manager.GetFinques();

            for (int i = 0; i < finques.Count; i++)
            {
                Finca finca_actual = finques[i];

                if (finca_actual.GetTbl().idFinca == p.GetTbl().idFinca)
                {
                    ret = finca_actual;
                    break;
                }
            }

            return ret;
        }

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
