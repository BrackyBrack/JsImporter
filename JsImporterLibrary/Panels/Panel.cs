using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsImporterLibrary
{
    public class Panel
    {
        public string Name { get; set; }
        internal DateTime SeedDate { get; set; }
        internal int NumberOfDaysInPanel { get; set; }
        internal int NumberOfDaysBetweenPanels { get; set; }
        public PanelType PanelType { get; set; }

        public List<OffPeriod> GetOffPeriods(DateTime startDate, DateTime endDate)
        {
            List<OffPeriod> offPeriods = new List<OffPeriod>();
            bool exceededEndDate = false;
            DateTime panelStartDate = this.SeedDate;
            DateTime panelEndDate = this.SeedDate.AddDays(NumberOfDaysInPanel);
            while (exceededEndDate == false)
            {
                if(panelEndDate < startDate)
                {
                    panelStartDate = panelEndDate.AddDays(NumberOfDaysBetweenPanels);
                    panelEndDate = panelStartDate.AddDays(NumberOfDaysInPanel);
                }
                else if(panelEndDate >= startDate && panelStartDate <= endDate)
                {
                    DateTime builddate = (panelStartDate < startDate) ? startDate : panelStartDate;
                    offPeriods.Add(new OffPeriod { StartDate = builddate, EndDate = panelEndDate });
                    panelStartDate = panelEndDate.AddDays(NumberOfDaysBetweenPanels);
                    panelEndDate = panelStartDate.AddDays(NumberOfDaysInPanel);
                }
                else
                {
                    exceededEndDate = true;
                }
            }
            return offPeriods;
        }
    }

    public enum PanelType
    {
        MOMO,
        TOTO,
        SeventyFive
    }
}
