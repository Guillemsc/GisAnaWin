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
            this.reportViewer.RefreshReport();
            SetInfo("GUILLEM SUNYER CALDÚ", "47861027M", "759674", "CRISTIAN VALLES", "84669047G", "957495");
        }

        public void SetInfo(string titular_explotacio, string titular_explotacio_nif, string titular_explotacio_ccpae,
            string assessor_explotacio, string assessor_explotacio_nif, string assessor_explotacio_registre)
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

            ReportParameter[] para = new ReportParameter[6];
            para[0] = new ReportParameter("titular_explotacio", titular_explotacio);
            para[1] = new ReportParameter("titular_explotacio_nif", titular_explotacio_nif);
            para[2] = new ReportParameter("titular_explotacio_ccpae", titular_explotacio_ccpae);
            para[3] = new ReportParameter("assessor_explotacio", assessor_explotacio);
            para[4] = new ReportParameter("assessor_explotacio_nif", assessor_explotacio_nif);
            para[5] = new ReportParameter("assessor_explotacio_registre", assessor_explotacio_registre);
            this.reportViewer.LocalReport.SetParameters(para);
        }
    }
}
