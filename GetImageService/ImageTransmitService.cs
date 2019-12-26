using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace GetImageService
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
	class ImageTransmitService : GetImageServiceContract
    {
        public Stream GetImage(string name)
        {
            WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
            return File.Open(@"Z:\Images&Video\2013 Helloween\" + name, FileMode.Open);
        }

		public Stream Connect()
		{
			string result = new ImageTable(new List<string>() {"1","2","3"}).TransformText();
			byte[] resultBytes = Encoding.UTF8.GetBytes(result);
			WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
			return new MemoryStream(resultBytes);
		}
	}
}
