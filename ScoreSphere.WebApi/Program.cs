using Microsoft.EntityFrameworkCore;
using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.BusinessLayer.Concrete;
using ScoreSphere.BusinessLayer.Mapping;
using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Concrete;
using ScoreSphere.DataAccessLayer.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(cfg => { }, typeof(GeneralMapping));
builder.Services.AddScoped<ITeamDal, EfTeamDal>();
builder.Services.AddScoped<ILeagueDal, EfLeagueDal>();
builder.Services.AddScoped<ISeasonDal, EfSeasonDal>();
builder.Services.AddScoped<IMatchDal, EfMatchDal>();
builder.Services.AddScoped<IGoalDal, EfGoalDal>();
builder.Services.AddScoped<IMatchEventDal, EfMatchEventDal>();
builder.Services.AddScoped<IMatchStatDal, EfMatchStatDal>();

builder.Services.AddScoped<ITeamService, TeamManager>();
builder.Services.AddScoped<ILeagueService, LeagueManager>();
builder.Services.AddScoped<ISeasonService, SeasonManager>();
builder.Services.AddScoped<IMatchService, MatchManager>();
builder.Services.AddScoped<IGoalService, GoalManager>();
builder.Services.AddScoped<IMatchEventService, MatchEventManager>();
builder.Services.AddScoped<IMatchStatService, MatchStatManager>();
builder.Services.AddScoped<IStandingService, StandingManager>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
