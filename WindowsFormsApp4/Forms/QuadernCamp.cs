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
using WindowsFormsApp4.DataSources;

namespace WindowsFormsApp4.Forms
{
    public partial class QuadernCamp : Form
    {
        public QuadernCamp()
        {
            InitializeComponent();
        }

        private void QuadernCamp_Load(object sender, EventArgs e)
        {
            ReportDataQuadernPag1 pag1 = new ReportDataQuadernPag1("a", "a", "a", "a", "a", "a");

            SetInfo(pag1);
        }

        public void SetInfo(ReportDataQuadernPag1 pag1)
        {
            this.Size = new System.Drawing.Size(983, 689);
            this.MaximizeBox = false;
            this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            //this.reportViewer.Clear();

            this.reportViewer.LocalReport.EnableExternalImages = true;
            System.Drawing.Printing.PageSettings setup = this.reportViewer.GetPageSettings();
            setup.Margins = new System.Drawing.Printing.Margins(5, 5, 5, 5);
            setup.Landscape = true;
            this.reportViewer.SetPageSettings(setup);

            report1DSBindingSource.DataSource = pag1;

            //ReportParameter[] para = new ReportParameter[6];
            //para[0] = new ReportParameter("titular_explotacio", titular_explotacio);
            //this.reportViewer.LocalReport.SetParameters(para);

            this.reportViewer.RefreshReport();
        }
    }
}
