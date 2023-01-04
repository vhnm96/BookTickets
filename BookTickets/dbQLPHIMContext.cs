using BookTickets.Controllers;
using System;

namespace BookTickets
{
    internal class dbQLPHIMContext
    {
        public object Movie { get; internal set; }

        internal static NguoidungController SingleOrDefault(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}