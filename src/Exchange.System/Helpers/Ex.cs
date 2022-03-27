using System;

namespace ExchangeSystem.Helpers
{
    public static class Ex
    {
        /// <exception cref="ArgumentException"></exception>
        public static void ThrowIfEmptyOrNull(string @value, string exceptionMessage = null)
        {
            exceptionMessage = exceptionMessage ?? $"{nameof(@value)} was empty or null!";
            var exception = CreateException<ArgumentException>(exceptionMessage);
            if (string.IsNullOrWhiteSpace(@value))
                throw exception;
        }

        /// <exception cref="ArgumentNullException"></exception>
        public static void ThrowIfNull<T>(T @value, string exceptionMessage = "")
        {
            exceptionMessage = exceptionMessage ?? $"{nameof(@value)} was empty or null!";
            var exception = CreateException<ArgumentNullException>(exceptionMessage);
            if (@value == null)
                throw exception;
        }

        public static void ThrowIfTrue<TException>(Func<bool> condition, string exceptionMessage = "")
            where TException : Exception
        {
            var passedException = CreateException<TException>(exceptionMessage);
            if (condition?.Invoke() ?? true)
                throw passedException;
        }

        public static void ThrowIfTrue<TException>(bool condition, string exceptionMessage = "")
            where TException : Exception
        {
            var passedException = CreateException<TException>(exceptionMessage);
            if (condition) throw passedException;
        }

        private static TException CreateException<TException>(params object[] args)
            where TException : Exception
        {
            var passedException = (TException)Activator.CreateInstance(typeof(TException), args: args);
            return passedException;
        }

        private static TException CreateException<TException>(string message)
            where TException : Exception =>
                CreateException<TException>((object)message);
    }
}
