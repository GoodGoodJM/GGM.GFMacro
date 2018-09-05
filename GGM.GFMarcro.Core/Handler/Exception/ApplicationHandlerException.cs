namespace GGM.GFMarcro.Core.Handler.Exception
{
    public enum ApplicationHandlerExceptionType
    {
        NOT_HANDLED_EXCEPTION,
        CANNOT_FIND_APPLICATION,

    }
    public class ApplicationHandlerException : System.Exception
    {
        public ApplicationHandlerException(ApplicationHandlerExceptionType type)
            : base(GetErrorMessage(type))
        {
            ErrorType = type;
        }

        ApplicationHandlerExceptionType ErrorType { get; }

        private static string GetErrorMessage(ApplicationHandlerExceptionType type)
        {
            string message = string.Empty;
            switch (type)
            {
                case ApplicationHandlerExceptionType.CANNOT_FIND_APPLICATION:
                    message = "Cannot find application window, please check application window is opened.";
                    break;
                default:
                    message = "This exception is not expected.";
                    break;
            }
            return message;
        }
    }
}
