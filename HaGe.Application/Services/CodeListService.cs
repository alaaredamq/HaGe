using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaGe.Application.Interfaces;
using HaGe.Application.Mapper;
using HaGe.Application.Models;
using HaGe.Core.Entities;
using HaGe.Core.Repositories;

namespace HaGe.Application.Services;

public class CodeListService : ICodeListService {
    private readonly ICodeListRepository _codeListRepository;
    private readonly ICodeListValueRepository _codeListValueRepository;

    public CodeListService(ICodeListRepository codeListRepository, ICodeListValueRepository codeListValueRepository) {
        _codeListRepository = codeListRepository ?? throw new ArgumentNullException(nameof(codeListRepository));
        _codeListValueRepository =
            codeListValueRepository ?? throw new ArgumentNullException(nameof(codeListValueRepository));
        _codeListValueRepository = codeListValueRepository;
    }

    public async Task<IEnumerable<CodeListValueModel>> GetCodeListValue(Guid codeListId) {
        var codeLists = _codeListValueRepository.ListAllAsync().Result;
        var codeList = codeLists.Where(x => x.CodeListId == codeListId);
        var retCodeList = ObjectMapper.Mapper.Map<IEnumerable<CodeListValueModel>>(codeList);
        return retCodeList;
    }

    public async Task<IEnumerable<CodeListValueModel>> GetCodeAllListValue() {
        var codeLists = _codeListValueRepository.ListAllAsync().Result;
        var retCodeList = ObjectMapper.Mapper.Map<IEnumerable<CodeListValueModel>>(codeLists);
        return retCodeList;
    }

    public async Task<IEnumerable<CodeListModel>> GetCodeLists() {
        var codeLists = _codeListRepository.ListAllAsync().Result;
        var retCodeList = ObjectMapper.Mapper.Map<IEnumerable<CodeListModel>>(codeLists);
        return retCodeList;
    }
    
    public async Task<CodeListModel>GetCodeList (Guid id) {
        var codeList = _codeListRepository.GetByIdAsync(id).Result;
        var retCodeList = ObjectMapper.Mapper.Map<CodeListModel>(codeList);
        return retCodeList;
    }
    
    public async Task<CodeListValueModel>GetCodeListValueByIdAsync (Guid id) {
        var codeList = _codeListValueRepository.GetByIdAsync(id).Result;
        var retCodeList = ObjectMapper.Mapper.Map<CodeListValueModel>(codeList);
        return retCodeList;
    }
    
    public CodeListValueModel GetCodeListValueById (Guid id) {
        var codeList = _codeListValueRepository.GetById(id);
        var retCodeList = ObjectMapper.Mapper.Map<CodeListValueModel>(codeList);
        return retCodeList;
    }

    public async Task<CodeList> GetKeyValue(string keyValue) {
        var codeLists = await _codeListRepository.ListAllAsync();
        return codeLists.FirstOrDefault(x => x.KeyValue == keyValue);
    }

    public CodeListValue GetCodeListById(Guid id) {
        return _codeListValueRepository.GetById(id);
    }

    public IQueryable<CodeListValue> GetAllValues() {
        return _codeListValueRepository.Table;
    }

    public Task<CodeListValue> CreateOrUpdate(CodeListValue codeList) {
        return _codeListValueRepository.SaveAsync(codeList);
    }

    public Task<CodeList> CreateOrUpdateCodeList(CodeList codeList){
        return _codeListRepository.SaveAsync(codeList);
    }
    
    public bool DeleteCodeListValue(CodeListValueModel codeList){
        var codeListValue = _codeListValueRepository.GetByIdAsync(codeList.Id).Result;
        _codeListValueRepository.Delete(codeListValue);
        return true;
    }
    
    public async Task<bool> DeleteCodeListAsync(CodeListModel codeList){
        var codeListValue = _codeListRepository.GetByIdAsync(codeList.Id).Result;
        _codeListRepository.DeleteAsync(codeListValue);
        return true;
    }
    
    public int DeleteCodeList(CodeListModel codeList){
        var codeListValue = _codeListRepository.GetById(codeList.Id);
        return _codeListRepository.Delete(codeListValue);
    }

}