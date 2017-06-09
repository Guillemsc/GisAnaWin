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
            this.gmap.MaxZoom = 23;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 531);
            this.Controls.Add(this.gmap);
            this.Name = "Form1";
            this.Text = "Finques Maps Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        override protected void OnLoad(EventArgs e)
        {
            gmap.MapProvider = GMap.NET.MapProviders.BingSatelliteMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmap.SetPositionByKeywords("Batea, España");
            gmap.ShowCenter = false;
            gmap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(gmap_MouseClick);

            point_manager = new PointsManager(gmap);
            propietaris_manager = new PropietarisManager();
            ui_manager = new UIManager(this);

            LoadUI();
        }

        public void LoadUI()
        {
            // UI
            map_win = new UI_Window("map_win", this);
            {
                UI_TextInput text_input = new UI_TextInput("corrdenates_input", new Point(600, 15), 50, 50);
                map_win.AddElement(text_input);
            }

            main_win = new UI_Window("main_window", this);
            {
                UI_Button b = new UI_Button("afegeix_propietari", new Point(15, 15), 193, 28, "Afegeix Propietari");
                b.GetElement().Click += new System.EventHandler(this.AfegeixPropietari);
                main_win.AddElement(b);

                UI_Text t = new UI_Text("propietaris_text", new Point(15, 50), 193, 40, "Propietaris: ");
                main_win.AddElement(t);

                UI_Panel p = new UI_Panel("propietaris_panel", new Point(15, 58), 193, 400);
                main_win.AddElement(p);
            }
            ui_manager.AddUIWindow(main_win);

            afegir_propietari_win = new UI_Window("afegir_propietari_window", this);
            {
                UI_Panel p2 = new UI_Panel("afegir_propietaris_panel", new Point(230, 13), 400, 60);
                afegir_propietari_win.AddElement(p2);
                p2.GetElement().BringToFront();
                {
                    UI_Text t2 = new UI_Text("nom_propietaris_text", new Point(5, 10), 20, 40, "Nom: ");
                    p2.AddElement(t2);

                    UI_TextInput ti = new UI_TextInput("nom_propietari_text_input", new Point(40, 7), 300, 30);
                    p2.AddElement(ti);

                    UI_Button b2 = new UI_Button("afegir_propietari_button", new Point(40, 30), 100, 25, "Afegir Propietari");
                    b2.GetElement().Click += new System.EventHandler(this.AfegirPropietari);
                    p2.AddElement(b2);

                }
            }
            afegir_propietari_win.SetEnabled(false);
            ui_manager.AddUIWindow(afegir_propietari_win);

            propietari_info_win = new UI_Window(" propietari_info_win", this);
            {
                UI_Text t3 = new UI_Text("nom_propietari", new Point(5, 10), 20, 100);
                propietari_info_win.AddElement(t3);

                UI_Button b3 = new UI_Button("afegeix_finca", new Point(15, 30), 193, 28, "Afegeix Finca");
                b3.GetElement().Click += new System.EventHandler(this.AfegeixFinca);
                propietari_info_win.AddElement(b3);

                UI_Text t4 = new UI_Text("finca_text", new Point(15, 60), 193, 40, "Finques: ");
                propietari_info_win.AddElement(t4);

                UI_Panel p3 = new UI_Panel("finques_panel", new Point(15, 80), 193, 200);
                propietari_info_win.AddElement(p3);

                UI_Button b4 = new UI_Button("crea_parcela", new Point(15, 300), 193, 28, "Crea Parcela");
                b4.GetElement().Click += new System.EventHandler(this.AfegeixParcela);
                propietari_info_win.AddElement(b4);
                b4.SetEnabled(false);

                UI_Button b5 = new UI_Button("canvia_propietari", new Point(15, 360), 193, 28, "Canvia de Propietari");
                b5.GetElement().Click += new System.EventHandler(this.CanviaPropietari);
                propietari_info_win.AddElement(b5);
            }
            ui_manager.AddUIWindow(propietari_info_win);
            propietari_info_win.SetEnabled(false);

            afegir_finca_win = new UI_Window("afegir_finca_win", this);
            {
                UI_Panel p4 = new UI_Panel("finca_panel", new Point(230, 13), 500, 60);
                afegir_finca_win.AddElement(p4);
                p4.GetElement().BringToFront();
                {
                    UI_Text tex = new UI_Text("nom_finca_text", new Point(5, 10), 20, 40, "Descripció finca: ");
                    p4.AddElement(tex);

                    UI_TextInput ti2 = new UI_TextInput("nom_finca_text_input", new Point(120, 7), 300, 80);
                    p4.AddElement(ti2);

                    UI_Button b6 = new UI_Button("afegir_finca_button", new Point(120, 30), 130, 25, "Afegir Finca");
                    b6.GetElement().Click += new System.EventHandler(this.AfegirFinca);
                    p4.AddElement(b6);
                }
            }
            ui_manager.AddUIWindow(afegir_finca_win);
            afegir_finca_win.SetEnabled(false);

            gmap.SendToBack();
        }

        UI_Window afegir_finca_win = null;
        UI_Window propietari_info_win = null;
        UI_Window afegir_propietari_win = null;
        UI_Window main_win = null;
        UI_Window map_win = null;

        public GMap.NET.WindowsForms.GMapControl gmap = null;
        public PropietarisManager propietaris_manager = null;
        public PointsManager point_manager = null;
        public UIManager ui_manager = null;
    }
}

