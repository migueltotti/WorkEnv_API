using System.ComponentModel.DataAnnotations;
using MediatR;

namespace WorkEnv.Application.Result;

public class Result
{
    public readonly Error Error;
    public readonly bool IsSuccess;
    public readonly List<ValidationResult> ValidationResults;

    public Result(bool isSuccess)
    {
        if(isSuccess is false)
            throw new ArgumentNullException("Result is Invalid!");
        
        IsSuccess = isSuccess;
        Error = default;
        ValidationResults = [];
    }

    public Result(Error error, bool isSuccess)
    {
        if ((isSuccess is false && error is null) ||
            (isSuccess is true && error is not null))
            throw new ArgumentNullException("Result is Invalid!");
        
        Error = error;
        IsSuccess = isSuccess;
        ValidationResults = [];
    }
    
    public Result(Error error, List<ValidationResult> validationResults, bool isSuccess)
    {
        if ((isSuccess is false && error is null) ||
            (isSuccess is true && error is not null) ||
            (validationResults.Count > 0))
            throw new ArgumentNullException("Result is Invalid!");
        
        Error = error;
        IsSuccess = isSuccess;
        ValidationResults = validationResults;
    }
    
    public static Result Success() => new Result(true);
    public static Result Failure(Error err) => new Result(err, false);
}

public class Result<TValue> where TValue : class
{
    public readonly TValue Value;
    public readonly Error Error;
    public readonly bool IsSuccess;
    public readonly List<ValidationResult> ValidationResults;

    public Result(TValue value, bool isSuccess)
    {
        if ((isSuccess is false && value is not null) ||
            (isSuccess is true && value is null))
            throw new ArgumentNullException("Result is Invalid!");
        
        Value = value;
        IsSuccess = isSuccess;
        Error = Error.None;
        ValidationResults = [];
    }

    public Result(Error error, bool isSuccess)
    {
        if ((isSuccess is false && error is null) ||
            (isSuccess is true && error is not null))
            throw new ArgumentNullException("Result is Invalid!");
        
        Error = error;
        IsSuccess = isSuccess;
        Value = default;
        ValidationResults = [];
    }

    public Result(Error error, List<ValidationResult> validationResults, bool isSuccess)
    {
        if ((isSuccess is false && error is null) ||
            (isSuccess is true && error is not null) ||
            (validationResults.Count > 0))
            throw new ArgumentNullException("Result is Invalid!");
        
        Error = error;
        IsSuccess = isSuccess;
        ValidationResults = validationResults;
        Value = default;
    }

    public static Result<TValue> Success(TValue value) => new Result<TValue>(value, true);
    public static Result<TValue> Failure(Error err) => new Result<TValue>(err, false);
    public static Result<TValue> Failure(Error err, List<ValidationResult> validationResults) => 
        new Result<TValue>(err, validationResults, false);
}