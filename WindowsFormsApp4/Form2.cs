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

            partes_linea_per_afegir.Clear(); 
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

            tblLineasPartesFinca1 parte_linea = new tblLineasPartesFinca1();
            parte_linea.CodigoEmpresa = "0";
            parte_linea.Descripcion = descripcio;
            parte_linea.idFamiliaCoste = treball.GetTbl().idCost;
            parte_linea.idLinea = 0;
            parte_linea.idParcela = 0;
            parte_linea.Observaciones = "";
            parte_linea.Precio = 0;
            parte_linea.Total = 0;
            parte_linea.Unidades = 0;

            partes_linea_per_afegir.Add(parte_linea);

            // --------

            grid.AddRow(treball.GetTbl().Descripcio, descripcio);

            treballs_combobox.CleanSelection();
            descripcio_text_input.SetText("");
        }

        private void EliminaParte(object sender, EventArgs e)
        {
            if (grid.IsSelected())
            {
                partes_linea_per_afegir.RemoveAt(grid.GetSelectedRowIndex());

                grid.DeleteRow(grid.GetSelectedRowIndex());
            }
        }

        private void Accepta(object sender, EventArgs e)
        {
            if (partes_linea_per_afegir.Count > 0)
            {
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

                    finca_actual.AddParte(parte);
                    server_manager.AddParteFinca(parte);

                    for (int l = 0; l < partes_linea_per_afegir.Count; l++)
                    {
                        for (int p = 0; p < parceles.Count; p++)
                        {
                            Parcela parcela_actual = parceles[p];

                            if (parcela_actual.GetTbl().idFinca != finca_actual.GetTbl().idFinca)
                                continue;

                            tblLineasPartesFinca1 linea = new tblLineasPartesFinca1();
                            linea.Descripcion = partes_linea_per_afegir[l].Descripcion;
                            linea.idFamiliaCoste = partes_linea_per_afegir[l].idFamiliaCoste;
                            linea.CodigoEmpresa = parcela_actual.GetTbl().CodigoEmpresa;
                            linea.idParcela = parcela_actual.GetTbl().idParcela;
                            linea.idLinea = GetPartesLineaNewId();
                            linea.idParte = parte.idParte;

                            parcela_actual.AddLineaParte(linea);
                            server_manager.AddLineaParteFinca(linea);

                            finca_actual.AddPartesLinea(linea);
                        }
                    }
                }

                server_manager.SubmitChanges();
            }

            //grid.CleanSelection();

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

            List<Finca> finques = propietaris_manager.GetFinques();
            List<Analitica> analitiques = propietaris_manager.GetAnalitiques();

            for(int i = 0; i < finques.Count; i++)
            {
                List<tblPartesFinca> partes = finques[i].GetPartes();

                for(int y = 0; y < partes.Count; y++)
                {
                    if (partes[y].idParte > ret)
                        ret = partes[y].idParte;
                }
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

            List<Finca> finques = propietaris_manager.GetFinques();

            for (int i = 0; i < finques.Count; i++)
            {
                List<tblLineasPartesFinca1> partes_l = finques[i].GetPartesLinea();

                for (int y = 0; y < partes_l.Count; y++)
                {
                    if (partes_l[y].idLinea > ret)
                        ret = partes_l[y].idLinea;
                }
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
