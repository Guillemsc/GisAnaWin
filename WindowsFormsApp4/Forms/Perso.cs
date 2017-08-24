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
    public partial class Perso : Form
    {
        public Perso(PropietarisManager _propietaris_manager, PointsManager _points_manager,
            ServerManager _server_manager, UIManager _ui_manager)
        {
            InitializeComponent();
            Carrega(_propietaris_manager, _points_manager, _server_manager, _ui_manager);
            ActualitzaLlistaPersonal();
        }

        void ActualitzaLlistaPersonal()
        {
            grid.Clear();
            grid.CleanSelection();

            List<Personal> personal = propietaris_manager.GetPersonal();

            List<Personal> to_grid = new List<Personal>();

            for (int i = 0; i < personal.Count; i++)
            {
                Personal p = personal[i];
                
                if (!personal_per_eliminar.Contains(p) && !personal_per_afegir.Contains(p))
                    to_grid.Add(p);
                   
            }

            for (int i = 0; i < personal_per_afegir.Count; i++)
            {
                Personal p = personal_per_afegir[i];

                to_grid.Add(p);
            }

            while (to_grid.Count > 0)
            {
                grid.AddRow(to_grid[0].GetTbl().nom, to_grid[0].GetTbl().nif, to_grid[0].GetTbl().numCarnet, to_grid[0].GetTbl().nivell, to_grid[0].GetTbl().id);

                to_grid.Remove(to_grid[0]);
            }
        }

        public void PersonalClick(object sender, EventArgs e)
        {
            if (!grid.IsSelected())
                return;

            int id = int.Parse((string)grid.GetRowCell(grid.GetSelectedRowIndex(), "id").Value);

            Personal personal = propietaris_manager.GetPersonalPerId(id.ToString());

            for (int i = 0; i < personal_per_afegir.Count; i++)
            {
                if (personal_per_afegir[i].GetTbl().id == id.ToString())
                    personal = personal_per_afegir[i];
            }

            if (personal == null)
                return;

            nom_text_input.SetText(personal.GetTbl().nom);
            nif_text_input.SetText(personal.GetTbl().nif);
            num_carnet_text_input.SetText(personal.GetTbl().numCarnet);
            qualificacio_text_input.SetText(personal.GetTbl().nivell);

            if(personal.GetTbl().personal != null && (bool)personal.GetTbl().personal)
            {
                propi_radiobutton.Check();
            }
            else if (personal.GetTbl().contractat != null && (bool)personal.GetTbl().contractat)
            {
                contractat_radiobutton.Check();
            }
            else if (personal.GetTbl().empresa != null && (bool)personal.GetTbl().empresa)
            {
                serveis_radiobutton.Check();
            }
        }

        public void Accepta(object sender, EventArgs e)
        {
            List<Personal> personal = propietaris_manager.GetPersonal();

            for (int i = 0; i < personal_per_eliminar.Count; i++)
            {
                bool exists = false;
                for (int y = 0; y < personal.Count; y++)
                {
                    if (personal[y].GetTbl() == personal_per_eliminar[i].GetTbl())
                        exists = true;
                }

                if (exists)
                    server_manager.DeletePersonal(personal_per_eliminar[i].GetTbl());

                propietaris_manager.GetPersonal().Remove(personal_per_eliminar[i]);
            }

            server_manager.SubmitChanges();

            for (int i = 0; i < personal_per_afegir.Count; i++)
            {
                bool exists = false;
                for (int y = 0; y < personal.Count; y++)
                {
                    if (personal[y].GetTbl().id == personal_per_afegir[i].GetTbl().id)
                        exists = true;
                }

                if (!exists)
                    propietaris_manager.AfegirPersonal(personal_per_afegir[i]);


                server_manager.AddPersonal(personal_per_afegir[i].GetTbl());
            }

            server_manager.SubmitChanges();

            grid.CleanSelection();

            personal_per_afegir.Clear();
            personal_per_eliminar.Clear();

            this.Hide();
        }

        public void Crea(object sender, EventArgs e)
        {
            if (!FormulariComplert())
                return;

            tblPersonal p = new tblPersonal();

            p.nom = nom_text_input.GetText();
            p.nif = nif_text_input.GetText();
            p.numCarnet = num_carnet_text_input.GetText();
            p.nivell = qualificacio_text_input.GetText();
            p.id = GetPersonalNewId().ToString();
            p.CodigoEmpresa = "0";

            var checkedButton = tipus_panel.GetElement().Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            if (checkedButton == propi_radiobutton.GetElement())
                p.personal = true;

            else if (checkedButton == contractat_radiobutton.GetElement())
                p.contractat = true;

            else if (checkedButton == serveis_radiobutton.GetElement())
                p.empresa = true;

            Personal personal = new Personal(p);

            personal_per_afegir.Add(personal);

            ActualitzaLlistaPersonal();
        }

        public void Elimina(object sender, EventArgs e)
        {
            if (!grid.IsSelected())
                return;

            int id = int.Parse((string)grid.GetRowCell(grid.GetSelectedRowIndex(), "id").Value);

            Personal personal = propietaris_manager.GetPersonalPerId(id.ToString());

            for (int i = 0; i < personal_per_afegir.Count; i++)
            {
                if (personal_per_afegir[i].GetTbl().id == id.ToString())
                {
                    personal = personal_per_afegir[i];
                    break;
                }
            }

            if (personal == null)
                return;

            bool exists = true;
            for (int i = 0; i < personal_per_afegir.Count; i++)
            {
                if (personal_per_afegir[i].GetTbl().id == personal.GetTbl().id)
                {
                    personal_per_afegir.RemoveAt(i);
                    exists = false;
                    break;
                }
            }

            if (exists)
                personal_per_eliminar.Add(personal);

            ActualitzaLlistaPersonal();
        }

        public void Actualitza(object sender, EventArgs e)
        {
            if (!grid.IsSelected())
                return;

            int id = int.Parse((string)grid.GetRowCell(grid.GetSelectedRowIndex(), "id").Value);

            Personal personal = propietaris_manager.GetPersonalPerId(id.ToString());

            for (int i = 0; i < personal_per_afegir.Count; i++)
            {
                if (personal_per_afegir[i].GetTbl().id == id.ToString())
                {
                    personal = personal_per_afegir[i];
                    break;
                }
            }

            if (personal == null)
                return;

            if (!FormulariComplert())
                return;

            personal_per_eliminar.Add(personal);

            tblPersonal p = new tblPersonal();

            p.nom = nom_text_input.GetText();
            p.nif = nif_text_input.GetText();
            p.numCarnet = num_carnet_text_input.GetText();
            p.nivell = qualificacio_text_input.GetText();
            p.id = personal.GetTbl().id;
            p.CodigoEmpresa = "0";

            var checkedButton = tipus_panel.GetElement().Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            if (checkedButton == propi_radiobutton.GetElement())
                p.personal = true;

            else if (checkedButton == contractat_radiobutton.GetElement())
                p.contractat = true;

            else if (checkedButton == serveis_radiobutton.GetElement())
                p.empresa = true;

            Personal nou_personal = new Personal(p);

            bool found = false;
            for (int i = 0; i < personal_per_afegir.Count; i++)
            {
                if (personal_per_afegir[i].GetTbl().id == nou_personal.GetTbl().id)
                {
                    personal_per_afegir.RemoveAt(i);
                    found = true;
                    break;
                }
            }

            if (!found)
                nou_personal.GetTbl().id = GetPersonalNewId().ToString();

            personal_per_afegir.Add(nou_personal);

            ActualitzaLlistaPersonal();
        }

        bool FormulariComplert()
        {
            if (nom_text_input.GetText() != "" && nif_text_input.GetText() != "" && num_carnet_text_input.GetText() != ""
                && qualificacio_text_input.GetText() != "")
                return true;
            return false;
        }

        public int GetPersonalNewId()
        {
            int ret = -1;

            List<Personal> personal = propietaris_manager.GetPersonal();

            for (int i = 0; i < personal.Count; i++)
            {
                if (int.Parse(personal[i].GetTbl().id) > ret)
                    ret = int.Parse(personal[i].GetTbl().id);
            }

            for (int i = 0; i < personal_per_afegir.Count; i++)
            {
                if (int.Parse(personal_per_afegir[i].GetTbl().id) > ret)
                    ret = int.Parse(personal_per_afegir[i].GetTbl().id);
            }

            ret++;

            return ret;
        }
    }
}
