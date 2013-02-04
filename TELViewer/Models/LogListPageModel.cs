using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Mvc;
using TELViewer.Core.Entities;
using TELViewer.Core.Repositories;

namespace TELViewer.Models
{
    public class LogListPageModel
    {
        public void InitializeModel(NameValueCollection queryString)
        {
            _queryString = queryString;
            GetSearchResult();
        }

        private void GetDropDownListData()
        {
            if (_loggerListData == null)
            {
                _loggerListData = _logRepository.GetLoggerList();
            }
            if (_levelListData == null)
            {
                _levelListData = _logRepository.GetLevelList();
            }
        }

        private void FillLevelList()
        {
            if (_levelList != null)
                return;

            _levelList = new List<SelectListItem>();
            foreach (var level in _levelListData)
            {
                _levelList.Add(new SelectListItem { Text = level, Value = level });
            }
            _levelList.Insert(0, new SelectListItem { Text = "All", Value = string.Empty });
        }

        private void FillLoggerList()
        {
            if (_loggerList != null)
                return;
            
            _loggerList = new List<SelectListItem>();
            foreach (var logger in _loggerListData)
            {
                _loggerList.Add(new SelectListItem { Text = logger, Value = logger });
            }
            _loggerList.Insert(0, new SelectListItem { Text = "All", Value = string.Empty });
        }

        public ICollection<Log> Logs { get; private set; }

        public string Logger 
        { 
            get
            {
                return !string.IsNullOrEmpty(_queryString["Logger"]) ? _queryString["Logger"] : string.Empty;
            }
        }

        public string Level
        {
            get
            {
                return !string.IsNullOrEmpty(_queryString["Level"]) ? _queryString["Level"] : string.Empty;
            }
        }

        public int Limit
        {
            get
            {
                return !string.IsNullOrEmpty(_queryString["Limit"]) ? int.Parse(_queryString["Limit"]) : 0;
            }
        }

        public string Message
        {
            get
            {
                return !string.IsNullOrEmpty(_queryString["Message"]) ? _queryString["Message"] : string.Empty;
            }
        }

        public string TimestampFrom
        {
            get
            {
                if (!string.IsNullOrEmpty(_queryString["Timestamp"]))
                {
                    var values = _queryString["Timestamp"].Split(',');
                    return values[0];                                    
                }
                return string.Empty;
            }
        }
        
        public string TimestampTo
        {
            get
            {
                if (!string.IsNullOrEmpty(_queryString["Timestamp"]))
                {
                    var values = _queryString["Timestamp"].Split(',');
                    return values[1];
                }
                return string.Empty;
            }
        }

        public string Id
        {
            get
            {
                return !string.IsNullOrEmpty(_queryString["Id"]) ? _queryString["Id"] : string.Empty;
            }             
        }

        private void GetSearchResult()
        {
            Logs = _logRepository.GetLogList(_queryString);
        }

        public IEnumerable<SelectListItem> GetLoggerList(string logger)
        {
            return new MultiSelectList(_loggerList, "Value", "Text", logger);
        }

        public IEnumerable<SelectListItem> GetLevelList(string level)
        {
            return new MultiSelectList(_levelList, "Value", "Text", level);
        }

        public IEnumerable<SelectListItem> GetLimitList(int limit)
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem {Text = "10", Value = "10"},
                new SelectListItem {Text = "20", Value = "20"},
                new SelectListItem {Text = "30", Value = "30"},
                new SelectListItem {Text = "40", Value = "40"},
                new SelectListItem {Text = "50", Value = "50"},
                new SelectListItem {Text = "100", Value = "100"},
            };

            return new MultiSelectList(list, "Value", "Text", limit.ToString());
        }

        public LogListPageModel(ILogRepository logRepository)
        {
            _logRepository = logRepository;
            GetDropDownListData();

            FillLoggerList();
            FillLevelList();
        }

        private List<SelectListItem> _loggerList;
        private static IEnumerable<string> _loggerListData;
        
        private List<SelectListItem> _levelList;
        private static IEnumerable<string> _levelListData;
        
        private readonly ILogRepository _logRepository;
        private NameValueCollection _queryString;

        
    }
}