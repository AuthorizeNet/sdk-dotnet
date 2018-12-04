namespace AuthorizeNet.Util
{
    using System;

    public class SensitiveDataConsoleLogger : SensitiveDataTextLogger
    {
        public SensitiveDataConsoleLogger() : base (Console.Out) {
        }

        public SensitiveDataConsoleLogger(bool useErrorStream) : base (useErrorStream ? Console.Error : Console.Out) {
        }
    }
}
