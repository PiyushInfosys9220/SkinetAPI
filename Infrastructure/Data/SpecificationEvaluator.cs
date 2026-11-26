using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class SpecificationEvalutor<T> where T:BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
           
            if (spec.Criteria != null)
            {
                inputQuery = inputQuery.Where(spec.Criteria); // x =>x.Brand == brand
            }
            if(spec.OrderBy != null)
            {
                inputQuery = inputQuery.OrderBy(spec.OrderBy);
            }
            if(spec.OrderByDescending != null)
            {
                inputQuery = inputQuery.OrderByDescending(spec.OrderByDescending);
            }
            return inputQuery;
        }
    }
}
