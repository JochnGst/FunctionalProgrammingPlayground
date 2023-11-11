using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgrammingPlayground
{
    public static class FunctionalHelpers
    {
        public static TToType Map<TFromType, TToType>(this TFromType @this, Func<TFromType, TToType> f)
        {
            return f(@this);
        }

        public static TOutput Fork<TInput, TOutput>(this TInput @this, Func<IEnumerable<TOutput>,TOutput> joinFunc, params Func<TInput, TOutput>[] prongs) 
        {
            return prongs
                .Select(x => x(@this))
                .Map(joinFunc);
        }

    }
}
