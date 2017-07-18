using Microsoft.Reporting.WinForms;

namespace WindowsFormsApp4
{
    partial class Form4
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
        private void InitializeComponent(PropietarisManager _propietaris_manager, PointsManager _points_manager, ServerManager _server_manager, UIManager _ui_manager)
        {
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 360);
            this.Name = "Form4";
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.ResumeLayout(false);

            // Set Managers ----------------------
            propietaris_manager = _propietaris_manager;
            point_manager = _points_manager;
            server_manager = _server_manager;
            ui_manager = _ui_manager;
            // -----------------------------------

            // UI --------------------------------
            LoadUI();
            // -----------------------------------

        }

        #endregion

        public void LoadUI()
        {
            ReportDataSource RDS = new ReportDataSource("Report1.rdlc");

            report_viewer = new Microsoft.Reporting.WinForms.ReportViewer();
            report_viewer.Location = new System.Drawing.Point(0, 0);
            report_viewer.Name = "reportViewer1";
            report_viewer.ServerReport.BearerToken = null;
            report_viewer.Size = new System.Drawing.Size(this.Size.Width, this.Size.Height);
            report_viewer.TabIndex = 0;
            //report_viewer.LocalReport.DataSources.Add(RDS);
            report_viewer.Anchor = (System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top);
            this.Controls.Add(report_viewer);
            report_viewer.LocalReport.ReportEmbeddedResource = "RDS";
            report_viewer.RefreshReport();
        }

        private Microsoft.Reporting.WinForms.ReportViewer report_viewer;

        public PropietarisManager propietaris_manager = null;
        public PointsManager point_manager = null;
        public UIManager ui_manager = null;
        public IDManager id_manager = null;
        public ServerManager server_manager = null;
    }


}