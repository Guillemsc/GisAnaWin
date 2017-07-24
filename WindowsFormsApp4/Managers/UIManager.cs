using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApp4
{
    public class UIManager
    {
        public UIManager(Control control)
        {
            _control = control;
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
        public UI_Window(Control control)
        {
            _control = control;
        }

        public void AddElement(UI_Element element)
        {
            _control.Controls.Add(element.GetElement());
            elements.Add(element);
        }

        public void RemoveElement(UI_Element element)
        {
            _control.Controls.Remove(element.GetElement());
        }

        public bool GetEnabled() { return enabled; }

        public bool GetVisible() { return visible; }

        public void SetEnabled(bool set)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].SetEnabled(set);
            }

            enabled = set;
        }

        public void SetVisible(bool set)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].SetVisible(set);
            }

            visible = set;
        }

        public List<UI_Element> elements = new List<UI_Element>();
        Control _control = null;
        bool enabled = true;
        bool visible = true;
    }

    public class UI_Element
    {
        public UI_Element(string type)
        {
            _type = type;
        }

        public void SetElement(Control element)
        {
            _element = element;
        }

        public Control GetElement() { return _element; }

        public void SetVisible(bool set)
        {
            if (_element != null)
                _element.Visible = set;
        }

        public void BringToFront()
        {
            if (_element != null)
                _element.BringToFront();
        }

        public void SendToBack()
        {
            if (_element != null)
                _element.SendToBack();
        }

        public void SetDock(DockStyle dock)
        {
            if (_element != null)
                _element.Dock = dock;
        }

        public void Layer(int layer)
        {
            _element.TabIndex = layer;
        }

        public void SetEnabled(bool set)
        {
            _element.Enabled = set;
        }

        public void Focus()
        {
            _element.Focus();
        }

        public string GetTyp() { return _type; }

        public void OnLostFocus(EventHandler ev)
        {
            _element.LostFocus += ev;
        }

        Control _element = null;
        string _type;
    }

    public class UI_Button : UI_Element
    {
        public UI_Button(Point pos, int w, int h, string text, string name = "") : base("button")
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

        public void SetColor(Color back)
        {
            Button b = GetElement() as Button;
            b.BackColor = back;
        }
    }

    public class UI_Panel : UI_Element
    {
        public UI_Panel(Point pos, int w, int h, string name = "") : base("panel")
        {
            Panel p = new Panel();
            p.Name = name;
            p.Location = pos;
            p.Width = w;
            p.Height = h;
            p.TabIndex = 8;
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
        public UI_Text(Point pos, int w, int h, string text = "", string name = "") : base("text")
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

        public override string ToString()
        {
            Label l = GetElement() as Label;

            return l.Text;
        }

        public void SetText(string text)
        {
            Label l = GetElement() as Label;
            l.Text = text;
        }

        public void SetColor(Color back_color, Color fore_color)
        {
            Label l = GetElement() as Label;
            l.BackColor = back_color;
            l.ForeColor = fore_color;
        }

        public string GetText()
        {
            Label l = GetElement() as Label;
            return l.Text;
        }

        public void SetTextSize(int size)
        {
            Label l = GetElement() as Label;
            l.Font = new Font(l.Font.Name, size);
        }
    }

    public class UI_TextInput : UI_Element
    {
        public UI_TextInput(Point pos, int w, int h, string text = "", string name = "") : base("text_input")
        {
            RichTextBox rt = new RichTextBox();
            rt.Name = name;
            rt.Location = pos;
            rt.Width = w;
            rt.Height = h;
            rt.Text = text;
            rt.AutoSize = true;

            SetElement(rt);
        }

        public string GetText()
        {
            RichTextBox l = GetElement() as RichTextBox;
            return l.Text;
        }

        public void SetText(string text)
        {
            RichTextBox l = GetElement() as RichTextBox;
            l.Text = text;
        }
    }

    public class UI_MaskedTextInput : UI_Element
    {
        public UI_MaskedTextInput(Point pos, int w, int h, string text = "", string name = "") : base("masked_text_input")
        {
            MaskedTextBox rt = new MaskedTextBox();
            rt.Name = name;
            rt.Location = pos;
            rt.Width = w;
            rt.Height = h;
            rt.Text = text;
            rt.AutoSize = true;

            SetElement(rt);
        }

        public string GetText()
        {
            MaskedTextBox l = GetElement() as MaskedTextBox;
            return l.Text;
        }

        public void SetText(string text)
        {
            MaskedTextBox l = GetElement() as MaskedTextBox;
            l.Text = text;
        }

        public void OnTextChanged(EventHandler ev)
        {
            MaskedTextBox l = GetElement() as MaskedTextBox;
            l.TextChanged += ev;
        }
    }

    public class UI_ComboBox : UI_Element
    {
        public UI_ComboBox(Point pos, int w, int h, string name = "") : base("combo")
        {
            ComboBox cb = new ComboBox();
            {
                cb.Name = name;
                cb.Location = pos;
                cb.Width = w;
                cb.Height = h;
                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.Sorted = true;

                SetElement(cb);
            }
        }

        public void SetDrowDownVisibleItems(int set)
        {
            ComboBox cb = GetElement() as ComboBox;
            cb.IntegralHeight = false;
            cb.MaxDropDownItems = set;
        }

        public bool IsSelected()
        {
            ComboBox cb = GetElement() as ComboBox;

            if (cb.SelectedIndex > -1)
                return true;

            return false;
        }

        public object GetSelected()
        {
            object ret = null;

            if (IsSelected())
            {
                ComboBox cb = GetElement() as ComboBox;

                ret = cb.Items[cb.SelectedIndex];
            }

            return ret;
        }

        public void CleanSelection()
        {
            ComboBox cb = GetElement() as ComboBox;
            cb.SelectedIndex = -1;
            cb.SelectedItem = null;
        }

        public void AddElement(object one_object)
        {
            ComboBox cb = GetElement() as ComboBox;
            cb.Items.Add(one_object);
        }

        public void Clear()
        {
            ComboBox cb = GetElement() as ComboBox;
            cb.Items.Clear();
        }

        public void OpenDropDown()
        {
            ComboBox cb = GetElement() as ComboBox;
            cb.DroppedDown = true;
            cb.Focus();
        }

        public void SetSelectedElement(string name)
        {
            ComboBox cb = GetElement() as ComboBox;

            cb.SelectedIndex = cb.FindStringExact(name);
        }

        public void DropDown(EventHandler ev)
        {
            ComboBox cb = GetElement() as ComboBox;
            cb.DropDown += ev;
        }

        public void DropDownClosed(EventHandler ev)
        {
            ComboBox cb = GetElement() as ComboBox;
            cb.DropDownClosed += ev;
        }

        public void ItemSelected(EventHandler ev)
        {
            ComboBox cb = GetElement() as ComboBox;
            cb.SelectionChangeCommitted += ev;
        }
    }

    public class UI_ListBox : UI_Element
    {
        public UI_ListBox(Point pos, int w, int h, string name = "") : base("list_box")
        {
            ListBox lb = new ListBox();
            lb.Name = name;
            lb.Location = pos;
            lb.Width = w;
            lb.Height = h;

            SetElement(lb);
        }

        public void AddElement(UI_Element element)
        {
            ListBox lb = GetElement() as ListBox;

            lb.Items.Add(element.GetElement());
        }

        public void AddText(string text)
        {
            ListBox lb = GetElement() as ListBox;
            lb.Items.Add(text);
        }

        public void DeleteElement(Control element)
        {
            ListBox lb = GetElement() as ListBox;

            lb.Items.Remove(element);
        }

        public void Clear()
        {
            ListBox lb = GetElement() as ListBox;

            lb.Items.Clear();
        }

        public Control GetSelected()
        {
            Control ret = null;

            ListBox lb = GetElement() as ListBox;

            if (lb.SelectedIndex >= 0)
                ret = (Control)lb.SelectedItem;

            return ret;
        }

        public bool IsSelected()
        {
            bool ret = false;

            ListBox lb = GetElement() as ListBox;

            if (lb.SelectedIndex >= 0)
                ret = true;

            return ret;
        }

        public int Count()
        {
            ListBox lb = GetElement() as ListBox;

            return lb.Items.Count;
        }

        public void CleanSelection()
        {
            ListBox lb = GetElement() as ListBox;
            lb.SelectedItem = null;
            lb.SelectedIndex = -1;
        }

        public void ClearSelection()
        {
            ListBox lb = GetElement() as ListBox;
            lb.ClearSelected();
        }

        public void SetAutoSize(bool set)
        {
            ListBox lb = GetElement() as ListBox;
            lb.IntegralHeight = set;
        }
    }

    public class UI_DateSelect : UI_Element
    {
        public UI_DateSelect(Point pos, int w, int h, string name = "") : base("date_select")
        {
            DateTimePicker d = new DateTimePicker();
            d.Name = name;
            d.Location = pos;
            d.Width = w;
            d.Height = h;

            SetElement(d);
        }

        public string GetDateString()
        {
            DateTimePicker d = GetElement() as DateTimePicker;
            return d.Value.Date.ToShortDateString();
        }

        public DateTime GetDate()
        {
            DateTimePicker d = GetElement() as DateTimePicker;
            return d.Value;
        }

        public void SetDate(DateTime date)
        {
            DateTimePicker d = GetElement() as DateTimePicker;
            d.Value = date;
        }
    }

    public class UI_Grid : UI_Element
    {
        public UI_Grid(Point pos, int w, int h, string name = "") : base("data_grid")
        {
            DataGridView d = new DataGridView();
            d.Name = name;
            d.Location = pos;
            d.Width = w;
            d.Height = h;
            d.AllowDrop = false;
            d.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            d.AllowUserToAddRows = false;
            d.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            SetElement(d);
        }

        public void AddColumn(string text, int width)
        {
            DataGridView d = GetElement() as DataGridView;
            d.Columns.Add(text, text);
            d.Columns[d.Columns.Count - 1].Width = width;
        }

        public void AddRow(params object[] text)
        {
            DataGridView d = GetElement() as DataGridView;
            d.Rows.Add(text);
        }

        public void SetAllowDrop(bool set)
        {
            DataGridView d = GetElement() as DataGridView;
            d.AllowDrop = set;
        }

        public void Clear()
        {
            DataGridView d = GetElement() as DataGridView;
            d.Rows.Clear();
        }

        public string[] GetSelectedRow()
        {
            DataGridView d = GetElement() as DataGridView;

            string[] ret = new string[d.ColumnCount];

            if(IsSelected())
            {
                for(int i = 0; i < d.ColumnCount; i++)
                {
                    ret[i] = d.Rows[GetSelectedRowIndex()].Cells[i].Value as string;
                }
            }

            return ret;
        }

        public int GetSelectedRowIndex()
        {
            DataGridView d = GetElement() as DataGridView;
            return d.CurrentCell.RowIndex;
        }

        public void DeleteRow(int index)
        {
            DataGridView d = GetElement() as DataGridView;
            d.Rows.RemoveAt(index);
        }

        public void ModifyRow(int index, params object[] text)
        {
            DataGridView d = GetElement() as DataGridView;

            d.Rows[index].SetValues(text);
        }

        public bool IsSelected()
        {
            bool ret = false;
            DataGridView d = GetElement() as DataGridView;

            if (d.CurrentCell.Selected && d.CurrentCell.RowIndex >= 0 && d.CurrentCell.RowIndex < d.Rows.Count)
            {
                ret = true;
            }

            return ret;
        }

        public void CleanSelection()
        {
            DataGridView d = GetElement() as DataGridView;

            if (d.CurrentRow != null)
                d.CurrentRow.Selected = false;

            if (d.CurrentCell != null)
                d.CurrentCell.Selected = false;

            d.ClearSelection();
        }

        public void SetReadOnlyColumn(int index, bool set)
        {
            DataGridView d = GetElement() as DataGridView;
            d.Columns[index].ReadOnly = set;
        }

        public void SetColumnVisible(int index, bool set)
        {
            DataGridView d = GetElement() as DataGridView;
            d.Columns[index].Visible = set;
        }

        public DataGridViewRowCollection GetRows()
        {
            DataGridView d = GetElement() as DataGridView;

            return d.Rows;
        }
    }
}
