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
        [WebGet(UriTemplate = "GetImage/img={name}")] //http://localhost:46243/GetImage/img=1.jpg
		Stream GetImage(string name);

		[WebGet(UriTemplate = "Page", BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract]
		Stream Connect();		
	}
}
