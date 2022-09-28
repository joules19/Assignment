using Assignment.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Assignment.Models.ApiModels;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private DataAccess _dataAccess;
        public PersonController(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        #region Get All Persons
        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpGet, Route("GetAllpersons")]
        public IActionResult GetAllpersons()
        {
            try
            {
                //Fetching the persons Data object and assinging result to variable
                List<Person> persons = _dataAccess.GetAllPerson();

                //Declare new instance of Collections of type PersonVM
                List<PersonVM> personsObject = new();

                //Iterating through persons collections and assigning values to respective property
                foreach (var person in persons)
                {
                    personsObject.Add(new PersonVM
                    {
                        person_id = person.PersonId,
                        course_id = person.CourseId,
                        name = person.Name,
                        score = person.Score
                    });

                }

                return new JsonResult(personsObject)
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

        #region Post Bulk Persons Data

        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpPost, Route("BulkUploadPersonalData")]
        public async Task<IActionResult> BulkUploadPersonalData([FromBody] List<PersonVM> Persons)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Fetching the persons Data object and assinging result to property
                List<Person> persons = _dataAccess.GetAllPerson();

                //Checking if records exist in persons to handle respective response
                if (persons.Count() < 1)
                {
                    return new JsonResult("No data uploaded yet.")
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }

                foreach (var person in Persons)
                {

                    Person personsX = new();

                    personsX.PersonId = person.person_id;
                    personsX.CourseId = person.course_id;
                    personsX.Name = person.name;
                    personsX.Score = person.score;

                    _dataAccess.UploadPersonalData(personsX);
                }

                //Declare new instance of Collections of type PersonVM
                List<PersonVM> personObject = new();

                //Iterating through persons and assigning values to respective property
                foreach (var person in persons)
                {
                    personObject.Add(new PersonVM
                    {
                        person_id = person.PersonId,
                        course_id = person.CourseId,
                        name = person.Name,
                        score = person.Score
                    });
                }


                return new JsonResult(personObject)
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
