using System;

namespace UniversitetOppgave.Models
{
    internal class Utvekslingsstudent : Student
    {
        public string Hjemuniversitet { get; private set; }
        public string Land { get; private set; }
        public DateTime PeriodeFra { get; private set; }
        public DateTime PeriodeTil { get; private set; }

        public override int MaksAntallLaan => 3;

        public Utvekslingsstudent(
            string id,
            string navn,
            string epost,
            string hjemuniversitet,
            string land,
            DateTime periodeFra,
            DateTime periodeTil)
            : base(id, navn, epost)
        {
            Hjemuniversitet = hjemuniversitet;
            Land = land;
            PeriodeFra = periodeFra;
            PeriodeTil = periodeTil;
        }

        public override string HentInfo()
        {
            return $"Utvekslingsstudent: {Navn} ({StudentID}) fra {Hjemuniversitet}, {Land} " +
                   $"({PeriodeFra:dd.MM.yyyy} - {PeriodeTil:dd.MM.yyyy})";
        }
    }
}