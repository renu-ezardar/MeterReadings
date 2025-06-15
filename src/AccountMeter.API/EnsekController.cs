using AccountMeter.Common.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace AccountMeter.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnsekController : ControllerBase
    {
        [HttpPut("uploadReading")]
        public async Task<IActionResult> UploadReadingCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded or file is empty.");
            }

            if (Path.GetExtension(file.FileName).ToLower() != ".csv")
            {
                return BadRequest("Only CSV files are allowed.");
            }

            IList<Reading> readings = new List<Reading>();

            using (var stream = file.OpenReadStream())
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<ReadingMap>();
                readings = csv.GetRecords<Reading>().ToList();

            }

            return Ok(new { Message = "File uploaded successfully.", FileName = file.FileName });
        }
    }
}

