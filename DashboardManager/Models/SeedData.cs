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
            using (
                DashboardManagerContext context = 
                    new DashboardManagerContext(serviceProvider.GetRequiredService<DbContextOptions<DashboardManagerContext>>())
            )
            {
                /*context.RemoveRange(context.Departement);
                context.RemoveRange(context.Client);
                context.RemoveRange(context.Commercial);
                context.SaveChanges();*/
                if (context.Departement.Any() || context.Client.Any() || context.Commercial.Any())
                {
                    return;
                }

                Departement isere       = new Departement { Name = "Isère" ,Code = 38 };
                Departement savoie      = new Departement { Name = "Savoie", Code = 73 };
                Departement var         = new Departement { Name = "Var", Code = 83 };
                Departement creuse      = new Departement { Name = "Creuse", Code = 23 };
                Departement drome       = new Departement { Name = "Drôme", Code = 26 };
                Departement herault     = new Departement { Name = "Hérault", Code = 34 };
                Departement lot         = new Departement { Name = "Lot", Code = 46 };
                Departement marne       = new Departement { Name = "Marne ", Code = 51 };
                Departement pyrenee     = new Departement { Name = "Pyrénées-orientales", Code = 66 };
                Departement paris       = new Departement { Name = "Paris", Code = 75 };
                Departement vienne      = new Departement { Name = "Vienne", Code = 86 };
                Departement mayotte     = new Departement { Name = "Mayotte", Code = 976 };

                Client teddy        = new Client { Name = "Teddy S.", Departement = isere, City = "La Sure en Chartreuse" };
                Client kevin        = new Client { Name = "Kevin L.", Departement = savoie, City = "Vinay" };
                Client nicolas      = new Client { Name = "Nicolas T.", Departement = creuse, City = "Grenoble" };
                Client matthieu     = new Client { Name = "Matthieu T.", Departement = var, City = "Bourg-en-Bresse" };
                Client anais        = new Client { Name = "Anais L.", Departement = drome, City = "Paris" };
                Client tristan      = new Client { Name = "Tristan A.", Departement = marne, City = "Voiron" };
                Client lea          = new Client { Name = "Léa A.", Departement = paris, City = "La Murette" };
                Client olivier      = new Client { Name = "Olivier B.", Departement = vienne, City = "Toulon" };
                Client guillaume    = new Client { Name = "Guillaume C.", Departement = creuse, City = "Mours-Saint-Eusèbe" };
                Client caroline     = new Client { Name = "Caroline Z.", Departement = lot, City = "Issy-les-Moulineaux" };
                Client armand       = new Client { Name = "Armand C.", Departement = herault, City = "Crossey" };
                Client clara        = new Client { Name = "Clara G.", Departement = mayotte, City = "Voreppe" };
                Client lou          = new Client { Name = "Lou A.", Departement = savoie, City = "Coublevie" };
                Client matthias     = new Client { Name = "Matthias L.", Departement = pyrenee, City = "Coublevie" };
                Client baptiste     = new Client { Name = "Baptiste B.", Departement = herault, City = "Grenoble" };

                Commercial nathan = new Commercial 
                { 
                    Name = "Nathan", 
                    Clients = new List<Client> { teddy, kevin, lou, baptiste }, 
                    CatchmentArea = "Zone 1", 
                    Departement = isere, 
                    NbContracts = 3,
                    NbQuotes = 6, 
                    CreationDate = DateTime.Now 
                };
                Commercial patrick = new Commercial
                {
                    Name = "Patrick",
                    Clients = new List<Client> { nicolas, clara, armand },
                    CatchmentArea = "Zone 2",
                    Departement = creuse,
                    NbContracts = 53,
                    NbQuotes = 235,
                    CreationDate = DateTime.Now
                };
                Commercial cedric = new Commercial
                {
                    Name = "Cedric",
                    Clients = new List<Client> { matthias, caroline, guillaume },
                    CatchmentArea = "Zone 3",
                    Departement = herault,
                    NbContracts = 16,
                    NbQuotes = 65,
                    CreationDate = DateTime.Now
                };
                Commercial isabelle = new Commercial
                {
                    Name = "Isabelle",
                    Clients = new List<Client> { matthieu, anais, tristan },
                    CatchmentArea = "Zone 4",
                    Departement = vienne,
                    NbContracts = 13,
                    NbQuotes = 15,
                    CreationDate = DateTime.Now
                };
                Commercial arnaud = new Commercial
                {
                    Name = "Arnaud",
                    Clients = new List<Client> { lea, olivier },
                    CatchmentArea = "Zone 5",
                    Departement = lot,
                    NbContracts = 0,
                    NbQuotes = 5,
                    CreationDate = DateTime.Now
                };

                context.Departement.AddRange(isere, savoie, var, creuse, drome, herault, lot, marne, pyrenee, paris, vienne, mayotte);
                context.Client.AddRange(teddy, kevin, nicolas, matthieu, anais, tristan, lea, olivier, guillaume, caroline, armand, clara, lou, matthias, baptiste);
                context.Commercial.AddRange(nathan, patrick, cedric, isabelle, arnaud);
                context.SaveChanges();
            }
        }
    }
}