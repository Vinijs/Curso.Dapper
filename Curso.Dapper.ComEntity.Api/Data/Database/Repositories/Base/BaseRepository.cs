using Curso.Dapper.ComEntity.Api.Data.Database.Context;
using Curso.Dapper.ComEntity.Api.Data.Database.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace Curso.Dapper.ComEntity.Api.Data.Database.Repositories.Base;

public abstract class BaseRepository
{
    protected readonly IUnitOfWork UnitOfWork;
    protected readonly CursoDapperContext Context;
    protected readonly ILogger<BaseRepository> Logger;

    private readonly IAsyncPolicy _retryPolicy;
    private const int _retryCount = 3;


    protected BaseRepository(IUnitOfWork unitOfWork,
                             CursoDapperContext context,
                             ILogger<BaseRepository> logger)
    {
        UnitOfWork = unitOfWork;
        Context = context;
        Logger = logger;

        _retryPolicy = Policy.Handle<Exception>()
            .WaitAndRetryAsync(_retryCount, 
            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            onRetry : LogOnRetry);
    }

    protected async Task ExecuteWithRetry(string sql, object? param = null) 
        => await _retryPolicy.ExecuteAsync(async () =>
        {
            await Context.Database.GetDbConnection()
                    .ExecuteAsync(sql, param, UnitOfWork.Transaction);
        });

    protected async Task<TReturn> QueryFirstOrDefaultWithRetry<TReturn>(string sql, object? param = null) 
        => await _retryPolicy.ExecuteAsync(async () =>
        {
            return await Context.Database.GetDbConnection()
            .QueryFirstOrDefaultAsync<TReturn>(sql, param, UnitOfWork.Transaction);
        });

    protected async Task<IEnumerable<TReturn>> QueryWithRetry<TReturn>(string sql, object? param = null) 
        => await _retryPolicy.ExecuteAsync(async () =>
        {
            return await Context.Database.GetDbConnection()
                   .QueryAsync<TReturn>(sql, param, UnitOfWork.Transaction);
        });

    private void LogOnRetry(Exception exception, TimeSpan span, int tentativa, Polly.Context context)
    {
        Logger.LogWarning(exception, $"Tentativa {tentativa} de {context.PolicyKey} em {span.Seconds} segundos");
    }


}
