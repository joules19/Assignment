using Assignment.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Transactions;
using static Assignment.Models.ApiModels;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private DataAccess _dataAccess;
        public ReportsController(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        #region Get Progress Report By Id

        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpGet, Route("ReportPersonalProgress")]
        public IActionResult ReportPersonalProgress(string PersonId)
        {
            try
            {
                // Checking PersonId if valid
                if (PersonId == null)
                {
                    return BadRequest("Person Id was not supplied");
                }

                //Pass PersonId to Method in DataAccess to fetch progress record 
                var record = _dataAccess.FetchRecordsById(PersonId);

                //Checking if records exist supplied Id
                if (record == null)
                {
                    return new JsonResult("No records uploaded yet.")
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };

                }

                return new JsonResult(record)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

            }

            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        #endregion

    }
}
