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

        private void InitializeComponent(string[] args)
        {
            for(int i = 0; i < args.Length; i++)
            {
                string curr_string = args[i];

                switch(i)
                {
                    case 0:
                        finca_id = curr_string;
                        break;
                }
            }

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
            this.gmap.OnPolygonClick += new GMap.NET.WindowsForms.PolygonClick(this.gmap_PoligonClick);
            this.gmap.OnPolygonEnter += new GMap.NET.WindowsForms.PolygonEnter(this.gmap_PoligonEnter);
            this.gmap.OnPolygonLeave += new GMap.NET.WindowsForms.PolygonLeave(this.gmap_PoligonOut);
            this.gmap.OnMapDrag += new GMap.NET.MapDrag(this.UpdateLatLon);
            this.gmap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 531);
            this.Controls.Add(this.gmap);
            this.MinimumSize = new System.Drawing.Size(750, 531);
            this.Name = "Gis AnaWin";
            this.Text = "Gis AnaWin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // Load
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

            this.Text += (" (" + data_base + ")");
            // -----------------------------------

            // Gmap ------------------------------
            gmap.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
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
            text_input_nom.SetText("Batea, España");
            // -----------------------------------

            // Carrega info ----------------------
            ActualitzaPropietarisDesDeServidor();
            ActualitzaFinquesDesDeServidor();
            ActualitzaVarietatsDesDeServidor();
            ActualitzaTreballsDesDeServidor();
            ActualitzaParcelesDesDeServidor();

            ActualitzaLlistaParceles();

            UpdateLatLon();

            LoadArgs();
            // -----------------------------------

        }

        public void LoadUI()
        {
            // UI

            // General Map UI Input
            map_win = new UI_Window(this);
            {
                text_input_lat = new UI_TextInput(new Point(16, 485), 92, 50);
                text_input_lat.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                map_win.AddElement(text_input_lat);

                text_input_lon = new UI_TextInput(new Point(123, 485), 92, 50);
                text_input_lon.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                map_win.AddElement(text_input_lon);

                search_button_coor = new UI_Button(new Point(15, 460), 201, 23, "Cerca coordenades");
                search_button_coor.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                search_button_coor.GetElement().Click += new System.EventHandler(this.SearchLatLon);
                map_win.AddElement(search_button_coor);

                lat_text = new UI_Text(new Point(16, 510), 193, 40, "Lat");
                lat_text.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                map_win.AddElement(lat_text);

                lon_text = new UI_Text(new Point(123, 510), 193, 40, "Lon");
                lon_text.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                map_win.AddElement(lon_text);

                search_button_name = new UI_Button(new Point(15, 403), 201, 23, "Cerca nom");
                search_button_name.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                search_button_name.GetElement().Click += new System.EventHandler(this.SearchName);
                map_win.AddElement(search_button_name);

                text_input_nom = new UI_TextInput(new Point(16, 428), 200, 50);
                text_input_nom.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                map_win.AddElement(text_input_nom);

                mapsat_button = new UI_Button(new Point(690, 500), 100, 23, "Canvia a Mapa");
                mapsat_button.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
                mapsat_button.GetElement().Click += new System.EventHandler(this.SwitchMapSat);
                map_win.AddElement(mapsat_button);

                editor_parceles_ultim_guardat = new UI_Text(new Point(520, 505), 100, 25, "");
                editor_parceles_ultim_guardat.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
                map_win.AddElement(editor_parceles_ultim_guardat);
            }
            ui_manager.AddUIWindow(map_win);

            // Starting Window
            main_win = new UI_Window(this);
            {
                // Propietaris - Finques
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

                finques_text = new UI_Text(new Point(15, 65), 193, 40, "Finca: ");
                finques_text.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                main_win.AddElement(finques_text);

                nom_finca_panel = new UI_Panel(new Point(18, 84), 165, 23);
                nom_finca_panel.SetColor(Color.Cornsilk);
                main_win.AddElement(nom_finca_panel);

                open_finca_search_button = new UI_Button(new Point(190, 83), 26, 25, "...");
                open_finca_search_button.GetElement().Click += new System.EventHandler(this.ObreFinestraSeleccioFinca);
                main_win.AddElement(open_finca_search_button);

                finca_nom_text = new UI_Text(new Point(4, 5), 200, 30, "No hi ha finca seleccionada");
                nom_finca_panel.AddElement(finca_nom_text);

                propietari_finca_cerca = new UI_Button(new Point(18, 110), 46, 20, "Cerca");
                propietari_finca_cerca.GetElement().Click += new System.EventHandler(this.CercaPropietariFinca);
                main_win.AddElement(propietari_finca_cerca);
                propietari_finca_neteja = new UI_Button(new Point(70, 110), 46, 20, "Neteja");
                propietari_finca_neteja.GetElement().Click += new System.EventHandler(this.NetejaPropietariFinca);
                main_win.AddElement(propietari_finca_neteja);

                divisor_propietari_finca = new UI_Panel(new Point(18, 135), 195, 1);
                divisor_propietari_finca.SetColor(Color.Black);
                main_win.AddElement(divisor_propietari_finca);

                // Varietat
                varietat_text = new UI_Text(new Point(15, 145), 193, 40, "Varietat: ");
                varietat_text.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                main_win.AddElement(varietat_text);

                nom_varietat_panel = new UI_Panel(new Point(18, 164), 165, 23);
                nom_varietat_panel.SetColor(Color.Cornsilk);
                main_win.AddElement(nom_varietat_panel);

                open_varietat_search_button = new UI_Button(new Point(190, 163), 26, 25, "...");
                open_varietat_search_button.GetElement().Click += new System.EventHandler(this.ObreFinestraSeleccioVarietat);
                main_win.AddElement(open_varietat_search_button);

                varietat_nom_text = new UI_Text(new Point(4, 5), 200, 30, "No hi ha varietat seleccionada");
                nom_varietat_panel.AddElement(varietat_nom_text);

                varietat_cerca = new UI_Button(new Point(18, 190), 46, 20, "Cerca");
                varietat_cerca.GetElement().Click += new System.EventHandler(this.CercaVarietat);
                main_win.AddElement(varietat_cerca);
                varietat_neteja = new UI_Button(new Point(70, 190), 46, 20, "Neteja");
                varietat_neteja.GetElement().Click += new System.EventHandler(this.NetejaVarietat);
                main_win.AddElement(varietat_neteja);

                divisor_varietat = new UI_Panel(new Point(18, 215), 195, 1);
                divisor_varietat.SetColor(Color.Black);
                main_win.AddElement(divisor_varietat);

                // Treballs
                treballs_text = new UI_Text(new Point(15, 225), 100, 40, "Treballs: ");
                main_win.AddElement(treballs_text);

                nom_treball_panel = new UI_Panel(new Point(18, 245), 165, 23);
                nom_treball_panel.SetColor(Color.Cornsilk);

                open_treball_search_button = new UI_Button(new Point(190, 245), 26, 25, "...");
                open_treball_search_button.GetElement().Click += new System.EventHandler(this.ObreFinestraSeleccioTreball);
                main_win.AddElement(open_treball_search_button);

                treball_nom_text = new UI_Text(new Point(4, 5), 200, 30, "No hi ha treball seleccionat");
                nom_treball_panel.AddElement(treball_nom_text);
                main_win.AddElement(nom_treball_panel);

                treball_cerca = new UI_Button(new Point(18, 270), 46, 20, "Cerca");
                treball_cerca.GetElement().Click += new System.EventHandler(this.CercaTreball);
                main_win.AddElement(treball_cerca);
                treball_neteja = new UI_Button(new Point(70, 270), 46, 20, "Neteja");
                treball_neteja.GetElement().Click += new System.EventHandler(this.NetejaTreball);
                main_win.AddElement(treball_neteja);

                divisor_treball = new UI_Panel(new Point(18, 295), 195, 1);
                divisor_treball.SetColor(Color.Black);
                main_win.AddElement(divisor_treball);

                // Parceles
                llista_parceles_text = new UI_Text(new Point(15, 305), 200, 30, "Parceles:");
                main_win.AddElement(llista_parceles_text);

                llista_parceles_llista = new UI_ListBox(new Point(15, 325), 200, 80);
                llista_parceles_llista.GetElement().Click += new System.EventHandler(this.ParcelaClick);
                llista_parceles_llista.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                main_win.AddElement(llista_parceles_llista);

                // Editor parceles
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

                    editor_parceles_opcions = new UI_Button(new Point(330, 3), 100, 25, "Opcions parcela");
                    editor_parceles_opcions.GetElement().Click += new System.EventHandler(this.ObreFinestraOpcionsParcela);
                    editor_parceles_panel.AddElement(editor_parceles_opcions);
                }
                editor_parceles_panel.SetEnabled(false);
            }
            ui_manager.AddUIWindow(main_win);

            // Finestra seleccio propietari
            seleccio_propietari_win = new UI_Window(this);
            {
                seleccio_propietari_panel = new UI_Panel(new Point(230, 20), 350, 50);
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
                seleccio_finca_panel = new UI_Panel(new Point(230, 80), 350, 50);
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
                seleccio_varietat_panel = new UI_Panel(new Point(230, 145), 350, 50);
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

            // Finestra seleccio treball
            seleccio_treball_win = new UI_Window(this);
            {
                seleccio_treball_panel = new UI_Panel(new Point(230, 203), 350, 50);
                seleccio_treball_panel.SetColor(Color.Cornsilk);
                seleccio_treball_panel.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                seleccio_treball_win.AddElement(seleccio_treball_panel);

                seleccio_treball_noms_combobox = new UI_ComboBox(new Point(15, 15), 250, 100);
                seleccio_treball_noms_combobox.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                seleccio_treball_noms_combobox.SetDrowDownVisibleItems(30);
                seleccio_treball_panel.AddElement(seleccio_treball_noms_combobox);

                seleccio_treball_guarda_button = new UI_Button(new Point(270, 14), 70, 23, "Selecciona");
                seleccio_treball_guarda_button.GetElement().Click += new System.EventHandler(this.SeleccioTreballGuarda);
                seleccio_treball_panel.AddElement(seleccio_treball_guarda_button);

            }
            ui_manager.AddUIWindow(seleccio_treball_win);
            seleccio_treball_win.SetEnabled(false);

            guarda_canvis_win = new UI_Window(this);
            {
                guarda_canvis_panel = new UI_Panel(new Point(400, 220), 230, 70);
                guarda_canvis_panel.SetColor(Color.Cornsilk);
                guarda_canvis_panel.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.None);
                guarda_canvis_panel.GetElement().Dock = System.Windows.Forms.DockStyle.None;
                guarda_canvis_win.AddElement(guarda_canvis_panel);

                guarda_canvis_text = new UI_Text(new Point(5, 7), 25, 100, "Canvis guardats correctament");
                guarda_canvis_text.SetTextSize(12);
                guarda_canvis_panel.AddElement(guarda_canvis_text);

                guarda_canvis_button = new UI_Button(new Point(58, 35), 120, 30, "D'acord");
                guarda_canvis_button.GetElement().Click += new System.EventHandler(this.ConfirmaGuardaCanvis);
                guarda_canvis_panel.AddElement(guarda_canvis_button);
            }
            ui_manager.AddUIWindow(guarda_canvis_win);
            guarda_canvis_win.SetEnabled(false);

            // Always on bottom
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

        private void LoadArgs()
        {
            if(finca_id != "")
            {
                Finca f = GetFincaPerId(finca_id);

                if (f != null)
                {
                    propietaris_manager.finca_actual = f;

                    Propietari p = GetPropietariPerFinca(f);

                    propietaris_manager.propietari_actual = p;

                    propietari_nom_text.SetText(propietaris_manager.propietari_actual.GetTbl().Nombre);

                    finca_nom_text.SetText(propietaris_manager.finca_actual.GetTbl().Nom1);

                    ActualitzaLlistaParceles();
                }
            }
        }

        // Windows
        UI_Window main_win = null;
        UI_Window map_win = null;
        UI_Window seleccio_propietari_win = null;
        UI_Window seleccio_finca_win = null;
        UI_Window seleccio_varietat_win = null;
        UI_Window seleccio_treball_win = null;
        UI_Window guarda_canvis_win = null;

        // Elements
        UI_TextInput text_input_lat = null;
        UI_TextInput text_input_lon = null;
        UI_Button search_button_coor = null;
        UI_Text lat_text = null;
        UI_Text lon_text = null;
        UI_Button search_button_name = null;
        UI_TextInput text_input_nom = null;
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
        UI_Text treballs_text = null;
        UI_Panel nom_treball_panel = null;
        UI_Button open_treball_search_button = null;
        UI_Text treball_nom_text = null;
        UI_Text llista_parceles_text = null;
        UI_ListBox llista_parceles_llista = null;
        UI_Button propietari_finca_cerca = null;
        UI_Button propietari_finca_neteja = null;
        UI_Panel divisor_propietari_finca = null;
        UI_Button varietat_cerca = null;
        UI_Button varietat_neteja = null;
        UI_Panel divisor_varietat = null;
        UI_Button treball_cerca = null;
        UI_Button treball_neteja = null;
        UI_Panel divisor_treball = null;

        UI_Panel editor_parceles_panel = null;
        UI_Button editor_parceles_crea_button = null;
        UI_Button editor_parceles_elimina_button = null;
        UI_Button editor_parceles_guarda_button = null;
        UI_Text editor_parceles_ultim_guardat = null;
        UI_Button editor_parceles_opcions = null;

        UI_Panel seleccio_propietari_panel = null;
        UI_ComboBox seleccio_propietari_noms_combobox = null;
        UI_Button seleccio_propietari_guarda_button = null;

        UI_Panel seleccio_finca_panel = null;
        UI_ComboBox seleccio_finca_noms_combobox = null;
        UI_Button seleccio_finca_guarda_button = null;

        UI_Panel seleccio_varietat_panel = null;
        UI_ComboBox seleccio_varietat_noms_combobox = null;
        UI_Button seleccio_varietat_guarda_button = null;

        UI_Panel seleccio_treball_panel = null;
        UI_ComboBox seleccio_treball_noms_combobox = null;
        UI_Button seleccio_treball_guarda_button = null;

        UI_Panel guarda_canvis_panel = null;
        UI_Text guarda_canvis_text = null;
        UI_Button guarda_canvis_button = null;


        // Managers
        public GMap.NET.WindowsForms.GMapControl gmap = null;
        public PropietarisManager propietaris_manager = null;
        public PointsManager point_manager = null;
        public UIManager ui_manager = null;
        public IDManager id_manager = null;
        public ServerManager server_manager = null;

        // Starting args
        string finca_id = "";
    }
}

