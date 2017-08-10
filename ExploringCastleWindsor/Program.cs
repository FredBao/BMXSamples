// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ILogger type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ExploringCastleWindsor
{
    using System;
    using System.Diagnostics;

    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Castle.Windsor.Installer;

    /// <summary>
    /// The Logger interface.
    /// </summary>
    public interface IEntitiy
    {
        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void GetName();
    }

    /// <summary>
    /// The Logger interface.
    /// </summary>
    public interface ILogger : IEntitiy
    {
        string Name { get; set; }

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void Log(string message);
    }

    /// <summary>
    /// The logger.
    /// </summary>
    public class Logger : ILogger
    {
        public string Name { get; set; }

        public void GetName()
        {
            Console.WriteLine("Logger");
        }

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void Log(string message)
        {
            Console.WriteLine($"Logger1 : " + message);
        }
    }

    /// <summary>
    /// The logger.
    /// </summary>
    public class Logger2 : ILogger
    {
        public string Name { get; set; }

        public void GetName()
        {
            Console.WriteLine("Logger2");
        }

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void Log(string message)
        {
            Console.WriteLine($"Logger2 : " + message);
        }
    }

    /// <summary>
    /// The logger.
    /// </summary>
    public class Logger3 : ILogger
    {
        public string Name { get; set; }

        public void GetName()
        {
            Console.WriteLine("Logger3");
        }

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void Log(string message)
        {
            Console.WriteLine($"Logger3 {this.Name}: " + message);
        }
    }

    /// <summary>
    /// The logger.
    /// </summary>
    public class Logger4 : ILogger
    {
        public string Name { get; set; }

        public void GetName()
        {
            Console.WriteLine("Logger4");
        }

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void Log(string message)
        {
            Console.WriteLine($"Logger4 : " + message);
        }
    }

    public interface IController
    {
        void Log(string message);
    }

    public class ProductController : IController
    {
        private readonly ILogger logger;

        // Will get a OtherServiceImpl for myService.
        // MyServiceImpl would be given without the service override.
        public ProductController(ILogger logger)
        {
            this.logger = logger;
        }

        public void Log(string message)
        {
            this.logger.Log(message);
        }
    }

    public interface IFoo
    {
        void Foo();
    }

    public interface IBar
    {
        void Bar();
    }

    public interface IFooBarConfig
    {
        string Sex { get; set; }
    }

    public class FooBarConfig : IFooBarConfig
    {
        public string Sex { get; set; }
    }

    public class FooBar : IFoo, IBar
    {
        private string name;

        public string Sex { get; set; }

        public FooBar(string name)
        {
            this.name = name;
        }

        public void Bar()
        {
            Console.WriteLine($"In Bar...{this.name} {this.Sex}");
        }

        public void Foo()
        {
            Console.WriteLine($"In Foo...{this.name} {this.Sex}");
        }
    }

    /// <summary>
    /// The program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void Main(string[] args)
        {
            try
            {
                // Registering
                var container = new WindsorContainer();
                container.Register(Component.For<ILogger>().UsingFactoryMethod(() => new Logger4()));
                var logger3 = new Logger3();
                container.Register(Component.For<ILogger>().Instance(logger3));
                container.Register(Component.For<ILogger>().ImplementedBy<Logger2>().LifestyleTransient());
                container.Register(Component.For<ILogger>().ImplementedBy<Logger>().LifeStyle.Transient);
                //container.Register(Component.For<ILogger>().Instance(logger3).OnCreate((kernel, instance) => instance.Name += "aaaaaaaa"));
                //container.Register(Component.For<ILogger>().Instance(logger3).Named("myservice.default"));

                // Resolving
                //var logger = container.Resolve<ILogger>("myservice.default");
                //logger.Log("Hello World!");

                var loggers = container.ResolveAll<ILogger>();

                foreach (var logger in loggers)
                {
                    logger.Log("Hello World!");
                }

                container.Register(Component.For<IController>().ImplementedBy<ProductController>());

                var product = container.Resolve<IController>();

                product.Log("Hello World! - ProductController");

                var config = new FooBarConfig { Sex = "Test..." };

                ////container.Register(Component.For<IFoo, IBar>().ImplementedBy<FooBar>().DependsOn(Dependency.OnValue("name", "Fred")).DependsOn(Dependency.OnValue<IFooBarConfig>(config)));

                container.Register(Component.For<IFoo, IBar>().ImplementedBy<FooBar>().DependsOn(Dependency.OnValue("name", "Fred")).DependsOn(Property.ForKey("Sex").Eq("Caption Hook")));

                var foo = container.Resolve<IFoo>();

                foo.Foo();

                var bar = container.Resolve<IBar>();

                bar.Bar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadKey();
        }
    }
}
