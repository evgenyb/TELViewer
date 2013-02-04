using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using NHibernate;
using NHibernate.Criterion;
using TELViewer.Core.Entities;
using TELViewer.Core.Extensions;

namespace TELViewer.Core.Repositories.Impl
{
    public class LogRepository : ILogRepository
    {
        const int DefaultItemsCount = 50;

        public ICollection<Log> GetLogList(NameValueCollection searchExample)
        {
            int limit = DefaultItemsCount;
            var criteria = _session.CreateCriteria<Log>();
            foreach (string key in searchExample.Keys)
            {
                if (key.Equals("limit", StringComparison.InvariantCultureIgnoreCase))
                {
                    limit = Convert.ToInt32(searchExample[key]);
                    continue;
                }
                
                string value = searchExample[key];
                if (!string.IsNullOrEmpty(value))
                {
                    switch (key)
                    {
                        case "Id":
                            criteria.AddEqRestrictionWithIntegerValue(key, value);
                            break;
                        case "Timestamp":
                            criteria.AddBetweenRestrictionWithFromToValues(value);
                            break;
                        case "Message":
                            criteria.AddLikeRestrictionWithStringValue(key, value);
                            break;
                        case "Level":
                        case "Logger":
                            criteria.AddEqRestrictionWithStringValue(key, value);
                            break;
                        default:
                            criteria.AddEqRestrictionWithStringValue(key, value);
                            break;
                    }
                }
            }

            return criteria
                .SetMaxResults(limit)
                .AddOrder(Order.Desc("Id"))
                .List<Log>();
        }

        public IEnumerable<string> GetLoggerList()
        {
            return _session
                .CreateQuery("select distinct log.Logger from Log log order by log.Logger")
                .Future<string>();
        }

        public IEnumerable<string> GetLevelList()
        {
            return _session
                .CreateQuery("select distinct log.Level from Log log order by log.Level")
                .Future<string>();
        }

        private readonly ISession _session;

        public LogRepository(ISession session)
        {
            _session = session;
        }
    }
}