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
    public partial class Form5 : Form
    {
        public Form5(PropietarisManager _propietaris_manager, PointsManager _points_manager, ServerManager _server_manager, UIManager _ui_manager)
        {
            InitializeComponent(_propietaris_manager, _points_manager, _server_manager, _ui_manager);
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            CarregaInformacioInicial();
        }

        private void CarregaInformacioInicial()
        {
            if (propietaris_manager.parcela_actual == null)
                return;

            grid.Clear();
            grid.CleanSelection();

            List<Analitica> analitiques = propietaris_manager.GetAnalitiques();

            Parcela parcela = propietaris_manager.parcela_actual;

            for(int i = 0; i < analitiques.Count; i++)
            {
                Analitica a = analitiques[i];

                if(a.GetTbl().idParcela == parcela.GetTbl().idParcela)
                    grid.AddRow(a.GetTbl().Fecha, a.GetTbl().IC, a.GetTbl().ph, a.GetTbl().grauAlc, a.GetTbl().DensitatProduccio);
            }
        }
    }
}
