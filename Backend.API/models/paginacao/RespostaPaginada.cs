namespace Backend.API.Models
{
    public class RespostaPaginada<T> : Resposta<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public RespostaPaginada(T Data, bool Succeeded, string[] Errors, string Message, int PageNumber, int PageSize, 
            Uri FirstPage, Uri LastPage,  int TotalPages,  int TotalRecords,  Uri NextPage,  Uri PreviousPage)
        {
            this.Data = Data;
            this.Succeeded = Succeeded;
            this.Errors = Errors;
            this.Message = Message;
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;            
            this.FirstPage = FirstPage;
            this.LastPage = LastPage;
            this.TotalPages = TotalPages;
            this.TotalRecords = TotalRecords;
            this.NextPage = NextPage;
            this.PreviousPage = PreviousPage;
        }
    }
}