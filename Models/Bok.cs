namespace UniversitetOppgave.Models
{
    internal class Bok
    {
        public string Id { get; private set; }
        public string Tittel { get; private set; }
        public string Forfatter { get; private set; }
        public int Aar { get; private set; }
        public int AntallEksemplarer { get; private set; }

        public Bok(string id, string tittel, string forfatter, int aar, int antallEksemplarer)
        {
            Id = id;
            Tittel = tittel;
            Forfatter = forfatter;
            Aar = aar;
            AntallEksemplarer = antallEksemplarer;
        }

        public string HentInfo(int utlaant)
        {
            int ledig = AntallEksemplarer - utlaant;
            return $"{Id} - {Tittel} av {Forfatter} ({Aar}) | Ledige: {ledig}";
        }
    }
}