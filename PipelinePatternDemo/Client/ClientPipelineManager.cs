namespace PipelinePatternDemo
{
    public delegate void ClientPipelineModule(Request request);

    public class ClientPipelineManager
    {
        public ClientPipelineModule modules { get; set; }

        public void AddModule(ClientPipelineModule module)
        {
            this.modules += module;
        }

        public void RemoveModule(ClientPipelineModule module)
        {
            this.modules -= module;
        }

        public void RunPipelineModules(Request request)
        {
            this.modules(request);
        }
    }
}