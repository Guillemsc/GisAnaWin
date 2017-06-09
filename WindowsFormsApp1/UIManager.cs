using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApp1
{
    public class UIManager
    {
        public UIManager(Control control)
        {
            _control = control;
        }

        public UI_Window GetUIWindow(string name)
        {
            UI_Window ret = null;

            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].GetName() == name)
                {
                    ret = windows[i];
                    break;
                }
            }

            return ret;
        }

        public UI_Element GetElement(string name)
        {
            UI_Element ret = null;

            for (int i = 0; i < windows.Count; i++)
            {
                UI_Window curr_win = windows[i];

                for(int y = 0; y < curr_win.elements.Count(); y++)
                {
                    if(curr_win.elements[y].GetName() == name)
                    {
                        ret = curr_win.elements[y];
                        break;
                    }

                    if(curr_win.elements[y].GetTyp() == "panel")
                    {
                        UI_Panel panel = curr_win.elements[y] as UI_Panel;

                        for(int z = 0; z < panel.elements.Count(); z++)
                        {
                            if (panel.elements[i].GetName() == name)
                                return panel.elements[i];
                        }
                    }
                }
            }

            return ret;
        }

        public void AddUIWindow(UI_Window window)
        {
            windows.Add(window);
        }

        List<UI_Window> windows = new List<UI_Window>();
        Control _control = null;
    }

    public class UI_Window
    {
        public UI_Window(string name, Control control)
        {
            _name = name;
            _control = control;
        }

        public string GetName() { return _name; }

        public void AddElement(UI_Element element)
        {
            _control.Controls.Add(element.GetElement());
            elements.Add(element);
        }

        public void RemoveElement(UI_Element element)
        {
            _control.Controls.Remove(element.GetElement());
        }

        public UI_Element GetElement(string name)
        {
            UI_Element ret = null;

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].GetName() == name)
                {
                    ret = elements[i];
                    break;
                }
            }

            return ret;
        }

        public void SetEnabled(bool set)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].SetEnabled(set);
            }
        }

        string _name;
        public List<UI_Element> elements = new List<UI_Element>();
        Control _control = null;
    }

    public class UI_Element
    {
        public UI_Element(string name, string type)
        {
            _name = name;
            _type = type;
        }

        public void SetElement(Control element)
        {
            _element = element;
        }

        public Control GetElement() { return _element; }

        public void SetEnabled(bool set)
        {
            if (_element != null)
                _element.Visible = set;
        }

        public void BringToFront()
        {
            if (_element != null)
                _element.BringToFront();
        }

        public void Layer(int layer)
        {
            _element.TabIndex = layer;
        }

        public string GetName() { return _name; }

        public string GetTyp() { return _type; }

        string _name;
        Control _element = null;
        string _type;
    }

    public class UI_Button : UI_Element
    {
        public UI_Button(string name, Point pos, int w, int h, string text) : base(name, "button")
        {
            Button b = new Button();
            b.Name = name;
            b.Location = pos;
            b.Width = w;
            b.Height = h;
            b.Text = text;

            SetElement(b);
        }

        public void SetText(string text)
        {
            Button b = GetElement() as Button;
            b.Text = text;
        }
    }

    public class UI_Panel : UI_Element
    {
        public UI_Panel(string name, Point pos, int w, int h) : base(name, "panel")
        {
            Panel p = new Panel();
            p.Name = name;
            p.Location = pos;
            p.Width = w;
            p.Height = h;
            p.TabIndex = 8;
            p.AutoScroll = false;
            p.HorizontalScroll.Enabled = false;
            p.HorizontalScroll.Visible = false;
            p.HorizontalScroll.Maximum = 0;
            p.AutoScroll = true;

            SetElement(p);
        }

        public void SetColor(Color back_color)
        {
            Panel p = GetElement() as Panel;
            p.BackColor = back_color;
        }

        public void AddElement(UI_Element element)
        {
            Panel p = GetElement() as Panel;
            p.Controls.Add(element.GetElement());
            elements.Add(element);
        }

        public void RemoveElement(UI_Element element)
        {
            Panel p = GetElement() as Panel;
            p.Controls.Remove(p);

            elements.Remove(element);
        }

        public void ClearPanel()
        {
            Panel p = GetElement() as Panel;
            p.Controls.Clear();

            elements.Clear();
        }

        public List<UI_Element> elements = new List<UI_Element>();
    }

    public class UI_Text : UI_Element
    {
        public UI_Text(string name, Point pos, int w, int h, string text = "") : base(name, "text")
        {
            Label l = new Label();
            l.Name = name;
            l.Location = pos;
            l.Width = w;
            l.Height = h;
            l.Text = text;
            l.AutoSize = true;

            SetElement(l);
        }

        public void SetText(string text)
        {
            Label l = GetElement() as Label;
            l.Text = text;
        }
    }

    public class UI_TextInput : UI_Element
    {
        public UI_TextInput(string name, Point pos, int w, int h, string text = "") : base(name, "text_input")
        {
            MaskedTextBox mt = new MaskedTextBox();
            mt.Name = name;
            mt.Location = pos;
            mt.Width = w;
            mt.Height = h;
            mt.Text = text;
            mt.AutoSize = true;

            SetElement(mt);
        }

        public string GetText()
        {
            MaskedTextBox l = GetElement() as MaskedTextBox;
            return l.Text;
        }
    }
}
