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
    public partial class Form4 : Form
    {
        public Form4(PropietarisManager propietaris_manager, PointsManager points_manager, ServerManager server_manager, UIManager ui_manager)
        {
            InitializeComponent(propietaris_manager, points_manager, server_manager, ui_manager);
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        public void Accepta(object sender, EventArgs e)
        {
            if (data_dataselect.GetDate() == null || intensitat_colorant_text_input.GetText() == "" || ph_text_input.GetText() == "" 
                || grau_text_input.GetText() == "" || densitat_text_input.GetText() == "" || estat_sanitari_text_input.GetText() == "" 
                || observacions_text_input.GetText() == "")
                return;

            Parcela parcela = propietaris_manager.GetParcelesSeleccionades()[0];

            tblAnaliticaFincaParcela analitica = new tblAnaliticaFincaParcela();

            analitica.Fecha = data_dataselect.GetDate();
            analitica.IC = (decimal)float.Parse(intensitat_colorant_text_input.GetText());
            analitica.ph = (decimal)float.Parse(ph_text_input.GetText());
            analitica.grauAlc = (decimal)float.Parse(grau_text_input.GetText());
            analitica.DensitatProduccio = (decimal)float.Parse(densitat_text_input.GetText());
            analitica.EstatSanitari = estat_sanitari_text_input.GetText();
            analitica.Observaciones = observacions_text_input.GetText();
            analitica.CodigoEmpresa = parcela.GetTbl().CodigoEmpresa;
            analitica.idAnalitica = GetAnaliticaNewId();
            analitica.idFinca = parcela.GetTbl().idFinca;
            analitica.idParte = GetPartesNewId();
            analitica.idParcela = parcela.GetTbl().idParcela;

            Analitica a = new Analitica(analitica);
            propietaris_manager.AfegirAnalitica(a);

            server_manager.AddAnalitica(analitica);

            server_manager.SubmitChanges();

            this.Close();
        }

        public int GetPartesNewId()
        {
            int ret = -1;

            List<Finca> finques = propietaris_manager.GetFinques();
            List<Analitica> analitiques = propietaris_manager.GetAnalitiques();

            for (int i = 0; i < finques.Count; i++)
            {
                List<tblPartesFinca> partes = finques[i].GetPartes();

                for (int y = 0; y < partes.Count; y++)
                {
                    if (partes[y].idParte > ret)
                        ret = partes[y].idParte;
                }
            }

            for (int i = 0; i < analitiques.Count; i++)
            {
                if (analitiques[i].GetTbl().idParte > ret)
                    ret = (int)analitiques[i].GetTbl().idParte;
            }


            ret++;

            return ret;
        }

        public int GetAnaliticaNewId()
        {
            int ret = -1;

            List<Analitica> analitiques = propietaris_manager.GetAnalitiques();

            for (int i = 0; i < analitiques.Count; i++)
            {
                if (analitiques[i].GetTbl().idParte > ret)
                    ret = (int)analitiques[i].GetTbl().idParte;
            }

            ret++;

            return ret;
        }
    }
}
