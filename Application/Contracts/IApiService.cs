namespace Application.Contracts
{
    public interface IApiService
    {
        Task<TResult> PostAsync<TResult>(ApiOption option, CancellationToken cancellationToken=default);
        Task PostWithOutResponseAsync(ApiOption option, CancellationToken cancellationToken = default);  

        Task<TResult> GetAsync<TResult>(ApiOption option, CancellationToken cancellationToken = default);
    }
}
