using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Todo.WebApp.DataModels
{
    public class ErrorResponse
    {
        public IReadOnlyList<string> Errors { get; }

        public ErrorResponse(IEnumerable<string> errorMessages)
        {
            if (null == errorMessages)
            {
                throw new ArgumentNullException(nameof(errorMessages));
            }

            if (errorMessages.Any(e => String.IsNullOrWhiteSpace(e)))
            {
                throw new ArgumentException("Errors must not be null, empty, or whitespace only");
            }

            if (!errorMessages.Any())
            {
                throw new ArgumentException("At least 1 error required");
            }

            this.Errors = new ReadOnlyCollection<string>(errorMessages.ToArray());
        }

        public ErrorResponse(params string[] errorMessages)
            : this((IEnumerable<string>)errorMessages)
        { }
    }
}
