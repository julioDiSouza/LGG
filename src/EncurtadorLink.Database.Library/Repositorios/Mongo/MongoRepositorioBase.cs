using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using EncurtadorLink.Entities.Library.Mongo;
using EncurtadorLink.Database.Library.Contextos;
using EncurtadorLink.Database.Library.Repositorios.Mongo.Interfaces;

namespace EncurtadorLink.Database.Library.Repositorios.Mongo
{
    public class MongoRepositorioBase<T> : IMongoRepositorioBase<T> where T : EntidadeId
    {
        private readonly IMongoCollection<T> _colecao;

        private FilterDefinitionBuilder<T> FilterBuilder => Builders<T>.Filter;
        private FilterDefinition<T> FiltroPorId(string id) => FilterBuilder.Eq(p => p.Id, id);
        private FilterDefinition<T> FiltroPorIds(IEnumerable<string> ids) => FilterBuilder.In(p => p.Id, ids);
        private Expression<Func<T, T>> CamposProjecao { get; set; }

        protected void AplicarProjecao(Expression<Func<T, T>> projecao)
        {
            CamposProjecao = projecao;
        }

        public MongoRepositorioBase(MongoDbContext contexto, string colecao)
        {
            _colecao = contexto.DataBase.GetCollection<T>(colecao);
        }

        public async Task AdicionarAsync(T entidade)
        {
            await _colecao.InsertOneAsync(entidade);
        }

        public async Task AdicionarVariosAsync(IEnumerable<T> entidades)
        {
            await _colecao.InsertManyAsync(entidades);
        }

        public async Task AtualizarAsync(FilterDefinition<T> filtro, UpdateDefinition<T> atualizador, UpdateOptions updateOptions)
        {
            await _colecao.UpdateOneAsync(filtro, atualizador, options: updateOptions);
        }

        public async Task AtualizarVariosAsync(FilterDefinition<T> filtro, UpdateDefinition<T> atualizador, UpdateOptions updateOptions)
        {
            await _colecao.UpdateManyAsync(filtro, atualizador, options: updateOptions);
        }

        public IQueryable<T> Consultar(Expression<Func<T, bool>> query)
        {
            return _colecao.AsQueryable().Where(query);
        }

        public async Task<T> ConsultarPrimeiroAsync(Expression<Func<T, bool>> query)
        {
            return await _colecao.AsQueryable().FirstOrDefaultAsync(query);
        }

        public async Task<T> ConsultarPorIdAsync(string id)
        {
            if (CamposProjecao is null)
                return (await Consultar(p => p.Id == id).FirstOrDefaultAsync())!;

            return (await Consultar(p => p.Id == id).Select(CamposProjecao).FirstOrDefaultAsync())!;
        }

        public async Task<IEnumerable<T>> ConsultarPorIdsAsync(IEnumerable<string> ids)
        {
            if (CamposProjecao is null)
                return await Consultar(p => ids.Contains(p.Id)).ToListAsync();

            return await Consultar(p => ids.Contains(p.Id)).Select(CamposProjecao).ToListAsync();
        }

        public async Task DeletarAsync(string id)
        {
            await _colecao.DeleteOneAsync(FiltroPorId(id));
        }

        public async Task DeletarVariosAsync(FilterDefinition<T> filtro)
        {
            await _colecao.DeleteManyAsync(filtro);
        }

        public async Task DeletarVariosAsync(Expression<Func<T, bool>> query)
        {
            await _colecao.DeleteManyAsync(query);
        }

        public async Task<bool> ExisteAsync(string id)
        {
            return await Consultar(e => e.Id == id).AnyAsync();
        }

        public async Task<bool> ExisteAsync(Expression<Func<T, bool>> query)
        {
            return await _colecao.AsQueryable().AnyAsync(query);
        }
    }
}
