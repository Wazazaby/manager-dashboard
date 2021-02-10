using DashboardManager.Data;
using DashboardManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RazorPagesMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DashboardManagerContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DashboardManagerContext>>()))
            {
                /*foreach (var entity in context.Departement)
                    context.Departement.Remove(entity);
                foreach (var entity in context.Client)
                    context.Client.Remove(entity);
                foreach (var entity in context.Commercial)
                    context.Commercial.Remove(entity);*/

                context.Commercial.RemoveRange(context.Commercial);
                context.Departement.RemoveRange(context.Departement);
                context.Client.RemoveRange(context.Client);
                context.SaveChanges();
                // Look for any movies.
                if (context.Departement.Any() || context.Client.Any() || context.Commercial.Any())
                {
                    return;   // DB has been seeded
                }

                Departement i = new Departement { Name = "Isère" ,Code = 38 };
                Departement s = new Departement { Name = "Savoie", Code = 73 };
                Departement v = new Departement { Name = "Var", Code = 83 };

                Client t = new Client { Name = "Teddy", Departement = i, City = "La Sure en Chartreuse" };
                Client k = new Client { Name = "Kevin", Departement = v, City = "Vinay" };
                List<Client> list = new List<Client>();
                list.Add(t);
                list.Add(k);
                Commercial n = new Commercial 
                { 
                    Name = "Nicolas", 
                    Clients = list, 
                    CatchmentArea = "Zone", 
                    Departement = i, 
                    NbContracts = 3,
                    NbQuotes = 6, 
                    CreationDate = DateTime.Now 
                };
                Commercial p = new Commercial
                {
                    Name = "Patrick",
                    Clients = list,
                    CatchmentArea = "ZIP",
                    Departement = v,
                    NbContracts = 53,
                    NbQuotes = 235,
                    CreationDate = DateTime.Now
                };
                Commercial c = new Commercial
                {
                    Name = "Cedric",
                    Clients = list,
                    CatchmentArea = "Zone commercial",
                    Departement = s,
                    NbContracts = 33,
                    NbQuotes = 65,
                    CreationDate = DateTime.Now
                };
                context.Departement.AddRange(i, s, v);
                context.Client.AddRange(t, k);
                context.Commercial.AddRange(n, p, c);
                context.SaveChanges();
            }
        }
    }
}