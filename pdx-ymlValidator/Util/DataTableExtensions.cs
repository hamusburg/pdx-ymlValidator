﻿using System.Data;
using System.IO;
using System.Text;

namespace pdx_ymlValidator.Util
{
    public static class DataTableExtensions
    {
        public static void WriteToCsvFile(this DataTable dataTable, string filePath)
        {
            StringBuilder fileContent = new StringBuilder();

            foreach (var col in dataTable.Columns)
            {
                fileContent.Append(col.ToString() + ",");
            }

            fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);

            foreach (DataRow dr in dataTable.Rows)
            {
                foreach (var column in dr.ItemArray)
                {
                    fileContent.Append("\"" + column.ToString() + "\",");
                }
                fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
            }
            File.WriteAllText(filePath, fileContent.ToString());
        }
    }
}
