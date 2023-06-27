using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaGe.Application.Models;
using HaGe.Core.Entities;

namespace HaGe.Application.Interfaces; 

public interface ICodeListService {
    Task<CodeList> GetKeyValue(string keyValue);
    Task<IEnumerable<CodeListValueModel>> GetCodeListValue(Guid id);
    Task<IEnumerable<CodeListValueModel>> GetCodeAllListValue();
    Task<IEnumerable<CodeListModel>> GetCodeLists();
    Task<CodeListModel>GetCodeList (Guid id);
    Task<CodeListValueModel>GetCodeListValueByIdAsync (Guid id);
    CodeListValueModel GetCodeListValueById (Guid id);
    IQueryable<CodeListValue> GetAllValues();
    Task<CodeListValue> CreateOrUpdate(CodeListValue codeList);
    Task<CodeList> CreateOrUpdateCodeList(CodeList codeList);
    bool DeleteCodeListValue(CodeListValueModel codeList);
    int DeleteCodeList(CodeListModel codeList);
}