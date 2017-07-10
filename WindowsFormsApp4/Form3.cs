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
            CarregaInformacioInicial();
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

        // ----------------------------------------------------------------------- Utils
        // -----------------------------------------------------------------------------

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

            if(propietari != null)
            {
                propietari_nom_text.SetText(propietari.GetTbl().Nombre);
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

                grid.AddRow(treball.GetTbl().Descripcio, lineas[i].Descripcion);
            }
        }
     
        // ------------------------------------------------------------------ Actualitza
        // -----------------------------------------------------------------------------

    }
}


