using System;
using System.IO;
using System.Text.Json.Serialization;
using Proyecto_Final_Sistema_Bancario.Utils;

var builder = WebApplication.CreateBuilder(args);

// 1) Configuración de servicios
builder.Services
    .AddControllers()
    .AddJsonOptions(opts =>
        opts.JsonSerializerOptions.Converters
            .Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 2) Preparar rutas de los JSON en la carpeta Data/JSON del proyecto
var root = app.Environment.ContentRootPath;
var dataJson = Path.Combine(root, "Data", "JSON");

var archivos = new (string Nombre, string Ruta)[]
{
    ("clientes.json",      Path.Combine(dataJson, "clientes.json")),
    ("tarjetas.json",      Path.Combine(dataJson, "tarjetas.json")),
    ("transacciones.json", Path.Combine(dataJson, "transacciones.json")),
    ("gestiones.json",     Path.Combine(dataJson, "gestiones.json"))
};

// Verificar que todos existan
foreach (var (nombre, ruta) in archivos)
{
    if (!File.Exists(ruta))
        throw new FileNotFoundException($"No se encontró el archivo {nombre} en:", ruta);
}

// 3) Carga inicial de datos desde JSON (antes de MapControllers)
CargaInicial.CargarClientes(archivos[0].Ruta);
CargaInicial.CargarTarjetas(archivos[1].Ruta);
CargaInicial.CargarTransacciones(archivos[2].Ruta);
CargaInicial.CargarGestiones(archivos[3].Ruta);

// 4) Middleware
app.UseSwagger();
app.UseSwaggerUI();
// app.UseAuthorization(); // Descomenta si agregas auth

// 5) Mapeo de controladores y lanzamiento
app.MapControllers();
app.Run();