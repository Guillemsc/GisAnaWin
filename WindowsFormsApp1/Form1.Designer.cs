using System;

namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        ///

        private void InitializeComponent()
        {
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.crea = new System.Windows.Forms.Button();
            this.neteja = new System.Windows.Forms.Button();
            this.propietaris_text = new System.Windows.Forms.Label();
            this.afegeix_button = new System.Windows.Forms.Button();
            this.propietaris_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.main_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.propietaris_panel.SuspendLayout();
            this.main_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // gmap
            // 
            this.gmap.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(224, -1);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.gmap.MaxZoom = 18;
            this.gmap.MinZoom = 2;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(600, 550);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 13D;
            this.gmap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseClick);
            // 
            // crea
            // 
            this.crea.Location = new System.Drawing.Point(3, 108);
            this.crea.Name = "crea";
            this.crea.Size = new System.Drawing.Size(193, 51);
            this.crea.TabIndex = 2;
            this.crea.Text = "Crea nova parcel·la";
            this.crea.UseVisualStyleBackColor = true;
            this.crea.Click += new System.EventHandler(this.crea_Click);
            // 
            // neteja
            // 
            this.neteja.Location = new System.Drawing.Point(3, 74);
            this.neteja.Name = "neteja";
            this.neteja.Size = new System.Drawing.Size(193, 28);
            this.neteja.TabIndex = 3;
            this.neteja.Text = "Neteja";
            this.neteja.UseVisualStyleBackColor = true;
            this.neteja.Click += new System.EventHandler(this.neteja_Click);
            // 
            // propietaris_text
            // 
            this.propietaris_text.AutoSize = true;
            this.propietaris_text.Location = new System.Drawing.Point(3, 0);
            this.propietaris_text.Name = "propietaris_text";
            this.propietaris_text.Size = new System.Drawing.Size(59, 13);
            this.propietaris_text.TabIndex = 4;
            this.propietaris_text.Text = "Propietaris:";
            this.propietaris_text.Click += new System.EventHandler(this.label1_Click);
            // 
            // afegeix_button
            // 
            this.afegeix_button.Location = new System.Drawing.Point(15, 12);
            this.afegeix_button.Name = "afegeix_button";
            this.afegeix_button.Size = new System.Drawing.Size(193, 28);
            this.afegeix_button.TabIndex = 5;
            this.afegeix_button.Text = "Afegeix Propietari";
            this.afegeix_button.UseVisualStyleBackColor = true;
            this.afegeix_button.Click += new System.EventHandler(this.afegeix_button_Click);
            // 
            // propietaris_panel
            // 
            this.propietaris_panel.Controls.Add(this.propietaris_text);
            this.propietaris_panel.Location = new System.Drawing.Point(3, 3);
            this.propietaris_panel.Name = "propietaris_panel";
            this.propietaris_panel.Size = new System.Drawing.Size(194, 65);
            this.propietaris_panel.TabIndex = 6;
            // 
            // main_panel
            // 
            this.main_panel.Controls.Add(this.propietaris_panel);
            this.main_panel.Controls.Add(this.neteja);
            this.main_panel.Controls.Add(this.crea);
            this.main_panel.Location = new System.Drawing.Point(15, 46);
            this.main_panel.Name = "main_panel";
            this.main_panel.Size = new System.Drawing.Size(203, 473);
            this.main_panel.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 531);
            this.Controls.Add(this.main_panel);
            this.Controls.Add(this.afegeix_button);
            this.Controls.Add(this.gmap);
            this.Name = "Form1";
            this.Text = "Finques Maps Test";
            this.propietaris_panel.ResumeLayout(false);
            this.propietaris_panel.PerformLayout();
            this.main_panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        override protected void OnLoad(EventArgs e)
        {
            gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmap.SetPositionByKeywords("Paris, France");
            gmap.ShowCenter = false;

            point_manager = new PointsManager(gmap);
        }


        public GMap.NET.WindowsForms.GMapControl gmap = null;
        public PointsManager point_manager = null;
        private System.Windows.Forms.Button crea;
        private System.Windows.Forms.Button neteja;
        private System.Windows.Forms.Label propietaris_text;
        private System.Windows.Forms.Button afegeix_button;
        private System.Windows.Forms.FlowLayoutPanel propietaris_panel;
        private System.Windows.Forms.FlowLayoutPanel main_panel;
    }
}

