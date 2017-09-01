using System.Drawing;
using System.Collections.Generic;

namespace WindowsFormsApp4
{
    partial class ModificaPartes
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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModificaPartes));
            this.SuspendLayout();
            // 
            // ModificaPartes
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModificaPartes";
            this.ResumeLayout(false);

        }

        #endregion

        void Carrega(Principal _principal, PropietarisManager _propietaris_manager, PointsManager _points_manager, ServerManager _server_manager, 
            UIManager _ui_manager)
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 510);
            this.Name = "Visualitza / Modifica Partes";
            this.Text = this.Name;
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Set Managers ----------------------
            propietaris_manager = _propietaris_manager;
            point_manager = _points_manager;
            server_manager = _server_manager;
            ui_manager = _ui_manager;
            principal = _principal;
            // -----------------------------------

            // UI --------------------------------
            LoadUI();
            // -----------------------------------
        }

        public void LoadUI()
        {
            main_window = new UI_Window(this);
            {
                propietari_text = new UI_Text(new Point(20, 15), 200, 30, "Propietari:");
                propietari_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(propietari_text);

                finca_text = new UI_Text(new Point(400, 15), 200, 30, "Finca:");
                finca_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(finca_text);

                propietari_nom_text = new UI_Text(new Point(120, 15), 200, 30, "TOMAS DEOSDAT OMEDES");
                propietari_nom_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(propietari_nom_text);

                finca_nom_text = new UI_Text(new Point(456, 15), 200, 30, "FINCA1");
                finca_nom_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(finca_nom_text);

                treballs_text = new UI_Text(new Point(20, 50), 80, 100, "Treball:");
                treballs_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(treballs_text);

                treballs_combobox = new UI_ComboBox(new Point(25, 70), 180, 40);
                treballs_combobox.SetFont("Courier New", 8.5f);
                main_window.AddElement(treballs_combobox);

                data_text = new UI_Text(new Point(240, 50), 30, 100, "Data:");
                data_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(data_text);

                data_dataselect = new UI_DateSelect(new Point(242, 70), 200, 100);
                data_dataselect.SetFont("Courier New", 8.5f);
                main_window.AddElement(data_dataselect);

                descripcio_text = new UI_Text(new Point(20, 110), 100, 80, "Descripció:");
                descripcio_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(descripcio_text);

                descripcio_text_input = new UI_TextInput(new Point(22, 130), 421, 70);
                descripcio_text_input.SetFont("Courier New", 8.5f);
                main_window.AddElement(descripcio_text_input);

                eficacia_text = new UI_Text(new Point(460, 110), 100, 40, "Eficacia:");
                eficacia_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(eficacia_text);

                eficacia_combobox = new UI_ComboBox(new Point(460, 130), 150, 30);
                eficacia_combobox.SetFont("Courier New", 8.5f);
                main_window.AddElement(eficacia_combobox);

                adob_text = new UI_Text(new Point(660, 110), 100, 40, "Adob:");
                adob_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(adob_text);

                adob_combobox = new UI_ComboBox(new Point(660, 130), 150, 30);
                adob_combobox.SetFont("Courier New", 8.5f);
                main_window.AddElement(adob_combobox);

                personal_text = new UI_Text(new Point(460, 160), 160, 40, "Personal:");
                personal_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(personal_text);

                personal_combobox = new UI_ComboBox(new Point(460, 180), 150, 30);
                personal_combobox.SetFont("Courier New", 8.5f);
                main_window.AddElement(personal_combobox);

                maquinaria_text = new UI_Text(new Point(660, 160), 160, 40, "Maquinaria:");
                maquinaria_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(maquinaria_text);

                maquinaria_combobox = new UI_ComboBox(new Point(660, 180), 150, 30);
                maquinaria_combobox.SetFont("Courier New", 8.5f);
                main_window.AddElement(maquinaria_combobox);

                unitats_text = new UI_Text(new Point(20, 212), 100, 80, "Unitats:");
                unitats_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(unitats_text);

                unitats_mesura_combobox = new UI_ComboBox(new Point(140, 230), 50, 30);
                unitats_mesura_combobox.SetFont("Courier New", 8.5f);
                main_window.AddElement(unitats_mesura_combobox);

                estat_panel = new UI_Panel(new Point(255, 230), 380, 30);
                estat_panel.SetFont("Courier New", 8.5f);
                main_window.AddElement(estat_panel);

                pendent_check = new UI_RadioButton(new Point(0, 0), "Pendent");
                pendent_check.SetFont("Courier New", 8.5f);
                estat_panel.AddElement(pendent_check);
                proces_check = new UI_RadioButton(new Point(120, 0), "Procés");
                proces_check.SetFont("Courier New", 8.5f);
                estat_panel.AddElement(proces_check);
                acabat_check = new UI_RadioButton(new Point(240, 0), "Acabat");
                acabat_check.SetFont("Courier New", 8.5f);
                estat_panel.AddElement(acabat_check);

                fertirrigacio_checkbox = new UI_CheckBox(new Point(640, 225), 130, 30, "Fertirrigacio");
                fertirrigacio_checkbox.SetFont("Courier New", 8.5f);
                main_window.AddElement(fertirrigacio_checkbox);

                unitats_text_input = new UI_MaskedTextInput(new Point(22, 230), 100, 70);
                unitats_text_input.SetFont("Courier New", 8.5f);
                main_window.AddElement(unitats_text_input);

                remove_treball_button = new UI_Button(new Point(160, 475), 130, 25, "Elimina parte");
                remove_treball_button.AddImage(principal.imageList1, 3);
                remove_treball_button.SetFont("Courier New", 8.5f);
                remove_treball_button.GetElement().Click += new System.EventHandler(this.EliminaLineaParte);
                main_window.AddElement(remove_treball_button);

                modifica_treball_button = new UI_Button(new Point(21, 475), 130, 25, "Modifica parte");
                modifica_treball_button.AddImage(principal.imageList1, 8);
                modifica_treball_button.SetFont("Courier New", 8.5f);
                modifica_treball_button.GetElement().Click += new System.EventHandler(this.ModificaParteSeleccionat);
                main_window.AddElement(modifica_treball_button);

                grid = new UI_Grid(new Point(22, 260), 870, 200);
                grid.AddColumn("Treball", 70, true); grid.AddColumn("Descripció", 300); grid.AddColumn("Unitats", 200); grid.AddColumn("tblLinea", 0, true, false);
                grid.AddColumn("Unitat Metrica", 130, true); grid.AddColumn("Estat", 200, true); grid.AddColumn("Parcela viti", 100, true); grid.AddColumn("Ha", 60, true);
                grid.AddColumn("Fertirrigació", 100, true); grid.AddColumn("Eficacia tractament", 70, true);
                grid.AddColumn("Aplicador", 100, true); grid.AddColumn("Maquinaria", 100, true); grid.AddColumn("Adob", 100, true);
                grid.GetElement().Click += new System.EventHandler(this.LineaParteClick);
                grid.SetFont("Courier New", 8.5f);
                main_window.AddElement(grid);

                accepta_button = new UI_Button(new Point(773, 475), 120, 25, "Desa i surt");
                accepta_button.AddImage(principal.imageList1, 2);
                accepta_button.SetFont("Courier New", 8.5f);
                accepta_button.SetColor(Color.Cornsilk);
                accepta_button.GetElement().Click += new System.EventHandler(this.Accepta);
                main_window.AddElement(accepta_button);
            }
        }

        // UI
        UI_Window main_window = null;

        UI_Text propietari_text = null;
        UI_Text propietari_nom_text = null;

        UI_Text finca_text = null;
        UI_Text finca_nom_text = null;

        UI_Text treballs_text = null;
        UI_ComboBox treballs_combobox = null;
        UI_Text data_text = null;
        UI_DateSelect data_dataselect = null;
        UI_Text descripcio_text = null;
        UI_TextInput descripcio_text_input = null;
        UI_Text eficacia_text = null;
        UI_ComboBox eficacia_combobox = null;
        UI_Text personal_text = null;
        UI_ComboBox personal_combobox = null;
        UI_Text maquinaria_text = null;
        UI_ComboBox maquinaria_combobox = null;
        UI_Text adob_text = null;
        UI_ComboBox adob_combobox = null;
        UI_Grid grid = null;
        UI_Button remove_treball_button = null;
        UI_Button modifica_treball_button = null;
        UI_Button accepta_button = null;
        UI_Text unitats_text = null;
        UI_ComboBox unitats_mesura_combobox = null;
        UI_Panel estat_panel = null;
        UI_RadioButton pendent_check = null;
        UI_RadioButton proces_check = null;
        UI_RadioButton acabat_check = null;
        UI_MaskedTextInput unitats_text_input = null;
        UI_CheckBox fertirrigacio_checkbox = null;


        public PropietarisManager propietaris_manager = null;
        public PointsManager point_manager = null;
        public UIManager ui_manager = null;
        public IDManager id_manager = null;
        public ServerManager server_manager = null;

        Principal principal = null;

        List<tblLineasPartesFinca> partes_linea_per_afegir = new List<tblLineasPartesFinca>();
        List<tblLineasPartesFinca> partes_linea_per_eliminar = new List<tblLineasPartesFinca>();
    }
}