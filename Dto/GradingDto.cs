using System.Runtime.InteropServices.JavaScript;

namespace Student.Web.Api.Dto;

public class GradingDto
{
    public int Id { get; set; }
    public int Grade { get; set; }

  /*  public string? Remarks
    {
        get
        {
            return Grade >= 75 ? "Passed" : "Failed";
        }
    }*/



}

