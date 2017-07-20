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

            Parcela parcela = propietaris_manager.GetParcelesSeleccionades()[0];

            List<Analitica> to_grid = new List<Analitica>();

            for(int i = 0; i < analitiques.Count; i++)
            {
                Analitica a = analitiques[i];

                if (a.GetTbl().idParcela == parcela.GetTbl().idParcela)
                {
                    if (!analitiques_per_eliminar.Contains(a) && !analitiques_per_afegir.Contains(a))
                        to_grid.Add(a);
                }
            }

            for(int i = 0; i < analitiques_per_afegir.Count; i++)
            {
                Analitica a = analitiques_per_afegir[i];
                DateTime data = (DateTime)a.GetTbl().Fecha;

                if (a.GetTbl().idParcela == parcela.GetTbl().idParcela)
                    to_grid.Add(a);
            }

            while (to_grid.Count > 0)
            {
                Analitica older = to_grid[0];

                for (int i = 0; i < to_grid.Count; i++)
                {
                    DateTime old = (DateTime)older.GetTbl().Fecha;
                    DateTime curr = (DateTime)to_grid[i].GetTbl().Fecha;

                    int result = old.CompareTo(curr);

                    if (result < 0)
                        older = to_grid[i];
                }

                DateTime data = (DateTime)older.GetTbl().Fecha;
                grid.AddRow(data.ToLongDateString(), older.GetTbl().IC.ToString(), older.GetTbl().ph.ToString(), older.GetTbl().grauAlc.ToString(), older.GetTbl().DensitatProduccio.ToString(), older.GetTbl().idAnalitica.ToString());

                to_grid.Remove(older);
            }
        }

        public void AnaliticaClick(object sender, EventArgs e)
        {
            if (!grid.IsSelected())
                return;

            string[] str = grid.GetSelectedRow();

            int id = int.Parse(str[5]);

            Analitica analitica = GetAnaliticaPerId(id);

            for (int i = 0; i < analitiques_per_afegir.Count; i++)
            {
                if (analitiques_per_afegir[i].GetTbl().idAnalitica == id)
                    analitica = analitiques_per_afegir[i];
            }

            if (analitica == null)
                return;

            data_dataselect.SetDate((DateTime)analitica.GetTbl().Fecha);
            intensitat_colorant_text_input.SetText(analitica.GetTbl().IC.ToString());
            ph_text_input.SetText(analitica.GetTbl().ph.ToString());
            grau_text_input.SetText(analitica.GetTbl().grauAlc.ToString());
            densitat_text_input.SetText(analitica.GetTbl().DensitatProduccio.ToString());
            estat_sanitari_text_input.SetText(analitica.GetTbl().EstatSanitari);
            observacions_text_input.SetText(analitica.GetTbl().Observaciones);
        }

        public void Elimina(object sender, EventArgs e)
        {
            if (!grid.IsSelected())
                return;

            string[] str = grid.GetSelectedRow();

            int id = int.Parse(str[5]);

            Analitica analitica = GetAnaliticaPerId(id);

            if (analitica == null)
                return;

            for (int i = 0; i < analitiques_per_afegir.Count; i++)
            {
                if(analitiques_per_afegir[i].GetTbl().idAnalitica == analitica.GetTbl().idAnalitica)
                {
                    analitiques_per_afegir.RemoveAt(i);
                    break;
                }
            }

            analitiques_per_eliminar.Add(analitica);

            CarregaInformacioInicial();
        }

        public void Crea(object sender, EventArgs e)
        {
            if (data_dataselect.GetDate() == null || intensitat_colorant_text_input.GetText() == "" || ph_text_input.GetText() == ""
                    || grau_text_input.GetText() == "" || densitat_text_input.GetText() == "" || estat_sanitari_text_input.GetText() == ""
                    || observacions_text_input.GetText() == "")
                return;

            float test;

            if (!float.TryParse(intensitat_colorant_text_input.GetText(), out test) || !float.TryParse(ph_text_input.GetText(), out test)
                || !float.TryParse(grau_text_input.GetText(), out test) || !float.TryParse(densitat_text_input.GetText(), out test))
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

            analitiques_per_afegir.Add(a);

            CarregaInformacioInicial();
        }

        public void Actualitza(object sender, EventArgs e)
        {
            if (!grid.IsSelected())
                return;

            string[] str = grid.GetSelectedRow();

            int id = int.Parse(str[5]);

            Analitica analitica = GetAnaliticaPerId(id);

            for (int i = 0; i < analitiques_per_afegir.Count; i++)
            {
                if (analitiques_per_afegir[i].GetTbl().idAnalitica == id)
                {
                    analitica = analitiques_per_afegir[i];
                    break;
                }
            }

            if (analitica == null)
                return;

            analitiques_per_eliminar.Add(analitica);

            tblAnaliticaFincaParcela a = new tblAnaliticaFincaParcela();

            a.Fecha = data_dataselect.GetDate();
            a.IC = (decimal)float.Parse(intensitat_colorant_text_input.GetText());
            a.ph = (decimal)float.Parse(ph_text_input.GetText());
            a.grauAlc = (decimal)float.Parse(grau_text_input.GetText());
            a.DensitatProduccio = (decimal)float.Parse(densitat_text_input.GetText());
            a.EstatSanitari = estat_sanitari_text_input.GetText();
            a.Observaciones = observacions_text_input.GetText();
            a.CodigoEmpresa = analitica.GetTbl().CodigoEmpresa;
            a.idAnalitica = analitica.GetTbl().idAnalitica;
            a.idFinca = analitica.GetTbl().idFinca;
            a.idParte = analitica.GetTbl().idParte;
            a.idParcela = analitica.GetTbl().idParcela;

            Analitica nova_analitica = new Analitica(a);

            for (int i = 0; i < analitiques_per_afegir.Count; i++)
            {
                if(analitiques_per_afegir[i].GetTbl().idAnalitica == nova_analitica.GetTbl().idAnalitica)
                {
                    analitiques_per_afegir.RemoveAt(i);
                    break;
                }
            }

            analitiques_per_afegir.Add(nova_analitica);

            CarregaInformacioInicial();

            grid.CleanSelection();
            data_dataselect.Focus();
        }

        public void Accepta(object sender, EventArgs e)
        {
            for (int i = 0; i < analitiques_per_eliminar.Count; i++)
            {
                if (propietaris_manager.GetAnalitiques().Contains(analitiques_per_eliminar[i]))
                    server_manager.DeleteAnalitica(analitiques_per_eliminar[i].GetTbl());

                propietaris_manager.GetAnalitiques().Remove(analitiques_per_eliminar[i]);
            }

            for (int i = 0; i < analitiques_per_afegir.Count; i++)
            {
                propietaris_manager.AfegirAnalitica(analitiques_per_afegir[i]);
                server_manager.AddAnalitica(analitiques_per_afegir[i].GetTbl());
            }

            server_manager.SubmitChanges();

            intensitat_colorant_text_input.SetText("");
            ph_text_input.SetText("");
            grau_text_input.SetText("");
            densitat_text_input.SetText("");
            estat_sanitari_text_input.SetText("");
            observacions_text_input.SetText("");

            this.Hide();
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
