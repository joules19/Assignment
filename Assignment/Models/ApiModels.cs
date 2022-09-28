namespace Assignment.Models
{
    public class ApiModels
    {
        public class PersonVM
        {
            public string person_id { get; set; }
            public string course_id { get; set; }
            public string name { get; set; }
            public int score { get; set; }
        }

        public class CourseVM
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class ReportsVm
        {
            public string name { get; set; }
            public List<string> course_name { get; set; }
            public double score_avg { get; set; }

            public ReportsVm()
            {
                course_name = new List<string>();
            }
        }
    }
}
