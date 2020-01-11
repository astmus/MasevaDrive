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
        [WebGet(UriTemplate = "hid={name}")] //http://localhost:46243/hid=242E563C1393C7A3E14F875A6D858BF5
		Stream GetItem(string name);

		[WebGet(UriTemplate = "Root", BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract]
		Stream Root();
	}
}
