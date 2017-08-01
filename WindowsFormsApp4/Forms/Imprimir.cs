using Microsoft.Reporting.WinForms;
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
    public partial class Imprimir : Form
    {
        public Imprimir()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.reportViewer.RefreshReport();
        }

        public void SetInfo(List<ReportDataParte> partes, List<ReportDataAnalitica> analitiques, string nom_empresa, string imatge_url, string data)
        {
            this.Size = new System.Drawing.Size(983, 689);
            this.MaximizeBox = false;
            this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer.Clear();

            report1DSBindingSource.DataSource = partes;
            report2DSBindingSource.DataSource = analitiques;

            ReportParameter[] para = new ReportParameter[3];
            para[0] = new ReportParameter("data", data);
            para[1] = new ReportParameter("imatge_mapa", imatge_url);
            para[2] = new ReportParameter("nom_empresa", nom_empresa);

            this.reportViewer.LocalReport.EnableExternalImages = true;
            System.Drawing.Printing.PageSettings setup = this.reportViewer.GetPageSettings();
            setup.Margins = new System.Drawing.Printing.Margins(5, 5, 5, 5);
            setup.Landscape = true;
            this.reportViewer.SetPageSettings(setup);

            this.reportViewer.LocalReport.SetParameters(para);
            this.reportViewer.RefreshReport();
        }

        private void reportViewer_Load(object sender, EventArgs e)
        {

        }
    }
}

