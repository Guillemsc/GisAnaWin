﻿using System;
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

        public bool can_point = false;
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

        public void AfegirFinca(Finca finca)
        {
            finques.Add(finca);
        }

        public void LoadInfo()
        {
            for(int p = 0; p < finques.Count(); p++)
            {
     
            }
        }

        public void ClearDraw()
        {
            for (int i = 0; i < finques.Count(); i++)
            {
                finques[i].ClearDraw();
            }
        }

        public void Draw()
        {
            for (int i = 0; i < finques.Count(); i++)
            {
                finques[i].Draw();
            }
        }

        public Finca GetFincaPerNom(string nom)
        {
            Finca ret = null;

            for (int i = 0; i < finques.Count; i++)
            {
                if (finques[i].GetNom() == nom)
                {
                    ret = finques[i];
                    break;
                }
            }

            return ret;
        }

        public Finca GetFincaPerIndex(int index)
        {
            Finca ret = null;

            if(index < finques.Count())
            {
                ret = finques[index];
            }

            return ret;
        }

        public void EliminarFinca(Finca finca)
        {
            for (int i = 0; i < finques.Count; i++)
            {
                if (finques[i] == finca)
                {
                    finques.Remove(finca);
                    break;
                }
            }
        }
        private string _nom;
        public List<Finca> finques = new List<Finca>();
        public Finca finca_actual = null;
    }
}
