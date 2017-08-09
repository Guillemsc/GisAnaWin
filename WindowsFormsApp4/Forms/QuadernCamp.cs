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
            ReportDataQuadernPag2t1 pag2t1 = new ReportDataQuadernPag2t1("a", "a", "a", "a", "a");
            ReportDataQuadernPag2t2 pag2t2 = new ReportDataQuadernPag2t2("a", "a", "a", "a", "a");
            ReportDataQuadernPag2t3 pag2t3 = new ReportDataQuadernPag2t3("a", "a", "a", "a");
            ReportDataQuadernPag2t4 pag2t4 = new ReportDataQuadernPag2t4("a", "a", "a", "a", "a");
            ReportDataQuadernPag2t5 pag2t5 = new ReportDataQuadernPag2t5("a", "a", "a", "a", "a");
            ReportDataQuadernPag3 pag3 = new ReportDataQuadernPag3("a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a");
            ReportDataQuadernPag4 pag4 = new ReportDataQuadernPag4("a", "a", "a", "a", "a", "a", "a", "a", "a");
            ReportDataQuadernPag5 pag5 = new ReportDataQuadernPag5("a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a");
            ReportDataQuadernPag6 pag6 = new ReportDataQuadernPag6("a", "a", "a", "a", "a", "a", "a", "a");

            //SetInfo(pag1, pag2t1, pag2t2, pag2t3, pag2t4, pag2t5, pag3, pag4, pag5, pag6);
        }

        public void SetInfo(ReportDataQuadernPag1 pag1, List<ReportDataQuadernPag2t1> pag2t1, List<ReportDataQuadernPag2t2> pag2t2, 
            List<ReportDataQuadernPag2t3> pag2t3, List<ReportDataQuadernPag2t4> pag2t4, List<ReportDataQuadernPag2t5> pag2t5,
            List<ReportDataQuadernPag3> pag3, List<ReportDataQuadernPag4> pag4, List<ReportDataQuadernPag5> pag5, List<ReportDataQuadernPag6> pag6)
        {
            this.Size = new System.Drawing.Size(983, 689);
            this.MaximizeBox = false;
            this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            //this.reportViewer.Clear();

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


            this.reportViewer.LocalReport.EnableExternalImages = true;
            System.Drawing.Printing.PageSettings setup = this.reportViewer.GetPageSettings();
            setup.Margins = new System.Drawing.Printing.Margins(5, 5, 5, 5);
            setup.Landscape = true;
            this.reportViewer.SetPageSettings(setup);

            this.reportViewer.RefreshReport();
        }
    }
}
