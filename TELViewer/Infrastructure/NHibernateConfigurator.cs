using System.Configuration;
using HibernatingRhinos.NHibernate.Profiler.Appender;
using NHibernate;
using Configuration = NHibernate.Cfg.Configuration;

namespace TELViewer.Infrastructure
{
    public class NHibernateConfigurator
    {
        readonly Configuration _configuration;

        public NHibernateConfigurator()
        {
            _configuration = new Configuration();
            _configuration.Configure();
            _configuration.Properties.Add("use_proxy_validator", "false");
            if (NHProfilerIsEnable())
            {
                NHibernateProfiler.Initialize();
            }
        }

        public ISessionFactory CreateSessionFactory()
        {
            return _configuration.BuildSessionFactory();
        }

        private static bool NHProfilerIsEnable()
        {
            var enableNHProfilerString = ConfigurationManager.AppSettings["EnableNHProfiler"];
            if (string.IsNullOrEmpty(enableNHProfilerString))
            {
                return false;
            }

            bool enableNHProfiler;
            bool.TryParse(enableNHProfilerString, out enableNHProfiler);

            return enableNHProfiler;
        }
    }
}