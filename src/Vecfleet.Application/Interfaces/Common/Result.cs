namespace Vecfleet.Application.Dtos;

public class Result
{
    public bool Success { get; set; }
    public List<string> Errors { get; set; }
    public Dictionary<string, string> Validations { get; set; }

    public static Result Successful()
    {
        return new Result
            { Success = true, Errors = new List<string>(), Validations = new Dictionary<string, string>() };
    }
    
    public static Result FailWithException(Exception exception)
    {
        return new Result
            { Success = true, Errors = new List<string>(){exception.Message}, Validations = new Dictionary<string, string>() };
    }
}