namespace UniversitetOppgave.Models
{
    internal class Ansatt : Bruker
    {
        public string AnsattID => Id;
        public string Stilling { get; private set; }
        public string Avdeling { get; private set; }

        public override int MaksAntallLaan => 10;

        public Ansatt(string id, string navn, string epost, string stilling, string avdeling)
            : base(id, navn, epost)
        {
            Stilling = stilling;
            Avdeling = avdeling;
        }

        public override string HentInfo()
        {
            return $"Ansatt: {Navn} ({AnsattID}) - {Stilling}, {Avdeling}";
        }
    }
}