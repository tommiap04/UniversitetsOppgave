using System;
using System.Collections.Generic;
using System.Linq;
using UniversitetOppgave.Models;

namespace UniversitetOppgave
{
    internal class Program
    {
        private static List<Student> studenter = [];
        private static List<Ansatt> ansatte = [];
        private static List<Kurs> kursListe = [];
        private static List<Bok> boeker = [];
        private static List<Laan> laanListe = [];

        static void Main()
        {
            LeggTilTestdata();

            bool kjor = true;

            while (kjor)
            {
                Console.WriteLine("\n--- UNIVERSITETSSYSTEM ---");
                Console.WriteLine("[1] Opprett kurs");
                Console.WriteLine("[2] Meld student til kurs");
                Console.WriteLine("[3] Print kurs og deltagere");
                Console.WriteLine("[4] Søk på kurs");
                Console.WriteLine("[5] Søk på bok");
                Console.WriteLine("[6] Lån bok");
                Console.WriteLine("[7] Returner bok");
                Console.WriteLine("[8] Registrer bok");
                Console.WriteLine("[9] Meld student av kurs");
                Console.WriteLine("[10] Vis aktive lån");
                Console.WriteLine("[11] Vis lånehistorikk");
                Console.WriteLine("[0] Avslutt");
                Console.Write("Velg et alternativ: ");

                string valg = Console.ReadLine();

                switch (valg)
                {
                    case "1":
                        OpprettKurs();
                        break;
                    case "2":
                        MeldStudentTilKurs();
                        break;
                    case "3":
                        PrintKursOgDeltagere();
                        break;
                    case "4":
                        SoekPaaKurs();
                        break;
                    case "5":
                        SoekPaaBok();
                        break;
                    case "6":
                        LaanBok();
                        break;
                    case "7":
                        ReturnerBok();
                        break;
                    case "8":
                        RegistrerBok();
                        break;
                    case "9":
                        MeldStudentAvKurs();
                        break;
                    case "10":
                        VisAktiveLaan();
                        break;
                    case "11":
                        VisLaanehistorikk();
                        break;
                    case "0":
                        kjor = false;
                        Console.WriteLine("Programmet avsluttes.");
                        break;
                    default:
                        Console.WriteLine("Ugyldig valg.");
                        break;
                }
            }
        }

        private static void LeggTilTestdata()
        {
            studenter.Add(new Student("S1", "Karl Johansen", "karl@epost.no"));
            studenter.Add(new Student("S2", "Mikkel Rev", "mikkel@epost.no"));
            studenter.Add(new Utvekslingsstudent(
                "S3",
                "Viljam Muurinen",
                "viljam@epost.no",
                "Helsinki University",
                "Finland",
                new DateTime(2026, 1, 5),
                new DateTime(2026, 6, 1)
            ));

            ansatte.Add(new Ansatt("A1", "Anders Andersen", "anders@uni.no", "Foreleser", "Markedsføring"));
            ansatte.Add(new Ansatt("A2", "Lise Nilsen", "lise@uni.no", "Bibliotekar", "Bibliotek"));

            kursListe.Add(new Kurs("CS1010", "Økonomi", 10, 2));
            kursListe.Add(new Kurs("VS9950", "Verdiskaping", 10, 3));

            boeker.Add(new Bok("B1", "Økonomi 101", "Kjell Aakesson", 2014, 2));
            boeker.Add(new Bok("B2", "Fra ingenting til noe", "Christin Berg", 2022, 1));
        }

        private static void OpprettKurs()
        {
            Console.Write("Kode: ");
            string kode = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(kode))
            {
                Console.WriteLine("Kurskode kan ikke være tom.");
                return;
            }

            if (kursListe.Any(k => k.Kode.Equals(kode, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Kurskode finnes allerede.");
                return;
            }

            Console.Write("Navn: ");
            string navn = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(navn))
            {
                Console.WriteLine("Kursnavn kan ikke være tomt.");
                return;
            }

            Console.Write("Studiepoeng: ");
            if (!int.TryParse(Console.ReadLine(), out int studiepoeng) || studiepoeng <= 0)
            {
                Console.WriteLine("Ugyldig studiepoeng.");
                return;
            }

            Console.Write("Maks antall plasser: ");
            if (!int.TryParse(Console.ReadLine(), out int maksAntallPlasser) || maksAntallPlasser <= 0)
            {
                Console.WriteLine("Ugyldig antall plasser.");
                return;
            }

            kursListe.Add(new Kurs(kode, navn, studiepoeng, maksAntallPlasser));
            Console.WriteLine("Kurs opprettet.");
        }

