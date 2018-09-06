namespace GGM.GFMarcro.Core.Recognition
{
    public interface IRecognition
    {
        SearchResult SearchImage(System.Drawing.Bitmap screenImage, System.Drawing.Bitmap targetImage);
    }
}