using AutoMapper.Internal.Mappers;
using HaGe.Application.Interfaces;
using HaGe.Application.Mapper;
using HaGe.Application.Models;
using HaGe.Core.Entities;
using HaGe.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HandGesture___Web.Admin.Controllers;

[Area("Admin")]
[Route("Admin/[controller]/[action]")]
public class SettingsController : WebBaseController {
    private ICodeListService _codeListService;

    public SettingsController(ICodeListService codeListService) {
        _codeListService = codeListService;
    }

    public IActionResult CodeList() {
        var model = new SettingsModel();

        model.AreaName = "Admin";
        model.ControllerName = "Settings";
        model.ActionName = "CodeList";
        model.Title = "CodeList";

        var codeListValue = _codeListService.GetCodeAllListValue();
        var codeList = _codeListService.GetCodeLists().Result;
        if (codeListValue.IsCompleted) {
            var result = codeListValue.Result;
            foreach (var item in codeList) {
                item.CodeListValues = result.Where(x => x.CodeListId == item.Id).ToList();
            }

            model.CodeLists = codeList.GroupBy(x => x.Group);

            return View(model);
        }

        return Json(new { success = false, message = codeListValue.Exception });
    }

    public IActionResult GetCodeLists(int count, string group) {
        return ViewComponent("CodeListItemsComponent", new {
            count = count,
            group = group
        });
    }

    [HttpGet]
    public IActionResult GetCodeListValue(Guid id) {
        var codeListValue = _codeListService.GetCodeListValueById(id);
        if (codeListValue != null) {
            return Json(new { success = true, message = "", data = codeListValue });
        }
        else {
            return Json(new { success = false, message = "CodeListValue not found" });
        }
    }

    [HttpPost]
    public JsonResult SubmitCodeListValueRecord(SubmitCodeListValueForm form) {
        if (form.Id != Guid.Empty) {
            var codeList = _codeListService.GetCodeListValueById(form.Id);
            codeList.Title = form.Title;
            codeList.Description = form.Title;
            codeList.CodeListId = codeList.CodeListId;

            codeList.CodeListId = codeList.CodeListId;
            codeList.ModificationDate = DateTime.Now;

            db.SaveChanges();
            return Json(new { success = true, message = "" });
        }
        else {
            var codeList = new CodeListValue(form.Title, form.Title, form.CodeListId);
            var result = _codeListService.CreateOrUpdate(codeList).Result;
            form.Id = codeList.Id;
        }

        return Json(new { success = true, message = "", data = form.Id });
    }

    [HttpPost]
    public JsonResult SubmitCodeListRecord(SubmitCodeListForm form) {
        if (!string.IsNullOrEmpty(form.Name)) {
            if (form.Id != null && form.Id != Guid.Empty) {
                var id = form.Id;
                var codeList = _codeListService.GetCodeList(id.Value).Result;
                codeList.Title = form.Name;
                codeList.KeyValue = form.Name.Replace(" ", "");
                codeList.Group = form.Group;
                codeList.ModificationDate = DateTime.Now;

                db.SaveChanges();
            }
            else {
                var exist = _codeListService.GetKeyValue(form.Name.ToLower().Replace(" ", ""));
                if (exist.Result != null) {
                    return Json(new { success = false, message = "CodeList Name already exist" });
                }

                var codeList = new CodeList(form.Name, form.Name.ToLower().Replace(" ", ""), form.Group);
                var result = _codeListService.CreateOrUpdateCodeList(codeList).Result;
                db.SaveChanges();
            }

            return Json(new { success = true, message = "CodeList Created successfully" });
        }
        else {
            return Json(new { success = false, message = "Invalid CodeList Name" });
        }
    }

    [HttpDelete]
    public JsonResult DeleteCodeListRecord(Guid id) {
        var codeList = _codeListService.GetCodeList(id).Result;
        var result = _codeListService.DeleteCodeList(codeList);
        return Json(new
            { success = result, message = result == 1 ? "CodeList deleted successfully" : "Error deleting CodeList" });
    }

    // [HttpPut]
    // public IActionResult UpdateCodeList(Guid id) {
        // var codelist = _codeListService.Up
    // }

    #region Nested Classes

    public class SubmitCodeListValueForm {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public string Summary { get; set; }
        public Guid CodeListId { get; set; }

        public SubmitCodeListValueForm() {
        }
    }

    public class SubmitCodeListForm {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }

        public SubmitCodeListForm() {
        }
    }

    #endregion
}