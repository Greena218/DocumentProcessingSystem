using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;

public static class DocumentDetailHelpers
{
    public static (string Text, int WordCount) ExtractTextAndWordCount(byte[] pdfBytes)
    {
        using var reader = new PdfReader(new MemoryStream(pdfBytes));
        var textBuilder = new StringBuilder();

        for (int i = 1; i <= reader.NumberOfPages; i++)
        {
            textBuilder.Append(PdfTextExtractor.GetTextFromPage(reader, i));
        }

        var text = textBuilder.ToString();
        var wordCount = text.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

        return (text, wordCount);
    }
}
