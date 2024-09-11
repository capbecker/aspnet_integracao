using Backend.API.Models;

namespace Backend.API.@interface
{
    public interface IProdutoRepository
    {
        //Task<>
        void insert(Produto produto);
        void update(Produto produto);
        void delete(Produto produto);
        Task<List<Produto>> findAll();
        Task<List<Produto>> findAll(FiltroPaginacao paginacao);
        Task<int> countAll();
        Task<Produto> findByNome(string nome);
        Task<Produto> findById(long id);
        Task<bool> SaveAllAsync();
    }
}
