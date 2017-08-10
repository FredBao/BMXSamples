namespace T4Demo
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Text;

    using CustomHost;

    using Microsoft.VisualStudio.TextTemplating;

    [Serializable]
    public class Entity
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int count = 9;
            List<Entity> entities = new List<Entity>();
            for (int i = 0; i < count; i++)
            {
                entities.Add(new Entity() { Name = i.ToString(), Value = DateTime.Now.ToString() });
            }

            string templatePath = ConfigurationManager.AppSettings["TemplatePath"].ToString();
            CustomTextTemplatingEngineHost host = new CustomTextTemplatingEngineHost();
            host.TemplateFileValue = templatePath;
            string input = File.ReadAllText(templatePath);
            host.Session = new TextTemplatingSession();
            host.Session.Add("entities", entities);

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