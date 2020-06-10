using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsImporterLibrary
{
    public class FileWriter
    {
        internal List<CrewMember> CrewMembers { get; set; }
        internal List<Panel> Panels { get; set; }
        internal string SaveLocation { get; set; }
        internal DateTime StartDate { get; set; }
        internal DateTime EndDate { get; set; }
        internal string PlanName { get; set; }

        public FileWriter(List<CrewMember> crewMembers, List<Panel> panels, DateTime startDate, DateTime endDate, string saveLocation, string planName)
        {
            this.CrewMembers = crewMembers;
            this.Panels = panels;
            //Get start and end dates from panels min and max
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.SaveLocation = saveLocation;
            this.PlanName = planName;
        }
        private void WriteHotelEtab()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("5");
            sb.AppendLine("ScrewId,");
            sb.AppendLine("SpairingId");
            sb.AppendLine("ShotelLocation,");
            sb.AppendLine("AcheckInTime");
            sb.AppendLine("AcheckOutTime,");
            System.IO.File.WriteAllText($"{SaveLocation}\\{PlanName}\\hotel_data_{PlanName}.etab",sb.ToString());
        }
        private void WriteTrainingEtab()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("5");
            sb.AppendLine("SLegId,");
            sb.AppendLine("SPairingId");
            sb.AppendLine("SCrewId,");
            sb.AppendLine("ISeqNo");
            sb.AppendLine("SCode,");
            System.IO.File.WriteAllText($"{SaveLocation}\\{PlanName}\\Training_Activities_{PlanName}.etab", sb.ToString());
        }
        private void WriteVariationsEtab()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("5");
            sb.AppendLine("SpairingId");
            sb.AppendLine("IactivityNo,");
            sb.AppendLine("Svariation,");
            System.IO.File.WriteAllText($"{SaveLocation}\\{PlanName}\\Variations_{PlanName}.etab", sb.ToString());
        }
        private void WriteCtf()
        {
            StringBuilder ctfSb = new StringBuilder();
            StringBuilder panelErrorSb = new StringBuilder();
            StringBuilder baseErrorSb = new StringBuilder();
            WriteStartOfCtf(ctfSb);
            foreach (CrewMember crew in CrewMembers)
            {
                WriteCrewSectionOfCtf(crew, ctfSb, baseErrorSb, panelErrorSb);
            }
            WriteEndOfCtf(ctfSb);

            System.IO.File.WriteAllText($"{SaveLocation}\\{PlanName}\\Pairings_{PlanName}.ctf", ctfSb.ToString());
            System.Threading.Thread.Sleep(500);
            System.IO.File.WriteAllText($"{SaveLocation}\\CrewWithPanelErrors_{PlanName}.txt", panelErrorSb.ToString());
            System.Threading.Thread.Sleep(500);
            System.IO.File.WriteAllText($"{SaveLocation}\\CrewWithBaseErrors_{PlanName}.txt", baseErrorSb.ToString());

        }

        private void WriteCrewSectionOfCtf(CrewMember crew, StringBuilder ctfSb, StringBuilder baseErrorSb, StringBuilder panelErrorSb) 
        {
            Panel crewPanel = Panels.FirstOrDefault(n => n.Name == crew.PanelName);
            if (crewPanel != null)
            {
                ctfSb.AppendLine($"CREW: 1 \"{crew.PersNr}\"");
                foreach (OffPeriod offPeriod in crewPanel.GetOffPeriods(StartDate, EndDate))
                {
                    double numberOfDays = (offPeriod.EndDate - offPeriod.StartDate).TotalDays + 1;
                    for (int dayInOffPeriod = 0; dayInOffPeriod < numberOfDays; dayInOffPeriod++)
                    {
                        DateTime currentDate = offPeriod.StartDate.AddDays(dayInOffPeriod);
                        string crewBase;
                        if (crew.HasContract(currentDate) && crew.NeedsJS(currentDate) && crew.HasValidBase(currentDate, out crewBase))
                        {
                                string start = currentDate.Year.ToString() + currentDate.Month.ToString().PadLeft(2, '0') + currentDate.Day.ToString().PadLeft(2, '0');
                                string end = currentDate.AddDays(dayInOffPeriod + 1).Year.ToString() + currentDate.AddDays(1).Month.ToString().PadLeft(2, '0') + currentDate.AddDays(1).Day.ToString().PadLeft(2, '0');
                                ctfSb.AppendLine($"{crew.PersNr} * F 0 {start} {crewBase} 0000 JS * * 0000 {crewBase} {end}");
                        }
                        else if(crew.HasContract(currentDate) && crew.HasValidBase(currentDate) == false)
                        {
                            baseErrorSb.AppendLine($"{crew.ThreeLetter} does not have a valid base for {currentDate.ToShortDateString()}");
                        }
                    }
                }
            }
            else
            {
                panelErrorSb.AppendLine($"{crew.ThreeLetter} Unable to find correct panel. The csv shows their panel as {crew.PanelName}");
            }
        }

        private void WriteStartOfCtf(StringBuilder sb)
        {
            string start = StartDate.Year.ToString() + StartDate.Month.ToString().PadLeft(2, '0') + StartDate.Day.ToString().PadLeft(2, '0');
            string end = EndDate.Year.ToString() + EndDate.Month.ToString().PadLeft(2, '0') + EndDate.Day.ToString().PadLeft(2, '0');
            sb.Append("# START OF FILE (Generated 18Feb2020 12:11 by brackda)");
            sb.AppendLine("#");
            sb.AppendLine("VERSION: 1");
            sb.AppendLine($"PERIOD: {start} 0000 - {end} 2359");
            sb.AppendLine("PLAN TYPE: DATED");
            sb.AppendLine("CREW COMPL: CP/FO/SO//CM/SC/CC//EP");
            sb.AppendLine("BASE CODE: *");
            sb.AppendLine($"FILE COMMENT: \"Plan: {PlanName}\"");
            sb.AppendLine("FILE COMMENT 2:\"\"");
            sb.AppendLine("OUTPUT FILE NAME:");
            sb.AppendLine("SECTION: CREW");
        }
        private void WriteEndOfCtf(StringBuilder sb)
        {

            string start = StartDate.Year.ToString() + StartDate.Month.ToString().PadLeft(2, '0') + StartDate.Day.ToString().PadLeft(2, '0');
            sb.AppendLine("SECTION: PAIRING");
            sb.AppendLine("PAIRING: 1 0000000002 \"\"  0/1/0//0/0/0//0 LGW");
            sb.AppendLine($"0000000002  1 F L * {start} LGW 0845 TOM   76 * 01  2035 LIR {start}");
            sb.AppendLine("EOPAIRING");
        }

        public void WriteAllFiles()
        {
            System.IO.Directory.CreateDirectory($"{SaveLocation}\\{PlanName}");
            WriteHotelEtab();
            WriteTrainingEtab();
            WriteVariationsEtab();
            WriteCtf();
        }
    }
}
