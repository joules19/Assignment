using static Assignment.Models.ApiModels;

namespace Assignment.Data
{
    //class library to serve as data container.
    public class DataAccess
    {

        #region Courses

        //Declare empty array of type Course class
        List<Course> Courses = new List<Course>() { };

        // Iterating through above collection *Course
        public List<Course> GetAllCourse()
        {
            return Courses;
        }

        //Method to upload course data
        public void UploadCourse(Course Course)
        {
            Courses.Add(new Course
            {
                Id = Course.Id,
                Name = Course.Name
            });
        }

        #endregion


        #region Person
        //Array of type person class and assign the values to each property
        List<Person> Persons = new List<Person>() { };

        // Iterating through above collection *Person
        public List<Person> GetAllPerson()
        {
            return Persons;
        }

        //Method to Upload Personal data
        public void UploadPersonalData(Person Person)
        {
            Persons.Add(new Person
            {
                PersonId = Person.PersonId,
                CourseId = Person.CourseId,
                Name = Person.Name,
                Score = Person.Score
            });
        }
        #endregion


        #region Reports 
        public ReportsVm? FetchRecordsById(string PersonId)
        {
            //Declare variable report of type ReportsVM
            ReportsVm report = new();

            //Return a list of type Person where provided Id matches with the iterated PersonId
            List<Person> persons = Persons.Where(person => person.PersonId == PersonId).ToList();

            //Checking if there are records. If not return a null value
            if (persons.Count() < 1)
            {
                return null;
            }

            //Assigning Person's name
            report.name = persons.FirstOrDefault().Name;

            //Getting the sum & avg of scores for a single person across all courses
            var sumScore = persons.Sum(x => x.Score);
            report.score_avg = Convert.ToDouble(sumScore / persons.Count());

            //Iterating over the returned persons list to append their corresponding course names to a list
            foreach (var person in persons)
            {
                foreach (var course in Courses)
                {
                    if (course.Id == person.CourseId)
                    {
                        report.course_name.Add(course.Name);
                    }
                }
            }

            return report;
        }
        #endregion

    }
}


