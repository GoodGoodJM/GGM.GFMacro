namespace GGM.GFMarcro.Core.Recognition.Exception
{
    public enum RecognitionExceptionType
    {
        NOT_HANDLED_EXCEPTION,
        SCREEN_IMAEG_IS_SMALLER_THAN_TARGET_IMAGE,
    }

    public class RecognitionException : System.Exception
    {
        public RecognitionException(RecognitionExceptionType type)
            : base(GetErrorMessage(type))
        {
            ErrorType = type;
        }

        public RecognitionExceptionType ErrorType { get; }

        private static string GetErrorMessage(RecognitionExceptionType type)
        {
            string message = string.Empty;
            switch (type)
            {
                case RecognitionExceptionType.SCREEN_IMAEG_IS_SMALLER_THAN_TARGET_IMAGE:
                    message = "ScreenImage is smaller than TargetImage, ScreenImage is must bigger than TargetImage.";
                    break;
                default:
                    message = "This exception is not expected.";
                    break;
            }
            return message;
        }
    }
}