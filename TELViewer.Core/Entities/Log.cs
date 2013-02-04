using System;

namespace TELViewer.Core.Entities
{
    public class Log
    {
        private const int MaxVisibleTextLength = 200;

        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Thread { get; set; }
        public virtual string Level { get; set; }
        public virtual string Logger { get; set; }
        public virtual string Message { get; set; }
        public virtual string Exception { get; set; }

        public virtual string MessageToShow
        {
            get { return Message.Length > MaxVisibleTextLength ? Message.Substring(0, MaxVisibleTextLength) + "..." : Message; }
        }
    }
}