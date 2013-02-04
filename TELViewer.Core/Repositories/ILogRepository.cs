using System.Collections.Generic;
using System.Collections.Specialized;
using TELViewer.Core.Entities;

namespace TELViewer.Core.Repositories
{
    public interface ILogRepository
    {
        ICollection<Log> GetLogList(NameValueCollection searchExample);
        IEnumerable<string> GetLoggerList();
        IEnumerable<string> GetLevelList();
    }
}