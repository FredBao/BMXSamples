namespace PipelinePatternDemo
{
    public class OperationFactory
    {
        public static IBuildOperationType Create(Request request)
        {
            if (request.Type == RequestType.App)
            {
                return new AppPipeline();
            }
            else
            {
                return new NetPipeline();
            }
        }
    }
}