using GenericParsing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CsvReader
    {
        public static DataTable GetCrewWorkPanels(string ctfLocation)
        {
            DataTable dtResult = new DataTable();
            using (GenericParserAdapter parser = new GenericParserAdapter(ctfLocation))
            {
                parser.FirstRowHasHeader = true;
                var dsResult = parser.GetDataSet();
                dtResult = dsResult.Tables[0];
            }

            return dtResult;
        }
    }
}
