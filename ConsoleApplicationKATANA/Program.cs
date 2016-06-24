using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin.Hosting;

namespace ConsoleApplicationKATANA
{

    using System.IO;
    using AppFunc = Func<IDictionary<string, object>, Task>;

    class Program
    {
        static void Main(string[] args)
        {
            string uri = "http://localhost:8080";
            using (WebApp.Start<Startup>(uri))
            {
                Console.WriteLine("started!");               
                Console.ReadKey();
                Console.WriteLine("stopping");
            };

        }
    }

    public class Startup     
    {
        public void Configuration(IAppBuilder appl)
        {
            //1. middleware
            appl.Use(async (environment, next) =>
            {
                foreach (var pair in environment.Environment)
                {
                    Console.WriteLine("{0}:{1}", pair.Key, pair.Value);
                }
                await next();
            }
            );

            //2. middleware
            appl.Use(async (environment, next) =>
            {
                Console.WriteLine("Requesting : "+ environment.Request.Path);
                
                await next();

                Console.WriteLine("Response: " + environment.Response.StatusCode);
            }
            );


            appl.UseHelloWorld();

            //appl.Use<HelloWorldComponent>();

            //app.UseWelcomePage(); 

            //app.Run(ctx => 
            //{
            //    return ctx.Response.WriteAsync("Hello there!");
            //});
        }
    }

    public static class AppBuilderExtensions
    {
        public static void UseHelloWorld(this IAppBuilder app)
        {
            app.Use<HelloWorldComponent>();
        }
    
    }

    public class HelloWorldComponent
    {
        AppFunc _next;
        public HelloWorldComponent(AppFunc next)
        {
            _next = next;
        }
        public /*async*/ Task Invoke(IDictionary<string, object> environment)
        {
            //await _next(environment);

            var responce = environment["owin.ResponseBody"] as Stream;
            using (var writer = new StreamWriter(responce))
            {
                return writer.WriteAsync("Hello there 2!!!");
            }
        }
    }

}
