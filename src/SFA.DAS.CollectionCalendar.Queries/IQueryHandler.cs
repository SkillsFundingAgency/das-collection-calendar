﻿namespace SFA.DAS.CollectionCalendar.Queries
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery
    {
        Task<TResult> Handle(TQuery query, CancellationToken cancellationToken = default);
    }
}
