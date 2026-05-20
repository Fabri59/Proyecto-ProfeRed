using Microsoft.AspNetCore.Identity;
using Proyecto_Red.Models;
using Proyecto_Red.Data;

namespace Proyecto_Red.Helpers
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            string[] roles = new[] { "Administrador", "Analista" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminEmail = "admin@empresa.com";
            if (await userManager.FindByEmailAsync(adminEmail) is null)
            {
                var admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    NombreCompleto = "Administrador Principal",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "Admin123$");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Administrador");
                }
            }

            // Seed Preguntas for Autodiagnóstico
            if (!context.Preguntas.Any())
            {
                var preguntas = new List<Pregunta>
                {
                    new Pregunta { Modulo = "Autodiagnóstico", PreguntaTexto = "La empresa cuenta con una visión clara y compartida por todos los empleados.", Estado = true },
                    new Pregunta { Modulo = "Autodiagnóstico", PreguntaTexto = "Los objetivos estratégicos están alineados con la misión y visión de la empresa.", Estado = true },
                    new Pregunta { Modulo = "Autodiagnóstico", PreguntaTexto = "La empresa realiza análisis periódicos del entorno interno y externo.", Estado = true },
                    new Pregunta { Modulo = "Autodiagnóstico", PreguntaTexto = "Los procesos de la empresa están documentados y estandarizados.", Estado = true },
                    new Pregunta { Modulo = "Autodiagnóstico", PreguntaTexto = "La empresa cuenta con indicadores de desempeño claros y medibles.", Estado = true },
                    new Pregunta { Modulo = "Autodiagnóstico", PreguntaTexto = "Los empleados conocen y entienden los objetivos de la empresa.", Estado = true },
                    new Pregunta { Modulo = "Autodiagnóstico", PreguntaTexto = "La empresa fomenta la innovación y la mejora continua.", Estado = true },
                    new Pregunta { Modulo = "Autodiagnóstico", PreguntaTexto = "La comunicación interna es efectiva y fluida.", Estado = true },
                    new Pregunta { Modulo = "Autodiagnóstico", PreguntaTexto = "La empresa cuenta con un sistema de gestión de calidad implementado.", Estado = true },
                    new Pregunta { Modulo = "Autodiagnóstico", PreguntaTexto = "Los recursos humanos están capacitados y motivados.", Estado = true }
                };

                context.Preguntas.AddRange(preguntas);
                await context.SaveChangesAsync();
            }
        }
    }
}
