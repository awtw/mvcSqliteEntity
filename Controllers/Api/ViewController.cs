using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using csvWeb_v3_sqlite.Models;

namespace csvWeb_v3_sqlite.Controllers.Api
{
    public class ViewController : ApiController
    {
        private ApplicationDbContext _context;
        public ViewController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetData()
        {
            var data = _context.TemperatureRecord.ToList();
            return Ok(data);
        }

        public IHttpActionResult GetData(string id)
        {
            var data = _context.TemperatureRecord.SingleOrDefault(c => c.ID == id);
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        public IHttpActionResult PostData(List<TemperatureRecord> temperatureRecord)
        {
            foreach (var record in temperatureRecord)
            {
                if (ModelState.IsValid)
                {
                    _context.TemperatureRecord.Add(record);
                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest();
                }
            }
            return Ok();
        }

        public IHttpActionResult PutData(string id, TemperatureRecord record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var data = _context.TemperatureRecord.SingleOrDefault(c => c.ID == id);
            if (data == null)
            {
                return NotFound();
            }

            data = record;
            _context.SaveChanges();
            return Ok();
        }

        public IHttpActionResult DeleteData(string id)
        {
            var data = _context.TemperatureRecord.SingleOrDefault(c => c.ID == id);
            if (data == null)
            {
                return NotFound();
            }

            _context.TemperatureRecord.Remove(data);
            _context.SaveChanges();
            return Ok();
        }
    }
}
