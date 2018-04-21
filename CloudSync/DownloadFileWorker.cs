using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSync
{
    public class DownloadFileWorker : CloudWorker
    {
        private string link;
        public DownloadFileWorker(string link) : base()
        {
            this.link = link;
        }
        public override void DoWork()
        {
            
        }
    }
}
