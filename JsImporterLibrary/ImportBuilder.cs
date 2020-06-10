using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsImporterLibrary
{
    public class ImportBuilder
    {
        private DateTime StartDate { get; }
        private DateTime EndDate { get; }
        private string CtfLocation { get; }
        private string Directory { get; }
        
        public ImportBuilder(DateTime startDate, DateTime endDate, string csvLocation)
        {
            StartDate = startDate;
            EndDate = endDate;
            CtfLocation = csvLocation;
            Directory = System.IO.Path.GetDirectoryName(csvLocation);
        }

        public void BuildImport()
        {
            List<Panel> panels = PanelBuilder.GetPanels();
            List<CrewMember> crewMembers = CrewBuilder.GetCrewMembers(CtfLocation);
            foreach (PanelType panelType in (PanelType[]) Enum.GetValues(typeof(PanelType)))
            {
                FileWriter fileWriter = new FileWriter(crewMembers.Where(n => n.PanelType == panelType).ToList(),
                    panels.Where(n => n.PanelType == panelType).ToList(),
                    this.StartDate, this.EndDate, this.Directory + "\\", panelType.ToString());
                fileWriter.WriteAllFiles();
            }
        }
    }
}
