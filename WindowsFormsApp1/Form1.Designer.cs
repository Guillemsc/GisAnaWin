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
            this.panelPropietaris = new System.Windows.Forms.FlowLayoutPanel();
            this.main_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.panelAfegeixPropietari = new System.Windows.Forms.Panel();
            this.errorText = new System.Windows.Forms.Label();
            this.buttonAfegeix = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelAfegeix = new System.Windows.Forms.MaskedTextBox();
            this.propietariActual = new System.Windows.Forms.Label();
            this.panelPropietaris.SuspendLayout();
            this.main_panel.SuspendLayout();
            this.panelAfegeixPropietari.SuspendLayout();
            this.SuspendLayout();
            // 
            // gmap
            // 
            this.gmap.AutoSize = true;
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
            this.afegeix_button.Location = new System.Drawing.Point(16, 27);
            this.afegeix_button.Name = "afegeix_button";
            this.afegeix_button.Size = new System.Drawing.Size(193, 28);
            this.afegeix_button.TabIndex = 5;
            this.afegeix_button.Text = "Afegeix Propietari";
            this.afegeix_button.UseVisualStyleBackColor = true;
            this.afegeix_button.Click += new System.EventHandler(this.afegeix_button_Click);
            // 
            // panelPropietaris
            // 
            this.panelPropietaris.Controls.Add(this.propietaris_text);
            this.panelPropietaris.Location = new System.Drawing.Point(3, 3);
            this.panelPropietaris.Name = "panelPropietaris";
            this.panelPropietaris.Size = new System.Drawing.Size(194, 65);
            this.panelPropietaris.TabIndex = 6;
            // 
            // main_panel
            // 
            this.main_panel.Controls.Add(this.panelPropietaris);
            this.main_panel.Controls.Add(this.neteja);
            this.main_panel.Controls.Add(this.crea);
            this.main_panel.Location = new System.Drawing.Point(12, 61);
            this.main_panel.Name = "main_panel";
            this.main_panel.Size = new System.Drawing.Size(203, 473);
            this.main_panel.TabIndex = 7;
            // 
            // panelAfegeixPropietari
            // 
            this.panelAfegeixPropietari.Controls.Add(this.errorText);
            this.panelAfegeixPropietari.Controls.Add(this.buttonAfegeix);
            this.panelAfegeixPropietari.Controls.Add(this.label1);
            this.panelAfegeixPropietari.Controls.Add(this.labelAfegeix);
            this.panelAfegeixPropietari.Location = new System.Drawing.Point(236, 27);
            this.panelAfegeixPropietari.Name = "panelAfegeixPropietari";
            this.panelAfegeixPropietari.Size = new System.Drawing.Size(432, 79);
            this.panelAfegeixPropietari.TabIndex = 8;
            this.panelAfegeixPropietari.Visible = false;
            // 
            // errorText
            // 
            this.errorText.AutoSize = true;
            this.errorText.Location = new System.Drawing.Point(164, 37);
            this.errorText.Name = "errorText";
            this.errorText.Size = new System.Drawing.Size(0, 13);
            this.errorText.TabIndex = 3;
            this.errorText.Click += new System.EventHandler(this.label2_Click);
            // 
            // buttonAfegeix
            // 
            this.buttonAfegeix.Location = new System.Drawing.Point(50, 41);
            this.buttonAfegeix.Name = "buttonAfegeix";
            this.buttonAfegeix.Size = new System.Drawing.Size(98, 23);
            this.buttonAfegeix.TabIndex = 2;
            this.buttonAfegeix.Text = "Afegeix";
            this.buttonAfegeix.UseVisualStyleBackColor = true;
            this.buttonAfegeix.Click += new System.EventHandler(this.buttonAfegeix_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nom";
            // 
            // labelAfegeix
            // 
            this.labelAfegeix.Location = new System.Drawing.Point(50, 14);
            this.labelAfegeix.Name = "labelAfegeix";
            this.labelAfegeix.Size = new System.Drawing.Size(363, 20);
            this.labelAfegeix.TabIndex = 0;
            // 
            // propietariActual
            // 
            this.propietariActual.AutoSize = true;
            this.propietariActual.Location = new System.Drawing.Point(15, 8);
            this.propietariActual.Name = "propietariActual";
            this.propietariActual.Size = new System.Drawing.Size(0, 13);
            this.propietariActual.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 531);
            this.Controls.Add(this.propietariActual);
            this.Controls.Add(this.panelAfegeixPropietari);
            this.Controls.Add(this.main_panel);
            this.Controls.Add(this.gmap);
            this.Controls.Add(this.afegeix_button);
            this.Name = "Form1";
            this.Text = "Finques Maps Test";
            this.panelPropietaris.ResumeLayout(false);
            this.panelPropietaris.PerformLayout();
            this.main_panel.ResumeLayout(false);
            this.panelAfegeixPropietari.ResumeLayout(false);
            this.panelAfegeixPropietari.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        override protected void OnLoad(EventArgs e)
        {
            gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmap.SetPositionByKeywords("Paris, France");
            gmap.ShowCenter = false;

            point_manager = new PointsManager(gmap);
            propietaris_manager = new PropietarisManager();
        }


        public GMap.NET.WindowsForms.GMapControl gmap = null;
        public PropietarisManager propietaris_manager = null;
        public PointsManager point_manager = null;
        private System.Windows.Forms.Button crea;
        private System.Windows.Forms.Button neteja;
        private System.Windows.Forms.Label propietaris_text;
        private System.Windows.Forms.Button afegeix_button;
        private System.Windows.Forms.FlowLayoutPanel panelPropietaris;
        private System.Windows.Forms.FlowLayoutPanel main_panel;
        private System.Windows.Forms.Panel panelAfegeixPropietari;
        private System.Windows.Forms.Button buttonAfegeix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox labelAfegeix;
        private System.Windows.Forms.Label errorText;
        private System.Windows.Forms.Label propietariActual;
    }
}

