using System.Threading.Tasks;
using Vocabulari;

internal class Program
{
    private static void Main(string[] args)
    {
        Element e = new();
        /*e.Afegir("aba");
        e.Afegir("ab");
        e.Afegir("amike");
        Console.WriteLine(e.Existeix("aba"));
        Console.WriteLine(e.Existeix("a"));
        Console.WriteLine(e.Existeix("b"));
        e.Elimina("aba");
        Console.WriteLine(e.Existeix("aba"));
        e.Afegir("aba");
        Console.WriteLine(e.Existeix("aba"));
        List<string> paraules = e.IntelliSense("a");
        foreach (string s in paraules) Console.WriteLine(s);*/
        StreamReader srParaules = new StreamReader("DiccionariFinal.txt");
        string paraula = srParaules.ReadLine();
        while(paraula != null)
        {
            e.Afegir(paraula.ToLower());
            paraula = srParaules.ReadLine();
        }
        List<string> paraules = e.IntelliSense("abacharia");
        foreach (string s in paraules) Console.Write(s + " ");
        Console.WriteLine();
        paraules = e.IntelliSense("abajar");
        foreach (string s in paraules) Console.Write(s + " ");
        Console.WriteLine();
        paraules = e.IntelliSense("espumajea");
        foreach (string s in paraules) Console.Write(s + " ");
    }
}