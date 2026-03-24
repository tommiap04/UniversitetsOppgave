using System.Collections.Generic;

namespace UniversitetOppgave.Models
{
    internal class Kurs
    {
        private readonly List<Student> deltakere = new List<Student>();

        public string Kode { get; private set; }
        public string Navn { get; private set; }
        public int Studiepoeng { get; private set; }
        public int MaksAntallPlasser { get; private set; }
        public IReadOnlyList<Student> Deltakere => deltakere.AsReadOnly();

        public Kurs(string kode, string navn, int studiepoeng, int maksAntallPlasser)
        {
            Kode = kode;
            Navn = navn;
            Studiepoeng = studiepoeng;
            MaksAntallPlasser = maksAntallPlasser;
        }

        public bool MeldPaStudent(Student student)
        {
            if (student == null)
            {
                return false;
            }

            if (deltakere.Contains(student))
            {
                return false;
            }

            if (deltakere.Count >= MaksAntallPlasser)
            {
                return false;
            }

            deltakere.Add(student);
            return true;
        }

        public bool MeldAvStudent(Student student)
        {
            if (student == null)
            {
                return false;
            }

            if (!deltakere.Contains(student))
            {
                return false;
            }

            deltakere.Remove(student);
            return true;
        }

        public string HentInfo()
        {
            return $"{Kode} - {Navn} - {Studiepoeng} studiepoeng - Plasser: {deltakere.Count}/{MaksAntallPlasser}";
        }
    }
}