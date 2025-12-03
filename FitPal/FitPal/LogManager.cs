using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitPal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class LogManager
    {
        public List<DailyLog> Logs { get; private set; } = new List<DailyLog>();

        public DailyLog GetOrCreateLog(DateTime date)
        {
            var log = Logs.FirstOrDefault(l => l.Date.Date == date.Date);

            if (log == null)
            {
                log = new DailyLog(date);
                Logs.Add(log);
            }

            return log;
        }
    }

}
