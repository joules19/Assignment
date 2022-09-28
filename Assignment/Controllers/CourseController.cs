using Assignment.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Assignment.Models.ApiModels;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private DataAccess _dataAccess;
        public CourseController(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        #region Get All Courses

        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpGet, Route("GetAllCourses")]
        public IActionResult GetAllCourses()
        {
            try
            {
                //Fetching the Courses Data object and assinging result to variable
                List<Course> courses = _dataAccess.GetAllCourse();

                //Checking if records exist in courses to handle respective response
                if (courses.Count() < 1)
                {
                    return new JsonResult("No courses uploaded yet.")
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }

                //Declare new instance of Collections of type CourseVM
                List<CourseVM> courseObject = new();

                //Iterating through courses and assigning values to respective property
                foreach (var course in courses)
                {
                    courseObject.Add(new CourseVM
                    {
                        id = course.Id,
                        name = course.Name,
                    });

                }

                return new JsonResult(courseObject)
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

        #region Post Single or Bulk Course Data

        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpPost, Route("BulkUploadCourseCatalogue")]
        public async Task<IActionResult> BulkUploadCourseCatalogue([FromBody] List<CourseVM> Courses)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Fetching the Courses Data object and assinging result to variable
                List<Course> courses = _dataAccess.GetAllCourse();

                foreach (var course in Courses)
                {
                    //Check if course details already exist
                    foreach (var item in courses)
                    {
                        if (item.Id == course.id )
                        {
                            return new JsonResult("Record exists for CourseId: " + course.id)
                            {
                                StatusCode = (int)HttpStatusCode.OK
                            };
                        }

                        if (item.Name == course.name)
                        {
                            return new JsonResult("Course record " + course.name + " already exists.")
                            {
                                StatusCode = (int)HttpStatusCode.OK
                            };
                        }

                    }

                    Course courseX = new();

                    courseX.Id = course.id;
                    courseX.Name = course.name;

                    _dataAccess.UploadCourse(courseX);
                }

                //Declare new instance of Collections of type CourseVM
                List<CourseVM> courseObject = new();

                //Iterating through courses and assigning values to respective property
                foreach (var course in courses)
                {
                    courseObject.Add(new CourseVM
                    {
                        id = course.Id,
                        name = course.Name
                    });

                }

                return new JsonResult(courseObject)
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
