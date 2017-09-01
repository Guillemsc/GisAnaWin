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
    public partial class Maquinaria : Form
    {
        public Maquinaria(Principal principal, PropietarisManager _propietaris_manager, PointsManager _points_manager,
            ServerManager _server_manager, UIManager _ui_manager)
        {
            InitializeComponent();
            Carrega(principal, _propietaris_manager, _points_manager, _server_manager, _ui_manager);
        }

        public void LoadF(object sender, EventArgs e)
        {
            maquinaria_per_afegir.Clear();
            maquinaria_per_eliminar.Clear();

            ActualitzaLlistaPropietaris();
            ActualitzaLlistaMaquinaria();

            tipus_text_input.SetText("");
            roma_text_input.SetText("");
            propietari_combo.CleanSelection();

            var checkedButton = tipus_panel.GetElement().Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            if (checkedButton != null)
                checkedButton.Checked = false;
        }

        private void ActualitzaLlistaPropietaris()
        {
            propietari_combo.Clear();
            propietari_combo.CleanSelection();

            List<Propietari> propietaris = propietaris_manager.GetPropietaris();

            for (int i = 0; i < propietaris.Count; i++)
            {
                propietari_combo.AddElement(propietaris[i]);
            }

            propietari_combo.CleanSelection();
        }

        void ActualitzaLlistaMaquinaria()
        {
            grid.Clear();
            grid.CleanSelection();

            List<Maquina> maquinaria = propietaris_manager.GetMaquinaria();

            List<Maquina> to_grid = new List<Maquina>();

            for (int i = 0; i < maquinaria.Count; i++)
            {
                Maquina p = maquinaria[i];

                if (!maquinaria_per_eliminar.Contains(p) && !maquinaria_per_afegir.Contains(p))
                    to_grid.Add(p);

            }

            for (int i = 0; i < maquinaria_per_afegir.Count; i++)
            {
                Maquina p = maquinaria_per_afegir[i];

                to_grid.Add(p);
            }

            while (to_grid.Count > 0)
            {
                DateTime compra = new DateTime();
                DateTime inspeccio = new DateTime();

                if (to_grid[0].GetTbl().dataCompra != null)
                    compra = (DateTime)to_grid[0].GetTbl().dataCompra;

                if (to_grid[0].GetTbl().darreraInspeccio != null)
                    inspeccio = (DateTime)to_grid[0].GetTbl().darreraInspeccio;

                grid.AddRow(to_grid[0].GetTbl().nomMaquina, compra.ToShortDateString(), to_grid[0].GetTbl().numRoma, inspeccio.ToShortDateString(), to_grid[0].GetTbl().id);

                to_grid.Remove(to_grid[0]);
            }
        }

        public void MaquinaClick(object sender, EventArgs e)
        {
            if (!grid.IsSelected())
                return;

            int id = int.Parse((string)grid.GetRowCell(grid.GetSelectedRowIndex(), "id").Value);

            Maquina maquina = propietaris_manager.GetMaquinaPerId(id.ToString());

            for (int i = 0; i < maquinaria_per_afegir.Count; i++)
            {
                if (maquinaria_per_afegir[i].GetTbl().id == id.ToString())
                    maquina = maquinaria_per_afegir[i];
            }

            if (maquina == null)
                return;

            Propietari prop = null;

            if (maquina.GetTbl().idProveedor != null)
                prop = propietaris_manager.GetPropietariPerId((int)maquina.GetTbl().idProveedor);
            else
                propietari_combo.CleanSelection();

            tipus_text_input.SetText(maquina.GetTbl().nomMaquina);

            if(maquina.GetTbl().dataCompra != null)
                data_data.SetDate((DateTime)maquina.GetTbl().dataCompra);

            roma_text_input.SetText(maquina.GetTbl().numRoma);

            if (maquina.GetTbl().darreraInspeccio != null) 
            inspeccio_data.SetDate((DateTime)maquina.GetTbl().darreraInspeccio);

            if (prop != null)
                propietari_combo.SetSelectedElement(prop.ToString());

            if (maquina.GetTbl().enPropietat != null && (bool)maquina.GetTbl().enPropietat)
            {
                propia_radiobutton.Check();
            }
            else if (maquina.GetTbl().llogada != null && (bool)maquina.GetTbl().llogada)
            {
                llogada_radiobutton.Check();
            }
        }

        public void Accepta(object sender, EventArgs e)
        {
            List<Maquina> maquinaria = propietaris_manager.GetMaquinaria();

            for (int i = 0; i < maquinaria_per_eliminar.Count; i++)
            {
                bool exists = false;
                for (int y = 0; y < maquinaria.Count; y++)
                {
                    if (maquinaria[y].GetTbl() == maquinaria_per_eliminar[i].GetTbl())
                        exists = true;
                }

                if (exists)
                    server_manager.DeleteMaquinaria(maquinaria_per_eliminar[i].GetTbl());

                propietaris_manager.GetMaquinaria().Remove(maquinaria_per_eliminar[i]);
            }

            server_manager.SubmitChanges();

            for (int i = 0; i < maquinaria_per_afegir.Count; i++)
            {
                bool exists = false;
                for (int y = 0; y < maquinaria.Count; y++)
                {
                    if (maquinaria[y].GetTbl().id == maquinaria_per_afegir[i].GetTbl().id)
                        exists = true;
                }

                if (!exists)
                    propietaris_manager.AfegirMaquinaria(maquinaria_per_afegir[i]);


                server_manager.AddMaquinaria(maquinaria_per_afegir[i].GetTbl());
            }

            server_manager.SubmitChanges();

            grid.CleanSelection();

            maquinaria_per_afegir.Clear();
            maquinaria_per_eliminar.Clear();

            this.Hide();
        }

        public void Crea(object sender, EventArgs e)
        {
            if (!FormulariComplert())
                return;

            tblMaquinaria p = new tblMaquinaria();
            Propietari propietari = propietari_combo.GetSelected() as Propietari;

            p.nomMaquina = tipus_text_input.GetText();
            p.dataCompra = data_data.GetDate();
            p.numRoma = roma_text_input.GetText();
            p.darreraInspeccio = inspeccio_data.GetDate();
            p.id = GetMaquinariaNewId().ToString();
            p.CodigoEmpresa = propietari.GetTbl().CodigoEmpresa;
            p.idProveedor = int.Parse(propietari.GetTbl().idProveedor);

            var checkedButton = tipus_panel.GetElement().Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            if (checkedButton == propia_radiobutton.GetElement())
                p.enPropietat = true;

            else if (checkedButton == llogada_radiobutton.GetElement())
                p.llogada = true;

            Maquina maquina = new Maquina(p);

            maquinaria_per_afegir.Add(maquina);

            ActualitzaLlistaMaquinaria();
        }

        public void Elimina(object sender, EventArgs e)
        {
            if (!grid.IsSelected())
                return;

            int id = int.Parse((string)grid.GetRowCell(grid.GetSelectedRowIndex(), "id").Value);

            Maquina maquina = propietaris_manager.GetMaquinaPerId(id.ToString());

            for (int i = 0; i < maquinaria_per_afegir.Count; i++)
            {
                if (maquinaria_per_afegir[i].GetTbl().id == id.ToString())
                {
                    maquina = maquinaria_per_afegir[i];
                    break;
                }
            }

            if (maquina == null)
                return;

            bool exists = true;
            for (int i = 0; i < maquinaria_per_afegir.Count; i++)
            {
                if (maquinaria_per_afegir[i].GetTbl().id == maquina.GetTbl().id)
                {
                    maquinaria_per_afegir.RemoveAt(i);
                    exists = false;
                    break;
                }
            }

            if (exists)
                maquinaria_per_eliminar.Add(maquina);

            ActualitzaLlistaMaquinaria();
        }

        public void Actualitza(object sender, EventArgs e)
        {
            if (!grid.IsSelected())
                return;

            int id = int.Parse((string)grid.GetRowCell(grid.GetSelectedRowIndex(), "id").Value);

            Maquina maquinaria = propietaris_manager.GetMaquinaPerId(id.ToString());

            for (int i = 0; i < maquinaria_per_afegir.Count; i++)
            {
                if (maquinaria_per_afegir[i].GetTbl().id == id.ToString())
                {
                    maquinaria = maquinaria_per_afegir[i];
                    break;
                }
            }

            if (maquinaria == null)
                return;

            if (!FormulariComplert())
                return;

            maquinaria_per_eliminar.Add(maquinaria);

            tblMaquinaria p = new tblMaquinaria();
            Propietari propietari = propietari_combo.GetSelected() as Propietari;

            p.nomMaquina = tipus_text_input.GetText();
            p.dataCompra = data_data.GetDate();
            p.numRoma = roma_text_input.GetText();
            p.darreraInspeccio = inspeccio_data.GetDate();
            p.id = maquinaria.GetTbl().id;
            p.CodigoEmpresa = propietari.GetTbl().CodigoEmpresa;
            p.idProveedor = int.Parse(propietari.GetTbl().idProveedor);

            var checkedButton = tipus_panel.GetElement().Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            if (checkedButton == propia_radiobutton.GetElement())
                p.enPropietat = true;

            else if (checkedButton == llogada_radiobutton.GetElement())
                p.llogada = true;

            Maquina nova_maquina = new Maquina(p);

            maquinaria_per_afegir.Add(nova_maquina);

            ActualitzaLlistaMaquinaria();

            grid.CleanSelection();
        }

        bool FormulariComplert()
        {
            if (tipus_text_input.GetText() != "" && data_data.GetDate() != null && roma_text_input.GetText() != ""
                && inspeccio_data.GetDate() != null)
                return true;
            return false;
        }

        public int GetMaquinariaNewId()
        {
            int ret = -1;

            List<Maquina> maquinaria = propietaris_manager.GetMaquinaria();

            for (int i = 0; i < maquinaria.Count; i++)
            {
                if (int.Parse(maquinaria[i].GetTbl().id) > ret)
                    ret = int.Parse(maquinaria[i].GetTbl().id);
            }

            for (int i = 0; i < maquinaria_per_afegir.Count; i++)
            {
                if (int.Parse(maquinaria_per_afegir[i].GetTbl().id) > ret)
                    ret = int.Parse(maquinaria_per_afegir[i].GetTbl().id);
            }

            ret++;

            return ret;
        }
    }
}
