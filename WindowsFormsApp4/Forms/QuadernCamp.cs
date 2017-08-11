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

        }

        public void SetInfo(ReportDataQuadernPag1 pag1, List<ReportDataQuadernPag2t1> pag2t1, List<ReportDataQuadernPag2t2> pag2t2, 
            List<ReportDataQuadernPag2t3> pag2t3, List<ReportDataQuadernPag2t4> pag2t4, List<ReportDataQuadernPag2t5> pag2t5,
            List<ReportDataQuadernPag3> pag3, List<ReportDataQuadernPag4> pag4, List<ReportDataQuadernPag5> pag5, List<ReportDataQuadernPag6> pag6)
        {
            this.reportViewer.Clear();
            this.reportViewer.RefreshReport();

            this.Size = new System.Drawing.Size(983, 689);
            this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);

            report1DSBindingSource.DataSource = pag1;
            report2DSBindingSource.DataSource = pag2t1;
            report3DSBindingSource.DataSource = pag2t2;
            report4DSBindingSource.DataSource = pag2t3;
            report5DSBindingSource.DataSource = pag2t4;
            report6DSBindingSource.DataSource = pag2t5;
            report7DSBindingSource.DataSource = pag3;
            report8DSBindingSource.DataSource = pag4;
            report9DSBindingSource.DataSource = pag5;
            report10DSBindingSource.DataSource = pag6;

            ReportParameter[] para = new ReportParameter[1];
            para[0] = new ReportParameter("Data", System.DateTime.Today.ToLongDateString());

            reportViewer.LocalReport.SetParameters(para);

            this.reportViewer.LocalReport.EnableExternalImages = true;
            System.Drawing.Printing.PageSettings setup = this.reportViewer.GetPageSettings();
            setup.Margins = new System.Drawing.Printing.Margins(5, 5, 5, 5);
            setup.Landscape = true;
            this.reportViewer.SetPageSettings(setup);

            this.reportViewer.RefreshReport();
        }
    }
}
