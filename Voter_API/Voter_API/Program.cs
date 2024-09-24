using Microsoft.AspNetCore.Mvc;
using Voter_API.Controllers;
using Voter_API.Interfaces;
using Voter_API.Models;
using Voter_API.Repository;
using Voter_API.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IVOTERRepository, VoterRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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


//private InterfaceItem<VoterModel> IVOTERRepository;


//public VoterController(InterfaceItem<VoterModel> VoterRepository)
//{
//    IVOTERRepository = VoterRepository;

//}

//[HttpGet]
//[Route("Person")]
//public List<VoterModel> Person()
//{
//    List<VoterModel> student = IVOTERRepository.getItem(0);
//    return student;
//}



//[HttpPost]
//[Route("savePerson")]
//public int savePerson(VoterModel model)
//{
//    try
//    {
//        int r;


//        if (model.VOTER_KEY == null)
//        {
//            model.VOTER_KEY = 0;
//        }

//        if (model.VOTER_KEY == 0)
//        {
//            r = IVOTERRepository.saveItem(model, "INSERT");



//        }
//        else
//        {
//            r = IVOTERRepository.saveItem(model, "UPDATE");


//        }

//        return r;



//    }

//    catch (Exception ex)
//    {
//        throw ex;
//    }


//}



//[HttpGet]
//[Route("DeletePerson/{id}")]
//public int DeletePerson(int id)
//{
//    VoterModel model = new VoterModel();
//    model.VOTER_KEY = id;
//    int r = IVOTERRepository.deleteItem(model, "DELETE");

//    return r;
//}


//[HttpGet]
//[Route("EditPerson/{id}")]
//public VoterModel EditPerson(int id)
//{

//    var lstobj = IVOTERRepository.getItem(id);
//    if (lstobj.Count == 1)
//    {
//        return lstobj[0];
//    }

//    return null;
//}





