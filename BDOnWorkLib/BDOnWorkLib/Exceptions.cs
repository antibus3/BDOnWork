using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDOnWorkLib
{
    class ErrorInsertExceptions : Exception
    {
        public ErrorInsertExceptions (string message)
        : base(message)
          { }
    }

    class ErrorDeleteException : Exception
    {
        public ErrorDeleteException (string message)
            : base (message)
        { }
    }

    class ErrorUpdateException : Exception
    {
        public ErrorUpdateException(string message)
            : base(message)
        { }
    }
}

