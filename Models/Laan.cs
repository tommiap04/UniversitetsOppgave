using System;

namespace UniversitetOppgave.Models
{
    internal class Laan
    {
        public Bok Bok { get; private set; }
        public Bruker Bruker { get; private set; }
        public DateTime Laanedato { get; private set; }
        public DateTime? Innleveringsdato { get; private set; }
        public bool Returnert { get; private set; }

        public Laan(Bok bok, Bruker bruker)
        {
            Bok = bok;
            Bruker = bruker;
            Laanedato = DateTime.Now;
            Returnert = false;
        }

        public void Returner()
        {
            if (!Returnert)
            {
                Returnert = true;
                Innleveringsdato = DateTime.Now;
            }
        }

        public string HentInfo()
        {
            string status = Returnert
                ? $"Returnert: {Innleveringsdato:dd.MM.yyyy}"
                : "Aktivt laan";

            return $"{Bok.Tittel} - {Bruker.HentInfo()} - Laant: {Laanedato:dd.MM.yyyy} - {status}";
        }
    }
}