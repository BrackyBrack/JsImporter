using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsImporterLibrary
{
    public static class CrewBuilder
    {
        public static List<CrewMember> GetCrewMembers(string csvLocation)
        {
            DataTable crewData = DataAccess.CsvReader.GetCrewWorkPanels(csvLocation);
            DataTable baseData = DataAccess.DataBaseReader.GetCrewData();
            DataTable jsData = DataAccess.DataBaseReader.GetJsData();
            List<CrewMember> crewMembers = new List<CrewMember>();
            List<CrewMember> crewMembersWithErrors = new List<CrewMember>();
            foreach (DataRow row in crewData.Rows)
            {
                string threeLetter = row[0].ToString();
                string panel = row[1].ToString().Trim('0', '.', '%').PadRight(2,'0') + row[2].ToString().Trim();

                if(panel != "10")
                {
                    var crewMembersBases = baseData.Select($"P_LTR_CODE = '{threeLetter}'");
                    if (crewMembersBases.Count() == 0)
                    {
                        crewMembersWithErrors.Add(new CrewMember { ThreeLetter = threeLetter });
                    }
                    else
                    {
                        string persNr = crewMembersBases[0][1].ToString();
                        List<CrewBase> bases = new List<CrewBase>();
                        List<CrewContract> contracts = new List<CrewContract>();
                        foreach (var baseRow in crewMembersBases)
                        {
                            bases.Add(new CrewBase
                            {
                                BaseName = baseRow[2].ToString(),
                                StartDate = DateTime.Parse(baseRow[3].ToString()),
                                EndDate = DateTime.Parse(baseRow[4].ToString())
                            });
                            contracts.Add(new CrewContract
                            {
                                StartDate = DateTime.Parse(baseRow[5].ToString()),
                                EndDate = DateTime.Parse(baseRow[6].ToString())
                            });
                        }
                        var existingJs = jsData.Select($"CLG_P_PERS_NR = '{persNr}'");
                        List<DateTime> existingJsDates = new List<DateTime>();
                        foreach (var jsRow in existingJs)
                        {
                            existingJsDates.Add(DateTime.Parse(jsRow[1].ToString()));
                        }

                        crewMembers.Add(new CrewMember { ThreeLetter = threeLetter, PersNr = persNr, PanelName = panel, Bases = bases, Contracts = contracts, ExistingJS = existingJsDates });
                    }
                }
            }
            BuildErrorFile(crewMembersWithErrors, csvLocation);
            return crewMembers;
        }

        private static void BuildErrorFile(List<CrewMember> crewMembersWithErrors, string csvLocation)
        {
            StringBuilder sb = new StringBuilder();
            foreach (CrewMember crewMember in crewMembersWithErrors)
            {
                sb.AppendLine(crewMember.ThreeLetter);
            }
            string saveLocation = System.IO.Directory.GetParent(csvLocation).ToString();
            System.IO.File.WriteAllText($"{saveLocation}\\CrewWithContractErrors.txt", sb.ToString());
        }
    }
}
