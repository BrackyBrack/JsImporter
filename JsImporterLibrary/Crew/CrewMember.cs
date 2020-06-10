using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsImporterLibrary
{
    public class CrewMember
    {
        public string ThreeLetter { get; set; }
        public string PanelName { get; set; }
        public string PersNr { get; set; }
        public PanelType PanelType
        {
            get
            {
                switch (PanelName)
                {
                    case "75A":
                    case "75B":
                    case "75C":
                    case "75D":
                        return PanelType.SeventyFive;
                    case "501":
                    case "502":
                    case "503":
                    case "504":
                        return PanelType.TOTO;
                    default:
                        return PanelType.MOMO;
                }
            } 
        }
        internal List<CrewBase> Bases { get; set; }
        internal List<CrewContract> Contracts { get; set; }
        internal List<DateTime> ExistingJS { get; set; }

        public bool HasContract(DateTime date)
        {
            CrewContract contract = this.Contracts.FirstOrDefault(n => n.StartDate <= date && n.EndDate >= date);
            return contract != null;
        }

        public bool NeedsJS(DateTime date)
        {
            return ExistingJS.Contains(date) == false;
        }

        internal bool HasValidBase(DateTime date, out string baseName)
        {
            CrewBase crewBase = this.Bases.FirstOrDefault(n => n.StartDate <= date && n.EndDate >= date);
            baseName = crewBase?.BaseName;
            return string.IsNullOrWhiteSpace(crewBase?.BaseName) == false;
        }

        internal bool HasValidBase(DateTime date)
        {
            CrewBase crewBase = this.Bases.FirstOrDefault(n => n.StartDate <= date && n.EndDate >= date);
            return string.IsNullOrWhiteSpace(crewBase?.BaseName) == false;
        }
    }
}
