namespace PipelinePatternDemo
{
    public delegate void OperationPipelineModules(Request request);

    public class OperationPipelineManger
    {
        public OperationPipelineModules modules { get; set; }

        public void AddModule(OperationPipelineModules module)
        {
            this.modules += module;
        }

        public void RunPipeline(Request request)
        {
            this.modules(request);
        }
    }
}