namespace Application.Contracts
{
    public interface IApiService
    {
        Task<TResult> PostAsync<TResult>(ApiOption option);

        Task<TResult> GetAsync<TResult>(ApiOption option);
    }
}
