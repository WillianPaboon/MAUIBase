using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Models.ValueObjects.Primitives
{
    /// <summary>
    /// Represents the result of an operation, indicating success or failure.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Gets a value indicating whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Gets the message associated with the result, providing additional information.
        /// </summary>
        public string? Message { get; }

        private Result(bool isSuccess, string? message)
        {
            if (string.IsNullOrWhiteSpace(message) && !isSuccess)
            {
                throw new ArgumentException("Message cannot be empty when the process fails.", nameof(message));
            }

            IsSuccess = isSuccess;
            Message = message;
        }

        /// <summary>
        /// Creates a successful result with an optional message.
        /// </summary>
        /// <param name="message">The message associated with the success result.</param>
        /// <returns>A successful <see cref="Result"/> instance.</returns>
        public static Result Success(string? message = null) => new(true, message);

        /// <summary>
        /// Creates a failure result with an error message.
        /// </summary>
        /// <param name="error">The error message associated with the failure result.</param>
        /// <returns>A failed <see cref="Result"/> instance.</returns>
        public static Result Failure(string error) => new(false, error);
    }
}
