using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using System.Drawing.Printing;
using System.Drawing.Imaging;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {
            InitializeComponent(args);
        }

        // -----------------------------------------------------------------------------
        // Botons ----------------------------------------------------------------------
        // -----------------------------------------------------------------------------

        private void ObreFinestraOpcionsParcela(object sender, EventArgs e)
        {
            MouseEventArgs m = e as MouseEventArgs;

            if(m.Button == MouseButtons.Right)
            {
                //int i = 0;
            }
        }

        // Event que es crida per a actualitzar la llista de propietaris
        private void ActualitzaLlistaPropietariEvent(object sender, EventArgs e)
        {
            ActualitzaLlistaPropietari();
        }

        // Event que es crida quan es selecciona un propietari en la "seleccio_propietari_noms_combobox"
        private void SeleccionaPropietariEvent(object sender, EventArgs e)
        {
            propietaris_manager.propietari_actual = seleccio_propietari_noms_combobox.GetSelected() as Propietari;
            ActualitzaLlistaParceles();
        }

        // Event que es crida per a actualitzar la llista de finques
        private void ActualitzaLlistaFinquesEvent(object sender, EventArgs e)
        {
            ActualitzaLlistaFinques();
        }

        // Event que es crida quan es selecciona una finca en la "seleccio_finca_noms_combobox"
        private void SeleccionaFincaEvent(object sender, EventArgs e)
        {
            propietaris_manager.finca_actual = seleccio_finca_noms_combobox.GetSelected() as Finca;
            ActualitzaLlistaParceles();
        }

        // Event que es crida per a actualitzar la llista de treballs
        private void ActualitzaLlistaTreballsEvent(object sender, EventArgs e)
        {
            ActualitzaLlistaTreballs();
        }

        // Event que es crida quan es selecciona un treball en la "seleccio_treball_noms_combobox"
        private void SeleccionaTreballEvent(object sender, EventArgs e)
        {
            propietaris_manager.treball_actual = seleccio_treball_noms_combobox.GetSelected() as Treball;
            ActualitzaLlistaParceles();
        }

        // Event que es crida per a actualitzar la llista de varietats
        private void ActualitzaLlistaVarietatsEvent(object sender, EventArgs e)
        {
            ActualitzaLlistaVarietats();
        }

        // Event que es crida quan es selecciona una varietat en la "seleccio_varietat_noms_combobox"
        private void SeleccionaVarietatEvent(object sender, EventArgs e)
        {
            propietaris_manager.varietat_actual = seleccio_varietat_noms_combobox.GetSelected() as Varietat;
            ActualitzaLlistaParceles();
        }

        // Event que es crida per a actualitzar la llista de parceles
        private void ActualitzaLlistaParcelesEvent(object sender, EventArgs e)
        {
            ActualitzaLlistaParceles();
        }

        // Neteja els camps de propietari i finca
        private void NetejaPropietariFinca(object sender, EventArgs e)
        {
            propietaris_manager.finca_actual = null;
            propietaris_manager.propietari_actual = null;

            seleccio_propietari_noms_combobox.CleanSelection();
            seleccio_finca_noms_combobox.CleanSelection();

            editor_parceles_panel.SetVisible(false);

            propietaris_manager.can_point = false;

            ActualitzaLlistaParceles();
        }

        // Neteja el camp de varietats
        private void NetejaVarietat(object sender, EventArgs e)
        {
            propietaris_manager.varietat_actual = null;

            seleccio_varietat_noms_combobox.CleanSelection();

            editor_parceles_panel.SetVisible(false);

            propietaris_manager.can_point = false;

            ActualitzaLlistaParceles();
        }

        // Neteja el camp de treball
        private void NetejaTreball(object sender, EventArgs e)
        {
            propietaris_manager.treball_actual = null;

            seleccio_treball_noms_combobox.CleanSelection();

            ActualitzaLlistaParceles();
        }

        // Selecciona la parcela que es clica de la llista de parceles
        public void ParcelaClick(object sender, EventArgs e)
        {
            Label l = null;

            if (llista_parceles_llista.IsSelected())
            {
                l = llista_parceles_llista.GetSelected() as Label;
            }

            if(l != null)
            {
                Parcela parcela = GetParcelaPerParcelaID(l.Name);

                SeleccionaParcelaActual(parcela, true, true);
            }
        }

        // Crea una nova parcela en el mapa
        public void CreaParcela(object sender, EventArgs e)
        {
            List<tblCoordenadesFincaParcela> coor_list = new List<tblCoordenadesFincaParcela>();

            for(int i = 0; i < point_manager.GetTmpMarcadors().Count; i++)
            {
                int parcela_id = propietaris_manager.parcela_actual.GetTbl().idParcela;
                double lat = point_manager.GetTmpMarcadors()[i].GetPos().Lat;
                double lon = point_manager.GetTmpMarcadors()[i].GetPos().Lng;
                string codigo_empresa = propietaris_manager.parcela_actual.GetTbl().CodigoEmpresa;
                int finca_di = propietaris_manager.finca_actual.GetTbl().idFinca;

                tblCoordenadesFincaParcela coor = server_manager.AddCoordenades(parcela_id, lat, lon, codigo_empresa, finca_di, i);
                coor_list.Add(coor);
            }

            Varietat v = GetVarietatPerParcela(propietaris_manager.parcela_actual);

            if (v != null)
            {
                propietaris_manager.parcela_actual.AddCoordenades(coor_list);

                propietaris_manager.parcela_actual.SetColor(v.color, v.color);

                point_manager.NetejaTmpMarcadors();

                editor_parceles_crea_button.SetEnabled(false);

                editor_parceles_elimina_button.SetEnabled(true);

                propietaris_manager.can_point = false;
            }
        }

        // Elimina la parcela seleccionada
        public void EliminaParcela(object sender, EventArgs e)
        {
            propietaris_manager.DeleteParcelaSeleccionada(propietaris_manager.parcela_actual);

            List<tblCoordenadesFincaParcela> coords = propietaris_manager.parcela_actual.GetCoordenades();

            for(int i = 0; i < coords.Count; i++)
                server_manager.DeleteCoordenades(coords[i]);
            
            propietaris_manager.parcela_actual.ClearPoints();
            editor_parceles_elimina_button.SetEnabled(false);

            propietaris_manager.can_point = true;

            // Delete info box
            if (propietaris_manager.curr_list_box != null)
            {
                propietaris_manager.curr_list_box.Hide();
                propietaris_manager.curr_list_box = null;
            }

            ActualitzaLlistaParcelesSeleccionades();
        }

        // Selecciona el parte que es clica
        public void ClickParte(object sender, EventArgs e)
        {
            if (!partes_seleccionats_listbox.IsSelected())
                return;

            Label l = partes_seleccionats_listbox.GetSelected() as Label;

            if (l != null)
            {
                tblPartesFinca lp = GetPartePerParteId(int.Parse(l.Name));

                if (lp != null)
                    propietaris_manager.parte_actual = lp;
            }

            elimina_parte_button.SetEnabled(true);
        }

        // Elimina el parte seleccionat
        public void EliminaParte(object sender, EventArgs e)
        {
            if(propietaris_manager.parte_actual == null)
                return;

            List<tblLineasPartesFinca1> lineas = GetPartesLineaPerParte(propietaris_manager.parte_actual);

            // Neteja finques
            for(int i = 0; i < lineas.Count; i++)
            {
                server_manager.DeleteLineaParteFinca(lineas[i]);
                propietaris_manager.EliminaParteLinea(lineas[i]);
            }

            propietaris_manager.EliminaParte(propietaris_manager.parte_actual);
            server_manager.DeleteParteFinca(propietaris_manager.parte_actual);

            server_manager.SubmitChanges();

            ActualitzaLlistaPartes();
        }

        // Envia els canvis al servidor
        public void GuardaCanvis(object sender, EventArgs e)
        {
            server_manager.SubmitChanges();

            var src = DateTime.Now;
            editor_parceles_ultim_guardat.SetText("Guardat correctament: " + src.Hour + ":" + src.Minute + ":" + src.Second);

            guarda_canvis_win.SetVisible(true);
        }

        // Desactiva la finestra de guarda canvis
        public void ConfirmaGuardaCanvis(object sender, EventArgs e)
        {
            guarda_canvis_win.SetVisible(false);
        }

        // Obre la finestra per a crear un nou parte
        public void ObreFormPartes(object sender, EventArgs e)
        {
            crea_parte_form.ShowDialog();
        }

        // Es crida quan es tanca la finestra per a crear un nou parte
        public void CreaParteTanca(object sender, FormClosedEventArgs e)
        {
            ActualitzaLlistaPartes();
        }

        // Obre la finestra per a modificar un parte
        public void ObreFormInfoPartes(object sender, EventArgs e)
        {
            if (partes_seleccionats_listbox.IsSelected())
                info_parte_form.ShowDialog();
        }

        // Es crida quan es tanca la finestra per a modificar un parte
        public void InfoParteTanca(object sender, FormClosedEventArgs e)
        {
            ActualitzaLlistaPartes();
        }

        // Obre la finestra de les analitiques
        public void ObreFormVisualitzaAnalitiques(object sender, EventArgs e)
        {
            visualitza_analitica_form.ShowDialog();
        }

        // Obre la finestra per a imprimir la imatge del mapa
        // i la informacio sobre partes i analitiques
        public void ImprimirMapa(object sender, EventArgs e)
        {
            this.Enabled = false;

            // Set form position
            Point pos = this.Location;
            Size size = this.Size;

            this.Location = new Point(0, 0);
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            // Capture screen
            Bitmap memoryImage = null;

            Graphics myGraphics = this.CreateGraphics();
            Size s = gmap.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);

            gmap.DrawToBitmap(memoryImage, new Rectangle(new Point(0, 0), new Size(gmap.Width, gmap.Height)));

            string folderName = AppDomain.CurrentDomain.BaseDirectory + "\\";
            string fileName = "image.png";
            memoryImage.Save(folderName + fileName, ImageFormat.Png);

            // Reset form position
            this.Location = pos;
            this.Size = size;

            // Fill report
            List<Parcela> parceles = propietaris_manager.GetParcelesSeleccionades();
            List<Analitica> analitiques = propietaris_manager.GetAnalitiques();

            List<ReportDataParte> info_partes = new List<ReportDataParte>();
            List<ReportDataAnalitica> info_analitiques = new List<ReportDataAnalitica>();

            for (int i = 0; i < parceles.Count; i++)
            {
                Finca f = GetFincaPerParcela(parceles[i]);

                if (f == null)
                    continue;

                List<tblPartesFinca> partes = GetPartesPerFincaId(f.GetTbl().idFinca);

                for(int p = 0; p < partes.Count; p++)
                {
                    List<tblLineasPartesFinca1> linea = GetPartesLineaPerParte(partes[p]);

                    for(int l = 0; l < linea.Count; l++)
                    {
                        if(linea[l].idParcela == parceles[i].GetTbl().idParcela)
                        {
                            Treball treball = GetTreballPerTreballId(linea[l].idFamiliaCoste);

                            if (treball == null)
                                continue;

                            DateTime date = (DateTime)partes[p].Fecha;

                            tblPartesFinca parte = GetPartePerParteId(linea[l].idParte);
                            info_partes.Add(new ReportDataParte(f.GetTbl().Nom1, parceles[i].GetTbl().idParcelaVinicola, parte.Estat, 
                                date.ToShortDateString(), treball.GetTbl().Descripcio, linea[l].Descripcion, 
                                linea[l].Unidades.ToString()));
                        }
                    }
                }

                for(int a = 0; a < analitiques.Count; a++)
                {
                    if(analitiques[a].GetTbl().idParcela == parceles[i].GetTbl().idParcela)
                    {
                        DateTime date = (DateTime)analitiques[a].GetTbl().Fecha;

                        info_analitiques.Add(new ReportDataAnalitica(parceles[i].GetTbl().idParcelaVinicola, date.ToShortDateString(),
                            analitiques[a].GetTbl().IC.ToString(), analitiques[a].GetTbl().EstatSanitari,
                            analitiques[a].GetTbl().ph.ToString(), analitiques[a].GetTbl().grauAlc.ToString(),
                            analitiques[a].GetTbl().DensitatProduccio.ToString(), analitiques[a].GetTbl().Observaciones));
                    }
                }
            }




            report_viewer_form.SetInfo(info_partes, info_analitiques, propietaris_manager.empresa_nom, "file:///" + folderName + fileName, System.DateTime.Today.ToLongDateString());
            report_viewer_form.ShowDialog();
            this.Enabled = true;
        }

        // ---------------------------------------------------------------------- Botons
        // -----------------------------------------------------------------------------


        // -----------------------------------------------------------------------------
        // Servidor --------------------------------------------------------------------
        // -----------------------------------------------------------------------------

        public void ActualitzaPropietarisDesDeServidor()
        {
            List<tblProveedores> proveedors = server_manager.GetProveedors();

            propietaris_manager.EliminaPropietaris();

            for (int i = 0; i < proveedors.Count; i++)
            {
                Propietari p = new Propietari(proveedors[i]);
                propietaris_manager.AfegirPropietari(p);
            }

            Console.WriteLine("Actualitzat Propietaris Des De Servidor");
            Console.WriteLine("----------------------------------");
        }

        public void ActualitzaFinquesDesDeServidor()
        {
            List<tblFinques> finques = server_manager.GetFinques();

            propietaris_manager.EliminaFinques();

            for(int i = 0; i < finques.Count; i++)
            {
                Finca f = new Finca(point_manager.overlay_finca, finques[i]);
                propietaris_manager.AfegirFinca(f);
            }

            Console.WriteLine("Actualitzat Finques Des De Servidor");
            Console.WriteLine("----------------------------------");
        }

        public void ActualitzaParcelesDesDeServidor()
        {
            List<tblParceles> parceles = server_manager.GetParceles();

            propietaris_manager.EliminaParceles();

            for (int i = 0; i < parceles.Count; i++)
            {
                Parcela p = new Parcela(point_manager.overlay_parcela, parceles[i]);
                p.SetText(p.GetTbl().idParcelaVinicola);
                propietaris_manager.AfegirParcela(p);
            }

            Console.WriteLine("Actualitzat Parceles Des De Servidor");
            Console.WriteLine("----------------------------------");

            ActualitzaCoordenadesDesDeServidor(propietaris_manager.GetParceles());
        }

        public void ActualitzaPartesDesDeServidor()
        {
            List<tblPartesFinca> partes = server_manager.GetPartesFinca();

            propietaris_manager.ClearPartes();

            for(int i = 0; i < partes.Count; i++)
            {
                propietaris_manager.AfegirParte(partes[i]);
            }

            Console.WriteLine("Actualitzat Partes Des De Servidor");
            Console.WriteLine("----------------------------------");
        }

        public void ActualitzaPartesLineaDesDeServidor()
        {
            List<tblLineasPartesFinca1> partes_linea = server_manager.GetLineasPartesFinca();

            propietaris_manager.ClearPartesLinea();

            for (int i = 0; i < partes_linea.Count; i++)
            {
                propietaris_manager.AfegirParteLinea(partes_linea[i]);
            }

            Console.WriteLine("Actualitzat Partes Linea Des De Servidor");
            Console.WriteLine("----------------------------------");
        }

        public void ActualitzaCoordenadesDesDeServidor(List<Parcela> parceles)
        {
            List<tblCoordenadesFincaParcela> coordenades = server_manager.GetCoordenades();

            propietaris_manager.EliminaCoordenades();

            for (int i = 0; i < coordenades.Count; i++)
                propietaris_manager.AfegirCoordenada(coordenades[i]);

            for(int z = 0; z < parceles.Count; z++)
            {
                Parcela p_actual = parceles[z];

                List<tblCoordenadesFincaParcela> c = GetCoordenadesPerParcela(p_actual);

                Varietat v = GetVarietatPerParcela(p_actual);

                p_actual.AddCoordenades(c);

                if(v != null)
                    p_actual.SetColor(v.color, v.color);
            }

            Console.WriteLine("Actualitzat Coordenades Des De Servidor");
            Console.WriteLine("----------------------------------");
        }

        public void ActualitzaVarietatsDesDeServidor()
        {
            List<tblTipoUva> varietats = server_manager.GetVarietats();
            List<tblColorProducto> colors = server_manager.GetColorsProducte();

            propietaris_manager.EliminaVarietats();

            for(int i = 0; i < varietats.Count; i++)
            {
                tblTipoUva varietat_actual = varietats[i];
                tblColorProducto color_actual = null;

                for(int y = 0; y < colors.Count; y++)
                {
                    if(colors[y].idColor == varietat_actual.idColor)
                    {
                        color_actual = colors[y];
                        break;
                    }
                }

                Varietat v = new Varietat(varietat_actual, color_actual);
                propietaris_manager.AfegirVarietat(v);
            }

            Console.WriteLine("Actualitzat Varietats Des De Servidor");
            Console.WriteLine("----------------------------------");
        }

        public void ActualitzaTreballsDesDeServidor()
        {
            List<tblFamiliesCost> treballs = server_manager.GetTreballs();

            propietaris_manager.EliminaTreballs();

            for(int i = 0; i < treballs.Count(); i++)
            {
                Treball t = new Treball(treballs[i]);
                propietaris_manager.AfegirTreball(t);
            }

            Console.WriteLine("Actualitzat Treballs Des De Servidor");
            Console.WriteLine("----------------------------------");
        }

        public void ActualitzaAnalitiquesDesDeServidor()
        {
            List<tblAnaliticaFincaParcela> analitiques = server_manager.GetAnalitiques();

            propietaris_manager.EliminaAnalitiques();

            for(int i = 0; i < analitiques.Count; i++)
            {
                Analitica a = new Analitica(analitiques[i]);
                propietaris_manager.AfegirAnalitica(a);
            }

            Console.WriteLine("Actualitzat Analitiques Des De Servidor");
            Console.WriteLine("----------------------------------");
        }

        // -------------------------------------------------------------------- Servidor
        // -----------------------------------------------------------------------------


        // -----------------------------------------------------------------------------
        // Utils -----------------------------------------------------------------------
        // -----------------------------------------------------------------------------

        public string EliminaAccents(string txt)
        {
            byte[] tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(txt);
            string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);
            return asciiStr;
        }

        public Propietari GetPropietariPerFinca(Finca finca)
        {
            Propietari ret = null;

            for (int i = 0; i < propietaris_manager.GetPropietaris().Count; i++)
            {
                string id1 = propietaris_manager.GetPropietaris()[i].GetTbl().idProveedor.Replace(" ", "");
                string id2 = finca.GetTbl().idProveedor.ToString().Replace(" ", "");

                if (id1 == id2)
                {
                    ret = propietaris_manager.GetPropietaris()[i];
                    break;
                }
            }

            return ret;
        }

        public List<Finca> GetFinquesPerPropietari(Propietari prop)
        {
            List<Finca> ret = new List<Finca>();

            for (int i = 0; i < propietaris_manager.GetFinques().Count; i++)
            {
                string id1 = prop.GetTbl().idProveedor.Replace(" ", "");
                string id2 = propietaris_manager.GetFinques()[i].GetTbl().idProveedor.ToString().Replace(" ", "");

                if (id1 == id2)
                    ret.Add(propietaris_manager.GetFinques()[i]);
            }

            return ret;
        }

        public List<Parcela> GetParcelesPerFinca(Finca fin)
        {
            List<Parcela> parceles = propietaris_manager.GetParceles();

            List<Parcela> ret = new List<Parcela>();

            for(int i = 0; i < parceles.Count; i++)
            {
                if(parceles[i].GetTbl().idFinca == fin.GetTbl().idFinca)
                {
                    ret.Add(parceles[i]);
                }
            }

            return ret;
        }

        public List<Parcela> GetParcelesPerPropietari(Propietari p)
        {
            List<Parcela> parceles = propietaris_manager.GetParceles();

            List<Parcela> ret = new List<Parcela>();

            for(int i = 0; i< parceles.Count; i++)
            {
                Finca f = GetFincaPerParcela(parceles[i]);

                if (f != null && f.GetTbl().idProveedor.ToString().Replace(" ", "") == p.GetTbl().idProveedor.Replace(" ", ""))
                {
                    ret.Add(parceles[i]);
                }
            }

            return ret;
        }

        public List<Parcela> GetParcelesPerVarietat(Varietat v)
        {
            List<Parcela> parceles = propietaris_manager.GetParceles();

            List<Parcela> ret = new List<Parcela>();

            for (int i = 0; i < parceles.Count; i++)
            {
                if(parceles[i].GetTbl().idVarietat == v.GetTbl().idTipoUva)
                {
                    ret.Add(parceles[i]);
                }
            }

            return ret;
        }

        public Finca GetFincaPerParcela(Parcela p)
        {
            Finca ret = null;

            List<Finca> finques = propietaris_manager.GetFinques();

            for(int i = 0; i < finques.Count; i++)
            {
                Finca finca_actual = finques[i];

                if(finca_actual.GetTbl().idFinca == p.GetTbl().idFinca)
                {
                    ret = finca_actual;
                    break;
                }
            }

            return ret;
        }

        public Propietari GetPropietariPerParcela(Parcela parcela)
        {
            Propietari ret = null;

            Finca finca = GetFincaPerParcela(parcela);

            ret = GetPropietariPerFinca(finca);

            return ret;
        }

        public bool PropietariTeVarietat(Propietari prop, Varietat var)
        {
            List<Finca> finques = GetFinquesPerPropietari(prop);

            for(int i = 0; i < finques.Count ; i++)
            {
                List<Parcela> parceles = GetParcelesPerFinca(finques[i]);

                for(int y = 0; y < parceles.Count; y++)
                {
                    if(parceles[y].GetTbl().idVarietat == var.GetTbl().idTipoUva)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Varietat GetVarietatPerParcela(Parcela parcela)
        {
            Varietat ret = null;

            List<Varietat> varietats = propietaris_manager.GetVarietats();

            for (int i = 0; i < varietats.Count; i++)
            {
                if (parcela.GetTbl().idVarietat == varietats[i].GetTbl().idTipoUva)
                {
                    ret = varietats[i];
                    break;
                }
            }

            return ret;
        }

        public Parcela GetParcelaPerParcelaID(string id)
        {
            Parcela ret = null;

            List<Parcela> parceles = propietaris_manager.GetParceles();

            for(int i = 0; i < parceles.Count; i++)
            {
                Parcela parcela_actual = parceles[i];
                if (parcela_actual.GetTbl().idParcela.ToString().Replace(" ", "") == id.ToString().Replace(" ", ""))
                {
                    ret = parcela_actual;
                    break;
                }
            }

            return ret;
        }

        public List<tblCoordenadesFincaParcela> GetCoordenadesPerParcela(Parcela pa)
        {
            List<tblCoordenadesFincaParcela> ret = new List<tblCoordenadesFincaParcela>();

            List<tblCoordenadesFincaParcela> coordenades = propietaris_manager.GetCoordenades();

            for(int i = 0; i < coordenades.Count; i++)
            {
                if(coordenades[i].idParcela == pa.GetTbl().idParcela)
                {
                    ret.Add(coordenades[i]);
                }
            }

            return ret;
        }

        public Parcela GetParcelaPolygon(GMapPolygon item)
        {
            Parcela ret = null;

            List<Parcela> parceles = propietaris_manager.GetParceles();

            for (int i = 0; i < parceles.Count; i++)
            {
                if(parceles[i].GetPolygon() == item)
                {
                    ret = parceles[i];
                    break;
                }
            }

            return ret;
        }

        public Finca GetFincaPerId(string id)
        {
            Finca ret = null;

            List<Finca> finques = propietaris_manager.GetFinques();

            for(int i = 0; i<finques.Count; i++)
            {
                if (finques[i].GetTbl().idFinca.ToString().Replace(" ", "") == id.Replace(" ", ""))
                {
                    ret = finques[i];
                }
            }

            return ret;
        }

        public tblPartesFinca GetPartePerParteId(int id)
        {
            List<tblPartesFinca> partes = propietaris_manager.GetPartes();

            for (int i = 0; i < partes.Count; i++)
            {
                if (partes[i].idParte == id)
                    return partes[i];
            }

            return null;
        }

        public List<tblLineasPartesFinca1> GetPartesLineaPerParte(tblPartesFinca parte)
        {
            List<tblLineasPartesFinca1> ret = new List<tblLineasPartesFinca1>();

            List<tblLineasPartesFinca1> partes_linea  = propietaris_manager.GetPartesLinea();

            for (int i = 0; i < partes_linea.Count; i++)
            {
                if (partes_linea[i].idParte == parte.idParte)
                    ret.Add(partes_linea[i]);
            }

            return ret;
        }

        public tblLineasPartesFinca1 GetLineaPartePerLineaID(int id)
        {
            List<tblLineasPartesFinca1> partes_linea = propietaris_manager.GetPartesLinea();

            for (int i = 0; i < partes_linea.Count; i++)
            {
                if (partes_linea[i].idLinea == id)
                    return partes_linea[i];
            }

            return null;
        }

        public List<tblPartesFinca> GetPartesPerFincaId(int id)
        {
            List<tblPartesFinca> ret = new List<tblPartesFinca>();

            List<tblPartesFinca> partes = propietaris_manager.GetPartes();

            for (int i = 0; i < partes.Count; i++)
            {
                if (partes[i].idFinca == id)
                {
                    ret.Add(partes[i]);
                }
            }

            return ret;
        }

        public Treball GetTreballPerTreballId(int id)
        {
            Treball ret = null;

            List<Treball> treballs = propietaris_manager.GetTreballs();

            for (int i = 0; i < treballs.Count; i++)
            {
                if (treballs[i].GetTbl().idCost == id)
                {
                    ret = treballs[i];
                    break;
                }
            }

            return ret;
        }

        public void SetZoomByMapDistances(double x, double y)
        {
            gmap.ZoomAndCenterMarkers(point_manager.overlay_finca.Id);
        }

        // ----------------------------------------------------------------------- Utils
        // -----------------------------------------------------------------------------

        // -----------------------------------------------------------------------------
        // Gmap ------------------------------------------------------------------------
        // -----------------------------------------------------------------------------

        // Click a un marcador per a eliminar-lo
        public void gmap_MarkerClick(GMapMarker item, MouseEventArgs e)
        {
            point_manager.EliminaTmpMarcadorSiEsTroba(item);

            if (point_manager.GetTmpMarcadors().Count >= 3 && !propietaris_manager.parcela_actual.HasPoints())
                editor_parceles_crea_button.SetEnabled(true);

            else
                editor_parceles_crea_button.SetEnabled(false);

            point_manager.deleting_marker = true;
        }

        // Click a la pantalla per crear punts
        private void gmap_MouseClick(object sender, MouseEventArgs e)
        {
            if (!propietaris_manager.can_point || propietaris_manager.parcela_actual == null)
                return;

            if (gmap.IsMouseOverMarker)
                return;

            if (point_manager.deleting_marker)
            {
                point_manager.deleting_marker = false;
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (propietaris_manager.finca_actual != null)
                {
                    double lat = gmap.FromLocalToLatLng(e.X, e.Y).Lat;
                    double lng = gmap.FromLocalToLatLng(e.X, e.Y).Lng;

                    Marcador m = new Marcador(lat, lng, point_manager.overlay_markers);
                    point_manager.AfegeixTmpMarcadors(m);

                    if (point_manager.GetTmpMarcadors().Count >= 3 && !propietaris_manager.parcela_actual.HasPoints())
                        editor_parceles_crea_button.SetEnabled(true);

                    else
                        editor_parceles_crea_button.SetEnabled(false);
                }
            }
        }

        // Es crida quan el mouse passa per damunt una parcela
        private void gmap_PoligonEnter(GMapPolygon item)
        {
            if (propietaris_manager.curr_list_box != null)
            {
                propietaris_manager.curr_list_box.Hide();
                this.Controls.Remove(propietaris_manager.curr_list_box);
                propietaris_manager.curr_list_box = null;
            }

            Parcela p = GetParcelaPolygon(item);

            if (p != null)
            {
                string nom = GetFincaPerParcela(p).GetTbl().Nom1;
                string propietari = GetPropietariPerParcela(p).GetTbl().Nombre;
                string varietat = GetVarietatPerParcela(p).GetTbl().Nombre;
                string vitivin = p.GetTbl().idParcelaVinicola;
                string any_plant = p.GetTbl().AnyPlantacio.ToString();
                string n_plantes = p.GetTbl().NumPlantes.ToString();
                string ha = p.GetTbl().Ha.ToString();
                string poligon = p.GetTbl().Poligon.Replace(" ", "");
                string parcela = p.GetTbl().Parcela.Replace(" ", "");

                GPoint gpos = gmap.FromLatLngToLocal(p.GetCenterPos());
                Point pos = new Point((int)gpos.X, (int)gpos.Y);

                propietaris_manager.curr_list_box = new ListBox();
                propietaris_manager.curr_list_box.Location = pos;
                propietaris_manager.curr_list_box.Width = 190;
                propietaris_manager.curr_list_box.Height = 110;
                propietaris_manager.curr_list_box.Show();

                propietaris_manager.curr_list_box.Items.Add("Finca: " + nom);
                propietaris_manager.curr_list_box.Items.Add("Propietari: " + propietari);
                propietaris_manager.curr_list_box.Items.Add("Varietat: " + varietat);
                propietaris_manager.curr_list_box.Items.Add("Parcela vitivinicola: " + vitivin);
                propietaris_manager.curr_list_box.Items.Add("Any: " + any_plant);
                propietaris_manager.curr_list_box.Items.Add("Nº Plant: " + n_plantes);
                propietaris_manager.curr_list_box.Items.Add("Ha: " + ha);
                propietaris_manager.curr_list_box.Items.Add(poligon + "/" + parcela);

                propietaris_manager.curr_list_box.Enabled = false;

                this.Controls.Add(propietaris_manager.curr_list_box);
                propietaris_manager.curr_list_box.BringToFront();

                propietaris_manager.can_point_back = propietaris_manager.can_point;
                propietaris_manager.can_point = false;
            }

            mouse_over_polygon = true;
        }

        // Es crida quan el mouse passa surt de damunt d'una parcela
        private void gmap_PoligonOut(GMapPolygon item)
        {
            if (propietaris_manager.curr_list_box != null)
            {
                propietaris_manager.curr_list_box.Hide();
                this.Controls.Remove(propietaris_manager.curr_list_box);
                propietaris_manager.curr_list_box = null;

                propietaris_manager.can_point = propietaris_manager.can_point_back;
            }

            mouse_over_polygon = false;
        }

        // Es crida quan es clica una parcela
        private void gmap_PoligonClick(GMapPolygon item, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Parcela par = GetParcelaPolygon(item);

                if (par == null)
                    return;

                SeleccionaParcelaActual(par, false, false);

                propietaris_manager.parcela_actual = par;

                // Seleccio parceles
                if (!propietaris_manager.ParcelesSeleccionadesConte(par))
                {
                    propietaris_manager.AddParcelaSeleccionada(par);
                    par.Highlight();
                }
                else
                {
                    propietaris_manager.DeleteParcelaSeleccionada(par);
                    par.DeHiglight();
                }

                ActualitzaLlistaParcelesSeleccionades();

                ActualitzaLlistaPartes();
            }
        }

        // Es crida quan es fa doble click sobre qualsevol punt del mapa
        private void gmap_DoubleClick(object sender, EventArgs e)
        {
            // Selecciona totes les parceles de una mateixa finca ----
            if (!mouse_over_polygon || propietaris_manager.parcela_actual == null)
                return;

            Finca finca = GetFincaPerParcela(propietaris_manager.parcela_actual);

            if (finca == null)
                return;

            if(propietaris_manager.parcela_actual.IsHighlighted())
                propietaris_manager.parcela_actual.DeHiglight();
            else
                propietaris_manager.parcela_actual.Highlight();

            List<Parcela> parceles = GetParcelesPerFinca(finca);

            int highlighted = 0;
            int all = 0;

            for (int i = 0; i < parceles.Count; i++)
            {
                if (parceles[i].HasPoints())
                {
                    if(parceles[i].IsHighlighted())
                        highlighted++;

                    all++;
                }
            } 
            
            for (int i = 0; i < parceles.Count; i++)
            {
                if (!parceles[i].HasPoints())
                    continue;

                if (highlighted == all && all > 0)
                {
                    propietaris_manager.DeleteParcelaSeleccionada(parceles[i]);
                    parceles[i].DeHiglight();
                }
                else
                {
                    if (!propietaris_manager.ParcelesSeleccionadesConte(parceles[i]))
                        propietaris_manager.AddParcelaSeleccionada(parceles[i]);

                    parceles[i].Draw();
                    parceles[i].Highlight();
                }
            }
               
            ActualitzaLlistaParcelesSeleccionades();

            ActualitzaLlistaPartes();
            // -------------------------------------------
        }

        // Canvia el mapa a satelit o mapa normal
        public void SwitchMapSat(object sender, EventArgs e)
        {         
            if (gmap.MapProvider == GMap.NET.MapProviders.GoogleMapProvider.Instance)
            {
                gmap.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
                mapsat_button.SetText("Canvia a Mapa");
            }
            else
            {
                gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
                mapsat_button.SetText("Canvia a Satel.lit");
            }
        }       

        public void SearchName(object sender, EventArgs e)
        {
            string search = text_input_nom.GetText();

            if (search.Replace(" ", "") != "")
            {
                gmap.SetPositionByKeywords(search);
            }
        }

        // Busca una coordenada amb latitud i longitud
        public void SearchLatLon(object sender, EventArgs e)
        {
            UI_MaskedTextInput lat = text_input_lat;
            UI_MaskedTextInput lon = text_input_lon;

            if (lat == null || lon == null)
                return;

            double num_lat = 0;
            bool is_lat = false;
            double num_lon = 0;
            bool is_lon = false;

            if (double.TryParse(lat.GetText(), System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out num_lat))
            {
                if (num_lat <= 360)
                    is_lat = true;
            }
            if (double.TryParse(lon.GetText(), System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out num_lon))
            {
                if (num_lon <= 360)
                    is_lon = true;
            }

            if (is_lat && is_lon)
                gmap.Position = new PointLatLng(num_lat, num_lon);
        }

        // ------------------------------------------------------------------------ Gmap
        // -----------------------------------------------------------------------------

        // -----------------------------------------------------------------------------
        // Actualitza ------------------------------------------------------------------
        // -----------------------------------------------------------------------------

        // Actualitza la latitud i la longitud, quan es mou la posició en el mapa
        public void UpdateLatLon()
        {
            if (text_input_lat != null && text_input_lon != null)
            {
                string lat_s = Math.Round(gmap.Position.Lat, 6).ToString();
                string lon_s = Math.Round(gmap.Position.Lng, 6).ToString();

                text_input_lat.SetText(lat_s.Replace(',', '.'));
                text_input_lon.SetText(lon_s.Replace(',', '.'));
            }
        }

        // -------------------------

        // Actualitza la llista de propietaris en la UI
        public void ActualitzaLlistaPropietari()
        {
            List<Propietari> proveedors = propietaris_manager.GetPropietaris();

            seleccio_propietari_noms_combobox.CleanSelection();

            seleccio_propietari_noms_combobox.Clear();

            for (int i = 0; i < proveedors.Count; i++)
            {
                seleccio_propietari_noms_combobox.AddElement(proveedors[i]);
            }
        }

        public void ActualitzaLlistaFinques()
        {
            seleccio_finca_noms_combobox.CleanSelection();

            seleccio_finca_noms_combobox.Clear();

            if (propietaris_manager.propietari_actual == null)
            {
                List<Finca> finques = propietaris_manager.GetFinques();

                for (int i = 0; i < finques.Count; i++)
                {
                    seleccio_finca_noms_combobox.AddElement(finques[i]);
                }
            }
            else
            {
                List<Finca> finques = GetFinquesPerPropietari(propietaris_manager.propietari_actual);

                for (int i = 0; i < finques.Count; i++)
                {
                    string id1 = propietaris_manager.propietari_actual.GetTbl().idProveedor.Replace(" ", "");
                    string id2 = finques[i].GetTbl().idProveedor.ToString().Replace(" ", "");

                    if(id1 == id2)
                        seleccio_finca_noms_combobox.AddElement(finques[i]);
                }
            }
        }

        public void ActualitzaLlistaVarietats()
        {
            seleccio_varietat_noms_combobox.Clear();

            List<Varietat> varietats = propietaris_manager.GetVarietats();

            if (propietaris_manager.propietari_actual == null)
            {
                for(int i = 0; i < varietats.Count; i++)
                {
                    seleccio_varietat_noms_combobox.AddElement(varietats[i]);
                }
            }
            else
            {
                for (int i = 0; i < varietats.Count; i++)
                {
                    if(PropietariTeVarietat(propietaris_manager.propietari_actual, varietats[i]))
                    {
                        seleccio_varietat_noms_combobox.AddElement(varietats[i]);
                    }
                }
            }
        }

        public void ActualitzaLlistaTreballs()
        {
            seleccio_treball_noms_combobox.Clear();

            List<Treball> treballs = propietaris_manager.GetTreballs();
            
            for(int i = 0; i < treballs.Count; i++)
            {
                seleccio_treball_noms_combobox.AddElement(treballs[i]);
            }
        }

        public void ActualitzaLlistaParceles()
        {
            this.Enabled = false;

            llista_parceles_llista.Clear();

            List<Parcela> parceles = new List<Parcela>();

            int comença = 0;
            int acaba = 0;

            Propietari propietari_actual = seleccio_propietari_noms_combobox.GetSelected() as Propietari;
            Finca finca_actual = seleccio_finca_noms_combobox.GetSelected() as Finca;
            Varietat varietat_actual = seleccio_varietat_noms_combobox.GetSelected() as Varietat;
            Treball treball_actual = seleccio_treball_noms_combobox.GetSelected() as Treball;

            if (propietari_actual != null || finca_actual != null || varietat_actual != null || treball_actual != null || (int.TryParse(any_comença.GetText(), out comença) && int.TryParse(any_acaba.GetText(), out acaba)))
            {
                for (int i = 0; i < propietaris_manager.GetParceles().Count;)
                {
                    Parcela parcela_actual = propietaris_manager.GetParceles()[i];

                    if (parcela_actual != null)
                        parcela_actual.ClearDraw();

                    // Propietaris
                    if (propietari_actual != null)
                    {
                        Finca f = GetFincaPerParcela(parcela_actual);

                        if (f != null)
                        {
                            if (f.GetTbl().idProveedor.ToString().Replace(" ", "") != propietari_actual.GetTbl().idProveedor.Replace(" ", ""))
                            {
                                ++i;
                                continue;
                            }
                        }
                    }

                    // Finques
                    if (finca_actual != null)
                    {
                        if (parcela_actual.GetTbl().idFinca != finca_actual.GetTbl().idFinca)
                        {
                            ++i;
                            continue;
                        }
                    }

                    // Varietat
                    if (varietat_actual != null)
                    {
                        if (parcela_actual.GetTbl().idVarietat.ToString().ToString().Replace(" ", "") != varietat_actual.GetTbl().idTipoUva.ToString().Replace(" ", ""))
                        {
                            ++i;
                            continue;
                        }
                    }

                    // Treballs
                    if (treball_actual != null)
                    {
                        bool stop = true;

                        List<tblLineasPartesFinca1> partes_l = propietaris_manager.GetPartesLinea();

                        for(int p = 0; p < partes_l.Count; p++)
                        {
                            if (partes_l[p].idParcela != parcela_actual.GetTbl().idParcela)
                            {
                                continue;
                            }

                            if (partes_l[p].idFamiliaCoste == treball_actual.GetTbl().idCost)
                            {
                                stop = false;
                                break;
                            }
                        }

                        if (stop)
                        {
                            ++i;
                            continue;
                        }
                    }

                    comença = 0;
                    acaba = 0;
                    int.TryParse(any_comença.GetText(), out comença);
                    int.TryParse(any_acaba.GetText(), out acaba);
                    if (!(parcela_actual.GetTbl().AnyPlantacio >= comença) || !(parcela_actual.GetTbl().AnyPlantacio <= acaba))
                    {
                        ++i;
                        continue;
                    }
                       

                    // Add
                    parceles.Add(parcela_actual);
                    ++i;
                }
            }
            else
            {
                List<Parcela> pa = propietaris_manager.GetParceles();

                for (int p = 0; p < pa.Count; p++)
                {
                    pa[p].Draw();
                }
            }

            // Print
            int acumulator = 5;
            for (int y = 0; y < parceles.Count; y++)
            {
                Varietat varietat = GetVarietatPerParcela(parceles[y]);

                if (varietat != null)
                {
                    UI_Text t = new UI_Text(new Point(5, acumulator), 100, 30, (y + 1) + ": "+ parceles[y].GetTbl().idParcelaVinicola + ": " + varietat.GetTbl().Nombre + ". Ha: " + parceles[y].GetTbl().Ha.ToString(), parceles[y].GetTbl().idParcela.ToString());

                    llista_parceles_llista.AddElement(t);
                    ListBox l = llista_parceles_llista.GetElement() as ListBox;
                    l.DisplayMember = "Text";

                    if (propietaris_manager.parcela_actual == parceles[y])
                        l.SelectedIndex = l.Items.Count - 1;

                    if (parceles[y] != null)
                        parceles[y].Draw();

                    acumulator += 18;
                }
            }

            if (llista_parceles_llista.Count() == 0 || !llista_parceles_llista.IsSelected())
            {
                editor_parceles_panel.SetVisible(false);
                propietaris_manager.can_point = false;
                llista_parceles_llista.CleanSelection();
            }

            this.Enabled = true;

            gmap.ZoomAndCenterMarkers(point_manager.overlay_parcela.Id);
        }

        public void ActualitzaLlistaParcelesSeleccionades()
        {
            List<Parcela> parceles = propietaris_manager.GetParcelesSeleccionades();

            if(parceles.Count > 0)
            {
                float ha_totals = 0;

                parceles_seleccionades_panel.SetVisible(true);

                parceles_seleccionades_listbox.Clear();
                for(int i = 0; i < parceles.Count; i++)
                {
                    ha_totals += (float)parceles[i].GetTbl().Ha;

                    UI_Text t = new UI_Text(new Point(5, 5), 100, 30, parceles[i].GetTbl().idParcelaVinicola, parceles[i].GetTbl().idParcela.ToString());

                    parceles_seleccionades_listbox.AddElement(t);
                    ListBox l = parceles_seleccionades_listbox.GetElement() as ListBox;
                    l.DisplayMember = "Text";

                }

                ha_valor_parceles_seleccionades_text.SetText(ha_totals.ToString("0.000"));

                afegir_partes_button.SetEnabled(true);

                if (parceles.Count == 1)
                {
                    visualitza_analitiques_button.SetEnabled(true);
                }
                else
                {
                    visualitza_analitiques_button.SetEnabled(false);
                }
            }
            else
            {
                parceles_seleccionades_panel.SetVisible(false);
                afegir_partes_button.SetEnabled(false);
                visualitza_analitiques_button.SetEnabled(false);
            }


        }

        public void ActualitzaLlistaPartes()
        {
            partes_seleccionats_listbox.Clear();

            List<Parcela> parceles = propietaris_manager.GetParcelesSeleccionades();

            List<tblPartesFinca> partes_to_add = new List<tblPartesFinca>();

            for (int i = 0; i < parceles.Count; i++)
            {
                Finca f = GetFincaPerParcela(parceles[i]);

                List<tblPartesFinca> partes = GetPartesPerFincaId(f.GetTbl().idFinca);

                for (int p = 0; p < partes.Count; p++)
                {
                    List<tblLineasPartesFinca1> lineas = GetPartesLineaPerParte(partes[p]);

                    for (int l = 0; l < lineas.Count; l++)
                    {
                        if (lineas[l].idParcela == parceles[i].GetTbl().idParcela)
                        {
                            if (!partes_to_add.Contains(partes[p]))
                                partes_to_add.Add(partes[p]);

                            break;
                        }
                    }
                }
            }

            for (int y = 0; y < partes_to_add.Count; y++)
            {
                Finca f = GetFincaPerId(partes_to_add[y].idFinca.ToString());
                UI_Text t = new UI_Text(new Point(5, 5), 100, 30,f.GetTbl().Nom1 + "  ID: " + partes_to_add[y].idParte.ToString(), partes_to_add[y].idParte.ToString());

                partes_seleccionats_listbox.AddElement(t);
                ListBox l = partes_seleccionats_listbox.GetElement() as ListBox;
                l.DisplayMember = "Text";
            }

            elimina_parte_button.SetEnabled(false);
        }

        public void SeleccionaParcelaActual(Parcela parcela, bool zoom = false, bool center = false)
        {
            if(propietaris_manager.parcela_actual != null && propietaris_manager.parcela_actual.HasPoints())
                propietaris_manager.parcela_actual.DeHighlightMarker();

            propietaris_manager.parcela_actual = parcela;

            if(propietaris_manager.parcela_actual != null && propietaris_manager.parcela_actual.HasPoints())
            propietaris_manager.parcela_actual.HighlightMarker();

            editor_parceles_panel.SetVisible(true);
            if (propietaris_manager.parcela_actual.HasPoints())
            {
                editor_parceles_elimina_button.SetEnabled(true);
                editor_parceles_crea_button.SetEnabled(false);

                if (center)
                    gmap.Position = propietaris_manager.parcela_actual.GetCenterPos();

                if(zoom)
                    gmap.Zoom = 18;

                propietaris_manager.can_point = false;
            }
            else
            {
                editor_parceles_crea_button.SetEnabled(point_manager.GetTmpMarcadors().Count >= 3);

                editor_parceles_elimina_button.SetEnabled(false);
                propietaris_manager.can_point = true;
            }

            Finca finca = GetFincaPerParcela(parcela);
            Propietari propietari = GetPropietariPerFinca(finca);

            if (finca != null && propietari != null)
            {
                propietaris_manager.finca_actual = finca;
                propietaris_manager.propietari_actual = propietari;
            }
        }


        // ------------------------------------------------------------------ Actualitza
        // -----------------------------------------------------------------------------
    }
}
