using ZaylandShop.ServiceTemplate.Web;

CreateHostBuilder(args).ConfigureAppConfiguration((hostingContext, config) =>
    {
        var env = hostingContext.HostingEnvironment;
        config.AddJsonFile("appsettings.json", optional: true,
                reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                optional: true, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureLogging((hostingContext, logging) =>
    {
        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
        logging.AddDebug();
        logging.AddFile("Logs\\log-{Date}.txt");
    })
    .Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseIISIntegration();
            webBuilder.UseStartup<Startup>();
        });