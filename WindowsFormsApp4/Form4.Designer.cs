using System.Drawing;
using System.Collections.Generic;

namespace WindowsFormsApp4
{
    partial class Form4
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
        private void InitializeComponent(PropietarisManager _propietaris_manager, PointsManager _points_manager, ServerManager _server_manager, UIManager _ui_manager)
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 510);
            this.Name = "Analítiques";
            this.Text = this.Name;
            this.Load += new System.EventHandler(this.Form4_Load);
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
        #endregion

        public void LoadUI()
        {
            main_window = new UI_Window(this);
            {
                data_text = new UI_Text(new Point(15, 20), 30, 100, "Data:");
                main_window.AddElement(data_text);

                data_dataselect = new UI_DateSelect(new Point(18, 40), 200, 100);
                main_window.AddElement(data_dataselect);

                intensitat_colorant_text = new UI_Text(new Point(15, 80), 200, 30, "Intensitat colorant:");
                main_window.AddElement(intensitat_colorant_text);

                intensitat_colorant_text_input = new UI_MaskedTextInput(new Point(18, 100), 200, 30);
                main_window.AddElement(intensitat_colorant_text_input);

                ph_text = new UI_Text(new Point(255, 80), 255, 30, "pH:");
                main_window.AddElement(ph_text);

                ph_text_input = new UI_MaskedTextInput(new Point(258, 100), 100, 30);
                main_window.AddElement(ph_text_input);

                grau_text = new UI_Text(new Point(15, 140), 255, 30, "Grau alc:");
                main_window.AddElement(grau_text);

                grau_text_input = new UI_MaskedTextInput(new Point(18, 160), 100, 30);
                main_window.AddElement(grau_text_input);

                densitat_text = new UI_Text(new Point(255, 140), 255, 30, "Densitat prod:");
                main_window.AddElement(densitat_text);

                densitat_text_input = new UI_MaskedTextInput(new Point(258, 160), 100, 30);
                main_window.AddElement(densitat_text_input);

                estat_sanitari_text = new UI_Text(new Point(15, 200), 255, 30, "Estat sanitari:");
                main_window.AddElement(estat_sanitari_text);

                estat_sanitari_text_input = new UI_MaskedTextInput(new Point(18, 220), 470, 30);
                main_window.AddElement(estat_sanitari_text_input);

                observacions_text = new UI_Text(new Point(15, 260), 255, 30, "Observacions:");
                main_window.AddElement(observacions_text);

                observacions_text_input = new UI_TextInput(new Point(18, 280), 470, 175);
                main_window.AddElement(observacions_text_input);

                accepta_button = new UI_Button(new Point(388, 465), 100, 30, "Acceptar");
                //accepta_button.GetElement().Click += new System.EventHandler(this.Accepta);
                main_window.AddElement(accepta_button);
            }
        }

        // UI
        UI_Window main_window = null;

        UI_Text data_text = null;
        UI_DateSelect data_dataselect = null;

        UI_Text intensitat_colorant_text = null;
        UI_MaskedTextInput intensitat_colorant_text_input = null;

        UI_Text ph_text = null;
        UI_MaskedTextInput ph_text_input = null;

        UI_Text grau_text = null;
        UI_MaskedTextInput grau_text_input = null;

        UI_Text densitat_text = null;
        UI_MaskedTextInput densitat_text_input = null;

        UI_Text estat_sanitari_text = null;
        UI_MaskedTextInput estat_sanitari_text_input = null;

        UI_Text observacions_text = null;
        UI_TextInput observacions_text_input = null;

        UI_Button accepta_button = null;

        UI_Text propietari_text = null;
        UI_Text propietari_nom_text = null;

        UI_Text finca_text = null;
        UI_Text finca_nom_text = null;

        public PropietarisManager propietaris_manager = null;
        public PointsManager point_manager = null;
        public UIManager ui_manager = null;
        public IDManager id_manager = null;
        public ServerManager server_manager = null;
    }
}