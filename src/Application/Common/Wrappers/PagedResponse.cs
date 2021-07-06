namespace DeviceManager.Application.Common.Wrappers
{
    using System.Collections.Generic;

    public record PagedResponse<T>(int PageNumber,
                                   int PageSize,
                                   int TotalCount,
                                   IReadOnlyList<T> Data);
}
