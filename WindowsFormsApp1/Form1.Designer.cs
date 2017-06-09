using System;
using System.Drawing;

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
            this.panelAfegeixPropietari = new System.Windows.Forms.Panel();
            this.errorText = new System.Windows.Forms.Label();
            this.buttonAfegeix = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelAfegeix = new System.Windows.Forms.MaskedTextBox();
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 531);
            this.Controls.Add(this.panelAfegeixPropietari);
            this.Controls.Add(this.gmap);
            this.Name = "Form1";
            this.Text = "Finques Maps Test";
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
            ui_manager = new UIManager(this);

            // UI
            UI_Window main_win = new UI_Window("main_window", this);
            {
                UI_Button b = new UI_Button("afegeix_propietari", new Point(15, 15), 193, 28, "Afegeix Propietari");
                b.GetElement().Click += new System.EventHandler(this.AfegeixPropietari);
                main_win.AddElement(b);

                UI_Panel p = new UI_Panel("propietaris_panel", new Point(15, 48), 193, 100);
                main_win.AddElement(p);
                {
                    UI_Text t = new UI_Text("propietaris_text", new Point(0, 0), 193, 40, "Propietaris: ");
                    main_win.AddElement(t);
                    p.AddElement(t);
                }
            }
            ui_manager.AddUIWindow(main_win);

            UI_Window afegir_propietari_win = new UI_Window("afegir_propietari_window", this);
            {
                UI_Panel p2 = new UI_Panel("afegir_propietaris_panel", new Point(230, 13), 400, 60);
                afegir_propietari_win.AddElement(p2);
                p2.GetElement().BringToFront();
                {
                    UI_Text t2 = new UI_Text("nom_propietaris_text", new Point(5, 10), 20, 40, "Nom: ");
                    afegir_propietari_win.AddElement(t2);
                    p2.AddElement(t2);

                    UI_TextInput ti = new UI_TextInput("nom_propietari_text_input", new Point(40, 7), 300, 30);
                    afegir_propietari_win.AddElement(ti);
                    p2.AddElement(ti);

                    UI_Button b2 = new UI_Button("afegir_propietari_button", new Point(40, 30), 100, 25, "Afegir Propietari");
                    afegir_propietari_win.AddElement(b2);
                    p2.AddElement(b2);

                }
            }
            afegir_propietari_win.SetEnabled(false);
        }


        public GMap.NET.WindowsForms.GMapControl gmap = null;
        public PropietarisManager propietaris_manager = null;
        public PointsManager point_manager = null;
        public UIManager ui_manager = null;
        private System.Windows.Forms.Panel panelAfegeixPropietari;
        private System.Windows.Forms.Button buttonAfegeix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox labelAfegeix;
        private System.Windows.Forms.Label errorText;
    }
}

