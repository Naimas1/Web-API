using System.Collections.Generic;

public class Info
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public InfoStatus Status { get; set; } 
    public List<string> ImagesBase64 { get; set; } 

    public Info()
    {
        ImagesBase64 = new List<string>();
    }
}

public enum InfoStatus
{
    Draft,
    UnderReview,
    Rejected,
    Approved
}
