using System;
using System.IO;
using System.Net.Mail;

namespace Typesafe.Mailgun.Http
{
    /// <summary>
    /// Represents an multipart form part for a file attachment.
    /// </summary>
    public class LinkedResourceFormPart : FormPart
    {
        private readonly LinkedResource linkedResource;

        public LinkedResourceFormPart(LinkedResource linkedResource)
        {
            this.linkedResource = linkedResource;
        }

        public LinkedResource LinkedResource { get { return linkedResource; } }

        public override void WriteTo(StreamWriter writer, string boundary)
        {
            writer.Write("--{0}\r\nContent-Disposition: form-data; name=\"inline\"; filename=\"{1}\"\r\nContent-Type: {2}\r\nContent-Transfer-Encoding: base64\r\n\r\n",
                boundary,
                LinkedResource.ContentId,
                LinkedResource.ContentType.MediaType);

            var bytes = new byte[LinkedResource.ContentStream.Length];
            LinkedResource.ContentStream.Read(bytes, 0, (int)LinkedResource.ContentStream.Length);

            writer.Write(Convert.ToBase64String(bytes));
            writer.Write("\r\n");
        }
    }
}