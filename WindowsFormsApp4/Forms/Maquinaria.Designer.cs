using System.Drawing;
using System.Collections.Generic;

namespace WindowsFormsApp4.Forms
{
    partial class Maquinaria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Maquinaria));
            this.SuspendLayout();
            // 
            // Maquinaria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Maquinaria";
            this.Text = "Maquinaria";
            this.ResumeLayout(false);

        }


        #endregion

        void Carrega(Principal _principal, PropietarisManager _propietaris_manager, PointsManager _points_manager,
            ServerManager _server_manager, UIManager _ui_manager)
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 465);
            this.Name = "Maquinaria";
            this.Text = this.Name;
            this.ResumeLayout(false);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Load += new System.EventHandler(this.LoadF);

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

        void LoadUI()
        {
            main_window = new UI_Window(this);
            {
                grid = new UI_Grid(new Point(15, 15), 470, 405);
                grid.AddColumn("Tipus", 150, true); grid.AddColumn("Data compra", 150, true);
                grid.AddColumn("ROMA", 120, true); grid.AddColumn("Darrera inspecció", 120, true); grid.AddColumn("id", 120, true, false);
                grid.GetElement().Click += new System.EventHandler(this.MaquinaClick);
                grid.SetFont("Courier New", 8.5f);
                main_window.AddElement(grid);


                elimina_button = new UI_Button(new Point(14, 431), 140, 25, "Elimina màquina");
                elimina_button.GetElement().Click += new System.EventHandler(this.Elimina);
                elimina_button.AddImage(principal.imageList1, 3);
                elimina_button.SetFont("Courier New", 8.5f);
                main_window.AddElement(elimina_button);

                actualitza_button = new UI_Button(new Point(163, 431), 160, 25, "Actualitza màquina");
                actualitza_button.GetElement().Click += new System.EventHandler(this.Actualitza);
                actualitza_button.AddImage(principal.imageList1, 8);
                actualitza_button.SetFont("Courier New", 8.5f);
                main_window.AddElement(actualitza_button);

                crea_button = new UI_Button(new Point(333, 431), 130, 25, "Crea màquina");
                crea_button.GetElement().Click += new System.EventHandler(this.Crea);
                crea_button.AddImage(principal.imageList1, 6);
                crea_button.SetFont("Courier New", 8.5f);
                main_window.AddElement(crea_button);

                accepta_button = new UI_Button(new Point(845, 431), 110, 25, "Desa i surt");
                accepta_button.AddImage(principal.imageList1, 2);
                accepta_button.SetFont("Courier New", 8.5f);
                accepta_button.SetColor(Color.Cornsilk);
                accepta_button.GetElement().Click += new System.EventHandler(this.Accepta);

                main_window.AddElement(accepta_button);

                propietari_text = new UI_Text(new Point(500, 15), 150, 30, "Propietari");
                propietari_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(propietari_text);

                propietari_combo = new UI_ComboBox(new Point(500, 45), 200, 40);
                propietari_combo.SetFont("Courier New", 8.5f);
                main_window.AddElement(propietari_combo);

                tipus_text = new UI_Text(new Point(500, 85), 150, 30, "Tipus màquinaria");
                tipus_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(tipus_text);

                tipus_text_input = new UI_MaskedTextInput(new Point(500, 110), 200, 50);
                tipus_text_input.SetFont("Courier New", 8.5f);
                main_window.AddElement(tipus_text_input);

                data_text = new UI_Text(new Point(750, 85), 150, 30, "Data Compra");
                data_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(data_text);

                data_data = new UI_DateSelect(new Point(750, 110), 200, 50);
                data_data.SetFont("Courier New", 8.5f);
                main_window.AddElement(data_data);


                roma_text = new UI_Text(new Point(500, 155), 150, 30, "Número ROMA");
                roma_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(roma_text);

                roma_text_input = new UI_MaskedTextInput(new Point(500, 180), 200, 50);
                roma_text_input.SetFont("Courier New", 8.5f);
                main_window.AddElement(roma_text_input);

                inspeccio_text = new UI_Text(new Point(750, 155), 150, 30, "Darrera inspecció");
                inspeccio_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(inspeccio_text);

                inspeccio_data = new UI_DateSelect(new Point(750, 180), 200, 50);
                inspeccio_data.SetFont("Courier New", 8.5f);
                main_window.AddElement(inspeccio_data);

                tipus_panel = new UI_Panel(new Point(500, 220), 400, 100);
                tipus_panel.SetFont("Courier New", 8.5f);
                main_window.AddElement(tipus_panel);

                propia_radiobutton = new UI_RadioButton(new Point(0, 0), "Propia");
                propia_radiobutton.SetFont("Courier New", 8.5f);
                tipus_panel.AddElement(propia_radiobutton);

                llogada_radiobutton = new UI_RadioButton(new Point(130, 0), "Llogada");
                llogada_radiobutton.SetFont("Courier New", 8.5f);
                tipus_panel.AddElement(llogada_radiobutton);
            }
        }
            

        UI_Window main_window = null;
        UI_Grid grid = null;

        UI_Text propietari_text = null;
        UI_ComboBox propietari_combo = null;
        UI_Text tipus_text = null;
        UI_MaskedTextInput tipus_text_input = null;
        UI_Text data_text = null;
        UI_DateSelect data_data = null;
        UI_Text roma_text = null;
        UI_MaskedTextInput roma_text_input = null;
        UI_Text inspeccio_text = null;
        UI_DateSelect inspeccio_data = null;

        UI_Panel tipus_panel = null;
        UI_RadioButton propia_radiobutton = null;
        UI_RadioButton llogada_radiobutton = null;

        UI_Button crea_button = null;
        UI_Button actualitza_button = null;
        UI_Button elimina_button = null;

        UI_Button accepta_button = null;

        public PropietarisManager propietaris_manager = null;
        public PointsManager point_manager = null;
        public UIManager ui_manager = null;
        public IDManager id_manager = null;
        public ServerManager server_manager = null;

        Principal principal = null;

        List<Maquina> maquinaria_per_afegir = new List<Maquina>();
        List<Maquina> maquinaria_per_eliminar = new List<Maquina>();
    }
}