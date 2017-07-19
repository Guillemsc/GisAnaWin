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
                DateTime data = (DateTime)a.GetTbl().Fecha;

                if (a.GetTbl().idParcela == parcela.GetTbl().idParcela)
                    grid.AddRow(data.ToLongDateString(), a.GetTbl().IC.ToString(), a.GetTbl().ph.ToString(), a.GetTbl().grauAlc.ToString(), a.GetTbl().DensitatProduccio.ToString(), a.GetTbl().idAnalitica.ToString());
            }
        }

        public void AnaliticaClick(object sender, EventArgs e)
        {
            if (!grid.IsSelected())
                return;

            string[] str = grid.GetSelectedRow();

            int id = int.Parse(str[5]);

            Analitica analitica = GetAnaliticaPerId(id);



        }

        Analitica GetAnaliticaPerId(int id)
        {
            Analitica ret = null;

            List<Analitica> analitiques = propietaris_manager.GetAnalitiques();

            for (int i = 0; i < analitiques.Count; i++)
            {
                if (analitiques[i].GetTbl().idAnalitica == id)
                {
                    ret = analitiques[i];
                    break;
                }
            }

            return ret;
        }
    }
}
