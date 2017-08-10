namespace PipelinePatternDemo
{
    using System;

    class Program
    {
        private static void DealAppReqeust(Request reqeust)
        {
            throw new NotImplementedException();
        }

        private static void DealNETReqeust(Request reqeust)
        {
            throw new NotImplementedException();
        }

        static void Main(string[] args)
        {
            Request reqeust = new Request() { RequestHead = "1", Type = RequestType.App, Content = "balabakakabb", };
            ClientPipelineModules modules = new ClientPipelineModules();
            ClientPipelineManager manager = new ClientPipelineManager();
            manager.AddModule(modules.ValidateRequest);
            manager.AddModule(modules.AddRequestHead);
            manager.AddModule(modules.TransferReqeustFormat);
            manager.AddModule(modules.ZipRequestContent);
            manager.RunPipelineModules(reqeust);

            // ServerSideHandleLogicWithoutPipelinePattern(reqeust);
            ServerSideHandleLogicWithPipelinePattern(reqeust);
        }

        private static void ServerSideHandleLogicWithoutPipelinePattern(Request reqeust)
        {
            switch (reqeust.Type)
            {
                case RequestType.App:
                    DealAppReqeust(reqeust);
                    break;
                case RequestType.DotNET:
                    DealNETReqeust(reqeust);
                    break;
                default: throw new NotImplementedException();
            }
        }

        private static void ServerSideHandleLogicWithPipelinePattern(Request reqeust)
        {
            IBuildOperationType type = OperationFactory.Create(reqeust);
            OperationPipelineManger manager = type.BuildPipeline(reqeust);
            manager.RunPipeline(reqeust);
        }
    }
}