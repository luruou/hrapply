using System;
using System.Collections.Generic;
using System.Web;
using System.Data;

namespace ApplyPromote
{
    public class ConvertDTtoStringArray
    {
        String [] strArray;

        public void Execute(DataTable dtTable)
        {
            strArray = new string[dtTable.Rows.Count + 1];

            strArray[0] = "";

            for (int i = 0; i <= dtTable.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dtTable.Columns.Count - 1; j++)
                {
                     strArray[i+1] = dtTable.Rows[i][j].ToString();
                }
            }
        }

        public void ExecuteZero(DataTable dtTable)
        {
            strArray = new string[dtTable.Rows.Count];

            for (int i = 0; i <= dtTable.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dtTable.Columns.Count - 1; j++)
                {

                    strArray[i] = dtTable.Rows[i][j].ToString();
                }
            }


        }

        public String[] GetStringArray(DataTable dt)
        {
            Execute(dt);
            return strArray;
        }

        public String[] GetStringArrayWithZero(DataTable dt)
        {
            ExecuteZero(dt);
            return strArray;
        }
    }
}
