namespace Results
{
    /// <summary>
    ///     Defines standard error codes used in the Result pattern.
    /// </summary>
    /// <remarks>
    ///     Error codes provide a standardized way to categorize errors. The enum is defined as a uint to allow for a
    ///     wide range of error codes and compatibility with common error code conventions.
    /// </remarks>
    public enum ErrorCode : uint
    {
        /// <summary>
        ///     Represents no error (value: 0x00000000).
        /// </summary>
        /// <remarks>
        ///     This is the default value and indicates that no error has occurred. Used primarily for default error
        ///     instances.
        /// </remarks>
        None = 0x00000000,

        /// <summary>
        ///     Represents an error caused by an exception (value: 0x8333ffff).
        /// </summary>
        /// <remarks>
        ///     This code is used when an exception is converted to an <see cref="Error" /> object via the
        ///     <see cref="Error.FromException(Exception)" /> method.
        /// </remarks>
        Exception = 0x8333ffff,

        /// <summary>
        ///     Represents an unexpected or uncategorized error (value: 0xffffffff).
        /// </summary>
        /// <remarks>
        ///     This is used for errors that don't fit any specific category or when the exact cause of an error is
        ///     unknown.
        /// </remarks>
        Unexpected = 0xffffffff
    }
}