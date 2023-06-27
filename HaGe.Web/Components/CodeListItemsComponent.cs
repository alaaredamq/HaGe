using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using HaGe.Application.Mapper;
using HaGe.Application.Models;
using HaGe.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HaGe.Web.Components; 

public class CodeListItemsComponent  : ViewComponent
{
    private readonly HaGeContext db;

    public CodeListItemsComponent(HaGeContext context)
    {
        db = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(string group)
    {
        var codeList = db.CodeList.Where(x => x.Group == group).ToList();
        var codeListValue = db.CodeListValue.Where(x => codeList.Select(y => y.Id).Contains(x.CodeListId)).ToList();

        var codeListModel = ObjectMapper.Mapper.Map<List<CodeListModel>>(codeList);
        var codeListValueModel = ObjectMapper.Mapper.Map<List<CodeListValueModel>>(codeListValue);

        foreach (var item in codeListModel)
        {
            item.CodeListValues = codeListValueModel.Where(x => x.CodeListId == item.Id).ToList();
        }
        
        return View(codeListModel);
    }
}