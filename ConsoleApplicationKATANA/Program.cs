using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin.Hosting;

namespace ConsoleApplicationKATANA
{
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
        public void Configuration(IAppBuilder app)
        {


            app.UseWelcomePage(); 

            //app.Run(ctx => 
            //{
            //    return ctx.Response.WriteAsync("Hello there!");
            //});
        }




    }


}
