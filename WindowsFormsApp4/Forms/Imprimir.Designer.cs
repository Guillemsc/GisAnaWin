﻿using System.Drawing;

namespace WindowsFormsApp4
{
    partial class Imprimir
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Imprimir));
            this.SuspendLayout();
            // 
            // Imprimir
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Imprimir";
            this.ResumeLayout(false);

        }

        #endregion

        void Carrega()
        {
            this.components = new System.ComponentModel.Container();

            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.report1DSBindingSource = new System.Windows.Forms.BindingSource(this.components);

            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.report2DSBindingSource = new System.Windows.Forms.BindingSource(this.components);

            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.report1DSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.report2DSBindingSource)).BeginInit();

            this.SuspendLayout();
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));

            reportDataSource1.Value = this.report1DSBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            reportDataSource1.Name = "DataSet1";

            reportDataSource2.Value = this.report2DSBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource2);
            reportDataSource2.Name = "DataSet2";

            this.reportViewer.LocalReport.ReportEmbeddedResource = "WindowsFormsApp4.Reports.ReportImprimirMapa.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(0, 0);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(983, 689);
            this.reportViewer.TabIndex = 0;
            this.reportViewer.Load += new System.EventHandler(this.reportViewer_Load);
            // 
            // Imprimir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 689);
            this.Controls.Add(this.reportViewer);
            this.Name = "Imprimir";
            this.Text = this.Name;
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.report1DSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.report2DSBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.MaximizeBox = false;
        }

        private System.Windows.Forms.BindingSource report1DSBindingSource;
        private System.Windows.Forms.BindingSource report2DSBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
    }
}