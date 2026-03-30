namespace colekta_api.Models.ResultsModel;

public class Result
{
    protected Result(bool isSuccess, string message, IReadOnlyList<string>? errors = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        Errors = errors;
    }

    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }
    public IReadOnlyList<string> Errors { get; private set; }
}