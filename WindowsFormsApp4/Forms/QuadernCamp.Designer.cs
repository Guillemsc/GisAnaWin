﻿namespace WindowsFormsApp4.Forms
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
            this.components = new System.ComponentModel.Container();

            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.report1DSBindingSource = new System.Windows.Forms.BindingSource(this.components);

            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.report1DSBindingSource)).BeginInit();

            this.SuspendLayout();
            // 
            // reportViewer
            // 
            reportDataSource1.Value = this.report1DSBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            reportDataSource1.Name = "DataSet1";

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
            this.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)(this.report1DSBindingSource)).EndInit();

        }

        #endregion

        private System.Windows.Forms.BindingSource report1DSBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
    }
}