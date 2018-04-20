using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSync.Models
{
    public class OneDriveFolder
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public uint Size { get; set; }

        public string FormattedSize
        {
            get {
                string[] suf = { "B", "KB", "MB", "GB", "TB"}; //Longs run out around EB
                if (Size == 0)
                    return "0 " + suf[0];
                long bytes = Math.Abs(Size);
                int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
                double num = Math.Round(bytes / Math.Pow(1024, place), 1);
                return (Math.Sign(Size) * num)+ " "+ suf[place];
            }
        }
    }


}
