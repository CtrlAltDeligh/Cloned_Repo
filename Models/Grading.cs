namespace Student.Web.Api.Models
{
    public class Grading
    {
        public  Grading(){}
        public Grading(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
        public int Grade { get; set; }
      

        public  string? Remarks
        {
            get
            {
                return Grade >= 75 ? "Passed" : "Failed";
            }
            
        }


    }
}