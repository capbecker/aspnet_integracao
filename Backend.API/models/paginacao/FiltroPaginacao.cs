namespace Backend.API.Models
{
    public class FiltroPaginacao
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public FiltroPaginacao()
        {
            this.pageNumber = 1;
            this.pageSize = 10;
        }
        public FiltroPaginacao(int pageNumber, int pageSize)
        {
            this.pageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.pageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}