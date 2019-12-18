using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using CsvHelper;

namespace csvWeb_v3_sqlite.Models
{
    public class CsvHelper
    {
        public static List<dynamic> Import(HttpPostedFileBase tempFile)
        {
            List<dynamic> context = null;
            if (tempFile != null)
            {
                using (var reader = new StreamReader(tempFile.InputStream))
                {
                    using (var csv = new CsvReader(reader))
                    {
                        var records = csv.GetRecords<dynamic>();
                        context = records.ToList();
                    }
                }
            }
            return context;
        }

        public static byte[] ExportDownload(List<TemperatureRecord> data)
        {
            MemoryStream memoryStream = new MemoryStream();
            using (StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
            {
                using (var csvWriter = new CsvWriter(streamWriter))
                {
                    csvWriter.WriteHeader<TemperatureRecord>();
                    csvWriter.Configuration.Delimiter = ",";
                    csvWriter.Configuration.AutoMap<TemperatureRecord>();
                    csvWriter.NextRecord();
                    foreach (var row in data)
                    {
                        csvWriter.WriteRecord(row);
                        csvWriter.NextRecord();
                    }
                    streamWriter.Flush();
                }
            }

            return memoryStream.GetBuffer();
        }
    }

    
}