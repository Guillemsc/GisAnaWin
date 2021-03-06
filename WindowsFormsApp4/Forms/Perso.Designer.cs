﻿using System;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Perso));
            this.SuspendLayout();
            // 
            // Perso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 430);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Perso";
            this.Text = "Perso";
            this.ResumeLayout(false);

        }

        #endregion

        void Carrega(Principal _principal, PropietarisManager _propietaris_manager, PointsManager _points_manager,
            ServerManager _server_manager, UIManager _ui_manager)
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 470);
            this.Name = "Personal";
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
                grid.AddColumn("Nom", 150, true); grid.AddColumn("NIF", 150, true); grid.AddColumn("Núm aplicador", 70, true);
                grid.AddColumn("Qualificació", 120, true); grid.AddColumn("id", 120, true, false);
                grid.GetElement().Click += new System.EventHandler(this.PersonalClick);
                grid.SetFont("Courier New", 8.5f);
                main_window.AddElement(grid);


                elimina_button = new UI_Button(new Point(337, 431), 140, 25, "Elimina persona");
                elimina_button.GetElement().Click += new System.EventHandler(this.Elimina);
                elimina_button.AddImage(principal.imageList1, 3);
                elimina_button.SetFont("Courier New", 8.5f);
                main_window.AddElement(elimina_button);

                actualitza_button = new UI_Button(new Point(153, 431), 175, 25, "Actualitza persona");
                actualitza_button.GetElement().Click += new System.EventHandler(this.Actualitza);
                actualitza_button.AddImage(principal.imageList1, 8);
                actualitza_button.SetFont("Courier New", 8.5f);
                main_window.AddElement(actualitza_button);

                crea_button = new UI_Button(new Point(14, 431), 130, 25, "Crea persona");
                crea_button.GetElement().Click += new System.EventHandler(this.Crea);
                crea_button.AddImage(principal.imageList1, 6);
                crea_button.SetFont("Courier New", 8.5f);
                main_window.AddElement(crea_button);

                accepta_button = new UI_Button(new Point(845, 431), 110, 25, "Desa i surt");
                accepta_button.GetElement().Click += new System.EventHandler(this.Accepta);
                accepta_button.AddImage(principal.imageList1, 2);
                accepta_button.SetFont("Courier New", 8.5f);
                accepta_button.SetColor(Color.Cornsilk);
                main_window.AddElement(accepta_button);

                propietari_text = new UI_Text(new Point(500, 15), 150, 30, "Propietari");
                propietari_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(propietari_text);

                propietari_combo = new UI_ComboBox(new Point(500, 45), 200, 40);
                propietari_combo.SetFont("Courier New", 8.5f);
                main_window.AddElement(propietari_combo);

                nom_text = new UI_Text(new Point(500, 85), 150, 30, "Nom");
                nom_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(nom_text);

                nom_text_input = new UI_MaskedTextInput(new Point(500, 110), 200, 50);
                nom_text_input.SetFont("Courier New", 8.5f);
                main_window.AddElement(nom_text_input);

                nif_text = new UI_Text(new Point(750, 85), 150, 30, "NIF");
                nif_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(nif_text);

                nif_text_input = new UI_MaskedTextInput(new Point(750, 110), 200, 50);
                nif_text_input.SetFont("Courier New", 8.5f);
                main_window.AddElement(nif_text_input);


                num_carnet_text = new UI_Text(new Point(500, 155), 150, 30, "Num aplicador");
                num_carnet_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(num_carnet_text);

                num_carnet_text_input = new UI_MaskedTextInput(new Point(500, 180), 200, 50);
                num_carnet_text_input.SetFont("Courier New", 8.5f);
                main_window.AddElement(num_carnet_text_input);

                qualificacio_text = new UI_Text(new Point(750, 155), 150, 30, "Nivell qualificació");
                qualificacio_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(qualificacio_text);

                qualificacio_text_input = new UI_MaskedTextInput(new Point(750, 180), 200, 50);
                qualificacio_text_input.SetFont("Courier New", 8.5f);
                main_window.AddElement(qualificacio_text_input);

                tipus_panel = new UI_Panel(new Point(500, 210), 400, 100);
                main_window.AddElement(tipus_panel);

                propi_radiobutton = new UI_RadioButton(new Point(0, 0), "Propi");
                propi_radiobutton.SetFont("Courier New", 8.5f);
                tipus_panel.AddElement(propi_radiobutton);

                contractat_radiobutton = new UI_RadioButton(new Point(130, 0), "Contractat");
                contractat_radiobutton.SetFont("Courier New", 8.5f);
                tipus_panel.AddElement(contractat_radiobutton);

                serveis_radiobutton = new UI_RadioButton(new Point(260, 0), "Serveis");
                serveis_radiobutton.SetFont("Courier New", 8.5f);
                tipus_panel.AddElement(serveis_radiobutton);
            }

        }

        UI_Window main_window = null;
        UI_Grid grid = null;

        UI_Text propietari_text = null;
        UI_ComboBox propietari_combo = null;
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

        Principal principal = null;

        List<Personal> personal_per_afegir = new List<Personal>();
        List<Personal> personal_per_eliminar = new List<Personal>();
    }
}