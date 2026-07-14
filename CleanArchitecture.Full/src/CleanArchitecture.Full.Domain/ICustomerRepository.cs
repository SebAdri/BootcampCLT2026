namespace CleanArchitecture.Full.Domain;

public interface ICustomerRepository
{
    Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
    void Update(Customer customer);
    void Remove(Customer customer);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}
