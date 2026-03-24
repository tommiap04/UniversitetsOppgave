using System.Collections.Generic;

namespace UniversitetOppgave.Models
{
    internal class Student : Bruker
    {
        private readonly List<Kurs> kursListe = new List<Kurs>();

        public string StudentID => Id;
        public IReadOnlyList<Kurs> KursListe => kursListe.AsReadOnly();

        public override int MaksAntallLaan => 5;

        public Student(string id, string navn, string epost)
            : base(id, navn, epost)
        {
        }

        public void LeggTilKurs(Kurs kurs)
        {
            if (kurs != null && !kursListe.Contains(kurs))
            {
                kursListe.Add(kurs);
            }
        }

        public void FjernKurs(Kurs kurs)
        {
            if (kurs != null && kursListe.Contains(kurs))
            {
                kursListe.Remove(kurs);
            }
        }

        public override string HentInfo()
        {
            return $"Student: {Navn} ({StudentID})";
        }
    }
}