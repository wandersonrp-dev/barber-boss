using Microsoft.Extensions.Logging;
using Moq;

namespace Common.Tests.Utilities.Logger;
public class LoggerBuilder<T> where T : class
{
    public static ILogger<T> Build()
    {
        return new Mock<ILogger<T>>().Object;
    }
}
