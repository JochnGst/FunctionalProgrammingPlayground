using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgrammingPlayground
{
    public abstract class Maybe<T>
    {
        public abstract T Value { get; }
        public static implicit operator T(Maybe<T> thisObj) => thisObj.Value;
    }

    public class Some<T> : Maybe<T>
    {
        public override T Value { get; }

        public Some(T val)
        {
            Value = val;
        }
    }

    public class Nothing<T> : Maybe<T>
    {
        public override T? Value => default;
    }

    public class Error<T> : Maybe<T>
    {
        public Error() { }
        public Error(string message)
        {
            Message = message;
        }
        public override T? Value => default;
        public string Message { get; set; } = string.Empty;
    }

    public static class MaybeExtentions
    {
        public static Maybe<T> ToMaybe<T>(this T thisObj) => new Some<T>(thisObj);

        public static Maybe<TToType> Bind<TFromType, TToType>(this Maybe<TFromType> thisObj, Func<TFromType, TToType> func)
        {
            switch (thisObj)
            {
                case Some<TFromType> some when !EqualityComparer<TFromType>.Default.Equals(some.Value, default(TFromType)):
                    try
                    {
                        return func(some).ToMaybe();
                    }
                    catch (Exception e)
                    {
                        return new Error<TToType>(e.Message);
                    }
                default:
                    return new Nothing<TToType>();
            }
        }
    }
}
