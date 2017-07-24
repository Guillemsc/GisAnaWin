using System.Drawing;
using System.Collections.Generic;

namespace WindowsFormsApp4
{
    partial class Form2
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
            this.SuspendLayout();
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 389);
            this.Name = "Afegir Partes";
            this.Text = this.Name;
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            #endregion

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

        public void LoadUI()
        {
            main_window = new UI_Window(this);
            {
                treballs_text = new UI_Text(new Point(20, 10), 30, 100, "Treball:");
                main_window.AddElement(treballs_text);

                treballs_combobox = new UI_ComboBox(new Point(25, 30), 180, 40);
                main_window.AddElement(treballs_combobox);

                data_text = new UI_Text(new Point(240, 10), 30, 100, "Data:");
                main_window.AddElement(data_text);

                data_dataselect = new UI_DateSelect(new Point(242, 30), 200, 100);
                main_window.AddElement(data_dataselect);

                descripcio_text = new UI_Text(new Point(20, 70), 100, 30, "Descripció:");
                main_window.AddElement(descripcio_text);

                descripcio_text_input = new UI_TextInput(new Point(22, 90), 421, 70);
                main_window.AddElement(descripcio_text_input);

                add_treball_button = new UI_Button(new Point(463, 90), 30, 30, "+");
                add_treball_button.GetElement().Click += new System.EventHandler(this.AfegeigParte);
                main_window.AddElement(add_treball_button);

                remove_treball_button = new UI_Button(new Point(463, 130), 30, 30, "-");
                remove_treball_button.GetElement().Click += new System.EventHandler(this.EliminaParte);
                main_window.AddElement(remove_treball_button);

                grid = new UI_Grid(new Point(22, 180), 470, 150);
                grid.AddColumn("Treball", 50); grid.AddColumn("Descripció", 300); grid.AddColumn("Unitats", 70); grid.AddColumn("tblLinea", 0);
                grid.SetReadOnlyColumn(0, true);
                grid.SetColumnVisible(3, false);
                main_window.AddElement(grid);

                accepta_button = new UI_Button(new Point(393, 345), 100, 30, "Acceptar");
                accepta_button.GetElement().Click += new System.EventHandler(this.Accepta);
                main_window.AddElement(accepta_button);

            }
        }

        // UI
        UI_Window main_window = null;

        UI_Text treballs_text = null;
        UI_ComboBox treballs_combobox = null;
        UI_Text data_text = null;
        UI_DateSelect data_dataselect = null;
        UI_Text descripcio_text = null;
        UI_TextInput descripcio_text_input = null;
        UI_Grid grid = null;
        UI_Button add_treball_button = null;
        UI_Button remove_treball_button = null;
        UI_Button accepta_button = null;

        public PropietarisManager propietaris_manager = null;
        public PointsManager point_manager = null;
        public UIManager ui_manager = null;
        public IDManager id_manager = null;
        public ServerManager server_manager = null;

        GMap.NET.WindowsForms.GMapControl gmap = null;
    }
}