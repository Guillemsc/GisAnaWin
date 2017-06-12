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
            this.gmap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gmap_MarkerClick);
            this.gmap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseClick);
            this.gmap.OnMapDrag += new GMap.NET.MapDrag(this.UpdateLatLon);
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
            gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmap.SetPositionByKeywords("Batea, España");
            gmap.ShowCenter = false;

            point_manager = new PointsManager(gmap);
            propietaris_manager = new PropietarisManager();
            ui_manager = new UIManager(this);
            id_manager = new IDManager();

            LoadUI();

            UpdateLatLon();
        }

        public void LoadUI()
        {
            // UI

            // General Map UI Input
            map_win = new UI_Window("map_win", this);
            {
                text_input_lat = new UI_TextInput("cordenates_lat", new Point(8, 480), 100, 50);
                map_win.AddElement(text_input_lat);
                text_input_lon = new UI_TextInput("cordenates_lon", new Point(115, 480), 100, 50);
                map_win.AddElement(text_input_lon);

                UI_Button search_button = new UI_Button("search_button", new Point(7, 450), 50, 23, "Cerca");
                search_button.GetElement().Click += new System.EventHandler(this.SearchLatLon);
                map_win.AddElement(search_button);

                UI_Text lat_text = new UI_Text("lat_text", new Point(8, 505), 193, 40, "Lat");
                //lat_text.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
                map_win.AddElement(lat_text);

                UI_Text lon_text = new UI_Text("lon_text", new Point(115, 505), 193, 40, "Lon");
                map_win.AddElement(lon_text);

                UI_Button mapsat_button = new UI_Button("mapsat_button", new Point(690, 500), 100, 23, "Canvia a satel.lit");
                mapsat_button.GetElement().Click += new System.EventHandler(this.SwitchMapSat);
                map_win.AddElement(mapsat_button);
            }
            ui_manager.AddUIWindow(map_win);

            // Starting Window
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

            // Finestra per a afegir un propietari
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

                    UI_Button afegir_propietari_exit_button = new UI_Button("afegir_propietari_exit_button", new Point(375, 5), 19, 19, "X");
                    afegir_propietari_exit_button.GetElement().Click += new System.EventHandler(this.TancarAfegeixPropietari);
                    p2.AddElement(afegir_propietari_exit_button);

                }
            }
            afegir_propietari_win.SetEnabled(false);
            ui_manager.AddUIWindow(afegir_propietari_win);

            // Finestra del propietari
            propietari_info_win = new UI_Window("propietari_info_win", this);
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

                UI_Button elimina_parcela_button = new UI_Button("elimina_parcela_button", new Point(15, 300), 193, 28, "Elimina Parcela");
                elimina_parcela_button.GetElement().Click += new System.EventHandler(this.AfegeixParcela);
                propietari_info_win.AddElement(elimina_parcela_button);
                elimina_parcela_button.SetEnabled(false);

                UI_Button b5 = new UI_Button("canvia_propietari", new Point(15, 360), 193, 28, "Canvia de Propietari");
                b5.GetElement().Click += new System.EventHandler(this.CanviaPropietari);
                propietari_info_win.AddElement(b5);
            }
            ui_manager.AddUIWindow(propietari_info_win);
            propietari_info_win.SetEnabled(false);

            // Finestra per a afegir una finca
            afegir_finca_win = new UI_Window("afegir_finca_win", this);
            {
                UI_Panel p4 = new UI_Panel("finca_panel", new Point(230, 13), 500, 60);
                afegir_finca_win.AddElement(p4);
                p4.GetElement().BringToFront();
                {
                    UI_Text tex = new UI_Text("nom_finca_text", new Point(5, 10), 20, 40, "Nom finca:");
                    p4.AddElement(tex);

                    UI_TextInput ti2 = new UI_TextInput("nom_finca_text_input", new Point(80, 7), 300, 80);
                    p4.AddElement(ti2);

                    UI_Button b6 = new UI_Button("afegir_finca_button", new Point(80, 30), 130, 25, "Afegir Finca");
                    b6.GetElement().Click += new System.EventHandler(this.AfegirFinca);
                    p4.AddElement(b6);
                }
            }
            ui_manager.AddUIWindow(afegir_finca_win);
            afegir_finca_win.SetEnabled(false);

            opcions_finca_win = new UI_Window("opcions_finca_win", this);
            {
                UI_Panel opcions_finca_panel = new UI_Panel("opcions_finca_panel", new Point(230, 13), 500, 60);
                opcions_finca_win.AddElement(opcions_finca_panel);
                opcions_finca_panel.GetElement().BringToFront();
                {
                    UI_Text opcions_nom_finca_text = new UI_Text("opcions_nom_finca_text", new Point(5, 10), 20, 40, "Nom finca:");
                    opcions_finca_panel.AddElement(opcions_nom_finca_text);

                    UI_TextInput opcions_nom_finca_text_input = new UI_TextInput("opcions_nom_finca_text_input", new Point(80, 7), 300, 80);
                    opcions_finca_panel.AddElement(opcions_nom_finca_text_input);

                    UI_Button actualitza_finca = new UI_Button("actualitza_finca", new Point(80, 30), 130, 25, "Actualitza Finca");
                    actualitza_finca.GetElement().Click += new System.EventHandler(this.ActualitzaFinca);
                    opcions_finca_panel.AddElement(actualitza_finca);
                }
            }
            ui_manager.AddUIWindow(opcions_finca_win);
            opcions_finca_win.SetEnabled(false);

            gmap.SendToBack();
        }

        // Windows
        UI_Window opcions_finca_win = null;
        UI_Window afegir_finca_win = null;
        UI_Window propietari_info_win = null;
        UI_Window afegir_propietari_win = null;
        UI_Window main_win = null;
        UI_Window map_win = null;

        // Necessary Elements
        UI_TextInput text_input_lat = null;
        UI_TextInput text_input_lon = null;

        // Managers
        public GMap.NET.WindowsForms.GMapControl gmap = null;
        public PropietarisManager propietaris_manager = null;
        public PointsManager point_manager = null;
        public UIManager ui_manager = null;
        public IDManager id_manager = null;
    }
}

