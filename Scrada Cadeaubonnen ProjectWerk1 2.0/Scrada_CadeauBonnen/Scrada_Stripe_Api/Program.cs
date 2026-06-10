
using Scrada_Stripe_Api.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using ScradaCadeauBonnen_BL.Interfaces;
using ScradaCadeauBonnen_DL.Repo;

namespace Scrada_Stripe_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer(); 
            builder.Services.AddSwaggerGen();           
            builder.Services.AddScoped<StripeService>();

            builder.Services.AddScoped<ITransactieRepository>(provider =>
            new TransactieRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

       

            var app = builder.Build();

            // Enable Swagger in development
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();      
                app.UseSwaggerUI();  
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.MapGet("/betaling/success", () => Results.Content("<h2>Je betaling is succesvol afgerond!</h2>", "text/html"));



            app.MapGet("/betaling/success", () =>
    Results.Content(@"
        <html>
        <head>
            <style>
                body {
                    font-family: Arial, sans-serif;
                    background: #f2f2f2;
                    display: flex;
                    justify-content: center;
                    align-items: center;
                    height: 100vh;
                    margin: 0;
                }
                .msg {
                    background: white;
                    padding: 30px 50px;
                    border-radius: 10px;
                    box-shadow: 0 0 10px rgba(0,0,0,0.1);
                    text-align: center;
                }
                .msg h2 {
                    color: green;
                    margin: 0;
                }
            </style>
        </head>
        <body>
            <div class='msg'>
                <h2>Betaling gelukt!</h2>
                <p>Bedankt voor je aankoop.</p>
            </div>
        </body>
        </html>", "text/html"));



            app.MapGet("/betaling/cancel", () =>
                Results.Content("<h2>Je hebt de betaling geannuleerd.</h2>", "text/html"));
            app.Run();

            

            

        }
    }
}
