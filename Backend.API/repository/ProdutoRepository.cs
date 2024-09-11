using Backend.API.@interface;
using Backend.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Backend.API.repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly GetwayContext _context;

        public ProdutoRepository(GetwayContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void delete(Produto produto)
        {
            _context.Produto.Remove(produto);
        }

        public async Task<List<Produto>> findAll()
        {
            return await _context.Produto.ToListAsync();
        }

        public async Task<List<Produto>> findAll(FiltroPaginacao paginacao)
        {
            return await _context.Produto
                .Skip((paginacao.pageNumber - 1) * paginacao.pageSize)
                .Take(paginacao.pageSize)
                .ToListAsync(); 
        }

        public async Task<int> countAll()
        {
            return await _context.Produto.CountAsync();
        }

        public async Task<Produto> findByNome(string nome)
        {
            return await _context.Produto.Where(x => x.Nome == nome).FirstOrDefaultAsync();
        }

        public async Task<Produto> findById(long id)
        {
            return await _context.Produto.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void insert(Produto produto)
        {
            _context.Produto.Add(produto);
        }

        public void update(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
        }
    }
}
