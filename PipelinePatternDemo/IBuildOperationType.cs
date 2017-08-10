namespace PipelinePatternDemo
{
    public interface IBuildOperationType
    {
        OperationPipelineManger BuildPipeline(Request request);
    }
}