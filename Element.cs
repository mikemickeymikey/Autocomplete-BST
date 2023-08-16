
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Vocabulari
{
    public class Element
    {
        private bool formaParaula;
        private Dictionary<char, Element> fills;

        public Element()
        {
            Fills = new Dictionary<char, Element>();
        }

        public bool FormaParaula { get => formaParaula; set => formaParaula = value; }
        public Dictionary<char, Element> Fills { get => fills; set => fills = value; }

        public void Afegir(string paraula)
        {
            if (!Fills.ContainsKey(paraula[0])) Fills.Add(paraula[0], new Element());
            else if (paraula.Length == 1) FormaParaula = true;
            if(paraula.Length > 1)AfegirParaula(paraula.Remove(0, 1), Fills[paraula[0]]);
        }

        private void AfegirParaula(string paraula, Element e)
        {
            Element seguent;
            if (e.Fills.ContainsKey(paraula[0])) seguent = e.Fills[paraula[0]];
            else
            {
                seguent = new Element();
                e.Fills.Add(paraula[0], seguent);
            }

            if (paraula.Length == 1) seguent.formaParaula = true;
            else AfegirParaula(paraula.Remove(0, 1), seguent);
        }

        public bool Existeix(string paraula)
        {
            bool existeix = false;
            if (Fills.ContainsKey(paraula[0]))
            {
                if (paraula.Length == 1 && FormaParaula) existeix = true;
                else if(paraula.Length != 1) existeix = ExisteixParaula(paraula.Remove(0, 1), Fills[paraula[0]]);
            }
            return existeix;
        }

        private bool ExisteixParaula(string paraula, Element e)
        {
            bool existeix = false;
            if (e.Fills.ContainsKey(paraula[0]))
            {
                if(paraula.Length != 1) existeix = ExisteixParaula(paraula.Remove(0, 1), e.Fills[paraula[0]]);
            }
            if (e.Fills[paraula[0]].FormaParaula) existeix = true;
            return existeix;
        }

        public void Elimina(string paraula)
        {
            EliminaParaula(this, paraula, 0);
        }

        //Arribem al final de la paraula i fem formaParaula = false
        private void EliminaParaula(Element e, string paraula, int index)
        {
            if (index == paraula.Length)
            {
                if (e.FormaParaula == true && e.Fills.Count == 0)
                {
                    e.FormaParaula = false;
                }
            }
            else EliminaParaula(e.Fills[paraula[index]], paraula, index + 1);
        }

        public List<string> IntelliSense(string paraula)
        {
            return IntelliSense(this, paraula, 0, new List<string>());
        }

        private List<string> IntelliSense(Element e, string paraula, int index, List<string> paraules)
        {
            if (index == paraula.Length)
            {
                Sugerencies(e, paraula, paraules);
            }
            else IntelliSense(e.Fills[paraula[index]], paraula, index + 1, paraules);
            return paraules;
        }

        private void Sugerencies(Element e, string paraula, List<string> paraules)
        {
            if (e.FormaParaula) paraules.Add(paraula);
            foreach(char c in e.Fills.Keys)
            {
                Sugerencies(e.Fills[c], paraula + c, paraules);
            }
        }
    }  
}
