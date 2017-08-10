namespace T4GenerateMultiFiles
{
    using System;
    using System.CodeDom.Compiler;
    using System.Configuration;
    using System.IO;
    using System.Text;

    using CustomHost;

    using Microsoft.VisualStudio.TextTemplating;

    class Program
    {
        static void Main(string[] args)
        {
            string templatePath = ConfigurationManager.AppSettings["TemplatePath"].ToString();
            CustomTextTemplatingEngineHost host = new CustomTextTemplatingEngineHost();
            host.TemplateFileValue = templatePath;
            string input = File.ReadAllText(templatePath);

            string output = new Engine().ProcessTemplate(input, host);

            StringBuilder errorWarn = new StringBuilder();
            foreach (CompilerError error in host.Errors)
            {
                errorWarn.Append(error.Line).Append(":").AppendLine(error.ErrorText);
            }

            File.AppendAllText(DateTime.Now.ToString("yyyyMMdd") + ".txt", errorWarn.ToString());
            File.AppendAllText(DateTime.Now.ToString("yyyyMMdd") + ".txt", output);
        }
    }
}