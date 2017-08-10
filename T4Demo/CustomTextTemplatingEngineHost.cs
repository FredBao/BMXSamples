namespace CustomHost
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using Microsoft.VisualStudio.TextTemplating;

    [Serializable]
    public class CustomTextTemplatingEngineHost : ITextTemplatingEngineHost, ITextTemplatingSessionHost
    {
        public List<int> ints = new List<int>() { 0, 1, 2, 3, 4 };

        internal string TemplateFileValue;

        private CompilerErrorCollection errorsValue;

        private Encoding fileEncodingValue = Encoding.UTF8;

        private string fileExtensionValue = ".txt";

        public CompilerErrorCollection Errors
        {
            get
            {
                return this.errorsValue;
            }
        }

        public Encoding FileEncoding
        {
            get
            {
                return this.fileEncodingValue;
            }
        }

        public string FileExtension
        {
            get
            {
                return this.fileExtensionValue;
            }
        }

        public ITextTemplatingSession Session { get; set; }

        public IList<string> StandardAssemblyReferences
        {
            get
            {
                return new[] { typeof(Uri).Assembly.Location };
            }
        }

        public IList<string> StandardImports
        {
            get
            {
                return new[] { "System" };
            }
        }

        public string TemplateFile
        {
            get
            {
                return this.TemplateFileValue;
            }
        }

        public ITextTemplatingSession CreateSession()
        {
            return this.Session;
        }

        public object GetHostOption(string optionName)
        {
            object returnObject;
            switch (optionName)
            {
                case "CacheAssemblies":
                    returnObject = true;
                    break;
                default:
                    returnObject = null;
                    break;
            }
            return returnObject;
        }

        public bool LoadIncludeText(string requestFileName, out string content, out string location)
        {
            content = string.Empty;
            location = string.Empty;

            if (File.Exists(requestFileName))
            {
                content = File.ReadAllText(requestFileName);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LogErrors(CompilerErrorCollection errors)
        {
            this.errorsValue = errors;
        }

        public AppDomain ProvideTemplatingAppDomain(string content)
        {
            return AppDomain.CreateDomain("Generation App Domain");
        }

        public string ResolveAssemblyReference(string assemblyReference)
        {
            if (File.Exists(assemblyReference))
            {
                return assemblyReference;
            }

            string candidate = Path.Combine(Path.GetDirectoryName(this.TemplateFile), assemblyReference);
            if (File.Exists(candidate))
            {
                return candidate;
            }

            return string.Empty;
        }

        public Type ResolveDirectiveProcessor(string processorName)
        {
            if (string.Compare(processorName, "XYZ", StringComparison.OrdinalIgnoreCase) == 0)
            {
                // return typeof();
            }

            throw new Exception("Directive Processor not found");
        }

        public string ResolveParameterValue(string directiveId, string processorName, string parameterName)
        {
            if (directiveId == null)
            {
                throw new ArgumentNullException("the directiveId cannot be null");
            }

            if (processorName == null)
            {
                throw new ArgumentNullException("the processorName cannot be null");
            }

            if (parameterName == null)
            {
                throw new ArgumentNullException("the parameterName cannot be null");
            }

            return string.Empty;
        }

        public string ResolvePath(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("the file name cannot be null");
            }

            if (File.Exists(fileName))
            {
                return fileName;
            }

            string candidate = Path.Combine(Path.GetDirectoryName(this.TemplateFile), fileName);
            if (File.Exists(candidate))
            {
                return candidate;
            }

            return fileName;
        }

        public void SetFileExtension(string extension)
        {
            this.fileExtensionValue = extension;
        }

        public void SetOutputEncoding(Encoding encoding, bool fromOutputDirective)
        {
            this.fileEncodingValue = encoding;
        }
    }
}