        private static void MeldStudentTilKurs()
        {
            Console.Write("StudentID: ");
            string studentId = Console.ReadLine();

            Console.Write("Kurskode: ");
            string kursKode = Console.ReadLine();

            Student student = studenter.FirstOrDefault(s =>
                s.StudentID.Equals(studentId, StringComparison.OrdinalIgnoreCase));

            Kurs kurs = kursListe.FirstOrDefault(k =>
                k.Kode.Equals(kursKode, StringComparison.OrdinalIgnoreCase));

            if (student == null || kurs == null)
            {
                Console.WriteLine("Fant ikke student eller kurs.");
                return;
            }

            bool meldtPaa = kurs.MeldPaStudent(student);

            if (!meldtPaa)
            {
                Console.WriteLine("Kunne ikke melde på student. Studenten er kanskje allerede meldt på, eller kurset er fullt.");
                return;
            }

            student.LeggTilKurs(kurs);
            Console.WriteLine("Student meldt på kurs.");
        }

        private static void MeldStudentAvKurs()
        {
            Console.Write("StudentID: ");
            string studentId = Console.ReadLine();

            Console.Write("Kurskode: ");
            string kursKode = Console.ReadLine();

            Student student = studenter.FirstOrDefault(s =>
                s.StudentID.Equals(studentId, StringComparison.OrdinalIgnoreCase));

            Kurs kurs = kursListe.FirstOrDefault(k =>
                k.Kode.Equals(kursKode, StringComparison.OrdinalIgnoreCase));

            if (student == null || kurs == null)
            {
                Console.WriteLine("Fant ikke student eller kurs.");
                return;
            }

            bool meldtAv = kurs.MeldAvStudent(student);

            if (!meldtAv)
            {
                Console.WriteLine("Studenten er ikke meldt på dette kurset.");
                return;
            }

            student.FjernKurs(kurs);
            Console.WriteLine("Student meldt av kurs.");
        }

        private static void PrintKursOgDeltagere()
        {
            if (kursListe.Count == 0)
            {
                Console.WriteLine("Ingen kurs registrert.");
                return;
            }

            foreach (Kurs kurs in kursListe)
            {
                Console.WriteLine("\n" + kurs.HentInfo());

                if (kurs.Deltakere.Count == 0)
                {
                    Console.WriteLine("Ingen deltagere.");
                }
                else
                {
                    foreach (Student student in kurs.Deltakere)
                    {
                        Console.WriteLine("- " + student.HentInfo());
                    }
                }
            }
        }

        private static void SoekPaaKurs()
        {
            Console.Write("Skriv inn kode eller navn: ");
            string soek = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(soek))
            {
                Console.WriteLine("Søket kan ikke vaere tomt.");
                return;
            }

            string soeketekst = soek.ToLower();

            List<Kurs> resultater = kursListe
                .Where(k => k.Kode.ToLower().Contains(soeketekst) || k.Navn.ToLower().Contains(soeketekst))
                .ToList();

            if (resultater.Count == 0)
            {
                Console.WriteLine("Ingen kurs funnet.");
                return;
            }

            Console.WriteLine("\nSøkeresultater:");
            foreach (Kurs kurs in resultater)
            {
                Console.WriteLine(kurs.HentInfo());
            }
        }

        private static void RegistrerBok()
        {
            Console.Write("ID: ");
            string id = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Bok-ID kan ikke være tom.");
                return;
            }

            if (boeker.Any(b => b.Id.Equals(id, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Bok-ID finnes allerede.");
                return;
            }

            Console.Write("Tittel: ");
            string tittel = Console.ReadLine();

            Console.Write("Forfatter: ");
            string forfatter = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(tittel) || string.IsNullOrWhiteSpace(forfatter))
            {
                Console.WriteLine("Tittel og forfatter må fylles ut.");
                return;
            }

            Console.Write("År: ");
            if (!int.TryParse(Console.ReadLine(), out int aar) || aar <= 0)
            {
                Console.WriteLine("Ugyldig år.");
                return;
            }

            Console.Write("Antall eksemplarer: ");
            if (!int.TryParse(Console.ReadLine(), out int antallEksemplarer) || antallEksemplarer <= 0)
            {
                Console.WriteLine("Ugyldig antall eksemplarer.");
                return;
            }

            boeker.Add(new Bok(id, tittel, forfatter, aar, antallEksemplarer));
            Console.WriteLine("Bok registrert.");
        }

