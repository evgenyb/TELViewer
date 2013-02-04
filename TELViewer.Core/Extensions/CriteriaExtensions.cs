using System;
using NHibernate;
using NHibernate.Criterion;

namespace TELViewer.Core.Extensions
{
    public static class CriteriaExtensions
    {
        public static void AddEqRestrictionWithIntegerValue(this ICriteria criteria, string key, string value)
        {
            criteria.Add(Restrictions.Eq(key, int.Parse(value)));
        }

        public static void AddEqRestrictionWithStringValue(this ICriteria criteria, string key, string value)
        {
            criteria.Add(Restrictions.Eq(key, value));
        }

        public static void AddLikeRestrictionWithStringValue(this ICriteria criteria, string key, string value)
        {
            criteria.Add(Restrictions.Like(key, "%" + value + "%"));
        }

        public static void AddBetweenRestrictionWithFromToValues(this ICriteria criteria, string fromToValues)
        {
            if (fromToValues.Equals(",", StringComparison.InvariantCultureIgnoreCase)) return;

            var values = fromToValues.Split(',');
            criteria.Add(Restrictions.Between("Date", 
                DateTime.Parse(values[0]),
                DateTime.Parse(values[1])));
        }
    }
}