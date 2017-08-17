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
        }

        public void PersonalClick(object sender, EventArgs e)
        {

        }

        public void Accepta(object sender, EventArgs e)
        {

        }

        public void Crea(object sender, EventArgs e)
        {

        }

        public void Elimina(object sender, EventArgs e)
        {

        }

        public void Actualitza(object sender, EventArgs e)
        {

        }
    }
}
