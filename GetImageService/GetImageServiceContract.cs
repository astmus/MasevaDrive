using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;

namespace GetImageService
{
    [ServiceContract]    
    interface GetImageServiceContract
    {
        [WebGet(UriTemplate = "img={name}")]
        Stream GetImage(string name);
    }
}
