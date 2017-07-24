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
            string descripcio = "";

            if (treballs_combobox.IsSelected())
                treball = treballs_combobox.GetSelected() as Treball;

            descripcio = descripcio_text_input.GetText();

            if (treball == null || data_dataselect.GetDate() == null || descripcio == "")
                return;

            List<Parcela> seleccionades = propietaris_manager.GetParcelesSeleccionades();

            for (int i = 0; i < seleccionades.Count; i++)
            {
                tblLineasPartesFinca1 parte_linea = new tblLineasPartesFinca1();
                parte_linea.CodigoEmpresa = "0";
                parte_linea.Descripcion = descripcio;
                parte_linea.idFamiliaCoste = treball.GetTbl().idCost;
                parte_linea.idLinea = 0;
                parte_linea.idParcela = seleccionades[i].GetTbl().idParcela;
                parte_linea.Observaciones = "";
                parte_linea.Precio = 0;
                parte_linea.Total = 0;
                parte_linea.Unidades = 0;

                grid.AddRow(treball.GetTbl().Descripcio, descripcio, 0.0, parte_linea);
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
                Finca finca = GetFincaPerParcela(parceles[i]);

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
                parte.idParte = GetPartesNewId();

                propietaris_manager.AfegirParte(parte);
                server_manager.AddParteFinca(parte);

                for (int r = 0; r < grid.GetRows().Count; r++)
                {
                    tblLineasPartesFinca1 li = grid.GetRows()[r].Cells[3].Value as tblLineasPartesFinca1;

                    for (int p = 0; p < parceles.Count; p++)
                    {
                        Parcela parcela_actual = parceles[p];

                        if (parcela_actual.GetTbl().idFinca == finca_actual.GetTbl().idFinca && parcela_actual.GetTbl().idParcela == li.idParcela)
                        {
                            tblLineasPartesFinca1 linea = new tblLineasPartesFinca1();
                            linea.Descripcion = grid.GetRows()[r].Cells[1].Value as string;
                            linea.idFamiliaCoste = li.idFamiliaCoste;
                            linea.CodigoEmpresa = parcela_actual.GetTbl().CodigoEmpresa;
                            linea.idParcela = parcela_actual.GetTbl().idParcela;
                            linea.idLinea = GetPartesLineaNewId();
                            linea.idParte = parte.idParte;

                            string dec = grid.GetRows()[r].Cells[2].Value.ToString();
                            linea.Unidades = decimal.Parse(dec);

                            propietaris_manager.AfegirParteLinea(linea);
                            server_manager.AddLineaParteFinca(linea);
                        }
                    }
                }
            }

            //server_manager.SubmitChanges();

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

        public int GetPartesNewId()
        {
            int ret = -1;

            List<tblPartesFinca> partes = propietaris_manager.GetPartes();
            List<Analitica> analitiques = propietaris_manager.GetAnalitiques();

            for (int i = 0; i < partes.Count; i++)
            {
                if (partes[i].idParte > ret)
                    ret = partes[i].idParte;
            }

            for(int i = 0; i < analitiques.Count; i++)
            {
                if (analitiques[i].GetTbl().idParte > ret)
                    ret = (int)analitiques[i].GetTbl().idParte;
            }

            ret++;

            return ret;
        }

        public int GetPartesLineaNewId()
        {
            int ret = -1;

            List<tblLineasPartesFinca1> partes_l = propietaris_manager.GetPartesLinea();

            for(int i = 0; i < partes_l.Count; i++)
            {
                if (partes_l[i].idLinea > ret)
                    ret = partes_l[i].idLinea;
            }

            ret++;

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
