using System;

// Classe base Portamonete
class Portamonete
{
    protected int moneteDa1Euro;
    protected int moneteDa2Euro;

    public Portamonete()
    {
        moneteDa1Euro = 0;
        moneteDa2Euro = 0;
    }

    public Portamonete(int numMoneteDa1Euro, int numMoneteDa2Euro)
    {
        moneteDa1Euro = numMoneteDa1Euro;
        moneteDa2Euro = numMoneteDa2Euro;
    }

    public virtual void Inserisci(int valore)
    {
        if (valore == 1)
            moneteDa1Euro++;
        else if (valore == 2)
            moneteDa2Euro++;
        else
            Console.WriteLine("Valore non ammissibile.");
    }

    public virtual void Inserisci(int valore, int numero)
    {
        if (valore == 1)
            moneteDa1Euro += numero;
        else if (valore == 2)
            moneteDa2Euro += numero;
        else
            Console.WriteLine("Valore non ammissibile.");
    }

    public int Denaro()
    {
        return moneteDa1Euro + (moneteDa2Euro * 2);
    }

    public virtual void DenaroPerTipo()
    {
        Console.WriteLine($"Monete da 1 euro: {moneteDa1Euro}");
        Console.WriteLine($"Monete da 2 euro: {moneteDa2Euro}");
    }
}

// Classe derivata Portafoglio
class Portafoglio : Portamonete
{
    protected int banconoteDa5Euro;
    protected int banconoteDa10Euro;
    protected int banconoteDa20Euro;

    public Portafoglio() : base()
    {
        banconoteDa5Euro = 0;
        banconoteDa10Euro = 0;
        banconoteDa20Euro = 0;
    }

    public Portafoglio(int numMoneteDa1Euro, int numMoneteDa2Euro, int numBanconoteDa5Euro, int numBanconoteDa10Euro, int numBanconoteDa20Euro)
        : base(numMoneteDa1Euro, numMoneteDa2Euro)
    {
        banconoteDa5Euro = numBanconoteDa5Euro;
        banconoteDa10Euro = numBanconoteDa10Euro;
        banconoteDa20Euro = numBanconoteDa20Euro;
    }

    public override void Inserisci(int valore)
    {
        if (valore == 1 || valore == 2)
            base.Inserisci(valore);
        else
        {
            this.Inserisci(valore, 1);
        }
    }

    public override void Inserisci(int valore, int numero)
    {
        if (valore == 1 || valore == 2)
            base.Inserisci(valore, numero);
        else
        {
            switch (valore)
            {
                case 5:
                    banconoteDa5Euro += numero;
                    break;
                case 10:
                    banconoteDa10Euro += numero;
                    break;
                case 20:
                    banconoteDa20Euro += numero;
                    break;
                default:
                    Console.WriteLine("Valore non ammissibile.");
                    break;
            }
        }
    }

    public override void DenaroPerTipo()
    {
        base.DenaroPerTipo();
        Console.WriteLine($"Banconote da 5 euro: {banconoteDa5Euro}");
        Console.WriteLine($"Banconote da 10 euro: {banconoteDa10Euro}");
        Console.WriteLine($"Banconote da 20 euro: {banconoteDa20Euro}");
    }

    public new int Denaro()
    {
        return base.Denaro() + (banconoteDa5Euro * 5) + (banconoteDa10Euro * 10) + (banconoteDa20Euro * 20);
    }

    public void Preleva(int euro)
    {
        int totaleDenaro = Denaro();
        if (euro > totaleDenaro)
        {
            Console.WriteLine("Non hai abbastanza denaro nel portafoglio.");
            return;
        }

        int resto = euro;

        // Preleva banconote
        int banconoteDa20DaPrelevare = Math.Min(resto / 20, banconoteDa20Euro);
        resto -= banconoteDa20DaPrelevare * 20;
        banconoteDa20Euro -= banconoteDa20DaPrelevare;

        int banconoteDa10DaPrelevare = Math.Min(resto / 10, banconoteDa10Euro);
        resto -= banconoteDa10DaPrelevare * 10;
        banconoteDa10Euro -= banconoteDa10DaPrelevare;

        int banconoteDa5DaPrelevare = Math.Min(resto / 5, banconoteDa5Euro);
        resto -= banconoteDa5DaPrelevare * 5;
        banconoteDa5Euro -= banconoteDa5DaPrelevare;

        // Preleva monete
        int moneteDa2DaPrelevare = Math.Min(resto / 2, moneteDa2Euro);
        resto -= moneteDa2DaPrelevare * 2;
        moneteDa2Euro -= moneteDa2DaPrelevare;

        int moneteDa1DaPrelevare = Math.Min(resto, moneteDa1Euro);
        moneteDa1Euro -= moneteDa1DaPrelevare;

        Console.WriteLine($"Hai prelevato {euro} euro dal portafoglio.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Test
        Portafoglio portafoglio = new Portafoglio(5, 3, 0, 2, 1);
        portafoglio.DenaroPerTipo();
        portafoglio.Inserisci(5);
        portafoglio.Inserisci(2);
        Console.WriteLine("Denaro totale: " + portafoglio.Denaro());

        portafoglio.Preleva(43);
        portafoglio.DenaroPerTipo();
        Console.WriteLine("Denaro totale: " + portafoglio.Denaro());
    }
}
