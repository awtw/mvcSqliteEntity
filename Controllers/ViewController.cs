using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using csvWeb_v3_sqlite.Models;

namespace csvWeb_v3_sqlite.Controllers
{
    public class ViewController : Controller
    {

        public static List<TemperatureRecord> CsvFileRecords = new List<TemperatureRecord>();

        private ApplicationDbContext _context;

        public bool HaveCsvFileRecords;

        public ViewController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: View

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(HttpPostedFileBase uploadFile)
        {
            CsvFileRecords.Clear();
           
            if (uploadFile != null && uploadFile.ContentLength > 0)
            {

                var temp = Models.CsvHelper.Import(uploadFile);
                foreach (var item in temp)
                {
                    var key = Guid.NewGuid().ToString();
                    CsvFileRecords.Add(new TemperatureRecord()
                    {
                        ID = key,
                        時間 = Convert.ToDateTime(item.時間),
                        第1段溫度顯示值 = Convert.ToDouble(item.第1段溫度顯示值),
                        第2段溫度顯示值 = Convert.ToDouble(item.第2段溫度顯示值),
                        第3段溫度顯示值 = Convert.ToDouble(item.第3段溫度顯示值),
                        第4段溫度顯示值 = Convert.ToDouble(item.第4段溫度顯示值),
                        第5段溫度顯示值 = Convert.ToDouble(item.第5段溫度顯示值),
                        第6段溫度顯示值 = Convert.ToDouble(item.第6段溫度顯示值)
                    });
                }
                HaveCsvFileRecords = true;
                ViewBag.HasFile = HaveCsvFileRecords;
            }
            else
            {
                ViewBag.Message = "You don't have any file here !";
            }

            return View(CsvFileRecords);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertDb()
        {
            if (CsvFileRecords != null)
            {
                foreach (var item in CsvFileRecords)
                {
                    _context.TemperatureRecord.Add(item);
                }

                _context.SaveChanges();

                CsvFileRecords.Clear();
                HaveCsvFileRecords = true;
                ViewBag.HasFile = HaveCsvFileRecords;
            }
            else
            {
                ViewBag.Message = "You don't have any file here !";
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TemperatureRecord temperatureRecord)
        {
            if (!ModelState.IsValid)
            {
                var temp = new TemperatureRecord();
                temperatureRecord.ID = temp.ID;
                return View("View", temp);
            }

            if (temperatureRecord.ID == "New")
            {
                var key = Guid.NewGuid().ToString();
                temperatureRecord.ID = key;
                _context.TemperatureRecord.Add(temperatureRecord);
               
            }
            else
            {
                var dataFromDb = _context.TemperatureRecord.Single(c => c.ID == temperatureRecord.ID);
                //dataFromDb.時間 = temperatureRecord.時間;
                //dataFromDb.第1段溫度顯示值 = temperatureRecord.第1段溫度顯示值;
                //dataFromDb.第2段溫度顯示值 = temperatureRecord.第2段溫度顯示值;
                //dataFromDb.第3段溫度顯示值 = temperatureRecord.第3段溫度顯示值;
                //dataFromDb.第4段溫度顯示值 = temperatureRecord.第4段溫度顯示值;
                //dataFromDb.第5段溫度顯示值 = temperatureRecord.第5段溫度顯示值;
                //dataFromDb.第6段溫度顯示值 = temperatureRecord.第6段溫度顯示值;
                var entry = _context.Entry(dataFromDb);
                entry.CurrentValues.SetValues(temperatureRecord);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "View");
        }

        public ActionResult New()
        {
            var temp = new TemperatureRecord();
            string key = "New";
            temp.ID = key;
            return View("View", temp);
        }

        public ActionResult Edit(string id)
        {
            var data = _context.TemperatureRecord.SingleOrDefault(c => c.ID == id);

            if (data == null)
            {
                return HttpNotFound();
            }
            var temp = new TemperatureRecord();
            temp = data;
            return View("View", temp);
        }
    }
}