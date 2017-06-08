using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class PropietarisManager
    {
        public class Propietari
        {
            Propietari(string nom)
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
            List<Parcela> parceles = new List<Parcela>();
        }

        public void AfegirPropietari(Propietari propietari)
        {
            propietaris.Add(propietari);
        }

        public Propietari GetPropietariPerNom(string nom)
        {
            Propietari ret = null;

            for(int i = 0; i<propietaris.Count; i++)
            {
                if(propietaris[i].GetNom() == nom)
                {
                    ret = propietaris[i];
                    break;
                }
            }

            return ret;
        }

        List<Propietari> propietaris = new List<Propietari>();
    }
}
