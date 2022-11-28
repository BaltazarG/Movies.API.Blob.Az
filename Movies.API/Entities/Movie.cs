namespace Movies.API.Entities
{
    public class Movie
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Valoration { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public string PdfPath { get; set; } = string.Empty;
        public string ExcelPath { get; set; } = string.Empty;
    }
}
