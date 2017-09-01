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
    public partial class Adobats : Form
    {
        public Adobats(Principal principal, PropietarisManager _propietaris_manager, PointsManager _points_manager,
            ServerManager _server_manager, UIManager _ui_manager)
        {
            InitializeComponent();
            Carrega(principal, _propietaris_manager, _points_manager, _server_manager, _ui_manager);
        }

        public void LoadF(object sender, EventArgs e)
        {
            adobs_per_afegir.Clear();
            adobs_per_eliminar.Clear();

            ActualitzaLlistaAdobs();

            nom_text_input.SetText("");
            num_registre_input.SetText("");
            formula_text_input.SetText("");
        }

        void ActualitzaLlistaAdobs()
        {
            grid.Clear();
            grid.CleanSelection();

            List<Adob> adobs = propietaris_manager.GetAdobs();

            List<Adob> to_grid = new List<Adob>();

            for (int i = 0; i < adobs.Count; i++)
            {
                Adob a = adobs[i];

                if (!adobs_per_eliminar.Contains(a) && !adobs_per_afegir.Contains(a))
                    to_grid.Add(a);

            }

            for (int i = 0; i < adobs_per_afegir.Count; i++)
            {
                Adob a = adobs_per_afegir[i];

                to_grid.Add(a);
            }

            while (to_grid.Count > 0)
            {
                grid.AddRow(to_grid[0].GetTbl().NomComercial, to_grid[0].GetTbl().NumRegistre, to_grid[0].GetTbl().Formula, to_grid[0].GetTbl().id);

                to_grid.Remove(to_grid[0]);
            }
        }

        public void AdobClick(object sender, EventArgs e)
        {
            if (!grid.IsSelected())
                return;

            int id = (int)grid.GetRowCell(grid.GetSelectedRowIndex(), "id").Value;

            Adob adob = propietaris_manager.GetAdobPerId(id.ToString());

            for (int i = 0; i < adobs_per_afegir.Count; i++)
            {
                if (adobs_per_afegir[i].GetTbl().id == id)
                    adob = adobs_per_afegir[i];
            }

            if (adob == null)
                return;

            nom_text_input.SetText(adob.GetTbl().NomComercial);
            num_registre_input.SetText(adob.GetTbl().NumRegistre.ToString());
            formula_text_input.SetText(adob.GetTbl().Formula);

        }

        public void Elimina(object sender, EventArgs e)
        {
            if (!grid.IsSelected())
                return;

            int id = (int)grid.GetRowCell(grid.GetSelectedRowIndex(), "id").Value;

            Adob adob = propietaris_manager.GetAdobPerId(id.ToString());

            for (int i = 0; i < adobs_per_afegir.Count; i++)
            {
                if (adobs_per_afegir[i].GetTbl().id == id)
                {
                    adob = adobs_per_afegir[i];
                    break;
                }
            }

            if (adob == null)
                return;

            bool exists = true;
            for (int i = 0; i < adobs_per_afegir.Count; i++)
            {
                if (adobs_per_afegir[i].GetTbl().id == adob.GetTbl().id)
                {
                    adobs_per_afegir.RemoveAt(i);
                    exists = false;
                    break;
                }
            }

            if (exists)
                adobs_per_eliminar.Add(adob);

            ActualitzaLlistaAdobs();
        }

        public void Actualitza(object sender, EventArgs e)
        {
            if (!grid.IsSelected())
                return;

            int id = (int)grid.GetRowCell(grid.GetSelectedRowIndex(), "id").Value;

            Adob adob = propietaris_manager.GetAdobPerId(id.ToString());

            for (int i = 0; i < adobs_per_afegir.Count; i++)
            {
                if (adobs_per_afegir[i].GetTbl().id == id)
                {
                    adob = adobs_per_afegir[i];
                    break;
                }
            }

            if (adob == null)
                return;

            if (!FormulariComplert())
                return;

            adobs_per_eliminar.Add(adob);

            tblProductesFitosanitaris p = new tblProductesFitosanitaris();

            p.NomComercial = nom_text_input.GetText();
            p.NumRegistre = int.Parse(num_registre_input.GetText());
            p.Formula = formula_text_input.GetText();
            p.CodigoEmpresa = "0";
            p.id = adob.GetTbl().id;

            Adob nou_adob = new Adob(p);

            adobs_per_afegir.Add(nou_adob);

            ActualitzaLlistaAdobs();
        }

        public void Crea(object sender, EventArgs e)
        {
            if (!FormulariComplert())
                return;

            tblProductesFitosanitaris p = new tblProductesFitosanitaris();

            p.NomComercial = nom_text_input.GetText();
            p.NumRegistre = int.Parse(num_registre_input.GetText());
            p.Formula = formula_text_input.GetText();
            p.CodigoEmpresa = "0";
            p.id = GetAdobNewId();

            Adob adob = new Adob(p);

            adobs_per_afegir.Add(adob);

            ActualitzaLlistaAdobs();
        }

        public void Accepta(object sender, EventArgs e)
        {
            List<Adob> adobs = propietaris_manager.GetAdobs();

            for (int i = 0; i < adobs_per_eliminar.Count; i++)
            {
                bool exists = false;
                for (int y = 0; y < adobs.Count; y++)
                {
                    if (adobs[y].GetTbl() == adobs_per_eliminar[i].GetTbl())
                        exists = true;
                }

                if (exists)
                    server_manager.DeleteAdob(adobs_per_eliminar[i].GetTbl());

                propietaris_manager.GetAdobs().Remove(adobs_per_eliminar[i]);
            }

            server_manager.SubmitChanges();

            for (int i = 0; i < adobs_per_afegir.Count; i++)
            {
                bool exists = false;
                for (int y = 0; y < adobs.Count; y++)
                {
                    if (adobs[y].GetTbl().id == adobs_per_afegir[i].GetTbl().id)
                        exists = true;
                }

                if (!exists)
                    propietaris_manager.AfegirAdob(adobs_per_afegir[i]);


                server_manager.AddAdob(adobs_per_afegir[i].GetTbl());
            }

            server_manager.SubmitChanges();

            grid.CleanSelection();

            adobs_per_afegir.Clear();
            adobs_per_eliminar.Clear();

            this.Hide();
        }

        public bool FormulariComplert()
        {
            int res = 0;
            if (!int.TryParse(num_registre_input.GetText(), out res))
                return false;

            if (nom_text_input.GetText() == "" || num_registre_input.GetText() == "" || formula_text_input.GetText() == "")
                return false;
            return true;
        }

        public int GetAdobNewId()
        {
            int ret = -1;

            List<Adob> adobs = propietaris_manager.GetAdobs();

            for (int i = 0; i < adobs.Count; i++)
            {
                if (adobs[i].GetTbl().id > ret)
                    ret = adobs[i].GetTbl().id;
            }

            for (int i = 0; i < adobs_per_afegir.Count; i++)
            {
                if (adobs_per_afegir[i].GetTbl().id > ret)
                    ret = adobs_per_afegir[i].GetTbl().id;
            }

            ret++;

            return ret;
        }
    }
}
