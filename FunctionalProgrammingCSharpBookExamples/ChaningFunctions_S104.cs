using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgrammingCSharpBookExamples;

internal class ChaningFunctions_S104
{
    public void Run()
    {
        var tempInF = 100m;
        var tempInCString = tempInF.Map(x => x - 32)
            .Map(x => x * 5)
            .Map(x => x / 9)
            .Map(x => Math.Round(x, 2))
            .Map(x => $"{x}°C");

        Console.WriteLine(tempInCString);

        var person = (FirstName: "Peter", LastName: "Schmidt", Age: 23, Town: "Mosbach");
        var consoleOutput = person.Fork(
            //join function
            prongs => string.Join(Environment.NewLine, prongs),

            //prongs
            x => $"My name is {x.FirstName} {x.LastName}",
            x => $"I am {x.Age} years old.",
            x => $"I live in {x.Town}"
            );
        Console.WriteLine(consoleOutput);

        (string FirstName, string LastName, int? Id, int? ActiveDirectoryId, int? EmergencyBackupCsvID) employee = (FirstName: "Peter", LastName: "Schmidt", Id: null, ActiveDirectoryId: null, EmergencyBackupCsvID: 23);
        var employeeId = employee.Alt(
                            x => x.Id,
                            x => x.ActiveDirectoryId,
                            x => x.EmergencyBackupCsvID);
        Console.WriteLine($"employeeId is: {employeeId}");

        ComposeSample();

        TransduceSample();
    }

    private void ComposeSample()
    {
        var formatDecimal = (decimal x) => x
            .Map(x => Math.Round(x, 2))
            .Map(x => $"{x} degrees");

        var tempInF = 100M;
        var celsiusToFahrenheit = (decimal x) => x
            .Map(x => x - 32)
            .Map(x => x * 5)
            .Map(x => x / 9);
        var fToCFormatted = celsiusToFahrenheit.Compose(formatDecimal);
        var output = fToCFormatted(tempInF);
        Console.WriteLine(output);

        var tempInC = 37.78M;
        var fahrenheitToCelsius = (decimal x) => x
            .Map(x => x * 9)
            .Map(x => x / 5)
            .Map(x => x + 32);
        var CTofFormatted = fahrenheitToCelsius.Compose(formatDecimal);
        var output2 = CTofFormatted(tempInC);
        Console.WriteLine(output2);


    }

    private void TransduceSample()
    {
        int[] numbers = [4, 8, 15, 16, 23, 42];

        var transformer = (IEnumerable<int> x) => x
             .Select(y => y + 5)
             .Select(y => y * 10)
             .Where(y => y > 100);
        var aggregator = (IEnumerable<int> x) => string.Join(", ", x);
        
        var output = numbers.Transduce(transformer, aggregator);
        Console.WriteLine(output);
    }
}

public static class MapExtensionMethods
{
    /// <summary>
    /// Transforming like Select from Linq but instead of executing func for
    /// every single element, Map is working on the hole Object.
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="thisObj"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static TOut Map<TIn, TOut>(this TIn thisObj, Func<TIn, TOut> func)
    {
        return func(thisObj);
    }
}

public static class ForkExtensionMethods
{
    /// <summary>
    /// Execute multiple operations on the input value and joining the results of the 
    /// single operations in the end.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    /// <param name="this"></param>
    /// <param name="joinFunc"></param>
    /// <param name="prongs"></param>
    /// <returns></returns>
    public static TOutput Fork<TInput, TOutput>(this TInput @this, Func<IEnumerable<TOutput>, TOutput> joinFunc, params Func<TInput, TOutput>[] prongs)
    {
        var intermediateValues = prongs.Select(x => x(@this));
        var returnValue = joinFunc(intermediateValues);
        return returnValue;

        // short/Linq version by using Map
        //return prongs
        //    .Select(x => x(@this))
        //    .Map(joinFunc);
    }
}

public static class AltCombinatorExtensionMethods
{
    /// <summary>
    /// Try one after another each alternative and finish after the first is working and not 
    /// returning a null value.
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="this"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    /// /// <exception cref="InvalidOperationException">Thrown when no method is working</exception>
    public static TOut Alt<TIn, TOut>(this TIn @this, params Func<TIn, TOut>[] args)
    {
        return args.Select(x => x(@this))
            .First(x => x is not null);
    }
}

public static class ComposeExtensionMethods
{
    /// <summary>
    /// Nearly the same Task as Map() but the return value is a Func delegate with
    /// the composed functionality
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="OldTOut"></typeparam>
    /// <typeparam name="NewTOut"></typeparam>
    /// <param name="this"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static Func<TIn, NewTOut> Compose<TIn, OldTOut, NewTOut>(this Func<TIn, OldTOut> @this, Func<OldTOut, NewTOut> func) =>
        x => func(@this(x));
}

public static class TransduceExtensionMethods
{
    /// <summary>
    /// Transduce = Transform and reduce 
    /// Transduce is a way of combining list-based operation with a final aggregation to receive a final result
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TFilterOut"></typeparam>
    /// <typeparam name="TFinalOut"></typeparam>
    /// <param name="this"></param>
    /// <param name="transformer"></param>
    /// <param name="aggregator"></param>
    /// <returns></returns>
    public static TFinalOut Transduce<TIn, TFilterOut, TFinalOut>(this IEnumerable<TIn> @this,
                                                                Func<IEnumerable<TIn>, IEnumerable<TFilterOut>> transformer,
                                                                Func<IEnumerable<TFilterOut>, TFinalOut> aggregator)
        // short lambda version
        //=> aggregator(transformer(@this));
    {
        var transformedItems = transformer(@this);
        var finalValue = aggregator(transformedItems);
        return finalValue;
    }
}
