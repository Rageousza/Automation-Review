using AbsaAutomation.src.main.Core;
using AbsaAutomation.src.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace AbsaAutomation.src.main.Tools
{
    public class DataReader
    {
        public DataTable readCSVfile(string CSVFile)
        {
            DataTable table = new DataTable();
            {
                var reader = new StreamReader(File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\src\Data\" + CSVFile));
                int counter = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var entries = line.Split(';');
                    if (counter == 0)
                    {
                        foreach (string header in entries)
                        {
                            table.Columns.Add(header);
                        }
                    }
                    else
                    {
                        DataRow row = table.NewRow();
                        for (int i = 0; i < entries.Length; i++)
                        {
                            row[i] = entries[i];
                        }
                        table.Rows.Add(row);
                    }
                    counter++;
                }
                return table;
            }
        }

        public string RetrieveDataFromDataTable(DataTable table, string columnName, int rowNumber) => (table.Rows[rowNumber])[columnName].ToString();

        public List<User> LoadJson(string JsonFile)
        {
            List<User> items;
            using (StreamReader r = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\src\Data\" + JsonFile))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<User>>(json);
            }
            return items;
        }

    }
}
