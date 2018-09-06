using GGM.GFMarcro.Core.Recognition.Exception;
using System.Drawing;

namespace GGM.GFMarcro.Core.Recognition
{
    using CVPoint = OpenCvSharp.Point;

    public class OpenCVRecognition : IRecognition
    {
        public SearchResult SearchImage(Bitmap screenImage, Bitmap targetImage)
        {
            if (screenImage == null)
                throw new System.ArgumentNullException(nameof(screenImage));
            if (targetImage == null)
                throw new System.ArgumentNullException(nameof(targetImage));
            if(screenImage.Width < targetImage.Width)
                throw new RecognitionException(RecognitionExceptionType.SCREEN_IMAEG_IS_SMALLER_THAN_TARGET_IMAGE);

            using (var screenMatrix = OpenCvSharp.Extensions.BitmapConverter.ToMat(screenImage))
            using (var targetMatrix = OpenCvSharp.Extensions.BitmapConverter.ToMat(targetImage))
            using (var result = screenMatrix.MatchTemplate(targetMatrix, OpenCvSharp.TemplateMatchModes.CCoeffNormed))
            {
                OpenCvSharp.Cv2.MinMaxLoc(result,
                    out double minimumSimilarity,
                    out double maximumSimilarity,
                    out CVPoint minimumPoint,
                    out CVPoint maximumPoint
                );
                return new SearchResult(minimumSimilarity, maximumSimilarity, new Point(minimumPoint.X, minimumPoint.Y), new Point(maximumPoint.X, maximumPoint.Y));
            }
            throw new System.NotImplementedException();
        }
    }
}