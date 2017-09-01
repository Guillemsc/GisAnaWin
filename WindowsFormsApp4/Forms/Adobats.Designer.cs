using System;
using System.Drawing;
using System.Collections.Generic;

namespace WindowsFormsApp4.Forms
{
    partial class Adobats
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

        void Carrega(Principal _principal, PropietarisManager _propietaris_manager, PointsManager _points_manager,
            ServerManager _server_manager, UIManager _ui_manager)
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 465);
            this.Name = "Adobs";
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
                grid.AddColumn("Nom", 150, true); grid.AddColumn("Num registre", 150, true); grid.AddColumn("Fórmula", 180, true);
                grid.AddColumn("id", 120, true, false);
                grid.GetElement().Click += new System.EventHandler(this.AdobClick);
                grid.SetFont("Courier New", 8.5f);
                main_window.AddElement(grid);

                elimina_button = new UI_Button(new Point(293, 431), 120, 25, "Elimina adob");
                elimina_button.GetElement().Click += new System.EventHandler(this.Elimina);
                elimina_button.AddImage(principal.imageList1, 3);
                elimina_button.SetFont("Courier New", 8.5f);
                main_window.AddElement(elimina_button);

                actualitza_button = new UI_Button(new Point(143, 431), 140, 25, "Actualitza adob");
                actualitza_button.GetElement().Click += new System.EventHandler(this.Actualitza);
                actualitza_button.AddImage(principal.imageList1, 8);
                actualitza_button.SetFont("Courier New", 8.5f);
                main_window.AddElement(actualitza_button);

                crea_button = new UI_Button(new Point(14, 431), 120, 25, "Crea adob");
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

                nom_text = new UI_Text(new Point(500, 15), 150, 30, "Nom");
                nom_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(nom_text);

                nom_text_input = new UI_MaskedTextInput(new Point(500, 40), 200, 50);
                nom_text_input.SetFont("Courier New", 8.5f);
                main_window.AddElement(nom_text_input);

                num_registre_text = new UI_Text(new Point(750, 15), 150, 30, "Num registre");
                num_registre_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(num_registre_text);

                num_registre_input = new UI_MaskedTextInput(new Point(750, 40), 200, 50);
                num_registre_input.SetFont("Courier New", 8.5f);
                main_window.AddElement(num_registre_input);

                formula_text = new UI_Text(new Point(500, 85), 150, 30, "Fórmula");
                formula_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(formula_text);

                formula_text_input = new UI_MaskedTextInput(new Point(500, 110), 450, 50);
                formula_text_input.SetFont("Courier New", 8.5f);
                main_window.AddElement(formula_text_input);
            }

        }

        UI_Window main_window = null;
        UI_Grid grid = null;

        UI_Text nom_text = null;
        UI_MaskedTextInput nom_text_input = null;
        UI_Text num_registre_text = null;
        UI_MaskedTextInput num_registre_input = null;
        UI_Text formula_text = null;
        UI_MaskedTextInput formula_text_input = null;

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

        List<Adob> adobs_per_afegir = new List<Adob>();
        List<Adob> adobs_per_eliminar = new List<Adob>();
    }
}