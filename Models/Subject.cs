namespace Student.Web.Api.Models
{
    public class Subject
    {
       
            public Subject(int id)
            {
                Id = id;
            }
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}