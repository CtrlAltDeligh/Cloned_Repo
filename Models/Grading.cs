namespace Student.Web.Api.Models
{
    public class Grading
    {
        public Grading(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
        public int Grade { get; set; }
        public string? Remarks { get; set; }

       
    }
}