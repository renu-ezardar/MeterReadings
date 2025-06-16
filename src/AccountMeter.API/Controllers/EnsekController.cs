using AccountMeter.API.Models;
using AccountMeter.BusinessLogic;
using AccountMeter.Common.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace AccountMeter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnsekController : ControllerBase
    {
        private readonly IReadingsProcessor _readingsProcessor;

        public EnsekController(IReadingsProcessor readingsProcessor )
        {
            _readingsProcessor = readingsProcessor;            
        }


        [HttpPost("meter-reading-uploads")]
        public IActionResult UploadReadingCsv(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded or file is empty.");

                if (Path.GetExtension(file.FileName).ToLower() != ".csv")
                    return BadRequest("Only CSV files are allowed.");

                IList<Reading> readings = new List<Reading>();

                using (var stream = file.OpenReadStream())
                using (var reader = new StreamReader(stream))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.Context.RegisterClassMap<ReadingMap>();
                    readings = csv.GetRecords<Reading>().ToList();
                }

                var response = _readingsProcessor.ProcessReadings(readings);
                if (response.Success)
                {
                    return Ok(new FileUploadResponse() 
                    {  
                        FailureReadingCount = response.FailureReadingCount , 
                        SuccessfulReadingCount = response.SuccessfulReadingCount
                    });
                }else
                    return BadRequest(response.ErrorMessage);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

