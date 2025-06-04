using System.Text.Json.Serialization;
using CistProxy.Utils;
using CistProxy.Utils.Repair;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapGet("/lists/groups",
    async (HttpContext x) => { return Results.Content(Requests.GetGroupsJson(), "application/json"); });

app.MapGet("/lists/teachers",
    async (HttpContext x) => { return Results.Content(Requests.GetTeachersJson(), "application/json"); });

app.MapGet("/lists/auditories",
    async (HttpContext x) => { return Results.Content(Requests.GetAuditoriesJson(), "application/json"); });

app.MapGet("schedule/groups/{id}", async (HttpContext x, long id) =>
{
    int type = x.Request.Query.ContainsKey("type") ? int.Parse(x.Request.Query["type"]) : 0;
    var json = JsonFixers.TryFix(Requests.GetEventsJson(type, id));
    return Results.Content(json, "application/json");
});

app.MapGet("schedule/teachers/{id}", async (HttpContext x, long id) =>
    {
        int type = x.Request.Query.ContainsKey("type") ? int.Parse(x.Request.Query["type"]) : 0;
        var json = JsonFixers.TryFix(Requests.GetEventsJson(type, id));
        return Results.Content(json, "application/json");
    });

app.MapGet("schedule/auditories/{id}", async (HttpContext x, long id) =>
    {
        int type = x.Request.Query.ContainsKey("type") ? int.Parse(x.Request.Query["type"]) : 0;
        var json = JsonFixers.TryFix(Requests.GetEventsJson(type, id));
        return Results.Content(json, "application/json");
    });

app.MapOpenApi(); 
app.MapScalarApiReference("/");
app.Run();