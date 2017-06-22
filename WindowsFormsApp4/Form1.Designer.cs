using System;
using System.Drawing;
using System.Xml.Linq;
using System.Configuration;

namespace WindowsFormsApp4
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

        private void InitializeComponent()
        {
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.SuspendLayout();
            // 
            // gmap
            // 
            this.gmap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gmap.AutoSize = true;
            this.gmap.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(229, -1);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaximumSize = new System.Drawing.Size(10000, 10000);
            this.gmap.MaxZoom = 23;
            this.gmap.MinZoom = 2;
            this.gmap.MouseWheelZoomEnabled = true;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(580, 551);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 13D;
            this.gmap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gmap_MarkerClick);
            this.gmap.OnPolygonEnter += new GMap.NET.WindowsForms.PolygonEnter(this.gmap_PoligonEnter);
            this.gmap.OnPolygonLeave += new GMap.NET.WindowsForms.PolygonLeave(this.gmap_PoligonOut);
            this.gmap.OnPolygonClick += new GMap.NET.WindowsForms.PolygonClick(this.gmap_PoligonClick);
            this.gmap.OnMapDrag += new GMap.NET.MapDrag(this.UpdateLatLon);
            this.gmap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseClick);
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

        //#endregion

        override protected void OnLoad(EventArgs e)
        {
            // Server config ---------------------
            XDocument xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Config" + Environment.UserName + ".xml");
            XElement connection = xdoc.Element("Configuracion").Element("ServidorPrograma");

            string id = connection.Element("Id").Value;
            string server = connection.Element("Server").Value;
            string data_base = connection.Element("DataBase").Value;
            string user = connection.Element("User").Value;
            string pass = connection.Element("Password").Value;

            UpdateConnectionConfig(server, data_base, user, pass);
            // -----------------------------------

            // Gmap ------------------------------
            gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmap.SetPositionByKeywords("Batea, España");
            gmap.ShowCenter = false;
            // -----------------------------------

            // Managers --------------------------
            point_manager = new PointsManager(gmap);
            propietaris_manager = new PropietarisManager();
            ui_manager = new UIManager(this);
            id_manager = new IDManager();
            server_manager = new ServerManager();
            // -----------------------------------

            // UI --------------------------------
            LoadUI();

            // Carrega info ----------------------
            ActualitzaPropietarisDesDeServidor();
            ActualitzaFinquesDesDeServidor();
            ActualitzaParcelesDesDeServidor();
            ActualitzaVarietatsDesDeServidor();

            ActualitzaLlistaParceles();

            UpdateLatLon();
            // -----------------------------------

        }

        public void LoadUI()
        {
            // UI

            // General Map UI Input
            map_win = new UI_Window(this);
            {
                text_input_lat = new UI_TextInput(new Point(8, 480), 100, 50);
                text_input_lat.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                map_win.AddElement(text_input_lat);

                text_input_lon = new UI_TextInput(new Point(115, 480), 100, 50);
                text_input_lon.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                map_win.AddElement(text_input_lon);

                search_button = new UI_Button(new Point(7, 450), 50, 23, "Cerca");
                search_button.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                search_button.GetElement().Click += new System.EventHandler(this.SearchLatLon);
                map_win.AddElement(search_button);

                lat_text = new UI_Text(new Point(8, 505), 193, 40, "Lat");
                lat_text.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                map_win.AddElement(lat_text);

                lon_text = new UI_Text(new Point(115, 505), 193, 40, "Lon");
                lon_text.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                map_win.AddElement(lon_text);

                mapsat_button = new UI_Button(new Point(690, 500), 100, 23, "Canvia a satel.lit");
                mapsat_button.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
                mapsat_button.GetElement().Click += new System.EventHandler(this.SwitchMapSat);
                map_win.AddElement(mapsat_button);
            }
            ui_manager.AddUIWindow(map_win);

            // Starting Window
            main_win = new UI_Window(this);
            {
                propietaris_text = new UI_Text(new Point(15, 15), 193, 40, "Propietari: ");
                propietaris_text.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                main_win.AddElement(propietaris_text);

                nom_propietari_panel = new UI_Panel(new Point(18, 34), 165, 23);
                nom_propietari_panel.SetColor(Color.Cornsilk);
                main_win.AddElement(nom_propietari_panel);

                open_propietari_search_button = new UI_Button(new Point(190, 33), 26, 25, "...");
                open_propietari_search_button.GetElement().Click += new System.EventHandler(this.ObreFinestraSeleccioPropietari);
                main_win.AddElement(open_propietari_search_button);

                propietari_nom_text = new UI_Text(new Point(4, 5), 200, 30, "No hi ha propietari seleccionat");
                nom_propietari_panel.AddElement(propietari_nom_text);


                finques_text = new UI_Text(new Point(15, 75), 193, 40, "Finca: ");
                finques_text.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                main_win.AddElement(finques_text);

                nom_finca_panel = new UI_Panel(new Point(18, 94), 165, 23);
                nom_finca_panel.SetColor(Color.Cornsilk);
                main_win.AddElement(nom_finca_panel);

                open_finca_search_button = new UI_Button(new Point(190, 93), 26, 25, "...");
                open_finca_search_button.GetElement().Click += new System.EventHandler(this.ObreFinestraSeleccioFinca);
                main_win.AddElement(open_finca_search_button);

                finca_nom_text = new UI_Text(new Point(4, 5), 200, 30, "No hi ha finca seleccionada");
                nom_finca_panel.AddElement(finca_nom_text);


                varietat_text = new UI_Text(new Point(15, 135), 193, 40, "Varietat: ");
                varietat_text.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                main_win.AddElement(varietat_text);

                nom_varietat_panel = new UI_Panel(new Point(18, 154), 165, 23);
                nom_varietat_panel.SetColor(Color.Cornsilk);
                main_win.AddElement(nom_varietat_panel);

                open_varietat_search_button = new UI_Button(new Point(190, 153), 26, 25, "...");
                open_varietat_search_button.GetElement().Click += new System.EventHandler(this.ObreFinestraSeleccioVarietat);
                main_win.AddElement(open_varietat_search_button);

                varietat_nom_text = new UI_Text(new Point(4, 5), 200, 30, "No hi ha varietat seleccionada");
                nom_varietat_panel.AddElement(varietat_nom_text);


                neteja_seleccions = new UI_Button(new Point(16, 200), 200, 23, "Neteja");
                neteja_seleccions.GetElement().Click += new System.EventHandler(this.NetejaSeleccions);
                main_win.AddElement(neteja_seleccions);


                llista_finques_text = new UI_Text(new Point(15, 230), 200, 30, "Parceles:");
                main_win.AddElement(llista_finques_text);

                llista_finques_panel = new UI_Panel(new Point(15, 250), 200, 180);
                llista_finques_panel.SetColor(Color.Cornsilk);
                llista_finques_panel.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                main_win.AddElement(llista_finques_panel);


                editor_parceles_panel = new UI_Panel(new Point(229, 0), 600, 30);
                editor_parceles_panel.SetColor(Color.Cornsilk);
                editor_parceles_panel.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
                main_win.AddElement(editor_parceles_panel);
                {
                    editor_parceles_crea_button = new UI_Button(new Point(5, 3), 100, 25, "Crea parcela");
                    editor_parceles_crea_button.GetElement().Click += new System.EventHandler(this.CreaParcela);
                    editor_parceles_panel.AddElement(editor_parceles_crea_button);

                    editor_parceles_elimina_button = new UI_Button(new Point(110, 3), 100, 25, "Elimina parcela");
                    editor_parceles_elimina_button.GetElement().Click += new System.EventHandler(this.EliminaParcela);
                    editor_parceles_panel.AddElement(editor_parceles_elimina_button);

                    editor_parceles_guarda_button = new UI_Button(new Point(220, 3), 100, 25, "Guarda canvis");
                    editor_parceles_guarda_button.GetElement().Click += new System.EventHandler(this.GuardaCanvis);
                    editor_parceles_panel.AddElement(editor_parceles_guarda_button);

                    editor_parceles_ultim_guardat = new UI_Text(new Point(330, 9), 100, 25, "");
                    editor_parceles_panel.AddElement(editor_parceles_ultim_guardat);
                }
                editor_parceles_panel.SetEnabled(false);
            }
            ui_manager.AddUIWindow(main_win);

            // Finestra seleccio propietari
            seleccio_propietari_win = new UI_Window(this);
            {
                seleccio_propietari_panel = new UI_Panel(new Point(250, 20), 350, 50);
                seleccio_propietari_panel.SetColor(Color.Cornsilk);
                seleccio_propietari_panel.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                seleccio_propietari_win.AddElement(seleccio_propietari_panel);

                seleccio_propietari_noms_combobox = new UI_ComboBox(new Point(15, 15), 250, 100);
                seleccio_propietari_noms_combobox.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                seleccio_propietari_noms_combobox.SetDrowDownVisibleItems(30);
                seleccio_propietari_panel.AddElement(seleccio_propietari_noms_combobox);

                seleccio_propietari_guarda_button = new UI_Button(new Point(270, 14), 70, 23, "Selecciona");
                seleccio_propietari_guarda_button.GetElement().Click += new System.EventHandler(this.SeleccioPropietariGuarda);
                seleccio_propietari_panel.AddElement(seleccio_propietari_guarda_button);
            }
            ui_manager.AddUIWindow(seleccio_propietari_win);
            seleccio_propietari_win.SetEnabled(false);

            // Finestra seleccio finca
            seleccio_finca_win = new UI_Window(this);
            {
                seleccio_finca_panel = new UI_Panel(new Point(250, 80), 350, 50);
                seleccio_finca_panel.SetColor(Color.Cornsilk);
                seleccio_finca_panel.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                seleccio_finca_win.AddElement(seleccio_finca_panel);

                seleccio_finca_noms_combobox = new UI_ComboBox(new Point(15, 15), 250, 100);
                seleccio_finca_noms_combobox.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                seleccio_finca_noms_combobox.SetDrowDownVisibleItems(30);
                seleccio_finca_panel.AddElement(seleccio_finca_noms_combobox);

                seleccio_finca_guarda_button = new UI_Button(new Point(270, 14), 70, 23, "Selecciona");
                seleccio_finca_guarda_button.GetElement().Click += new System.EventHandler(this.SeleccioFincaGuarda);
                seleccio_finca_panel.AddElement(seleccio_finca_guarda_button);
            }
            ui_manager.AddUIWindow(seleccio_finca_win);
            seleccio_finca_win.SetEnabled(false);

            // Finestra seleccio varietat
            seleccio_varietat_win = new UI_Window(this);
            {
                seleccio_varietat_panel = new UI_Panel(new Point(250, 145), 350, 50);
                seleccio_varietat_panel.SetColor(Color.Cornsilk);
                seleccio_varietat_panel.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                seleccio_varietat_win.AddElement(seleccio_varietat_panel);

                seleccio_varietat_noms_combobox = new UI_ComboBox(new Point(15, 15), 250, 100);
                seleccio_varietat_noms_combobox.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                seleccio_varietat_noms_combobox.SetDrowDownVisibleItems(30);
                seleccio_varietat_panel.AddElement(seleccio_varietat_noms_combobox);

                seleccio_varietat_guarda_button = new UI_Button(new Point(270, 14), 70, 23, "Selecciona");
                seleccio_varietat_guarda_button.GetElement().Click += new System.EventHandler(this.SeleccioVarietatGuarda);
                seleccio_varietat_panel.AddElement(seleccio_varietat_guarda_button);
            }
            ui_manager.AddUIWindow(seleccio_varietat_win);
            seleccio_varietat_win.SetEnabled(false);

            gmap.SendToBack();
        }

        private void UpdateConnectionConfig(string server, string database, string user, string pass)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");

            string ds = "Data Source = " + server + "; Initial Catalog = " + database + "; User ID = " + user + ";" + "password=" + pass;
            connectionStringsSection.ConnectionStrings["WindowsFormsApp4.Properties.Settings.AnaWinCellerBateaConnectionString"].ConnectionString = ds;

            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        // Windows
        UI_Window main_win = null;
        UI_Window map_win = null;
        UI_Window seleccio_propietari_win = null;
        UI_Window seleccio_finca_win = null;
        UI_Window seleccio_varietat_win = null;
        UI_Window guarda_finalitzat_win = null;

        // Elements
        UI_TextInput text_input_lat = null;
        UI_TextInput text_input_lon = null;
        UI_Button search_button = null;
        UI_Text lat_text = null;
        UI_Text lon_text = null;
        UI_Button mapsat_button = null;
        UI_Text propietaris_text = null;
        UI_Panel nom_propietari_panel = null;
        UI_Button open_propietari_search_button = null;
        UI_Text propietari_nom_text = null;
        UI_Text finques_text = null;
        UI_Panel nom_finca_panel = null;
        UI_Button open_finca_search_button = null;
        UI_Text finca_nom_text = null;
        UI_Text varietat_text = null;
        UI_Panel nom_varietat_panel = null;
        UI_Button open_varietat_search_button = null;
        UI_Text varietat_nom_text = null;
        UI_Button neteja_seleccions = null;
        UI_Text llista_finques_text = null;
        UI_Panel llista_finques_panel = null;

        UI_Panel editor_parceles_panel = null;
        UI_Button editor_parceles_crea_button = null;
        UI_Button editor_parceles_elimina_button = null;
        UI_Button editor_parceles_guarda_button = null;
        UI_Text editor_parceles_ultim_guardat = null;

        UI_Panel seleccio_propietari_panel = null;
        UI_ComboBox seleccio_propietari_noms_combobox = null;
        UI_Button seleccio_propietari_guarda_button = null;
        UI_Panel seleccio_finca_panel = null;
        UI_ComboBox seleccio_finca_noms_combobox = null;
        UI_Button seleccio_finca_guarda_button = null;

        UI_Panel seleccio_varietat_panel = null;
        UI_ComboBox seleccio_varietat_noms_combobox = null;
        UI_Button seleccio_varietat_guarda_button = null;

        UI_Panel guarda_finalitzat_panel = null;

        // Managers
        public GMap.NET.WindowsForms.GMapControl gmap = null;
        public PropietarisManager propietaris_manager = null;
        public PointsManager point_manager = null;
        public UIManager ui_manager = null;
        public IDManager id_manager = null;
        public ServerManager server_manager = null;
    }
}

