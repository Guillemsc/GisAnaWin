using System;
using System.Configuration;
using System.Drawing;
using System.Xml.Linq;
using System.Collections.Generic;

namespace WindowsFormsApp4
{
    partial class Principal
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
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(0, 0);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 2;
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
            this.gmap.Size = new System.Drawing.Size(150, 150);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 0D;
            // 
            // Principal
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Principal";
            this.ResumeLayout(false);

        }

        void LoadArgs(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string curr_string = args[i];

                switch (i)
                {
                    case 0:
                        finca_id = curr_string;
                        break;
                }
            }

            if (finca_id != "")
            {
                Finca f = propietaris_manager.GetFincaPerId(finca_id);

                if (f != null)
                {
                    propietaris_manager.finca_actual = f;

                    Propietari p = propietaris_manager.GetPropietariPerFinca(f);

                    propietaris_manager.propietari_actual = p;

                    ActualitzaLlistaPropietari();
                    ActualitzaLlistaFinques();
                    seleccio_propietari_noms_combobox.SetSelectedElement(propietaris_manager.propietari_actual.GetTbl().Nombre);
                    seleccio_finca_noms_combobox.SetSelectedElement(propietaris_manager.finca_actual.GetTbl().Nom1);

                    ActualitzaLlistaParceles();
                }
            }
        }

        // Load
        void Carrega()
        {

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
            this.gmap.EmptyTileColor = System.Drawing.Color.AliceBlue;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.MarkersEnabled = true;
            this.gmap.MaximumSize = new System.Drawing.Size(10000, 10000);
            this.gmap.MaxZoom = 23;
            this.gmap.MinZoom = 2;
            this.gmap.MouseWheelZoomEnabled = true;
            this.gmap.EmptyTileText = "No es pot carregar";
            this.gmap.IgnoreMarkerOnMouseWheel = true;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 2;
            this.gmap.RoutesEnabled = false;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Fractional;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Location = new System.Drawing.Point(232, 0);
            this.gmap.Size = new System.Drawing.Size(580, 600);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 14D;
            this.gmap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gmap_MarkerClick);
            this.gmap.OnPolygonClick += new GMap.NET.WindowsForms.PolygonClick(this.gmap_PoligonClick);
            this.gmap.OnPolygonEnter += new GMap.NET.WindowsForms.PolygonEnter(this.gmap_PoligonEnter);
            this.gmap.OnPolygonLeave += new GMap.NET.WindowsForms.PolygonLeave(this.gmap_PoligonOut);
            this.gmap.DoubleClick += new System.EventHandler(this.gmap_DoubleClick);
            this.gmap.OnMapDrag += new GMap.NET.MapDrag(this.UpdateLatLon);
            this.gmap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 581);
            this.Controls.Add(this.gmap);
            this.MinimumSize = new System.Drawing.Size(815, 606);
            this.Name = "Gis AnaWin";
            this.Text = this.Name;
            this.ResumeLayout(false);
            this.PerformLayout();

            // Managers --------------------------
            point_manager = new PointsManager(gmap);
            propietaris_manager = new PropietarisManager();
            ui_manager = new UIManager(this);
            id_manager = new IDManager();
            server_manager = new ServerManager();
            // -----------------------------------

            // Server config ---------------------
            XDocument xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Config" + Environment.UserName + ".xml");
            XElement connection = xdoc.Element("Configuracion").Element("ServidorPrograma");
            XElement empresa = xdoc.Element("Configuracion").Element("EmpresaInfo");

            string id = connection.Element("Id").Value;
            string server = connection.Element("Server").Value;
            string data_base = connection.Element("DataBase").Value;
            string user = connection.Element("User").Value;
            string pass = connection.Element("Password").Value;

            UpdateConnectionConfig(server, data_base, user, pass);

            this.Text += (" (" + data_base + ")");

            propietaris_manager.empresa_nom = empresa.Element("Nom").Value;
            // -----------------------------------

            // Gmap ------------------------------
            gmap.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gmap.SetPositionByKeywords("Batea, España");
            gmap.ShowCenter = false;
            // -----------------------------------

            // UI --------------------------------
            LoadUI();
            text_input_nom.SetText("Batea, España");
            // -----------------------------------

            // Carrega info ----------------------
            if (server_manager.CheckServerConnection())
            {
                Console.WriteLine("----------------------------------");
                ActualitzaPropietarisDesDeServidor();
                ActualitzaFinquesDesDeServidor();
                ActualitzaVarietatsDesDeServidor();
                ActualitzaTreballsDesDeServidor();
                ActualitzaParcelesDesDeServidor();
                ActualitzaPartesDesDeServidor();
                ActualitzaPartesLineaDesDeServidor();
                ActualitzaAnalitiquesDesDeServidor();
                ActualitzaTreballsDesDeServidor();
                ActualitzaUnitatsMetriquesDesDeServidor();
                ActualitzaMaquinariaDesDeServidor();
                ActualitzaPersonalDesDeServidor();
                ActualitzaAdobsDesDeServidor();

                ActualitzaLlistaParceles();

                UpdateLatLon();

            }

            gmap.ZoomAndCenterMarkers(point_manager.overlay_finca.Id);
            // -----------------------------------

            // Forms -----------------------------
            crea_parte_form = new AfegirPartes(propietaris_manager, point_manager, server_manager, ui_manager);
            crea_parte_form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(CreaParteTanca);

            info_parte_form = new ModificaPartes(propietaris_manager, point_manager, server_manager, ui_manager);
            info_parte_form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(InfoParteTanca);

            report_viewer_form = new Imprimir();

            visualitza_analitica_form = new Analitiques(propietaris_manager, point_manager, server_manager, ui_manager);

            quadern_camp_form = new Forms.QuadernCamp();

            configura_quadern_camp_form = new Forms.ConfiguraQuadernCamp(propietaris_manager, point_manager, server_manager, ui_manager, quadern_camp_form);

            personal_form = new Forms.Perso(propietaris_manager, point_manager, server_manager, ui_manager);

            maquinaria_form = new Forms.Maquinaria(propietaris_manager, point_manager, server_manager, ui_manager);

            adobats_form = new Forms.Adobats(propietaris_manager, point_manager, server_manager, ui_manager);
            // -----------------------------------

        }

        public void LoadUI()
        {
            // UI

            // General Map UI Input
            map_win = new UI_Window(this);
            {
                text_input_lat = new UI_MaskedTextInput(new Point(16, 535), 92, 50);
                text_input_lat.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                map_win.AddElement(text_input_lat);

                text_input_lon = new UI_MaskedTextInput(new Point(123, 535), 92, 50);
                text_input_lon.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                map_win.AddElement(text_input_lon);

                search_button_coor = new UI_Button(new Point(15, 510), 201, 23, "Cerca coordenades");
                search_button_coor.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                search_button_coor.GetElement().Click += new System.EventHandler(this.SearchLatLon);
                map_win.AddElement(search_button_coor);

                lat_text = new UI_Text(new Point(16, 560), 193, 40, "Lat");
                lat_text.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                map_win.AddElement(lat_text);

                lon_text = new UI_Text(new Point(123, 560), 193, 40, "Lon");
                lon_text.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                map_win.AddElement(lon_text);

                search_button_name = new UI_Button(new Point(15, 453), 201, 23, "Cerca nom");
                search_button_name.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                search_button_name.GetElement().Click += new System.EventHandler(this.SearchName);
                map_win.AddElement(search_button_name);

                text_input_nom = new UI_MaskedTextInput(new Point(16, 478), 200, 50);
                text_input_nom.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                map_win.AddElement(text_input_nom);

                mapsat_button = new UI_Button(new Point(690, 550), 100, 23, "Canvia a Mapa");
                mapsat_button.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
                mapsat_button.GetElement().Click += new System.EventHandler(this.SwitchMapSat);
                map_win.AddElement(mapsat_button);

                imprimir_button = new UI_Button(new Point(630, 550), 50, 23, "Imprimir");
                imprimir_button.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
                imprimir_button.GetElement().Click += new System.EventHandler(this.ImprimirMapa);
                map_win.AddElement(imprimir_button);

                imprimir_quadern_camp_button = new UI_Button(new Point(470, 550), 150, 23, "Imprimir quadern de camp");
                imprimir_quadern_camp_button.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
                imprimir_quadern_camp_button.GetElement().Click += new System.EventHandler(this.ImprimirQuadernCamp);
                map_win.AddElement(imprimir_quadern_camp_button); 

                editor_parceles_ultim_guardat = new UI_Text(new Point(520, 505), 100, 25, "");
                editor_parceles_ultim_guardat.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
                map_win.AddElement(editor_parceles_ultim_guardat);

                personal_button = new UI_Button(new Point(313, 550), 70, 23, "Personal");
                personal_button.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
                personal_button.GetElement().Click += new System.EventHandler(this.ObreFinestraPersonal);
                map_win.AddElement(personal_button);

                maquinaria_button = new UI_Button(new Point(390, 550), 70, 23, "Maquinaria");
                maquinaria_button.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
                maquinaria_button.GetElement().Click += new System.EventHandler(this.ObreFinestraMaquinaria);
                map_win.AddElement(maquinaria_button);

                brou_button = new UI_Button(new Point(245, 550), 60, 23, "Brou");
                brou_button.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
                brou_button.GetElement().Click += new System.EventHandler(this.ObreFinestraAdobats);
                map_win.AddElement(brou_button);
            }
            ui_manager.AddUIWindow(map_win);

            // Starting Window
            main_win = new UI_Window(this);
            {
                // Propietaris - Finques
                propietaris_text = new UI_Text(new Point(15, 15), 193, 40, "Propietari: ");
                propietaris_text.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                main_win.AddElement(propietaris_text);

                seleccio_propietari_noms_combobox = new UI_ComboBox(new Point(15, 35), 200, 80);
                seleccio_propietari_noms_combobox.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                seleccio_propietari_noms_combobox.SetDrowDownVisibleItems(30);
                seleccio_propietari_noms_combobox.DropDown(new System.EventHandler(this.ActualitzaLlistaPropietariEvent));
                seleccio_propietari_noms_combobox.ItemSelected(new System.EventHandler(this.SeleccionaPropietariEvent));
                main_win.AddElement(seleccio_propietari_noms_combobox);

                finques_text = new UI_Text(new Point(15, 65), 193, 40, "Finca: ");
                finques_text.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                main_win.AddElement(finques_text);

                seleccio_finca_noms_combobox = new UI_ComboBox(new Point(15, 84), 200, 100);
                seleccio_finca_noms_combobox.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                seleccio_finca_noms_combobox.SetDrowDownVisibleItems(30);
                seleccio_finca_noms_combobox.DropDown(new System.EventHandler(this.ActualitzaLlistaFinquesEvent));
                seleccio_finca_noms_combobox.ItemSelected(new System.EventHandler(this.SeleccionaFincaEvent));
                main_win.AddElement(seleccio_finca_noms_combobox);

                propietari_finca_neteja = new UI_Button(new Point(15, 110), 46, 20, "Neteja");
                propietari_finca_neteja.GetElement().Click += new System.EventHandler(this.NetejaPropietariFinca);
                main_win.AddElement(propietari_finca_neteja);

                divisor_propietari_finca = new UI_Panel(new Point(18, 135), 195, 1);
                divisor_propietari_finca.SetColor(Color.Black);
                main_win.AddElement(divisor_propietari_finca);

                // Varietat
                varietat_text = new UI_Text(new Point(15, 145), 193, 40, "Varietat: ");
                varietat_text.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                main_win.AddElement(varietat_text);

                seleccio_varietat_noms_combobox = new UI_ComboBox(new Point(15, 164), 200, 100);
                seleccio_varietat_noms_combobox.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                seleccio_varietat_noms_combobox.SetDrowDownVisibleItems(30);
                seleccio_varietat_noms_combobox.DropDown(new System.EventHandler(this.ActualitzaLlistaVarietatsEvent));
                seleccio_varietat_noms_combobox.ItemSelected(new System.EventHandler(this.SeleccionaVarietatEvent));
                main_win.AddElement(seleccio_varietat_noms_combobox);

                varietat_neteja = new UI_Button(new Point(15, 190), 46, 20, "Neteja");
                varietat_neteja.GetElement().Click += new System.EventHandler(this.NetejaVarietat);
                main_win.AddElement(varietat_neteja);

                divisor_varietat = new UI_Panel(new Point(18, 215), 195, 1);
                divisor_varietat.SetColor(Color.Black);
                main_win.AddElement(divisor_varietat);

                // Treballs
                treballs_text = new UI_Text(new Point(15, 225), 100, 40, "Treballs: ");
                main_win.AddElement(treballs_text);

                seleccio_treball_noms_combobox = new UI_ComboBox(new Point(15, 245), 200, 100);
                seleccio_treball_noms_combobox.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                seleccio_treball_noms_combobox.SetDrowDownVisibleItems(30);
                seleccio_treball_noms_combobox.DropDown(new System.EventHandler(this.ActualitzaLlistaTreballsEvent));
                seleccio_treball_noms_combobox.ItemSelected(new System.EventHandler(this.SeleccionaTreballEvent));
                main_win.AddElement(seleccio_treball_noms_combobox);

                treball_neteja = new UI_Button(new Point(15, 270), 46, 20, "Neteja");
                treball_neteja.GetElement().Click += new System.EventHandler(this.NetejaTreball);
                main_win.AddElement(treball_neteja);

                divisor_treball = new UI_Panel(new Point(18, 295), 195, 1);
                divisor_treball.SetColor(Color.Black);
                main_win.AddElement(divisor_treball);

                // Any vinya
                any_vinya_text = new UI_Text(new Point(15, 305), 100, 40, "Any: ");
                main_win.AddElement(any_vinya_text);

                a_vinya_text = new UI_Text(new Point(110, 332), 100, 40, "a");
                main_win.AddElement(a_vinya_text);

                any_comença = new UI_MaskedTextInput(new Point(16, 325), 92, 40, "1900");
                any_comença.OnLostFocus(ActualitzaLlistaParcelesEvent);
                main_win.AddElement(any_comença);

                any_acaba = new UI_MaskedTextInput(new Point(123, 325), 92, 40, DateTime.Now.Year.ToString());
                any_acaba.OnLostFocus(ActualitzaLlistaParcelesEvent);
                main_win.AddElement(any_acaba);

                divisor_any_vinya = new UI_Panel(new Point(18, 350), 195, 1);
                divisor_any_vinya.SetColor(Color.Black);
                main_win.AddElement(divisor_any_vinya);

                // Parceles
                llista_parceles_text = new UI_Text(new Point(15, 355), 200, 30, "Parceles:");
                main_win.AddElement(llista_parceles_text);

                llista_parceles_llista = new UI_ListBox(new Point(15, 375), 200, 80);
                llista_parceles_llista.GetElement().Click += new System.EventHandler(this.ParcelaClick);
                llista_parceles_llista.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom);
                main_win.AddElement(llista_parceles_llista);

                // Editor parceles
                editor_parceles_panel = new UI_Panel(new Point(229, 0), 440, 30);
                editor_parceles_panel.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                main_win.AddElement(editor_parceles_panel);
                {
                    editor_parceles_crea_button = new UI_Button(new Point(5, 3), 100, 25, "Crea parcela");
                    editor_parceles_crea_button.SetColor(Color.Cornsilk);
                    editor_parceles_crea_button.GetElement().Click += new System.EventHandler(this.CreaParcela);
                    editor_parceles_panel.AddElement(editor_parceles_crea_button);

                    editor_parceles_elimina_button = new UI_Button(new Point(110, 3), 100, 25, "Elimina parcela");
                    editor_parceles_elimina_button.SetColor(Color.Cornsilk);
                    editor_parceles_elimina_button.GetElement().Click += new System.EventHandler(this.EliminaParcela);
                    editor_parceles_panel.AddElement(editor_parceles_elimina_button);

                    editor_parceles_guarda_button = new UI_Button(new Point(220, 3), 100, 25, "Guarda canvis");
                    editor_parceles_guarda_button.SetColor(Color.Cornsilk);
                    editor_parceles_guarda_button.GetElement().Click += new System.EventHandler(this.GuardaCanvis);
                    editor_parceles_panel.AddElement(editor_parceles_guarda_button);

                    editor_parceles_opcions = new UI_Button(new Point(330, 3), 100, 25, "Opcions parcela");
                    editor_parceles_guarda_button.SetColor(Color.Cornsilk);
                    editor_parceles_panel.AddElement(editor_parceles_opcions);
                }
                editor_parceles_panel.SetVisible(false);

                // Parceles / Partes seleccionades / Analitiques
                parceles_seleccionades_panel = new UI_Panel(new Point(670, 0), 150, 480);
                parceles_seleccionades_panel.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
                {
                    afegir_partes_button = new UI_Button(new Point(5, 5), 120, 30, "Afegir partes");
                    afegir_partes_button.SetColor(Color.Cornsilk);
                    afegir_partes_button.GetElement().Click += new System.EventHandler(this.ObreFormPartes);
                    parceles_seleccionades_panel.AddElement(afegir_partes_button);

                    elimina_parte_button = new UI_Button(new Point(5, 37), 120, 30, "Elimina parte");
                    elimina_parte_button.SetColor(Color.Cornsilk);
                    elimina_parte_button.GetElement().Click += new System.EventHandler(this.EliminaParte);
                    elimina_parte_button.SetEnabled(false);
                    parceles_seleccionades_panel.AddElement(elimina_parte_button);

                    visualitza_analitiques_button = new UI_Button(new Point(5, 71), 120, 40, "Visualitza analítiques");
                    visualitza_analitiques_button.SetColor(Color.Cornsilk);
                    visualitza_analitiques_button.GetElement().Click += new System.EventHandler(this.ObreFormVisualitzaAnalitiques);
                    parceles_seleccionades_panel.AddElement(visualitza_analitiques_button);

                    parceles_selecionades_text = new UI_Text(new Point(2, 140), 100, 30, "Parceles seleccionades:");
                    parceles_seleccionades_panel.AddElement(parceles_selecionades_text);

                    parceles_seleccionades_listbox = new UI_ListBox(new Point(0, 160), 130, 150);
                    parceles_seleccionades_listbox.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
                    parceles_seleccionades_listbox.SetAutoSize(true);
                    parceles_seleccionades_panel.AddElement(parceles_seleccionades_listbox);

                    ha_parceles_seleccionades_text = new UI_Text(new Point(2, 310), 100, 30, "Ha totals:");
                    parceles_seleccionades_panel.AddElement(ha_parceles_seleccionades_text);

                    ha_valor_parceles_seleccionades_text = new UI_Text(new Point(52, 310), 100, 30, "0");
                    parceles_seleccionades_panel.AddElement(ha_valor_parceles_seleccionades_text);

                    partes_seleccionats_text = new UI_Text(new Point(2, 340), 100, 10, "Partes:");
                    parceles_seleccionades_panel.AddElement(partes_seleccionats_text);

                    partes_seleccionats_listbox = new UI_ListBox(new Point(0, 360), 130, 150);
                    partes_seleccionats_listbox.GetElement().Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
                    partes_seleccionats_listbox.SetAutoSize(true);
                    partes_seleccionats_listbox.GetElement().Click += new System.EventHandler(ClickParte);
                    partes_seleccionats_listbox.GetElement().DoubleClick += new System.EventHandler(ObreFormInfoPartes);
                    parceles_seleccionades_panel.AddElement(partes_seleccionats_listbox);
                }
                main_win.AddElement(parceles_seleccionades_panel);
                parceles_seleccionades_panel.SetVisible(false);
            }
            ui_manager.AddUIWindow(main_win);

            // Finestra confirmacio canvis guardats
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
            guarda_canvis_win.SetVisible(false);

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



        // Forms
        AfegirPartes       crea_parte_form = null;
        ModificaPartes     info_parte_form = null;
        Imprimir           report_viewer_form = null;
        Analitiques        visualitza_analitica_form = null;
        Forms.QuadernCamp  quadern_camp_form = null;
        Forms.ConfiguraQuadernCamp configura_quadern_camp_form = null;
        Forms.Perso        personal_form = null;
        Forms.Maquinaria   maquinaria_form = null;
        Forms.Adobats      adobats_form = null;
        
        // Windows
        UI_Window          main_win = null;
        UI_Window          map_win = null;
        UI_Window          guarda_canvis_win = null;

        // Elements
        UI_MaskedTextInput text_input_lat = null;
        UI_MaskedTextInput text_input_lon = null;
        UI_Button          search_button_coor = null;
        UI_Text            lat_text = null;
        UI_Text            lon_text = null;
        UI_Button          search_button_name = null;
        UI_MaskedTextInput text_input_nom = null;
        UI_Button          mapsat_button = null;
        UI_Button          imprimir_button = null;
        UI_Button          imprimir_quadern_camp_button = null;

        UI_Text            propietaris_text = null;
        UI_Text            finques_text = null;
        UI_Text            varietat_text = null;
        UI_Text            treballs_text = null;
        UI_Text            llista_parceles_text = null;
        UI_ListBox         llista_parceles_llista = null;
        UI_Button          propietari_finca_neteja = null;
        UI_Panel           divisor_propietari_finca = null;
        UI_Button          varietat_neteja = null;
        UI_Panel           divisor_varietat = null;
        UI_Button          treball_neteja = null;
        UI_Panel           divisor_treball = null;
        UI_Text            any_vinya_text = null;
        UI_Text            a_vinya_text = null;
        UI_MaskedTextInput any_comença = null;
        UI_MaskedTextInput any_acaba = null;
        UI_Panel           divisor_any_vinya = null;

        UI_Panel           editor_parceles_panel = null;
        UI_Button          editor_parceles_crea_button = null;
        UI_Button          editor_parceles_elimina_button = null;
        UI_Button          editor_parceles_guarda_button = null;
        UI_Text            editor_parceles_ultim_guardat = null;
        UI_Button          editor_parceles_opcions = null;
                           
        UI_Button          afegir_partes_button = null;
        UI_Panel           parceles_seleccionades_panel = null;
        UI_Text            parceles_selecionades_text = null;
        UI_ListBox         parceles_seleccionades_listbox = null;
        UI_Text            ha_parceles_seleccionades_text = null;
        UI_Text            ha_valor_parceles_seleccionades_text = null;
        UI_Text            partes_seleccionats_text = null;
        UI_ListBox         partes_seleccionats_listbox = null;
        UI_Button          elimina_parte_button = null;
        UI_Button          visualitza_analitiques_button = null;

        UI_ComboBox        seleccio_propietari_noms_combobox = null;
        UI_ComboBox        seleccio_finca_noms_combobox = null;
        UI_ComboBox        seleccio_varietat_noms_combobox = null;
                           
        UI_ComboBox        seleccio_treball_noms_combobox = null;

        UI_Panel           guarda_canvis_panel = null;
        UI_Text            guarda_canvis_text = null;
        UI_Button          guarda_canvis_button = null;

        UI_Button          personal_button = null;
        UI_Button          maquinaria_button = null;
        UI_Button          brou_button = null;


        // Managers
        public GMap.NET.WindowsForms.GMapControl gmap = null;
        public PropietarisManager propietaris_manager = null;
        public PointsManager point_manager = null;
        public UIManager ui_manager = null;
        public IDManager id_manager = null;
        public ServerManager server_manager = null;

        // Logic
        bool mouse_over_polygon = false;

        // Starting args
        string finca_id = "";
    }
}

