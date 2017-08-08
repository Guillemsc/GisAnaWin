using System.Drawing;

namespace WindowsFormsApp4.Forms
{
    partial class ConfiguraQuadernCamp
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
        private void InitializeComponent(PropietarisManager _propietaris_manager, PointsManager _points_manager, 
            ServerManager _server_manager, UIManager _ui_manager, Forms.QuadernCamp q_form)
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Imprimir";
            this.Text = this.Name;
            this.Load += new System.EventHandler(this.Form_Load);
            this.ClientSize = new System.Drawing.Size(230, 120);

            // Set Managers ----------------------
            propietaris_manager = _propietaris_manager;
            point_manager = _points_manager;
            server_manager = _server_manager;
            ui_manager = _ui_manager;

            quadern_form = q_form;
            // -----------------------------------

            // UI --------------------------------
            LoadUI();
            // -----------------------------------
        }

        public void LoadUI()
        {
            main_window = new UI_Window(this);
            {
                propietari_text = new UI_Text(new System.Drawing.Point(10, 10), 100, 30, "Selecciona propietari:");
                main_window.AddElement(propietari_text);

                propietaris_combobox = new UI_ComboBox(new System.Drawing.Point(12, 35), 200, 50);
                main_window.AddElement(propietaris_combobox);

                accepta_button = new UI_Button(new System.Drawing.Point(10, 70), 60, 30, "Accepta");
                accepta_button.GetElement().Click += new System.EventHandler(this.Accepta);
                main_window.AddElement(accepta_button);
            }
        }

        #endregion

        // UI
        UI_Window main_window = null;

        UI_ComboBox propietaris_combobox = null;
        UI_Text propietari_text = null;
        UI_Button accepta_button = null;

        public PropietarisManager propietaris_manager = null;
        public PointsManager point_manager = null;
        public UIManager ui_manager = null;
        public IDManager id_manager = null;
        public ServerManager server_manager = null;

        public Forms.QuadernCamp quadern_form = null;
    }

}