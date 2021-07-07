﻿namespace DeviceManager.Application.Common.Interfaces.Repositories
{
    using DeviceManager.Application.Common.Wrappers;
    using DeviceManager.Domain.Common;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGenericRepository<T, TKey>
        where T : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<T> GetByIdAsync(TKey id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<PagedResponse<T>> GetPagedReponseAsync(int pageNumber, int pageSize);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}