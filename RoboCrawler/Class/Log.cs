using System.ComponentModel.DataAnnotations;

public class Log
{
    [Key]
    public int IdLog { get; set; }
    public string CodRobot { get; set; }
    public string UserName { get; set; }
    public DateTime LogDate { get; set; }
    public string  StateDescription { get; set; }
    public string ResultFeedBack { get; set; }
    public int ProductID { get; set; }
}