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
            this.SuspendLayout();
            // 
            // gmap
            // 
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(228, 0);
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
            this.gmap.Size = new System.Drawing.Size(572, 529);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 13D;
            this.gmap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseClick);
            // 
            // crea
            // 
            this.crea.Location = new System.Drawing.Point(12, 12);
            this.crea.Name = "crea";
            this.crea.Size = new System.Drawing.Size(193, 51);
            this.crea.TabIndex = 2;
            this.crea.Text = "Crea Finca";
            this.crea.UseVisualStyleBackColor = true;
            // 
            // neteja
            // 
            this.neteja.Location = new System.Drawing.Point(12, 69);
            this.neteja.Name = "neteja";
            this.neteja.Size = new System.Drawing.Size(193, 28);
            this.neteja.TabIndex = 3;
            this.neteja.Text = "Neteja";
            this.neteja.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 531);
            this.Controls.Add(this.neteja);
            this.Controls.Add(this.crea);
            this.Controls.Add(this.gmap);
            this.Name = "Form1";
            this.Text = "Finques Maps Test";
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
    }
}

