﻿using Microsoft.Extensions.Configuration;

namespace Aura.Application.Helper;
public static class Configration
{
    public static IConfiguration config;

    static Configration()
    {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config = builder.Build();
    }
}
