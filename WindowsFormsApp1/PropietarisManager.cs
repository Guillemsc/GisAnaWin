using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApp1
{
    public class PropietarisManager
    {
        public List<Propietari> GetPropietaris()
        {
            return propietaris;
        }

        public void AfegirPropietari(Propietari propietari)
        {
            propietaris.Add(propietari);
        }

        public Propietari GetPropietariPerNom(string nom)
        {
            Propietari ret = null;

            for (int i = 0; i < propietaris.Count; i++)
            {
                if (propietaris[i].GetNom() == nom)
                {
                    ret = propietaris[i];
                    break;
                }
            }

            return ret;
        }

        List<Propietari> propietaris = new List<Propietari>();
        public List<Label> propietaris_texts = new List<Label>();

        public Propietari propietari_actual = null;
    }

    public class Propietari
    {
        public Propietari(string nom)
        {
            _nom = nom;
        }

        public string GetNom()
        {
            return _nom;
        }

        public void AfegirParcela(Parcela parcela)
        {
            parceles.Add(parcela);
        }

        public void LoadInfo()
        {
            for(int p = 0; p < parceles.Count(); p++)
            {
                parceles[p].Add();
            }
        }

        public void UnloadInfo()
        {
            for (int p = 0; p < parceles.Count(); p++)
            {
                parceles[p].Remove();
            }
        }

        public void EliminarParcela(Parcela parcela)
        {
            for (int i = 0; i < parceles.Count; i++)
            {
                if (parceles[i] == parcela)
                {
                    parceles.Remove(parcela);
                    break;
                }
            }
        }
        private string _nom;
        public List<Parcela> parceles = new List<Parcela>();

        public Finca parcela_actual = null;
    }
}
