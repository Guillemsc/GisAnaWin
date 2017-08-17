namespace WindowsFormsApp4.Forms
{
    partial class QuadernCamp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
    
        }

        #endregion

        void Carrega()
        {
            this.components = new System.ComponentModel.Container();

            // Pag1
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.report1DSBindingSource = new System.Windows.Forms.BindingSource(this.components);


            // Pag2t1
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.report2DSBindingSource = new System.Windows.Forms.BindingSource(this.components);

            // Pag2t2
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.report3DSBindingSource = new System.Windows.Forms.BindingSource(this.components);

            // Pag2t3
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.report4DSBindingSource = new System.Windows.Forms.BindingSource(this.components);

            // Pag2t4
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.report5DSBindingSource = new System.Windows.Forms.BindingSource(this.components);

            // Pag2t5
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.report6DSBindingSource = new System.Windows.Forms.BindingSource(this.components);

            // Pag3
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource7 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.report7DSBindingSource = new System.Windows.Forms.BindingSource(this.components);

            // Pag4
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource8 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.report8DSBindingSource = new System.Windows.Forms.BindingSource(this.components);

            // Pag5
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource9 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.report9DSBindingSource = new System.Windows.Forms.BindingSource(this.components);

            // Pag6
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource10 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.report10DSBindingSource = new System.Windows.Forms.BindingSource(this.components);


            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.report1DSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.report2DSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.report3DSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.report4DSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.report5DSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.report6DSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.report7DSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.report8DSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.report9DSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.report10DSBindingSource)).BeginInit();

            this.SuspendLayout();
            // 
            // reportViewer
            // 

            reportDataSource1.Value = this.report1DSBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            reportDataSource1.Name = "Pag1";

            reportDataSource2.Value = this.report2DSBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource2);
            reportDataSource2.Name = "Pag2t1";

            reportDataSource3.Value = this.report3DSBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource3);
            reportDataSource3.Name = "Pag2t2";

            reportDataSource4.Value = this.report4DSBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource4);
            reportDataSource4.Name = "Pag2t3";

            reportDataSource5.Value = this.report5DSBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource5);
            reportDataSource5.Name = "Pag2t4";

            reportDataSource6.Value = this.report6DSBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource6);
            reportDataSource6.Name = "Pag2t5";

            reportDataSource7.Value = this.report7DSBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource7);
            reportDataSource7.Name = "Pag3";

            reportDataSource8.Value = this.report8DSBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource8);
            reportDataSource8.Name = "Pag4";

            reportDataSource9.Value = this.report9DSBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource9);
            reportDataSource9.Name = "Pag5";

            reportDataSource10.Value = this.report10DSBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource10);
            reportDataSource10.Name = "Pag6";


            this.reportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer.LocalReport.ReportEmbeddedResource = "WindowsFormsApp4.Reports.ReportQuadernCamp.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(0, 0);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(983, 689);
            this.reportViewer.TabIndex = 1;
            // 
            // QuadernCamp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 689);
            this.Controls.Add(this.reportViewer);
            this.Name = "QuadernCamp";
            this.Text = this.Name;
            this.Load += new System.EventHandler(this.QuadernCamp_Load);
            this.MaximizeBox = false;

            ((System.ComponentModel.ISupportInitialize)(this.report1DSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.report2DSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.report3DSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.report4DSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.report5DSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.report6DSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.report7DSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.report8DSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.report9DSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.report10DSBindingSource)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.BindingSource report1DSBindingSource;
        private System.Windows.Forms.BindingSource report2DSBindingSource;
        private System.Windows.Forms.BindingSource report3DSBindingSource;
        private System.Windows.Forms.BindingSource report4DSBindingSource;
        private System.Windows.Forms.BindingSource report5DSBindingSource;
        private System.Windows.Forms.BindingSource report6DSBindingSource;
        private System.Windows.Forms.BindingSource report7DSBindingSource;
        private System.Windows.Forms.BindingSource report8DSBindingSource;
        private System.Windows.Forms.BindingSource report9DSBindingSource;
        private System.Windows.Forms.BindingSource report10DSBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
    }
}