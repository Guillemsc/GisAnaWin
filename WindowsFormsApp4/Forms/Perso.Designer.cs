using System;
using System.Drawing;
using System.Collections.Generic;

namespace WindowsFormsApp4.Forms
{
    partial class Perso
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
            this.SuspendLayout();
            // 
            // Perso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 430);
            this.Name = "Perso";
            this.Text = "Perso";
            this.ResumeLayout(false);

        }

        #endregion

        void Carrega(PropietarisManager _propietaris_manager, PointsManager _points_manager,
            ServerManager _server_manager, UIManager _ui_manager)
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 470);
            this.Name = "Personal";
            this.Text = this.Name;
            this.ResumeLayout(false);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Set Managers ----------------------
            propietaris_manager = _propietaris_manager;
            point_manager = _points_manager;
            server_manager = _server_manager;
            ui_manager = _ui_manager;
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
                grid.AddColumn("Nom", 150, true); grid.AddColumn("NIF", 150, true); grid.AddColumn("Núm aplicador", 70, true);
                grid.AddColumn("Qualificació", 120, true);
                grid.GetElement().Click += new System.EventHandler(this.PersonalClick);
                main_window.AddElement(grid);


                elimina_button = new UI_Button(new Point(14, 431), 100, 30, "Elimina analítica");
                elimina_button.GetElement().Click += new System.EventHandler(this.Elimina);
                main_window.AddElement(elimina_button);

                actualitza_button = new UI_Button(new Point(633, 431), 120, 30, "Actualitza analítica");
                actualitza_button.GetElement().Click += new System.EventHandler(this.Actualitza);
                main_window.AddElement(actualitza_button);

                crea_button = new UI_Button(new Point(503, 431), 120, 30, "Crea analítica");
                crea_button.GetElement().Click += new System.EventHandler(this.Crea);
                main_window.AddElement(crea_button);

                accepta_button = new UI_Button(new Point(880, 431), 100, 30, "Acceptar");
                accepta_button.GetElement().Click += new System.EventHandler(this.Accepta);
                main_window.AddElement(accepta_button);

                nom_text = new UI_Text(new Point(500, 15), 150, 30, "Nom");
                main_window.AddElement(nom_text);

                nom_text_input = new UI_MaskedTextInput(new Point(500, 40), 200, 50);
                main_window.AddElement(nom_text_input);

                nif_text = new UI_Text(new Point(750, 15), 150, 30, "NIF");
                main_window.AddElement(nif_text);

                nif_text_input = new UI_MaskedTextInput(new Point(750, 40), 200, 50);
                main_window.AddElement(nif_text_input);


                num_carnet_text = new UI_Text(new Point(500, 85), 150, 30, "Num aplicador");
                main_window.AddElement(num_carnet_text);

                num_carnet_text_input = new UI_MaskedTextInput(new Point(500, 110), 200, 50);
                main_window.AddElement(num_carnet_text_input);

                qualificacio_text = new UI_Text(new Point(750, 85), 150, 30, "Nivell qualificació");
                main_window.AddElement(qualificacio_text);

                qualificacio_text_input = new UI_MaskedTextInput(new Point(750, 110), 200, 50);
                main_window.AddElement(qualificacio_text_input);

                tipus_panel = new UI_Panel(new Point(500, 150), 400, 100);
                main_window.AddElement(tipus_panel);

                propi_radiobutton = new UI_RadioButton(new Point(0, 0), "Propi");
                tipus_panel.AddElement(propi_radiobutton);

                contractat_radiobutton = new UI_RadioButton(new Point(130, 0), "Contractat");
                tipus_panel.AddElement(contractat_radiobutton);

                serveis_radiobutton = new UI_RadioButton(new Point(260, 0), "Serveis");
                tipus_panel.AddElement(serveis_radiobutton);
            }

        }

        UI_Window main_window = null;
        UI_Grid grid = null;

        UI_Text nom_text = null;
        UI_MaskedTextInput nom_text_input = null;
        UI_Text nif_text = null;
        UI_MaskedTextInput nif_text_input = null;
        UI_Text num_carnet_text = null;
        UI_MaskedTextInput num_carnet_text_input = null;
        UI_Text qualificacio_text = null;
        UI_MaskedTextInput qualificacio_text_input = null;

        UI_Panel tipus_panel = null;
        UI_RadioButton propi_radiobutton = null;
        UI_RadioButton contractat_radiobutton = null;
        UI_RadioButton serveis_radiobutton = null;

        UI_Button crea_button = null;
        UI_Button actualitza_button = null;
        UI_Button elimina_button = null;

        UI_Button accepta_button = null;

        public PropietarisManager propietaris_manager = null;
        public PointsManager point_manager = null;
        public UIManager ui_manager = null;
        public IDManager id_manager = null;
        public ServerManager server_manager = null;

        List<Personal> personal_per_afegir = new List<Personal>();
        List<Personal> personal_per_eliminar = new List<Personal>();
    }
}