        private static void SoekPaaBok()
        {
            Console.Write("Skriv inn tittel eller forfatter: ");
            string soek = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(soek))
            {
                Console.WriteLine("Søket kan ikke være tomt.");
                return;
            }

            string soeketekst = soek.ToLower();

            List<Bok> resultater = boeker
                .Where(b => b.Tittel.ToLower().Contains(soeketekst) || b.Forfatter.ToLower().Contains(soeketekst))
                .ToList();

            if (resultater.Count == 0)
            {
                Console.WriteLine("Ingen bøker funnet.");
                return;
            }

            Console.WriteLine("\nSøkeresultater:");
            foreach (Bok bok in resultater)
            {
                int utlaant = laanListe.Count(l => l.Bok.Id == bok.Id && !l.Returnert);
                Console.WriteLine(bok.HentInfo(utlaant));
            }
        }

        private static void LaanBok()
        {
            Console.Write("Bok-ID: ");
            string bokId = Console.ReadLine();

            Console.Write("Bruker-ID: ");
            string brukerId = Console.ReadLine();

            Bok bok = boeker.FirstOrDefault(b =>
                b.Id.Equals(bokId, StringComparison.OrdinalIgnoreCase));

            Bruker bruker = studenter.Cast<Bruker>()
                .Concat(ansatte)
                .FirstOrDefault(b => b.Id.Equals(brukerId, StringComparison.OrdinalIgnoreCase));

            if (bok == null)
            {
                Console.WriteLine("Fant ikke bok.");
                return;
            }

            if (bruker == null)
            {
                Console.WriteLine("Fant ikke bruker.");
                return;
            }

            int utlaantAvBok = laanListe.Count(l => l.Bok.Id == bok.Id && !l.Returnert);

            if (utlaantAvBok >= bok.AntallEksemplarer)
            {
                Console.WriteLine("Ingen eksemplarer tilgjengelig.");
                return;
            }

            int aktiveLaanForBruker = laanListe.Count(l => l.Bruker.Id == bruker.Id && !l.Returnert);

            if (!bruker.KanLaane(aktiveLaanForBruker))
            {
                Console.WriteLine($"{bruker.HentInfo()} har nådd maks antall lånt ({bruker.MaksAntallLaan}).");
                return;
            }

            laanListe.Add(new Laan(bok, bruker));
            Console.WriteLine("Bok låt ut til: " + bruker.HentInfo());
        }

        private static void ReturnerBok()
        {
            Console.Write("Bok-ID: ");
            string bokId = Console.ReadLine();

            Console.Write("Bruker-ID: ");
            string brukerId = Console.ReadLine();

            Laan laan = laanListe.FirstOrDefault(l =>
                l.Bok.Id.Equals(bokId, StringComparison.OrdinalIgnoreCase) &&
                l.Bruker.Id.Equals(brukerId, StringComparison.OrdinalIgnoreCase) &&
                !l.Returnert);

            if (laan == null)
            {
                Console.WriteLine("Fant ikke aktivt lån.");
                return;
            }

            laan.Returner();
            Console.WriteLine("Bok returnert.");
        }

        private static void VisAktiveLaan()
        {
            List<Laan> aktiveLaan = laanListe.Where(l => !l.Returnert).ToList();

            if (aktiveLaan.Count == 0)
            {
                Console.WriteLine("Ingen aktive lån.");
                return;
            }

            Console.WriteLine("\nAktive lån:");
            foreach (Laan laan in aktiveLaan)
            {
                Console.WriteLine(laan.HentInfo());
            }
        }

        private static void VisLaanehistorikk()
        {
            if (laanListe.Count == 0)
            {
                Console.WriteLine("Ingen lån registrert.");
                return;
            }

            Console.WriteLine("\nLånehistorikk:");
            foreach (Laan laan in laanListe)
            {
                Console.WriteLine(laan.HentInfo());
            }
        }
    }
}