using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.CommenResults
{
    public class Result
    {
        //isSuccess
        //isFailure
        //Errors[code-description-type]
        protected readonly List<Error> _errors =[];
        public bool IsSuccess => _errors.Count==0;
        public bool IsFailure => !IsSuccess;
        public IReadOnlyList<Error> Errors => _errors;



        //No errors 
        protected Result() { 
        
        }
        //one error occurred
        protected Result(Error error) {
            _errors.Add(error);
        }
        //multiple errors occurred
        protected Result(List<Error> errors) {
            _errors.AddRange(errors);
        }

        //static methods
        public static Result Ok() {
            return new Result();
        }
        public static Result Fail(Error error) {
            return new Result(error);
        }
        public static Result Fail(List<Error> errors) {
            return new Result(errors);
        }
    }

    public class Result<T> : Result
    {
        private readonly T _Value;
        public T Valuet => IsSuccess ? _Value : throw new InvalidOperationException("Cannot access the value of a failed result.");

        //No errors 
        private Result(T value) : base() {
            _Value = value;
        }
        //one error occurred
        private Result(Error error) : base(error) {
            _Value = default!;
        }
        //multiple errors occurred
        private Result(List<Error> errors) : base(errors) {
            _Value = default!;
        }
        //static methods
        public static Result<T> Ok(T value) {
            return new Result<T>(value);
        }
        public static new Result<T> Fail(Error error) {
            return new Result<T>(error);
        }
        public static new Result<T> Fail(List<Error> errors) {
            return new Result<T>(errors);
        }
    }
}
