namespace colekta_api.Models.ResultsModel;

public class ResultData<T> : Result
{
    protected ResultData(T? data, bool isSuccess, string message = "", IReadOnlyList<string>? errors = null)
        : base(isSuccess, message, errors)
    {
        Data = data;
    }

    public T? Data { get; set; }

    public static ResultData<T> Success(T? data, string message = "")
        => new(data, true, message);

    public static ResultData<T> Error(string message)
        => new(default, false, message);

    public static ResultData<T> Error(IEnumerable<string> errors, string message = "")
    {
        var list = errors.ToList();
        return new(default, false, message, list);
    }
}