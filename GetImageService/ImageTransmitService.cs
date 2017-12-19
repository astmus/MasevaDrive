using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace GetImageService
{    
    class ImageTransmitService : GetImageServiceContract
    {
        public Stream GetImage(string name)
        {
            WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
            return File.Open(@"d:\Images&Video\2013 Other\" + name, FileMode.Open);
        }
    }
}
