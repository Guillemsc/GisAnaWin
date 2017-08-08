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
            this.Hide();
            quadern_form.ShowDialog();
        }
    }
}
