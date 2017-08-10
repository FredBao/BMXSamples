namespace PipelinePatternDemo
{
    public class AppPipeline : IBuildOperationType
    {
        public OperationPipelineManger BuildPipeline(Request request)
        {
            OperationPipelineManger manager = new OperationPipelineManger();
            manager.AddModule(req => { req.RequestHead = "This is a App request"; });
            return manager;
        }
    }

    public class NetPipeline : IBuildOperationType
    {
        public OperationPipelineManger BuildPipeline(Request request)
        {
            OperationPipelineManger manager = new OperationPipelineManger();
            manager.AddModule(req => { req.RequestHead = "This is a Net request"; });
            return manager;
        }
    }
}