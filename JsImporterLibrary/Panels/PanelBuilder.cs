using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsImporterLibrary
{
    public static class PanelBuilder
    {
        public static List<Panel> GetPanels()
        {
            List<Panel> panels = new List<Panel>
            {
                new Panel{Name = "50A", SeedDate = new DateTime(2021, 3, 24), NumberOfDaysInPanel = 27, NumberOfDaysBetweenPanels = 29, PanelType = PanelType.MOMO }
                , new Panel{Name = "50B", SeedDate = new DateTime(2021, 2, 24), NumberOfDaysInPanel = 27, NumberOfDaysBetweenPanels = 29, PanelType = PanelType.MOMO }
                , new Panel{Name = "50C", SeedDate = new DateTime(2021, 3, 31), NumberOfDaysInPanel = 27, NumberOfDaysBetweenPanels = 29, PanelType = PanelType.MOMO }
                , new Panel{Name = "50D", SeedDate = new DateTime(2021, 3, 3), NumberOfDaysInPanel = 27, NumberOfDaysBetweenPanels = 29, PanelType = PanelType.MOMO }
                , new Panel{Name = "50E", SeedDate = new DateTime(2021, 2, 9), NumberOfDaysInPanel = 27, NumberOfDaysBetweenPanels = 29, PanelType = PanelType.MOMO }
                , new Panel{Name = "50F", SeedDate = new DateTime(2021, 3, 9), NumberOfDaysInPanel = 27, NumberOfDaysBetweenPanels = 29, PanelType = PanelType.MOMO }
                , new Panel{Name = "50G", SeedDate = new DateTime(2021, 2, 16), NumberOfDaysInPanel = 27, NumberOfDaysBetweenPanels = 29, PanelType = PanelType.MOMO }
                , new Panel{Name = "50H", SeedDate = new DateTime(2021, 3, 16), NumberOfDaysInPanel = 27, NumberOfDaysBetweenPanels = 29, PanelType = PanelType.MOMO }
                , new Panel{Name = "501", SeedDate = new DateTime(2021, 3, 10), NumberOfDaysInPanel = 13, NumberOfDaysBetweenPanels = 15, PanelType = PanelType.TOTO }
                , new Panel{Name = "502", SeedDate = new DateTime(2021, 3, 17), NumberOfDaysInPanel = 13, NumberOfDaysBetweenPanels = 15, PanelType = PanelType.TOTO }
                , new Panel{Name = "503", SeedDate = new DateTime(2021, 3, 24), NumberOfDaysInPanel = 13, NumberOfDaysBetweenPanels = 15, PanelType = PanelType.TOTO }
                , new Panel{Name = "504", SeedDate = new DateTime(2021, 3, 31), NumberOfDaysInPanel = 13, NumberOfDaysBetweenPanels = 15, PanelType = PanelType.TOTO }
                , new Panel{Name = "75A", SeedDate = new DateTime(2021, 3, 17), NumberOfDaysInPanel = 6, NumberOfDaysBetweenPanels = 22, PanelType = PanelType.SeventyFive }
                , new Panel{Name = "75B", SeedDate = new DateTime(2021, 3, 24), NumberOfDaysInPanel = 6, NumberOfDaysBetweenPanels = 22, PanelType = PanelType.SeventyFive }
                , new Panel{Name = "75C", SeedDate = new DateTime(2021, 3, 31), NumberOfDaysInPanel = 6, NumberOfDaysBetweenPanels = 22, PanelType = PanelType.SeventyFive }
                , new Panel{Name = "75D", SeedDate = new DateTime(2021, 3, 10), NumberOfDaysInPanel = 6, NumberOfDaysBetweenPanels = 22, PanelType = PanelType.SeventyFive }
            };

            return panels;
        }
    }
}
