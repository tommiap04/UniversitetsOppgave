using UniversitetOppgave.Interface;

namespace UniversitetOppgave.Models
{
    internal abstract class Bruker : ILaaner
    {
        public string Id { get; private set; }
        public string Navn { get; private set; }
        public string Epost { get; private set; }

        public abstract int MaksAntallLaan { get; }

        protected Bruker(string id, string navn, string epost)
        {
            Id = id;
            Navn = navn;
            Epost = epost;
        }

        public bool KanLaane(int aktiveLaan)
        {
            return aktiveLaan < MaksAntallLaan;
        }

        public abstract string HentInfo();
    }
}