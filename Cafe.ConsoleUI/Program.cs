using Cafe.Application.Services;
using Cafe.ConsoleUI.Menus;
using Cafe.Domain.Factories;
using Cafe.Domain.Pricing;
using Cafe.Infrastructure.Factories;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IBeverageFactory, BeverageFactory>();
services.AddSingleton<IBeverageService, BeverageService>();
services.AddSingleton<MainMenu>();

var serviceProvider = services.BuildServiceProvider();
var menu = serviceProvider.GetRequiredService<MainMenu>();
menu.Run();