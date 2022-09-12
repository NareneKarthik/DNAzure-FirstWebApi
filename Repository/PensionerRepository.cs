using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Pensioner_Details.Controllers;

namespace Pensioner_Details.Repository
{
    public class PensionerRepository : IPensionerdetail
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PensionerRepository));
        //private IConfiguration configuration;

        
        public PensionerDetail PensionerDetailByAadhar(string aadhar)
        {
            List<PensionerDetail> pensionDetails = GetDetailsCsv();
            _log4net.Info("Pensioner details invoked by Aadhar Number!");
            return pensionDetails.FirstOrDefault(s => s.AadharNumber == aadhar);
        }

        public List<PensionerDetail> GetDetailsCsv()
        {
            _log4net.Info("Data is read from CSV file");  // Logging Implemented
            List<PensionerDetail> pensionerdetail = new List<PensionerDetail>();
            try
            {
                //string csvConn = configuration.GetValue<string>("MySettings:CsvConnection");  // Initializing the csvConn  for the File path
                //string csvConn = "details.csv";
                using (StreamReader sr = new StreamReader("details.csv"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        pensionerdetail.Add(new PensionerDetail() { Name = values[0], Dateofbirth = Convert.ToDateTime(values[1]), Pan = values[2], AadharNumber = values[3], SalaryEarned = Convert.ToInt32(values[4]), Allowances = Convert.ToInt32(values[5]), PensionType = (PensionTypeValue)Enum.Parse(typeof(PensionTypeValue), values[6]), BankName = values[7], AccountNumber = values[8], BankType = (BankType)Enum.Parse(typeof(BankType), values[9]) });
                    }

                }
            }
            catch (NullReferenceException e)
            {
                _log4net.Error("Values cannot be fetched from the Csv file" + e);
                return null;
            }
            catch (Exception e)
            {
                _log4net.Error("Values cannot be fetched from the Csv file" + e);
                return null;
            }
            return pensionerdetail.ToList();
        }

        public string FileString(PensionerDetail pension)
        {
            String fromFile = string.Format($"{pension.Name},{pension.Dateofbirth},{pension.Pan},{pension.AadharNumber},{pension.SalaryEarned},{pension.Allowances},{pension.PensionType},{pension.BankName},{pension.AccountNumber},{pension.BankType}");
            return fromFile;
        }


        public PensionerDetail AddNewPensioner(PensionerDetail pension)
        {
            string fromFile = FileString(pension);
            File.AppendAllText("details.csv", fromFile);
            return pension;
        }
        public PensionerDetail UpdatePensioner(PensionerDetail pension, string aadhar)
        {
            List<PensionerDetail> pensionDetails = GetDetailsCsv();
            var ExistingPensioner = pensionDetails.FirstOrDefault(c => c.Name == pension.Name && c.AadharNumber == aadhar);

            string newString = FileString(pension);

            string searchFor = ExistingPensioner.AadharNumber;
            List<string> lines = (File.ReadAllLines("details.csv")).ToList();
            for (int i = lines.Count - 1; i >= 0; i--)
            {
                if (lines[i].Contains(searchFor))
                {
                    lines.RemoveAt(i);
                    lines.Add(newString);
                }
            }
            File.WriteAllLines("details.csv", lines);
            return ExistingPensioner;
        }
        public PensionerDetail DeletePensioner(string aadhar)
        {
            List<PensionerDetail> pensionDetails = GetDetailsCsv();
            var ExistingPensioner = pensionDetails.FirstOrDefault(c => c.AadharNumber == aadhar);

            string searchFor = ExistingPensioner.AadharNumber;
            List<string> lines = (File.ReadAllLines("details.csv")).ToList();
            for (int i = lines.Count - 1; i >= 0; i--)
            {
                if (lines[i].Contains(searchFor))
                {
                    lines.RemoveAt(i);
                }
            }
            File.WriteAllLines("details.csv", lines);
            return ExistingPensioner;
        }

    }
}
