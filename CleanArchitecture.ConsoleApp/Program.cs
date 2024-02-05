using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

StreamerDbContext dbContext = new();


//await AddNewRecords();
//QueryStreaming();
//await QueryFilterEF();
await QueryLinq();
async Task QueryFilter() 
{
    Console.WriteLine("Ingrese una compania de streaming:");
    var busqueda = Console.ReadLine();
    var streamers = await dbContext!.Streamers!.Where(p => p.Nombre.Contains(busqueda)).ToListAsync();
    Console.WriteLine("Resultados de tu búsqueda:");
    foreach( var streamer in streamers)
    {
        Console.WriteLine(streamer.Nombre);
    }
}

async Task QueryLinq() 
{
    Console.WriteLine("Ingrese una compania de streaming:");
    var busqueda = Console.ReadLine();
    var streamers = await (from i in dbContext!.Streamers!
                           where EF.Functions.Like(i.Nombre, $"%{busqueda}%")
                           select i).ToListAsync();
    foreach (var streamer in streamers)
    {
        Console.WriteLine(streamer.Nombre);
    }
}


async Task QueryFilterEF()
{
    Console.WriteLine("Ingrese una compania de streaming:");
    var busqueda = Console.ReadLine();
    var streamers = await dbContext!.Streamers!.Where(p => EF.Functions.Like(p.Nombre, $"%{busqueda}%")).ToListAsync();
    Console.WriteLine("Resultados de tu búsqueda:");
    foreach (var streamer in streamers)
    {
        Console.WriteLine(streamer.Nombre);
    }
}
void QueryStreaming() 
{
    var streamers = dbContext!.Streamers!.ToList();
    
    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}

async Task AddNewRecords() 
{
    Streamer streamer = new()
    {
        Nombre = "Disney",
        Url = "https://disney.com"
    };

    dbContext!.Streamers!.Add(streamer);
    await dbContext.SaveChangesAsync();

    var peliculas = new List<Video>
{
    new Video
    {
        Nombre = "UP",
        StreamerId = streamer.Id
    },
    new Video
    {
        Nombre = "Lilo y Stitch",
        StreamerId = streamer.Id
    }

};
    await dbContext.AddRangeAsync(peliculas);
    await dbContext.SaveChangesAsync();
}
