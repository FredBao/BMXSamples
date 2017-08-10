namespace PipelinePatternDemo
{
    using System;

    public class ClientPipelineModules
    {
        public void AddRequestHead(Request request)
        {
            request.RequestHead = "Reqeust Head From Client.";
        }

        public void TransferReqeustFormat(Request request)
        {
        }

        public void ValidateRequest(Request request)
        {
            if (request == null || string.IsNullOrEmpty(request.Content) || string.IsNullOrEmpty(request.RequestHead))
            {
                throw new Exception("请求无效");
            }
        }

        public void ZipRequestContent(Request request)
        {
        }
    }
}