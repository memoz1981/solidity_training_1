using InfuraDeploy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var conf = new ConfigurationBuilder()
            .AddUserSecrets<Program>()  // Load secrets
            .Build();
// Setup DI
var services = new ServiceCollection();
var configuration = conf.GetSection("Configurations").Get<Configuration>(); 

services.AddSingleton(configuration);
services.AddSingleton<IWeb3Client, Web3Client>();

var provider = services.BuildServiceProvider();

// Resolve service
var client = provider.GetRequiredService<IWeb3Client>();
var balance = await client.GetBalance();

Console.WriteLine(balance); 