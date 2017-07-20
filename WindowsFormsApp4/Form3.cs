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
    public partial class Form3 : Form
    {
        public Form3(PropietarisManager propietaris_manager, PointsManager points_manager, ServerManager server_manager, UIManager ui_manager)
        {
            InitializeComponent(propietaris_manager, points_manager, server_manager, ui_manager);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            ActualitzaLlistaTreballs();
            CarregaInformacioInicial();
            grid.CleanSelection();
        }

        // ---------------------------------------------------------------------- Botons
        // -----------------------------------------------------------------------------

        //public void AfegeigLineaParte(object sender, EventArgs e)
        //{
        //    Treball treball = null;
        //    string descripcio = "";

        //    if (treballs_combobox.IsSelected())
        //        treball = treballs_combobox.GetSelected() as Treball;

        //    descripcio = descripcio_text_input.GetText();

        //    if (treball == null || data_dataselect.GetDate() == null || descripcio == "")
        //        return;
            


        //    tblLineasPartesFinca1 parte_linea = new tblLineasPartesFinca1();
        //    parte_linea.CodigoEmpresa = "0";
        //    parte_linea.Descripcion = descripcio;
        //    parte_linea.idFamiliaCoste = treball.GetTbl().idCost;
        //    parte_linea.idLinea = GetPartesLineaNewId();
        //    parte_linea.idParcela = 0;
        //    parte_linea.Observaciones = "";
        //    parte_linea.Precio = 0;
        //    parte_linea.Total = 0;
        //    parte_linea.Unidades = 0;
        //}

        public void EliminaLineaParte(object sender, EventArgs e)
        {
            if (!grid.IsSelected())
                return;
            
            string[] str = grid.GetSelectedRow();

            int id = int.Parse(str[2]);

            List<tblLineasPartesFinca1> lineas = GetLineasPerParteId(propietaris_manager.parte_actual.idParte);

            for(int i = 0; i < lineas.Count; i++)
            {
                if(lineas[i].idLinea == id)
                {
                    partes_linea_per_eliminar.Add(lineas[i]);
                    break;
                }
            }

            grid.DeleteRow(grid.GetSelectedRowIndex());
        }

        public void LineaParteClick(object sender, EventArgs e)
        {
            if (!grid.IsSelected())
                return;

            tblLineasPartesFinca1 linea_actual = null;
            Treball treball = null;

            string[] str = grid.GetSelectedRow();

            int id = int.Parse(str[2]);

            List<tblLineasPartesFinca1> lineas = GetLineasPerParteId(propietaris_manager.parte_actual.idParte);

            for (int i = 0; i < lineas.Count; i++)
            {
                if (lineas[i].idLinea == id)
                {
                    linea_actual = lineas[i];
                    break;
                }
            }

            if (linea_actual == null)
                return;

            for (int y = 0; y < partes_linea_per_afegir.Count; y++)
            {
                if (partes_linea_per_afegir[y].idLinea == linea_actual.idLinea)
                {
                    linea_actual = partes_linea_per_afegir[y];
                    break;
                }
            }

            treball = GetTreballPerTreballId(linea_actual.idFamiliaCoste);

            if (treball == null)
                return;

            treballs_combobox.SetSelectedElement(treball.GetTbl().Descripcio);
            data_dataselect.SetDate((DateTime)propietaris_manager.parte_actual.Fecha);
            descripcio_text_input.SetText(linea_actual.Descripcion);

            propietaris_manager.parte_linea_actual = linea_actual;
        }

        public void ModificaParteSeleccionat(object sender, EventArgs e)
        {
            if (!grid.IsSelected() || propietaris_manager.parte_linea_actual == null)
                return;

            Treball treball = treballs_combobox.GetSelected() as Treball;

            tblLineasPartesFinca1 nova_linea = new tblLineasPartesFinca1();

            nova_linea.CodigoEmpresa = propietaris_manager.parte_linea_actual.CodigoEmpresa;
            nova_linea.idFamiliaCoste = propietaris_manager.parte_linea_actual.idFamiliaCoste;
            nova_linea.idLinea = propietaris_manager.parte_linea_actual.idLinea;
            nova_linea.idParcela = propietaris_manager.parte_linea_actual.idParcela;
            nova_linea.idParte = propietaris_manager.parte_linea_actual.idParte;
            nova_linea.Observaciones = propietaris_manager.parte_linea_actual.Observaciones;
            nova_linea.Precio = propietaris_manager.parte_linea_actual.Precio;
            nova_linea.Total = propietaris_manager.parte_linea_actual.Total;
            nova_linea.Unidades = propietaris_manager.parte_linea_actual.Unidades;

            nova_linea.Descripcion = descripcio_text_input.GetText();
            nova_linea.idFamiliaCoste = treball.GetTbl().idCost;

            // Comprova que aquesta linea no ha sigut ja modificata i actualitza
            for (int y = 0; y < partes_linea_per_afegir.Count; y++)
            {
                if (partes_linea_per_afegir[y].idLinea == nova_linea.idLinea)
                {
                    partes_linea_per_afegir.RemoveAt(y);
                    break;
                }
            }

            partes_linea_per_eliminar.Add(propietaris_manager.parte_linea_actual);
            partes_linea_per_afegir.Add(nova_linea);

            propietaris_manager.parte_linea_actual = nova_linea;

            grid.ModifyRow(grid.GetSelectedRowIndex(), treball, nova_linea.Descripcion, nova_linea.idLinea.ToString());
        }

        public void Accepta(object sender, EventArgs e)
        {
            Finca finca = GetFincaPerParte(propietaris_manager.parte_actual);

            for (int i = 0; i < partes_linea_per_eliminar.Count; i++)
            {
                finca.EliminaPartesLinea(partes_linea_per_eliminar[i]);
                server_manager.DeleteLineaParteFinca(partes_linea_per_eliminar[i]);
            }

            for (int i = 0; i < partes_linea_per_afegir.Count; i++)
            {
                finca.AddPartesLinea(partes_linea_per_afegir[i]);
                server_manager.AddLineaParteFinca(partes_linea_per_afegir[i]);
            }

            server_manager.SubmitChanges();

            this.Close();
        }

        // ---------------------------------------------------------------------- Botons
        // -----------------------------------------------------------------------------

        // -----------------------------------------------------------------------------
        // Servidor --------------------------------------------------------------------
        // -----------------------------------------------------------------------------

        // -------------------------------------------------------------------- Servidor
        // -----------------------------------------------------------------------------

        // -----------------------------------------------------------------------------
        // Utils -----------------------------------------------------------------------
        // -----------------------------------------------------------------------------

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

        public Finca GetFincaPerParte(tblPartesFinca parte)
        {
            List<Finca> finques = propietaris_manager.GetFinques();

            for(int i = 0; i < finques.Count; i++)
            {
                Finca finca_actual = finques[i];

                for(int y = 0; y < finca_actual.GetPartes().Count; y++)
                {
                    if(parte == finca_actual.GetPartes()[y])
                    {
                        return finca_actual;
                    }
                }
            }

            return null;
        }

        public Propietari GetPropietariPerFinca(Finca finca)
        {
            Propietari ret = null;

            for (int i = 0; i < propietaris_manager.GetPropietaris().Count; i++)
            {
                string id1 = propietaris_manager.GetPropietaris()[i].GetTbl().idProveedor.Replace(" ", "");
                string id2 = finca.GetTbl().idProveedor.ToString().Replace(" ", "");

                if (id1 == id2)
                {
                    ret = propietaris_manager.GetPropietaris()[i];
                    break;
                }
            }

            return ret;
        }

        public Propietari GetPropietariPerParte(tblPartesFinca parte)
        {
            Propietari ret = null;

            Finca f = GetFincaPerParte(parte);

            if(f != null)
            {
                ret = GetPropietariPerFinca(f);
            }

            return ret;
        }

        public List<tblLineasPartesFinca1> GetLineasPerParteId(int id)
        {
            List<tblLineasPartesFinca1> ret = new List<tblLineasPartesFinca1>();

            List<Finca> finques = propietaris_manager.GetFinques();

            for (int i = 0; i < finques.Count; i++)
            {
                Finca finca_actual = finques[i];

                for (int y = 0; y < finca_actual.GetPartesLinea().Count; y++)
                {
                    if (id == finca_actual.GetPartesLinea()[y].idParte)
                    {
                        ret.Add(finca_actual.GetPartesLinea()[y]);
                    }
                }
            }

            return ret;
        }

        public tblPartesFinca GetPartePerParteId(int id)
        {
            List<Finca> finques = propietaris_manager.GetFinques();

            for (int i = 0; i < finques.Count; i++)
            {
                Finca finca_actual = finques[i];

                for (int y = 0; y < finca_actual.GetPartes().Count; y++)
                {
                    if (finca_actual.GetPartes()[y].idParte == id)
                        return finca_actual.GetPartes()[y];
                }
            }

            return null;
        }

        public Treball GetTreballPerTreballId(int id)
        {
            Treball ret = null;

            List<Treball> treballs = propietaris_manager.GetTreballs();

            for (int i = 0; i < treballs.Count; i++)
            {
                if (treballs[i].GetTbl().idCost == id)
                {
                    ret = treballs[i];
                    break;
                }
            }

            return ret;
        }

        public void EliminaLineaParteLocal(tblLineasPartesFinca1 parte)
        {
            List<Finca> finques = propietaris_manager.GetFinques();

            for (int i = 0; i < finques.Count; i++)
            {
                Finca finca_actual = finques[i];

                for (int y = 0; y < finca_actual.GetPartes().Count; y++)
                {
                    if (finca_actual.GetPartesLinea().Remove(parte))
                        return;
                }
            }
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

        public void CarregaInformacioInicial()
        {
            Propietari propietari = GetPropietariPerParte(propietaris_manager.parte_actual);
            Finca finca = GetFincaPerParte(propietaris_manager.parte_actual);

            if(propietari != null && finca != null)
            {
                propietari_nom_text.SetText(propietari.GetTbl().Nombre);
                finca_nom_text.SetText(finca.GetTbl().Nom1);
                ActualitzaLlistaPartesLlista();
            }
        }

        public void ActualitzaLlistaPartesLlista()
        {
            grid.Clear();

            if (propietaris_manager.parte_actual == null)
                return;

            List<tblLineasPartesFinca1> lineas = GetLineasPerParteId(propietaris_manager.parte_actual.idParte);

            for(int i = 0; i < lineas.Count; i++)
            {
                Treball treball = GetTreballPerTreballId(lineas[i].idFamiliaCoste);

                grid.AddRow(treball, lineas[i].Descripcion, lineas[i].idLinea.ToString());
            }

            grid.CleanSelection();
        }

        public void ActualitzaLlistaTreballs()
        {
            treballs_combobox.Clear();

            List<Treball> treballs = propietaris_manager.GetTreballs();

            for (int i = 0; i < treballs.Count; i++)
            {
                treballs_combobox.AddElement(treballs[i]);
            }
        }

        // ------------------------------------------------------------------ Actualitza
        // -----------------------------------------------------------------------------

    }
}


