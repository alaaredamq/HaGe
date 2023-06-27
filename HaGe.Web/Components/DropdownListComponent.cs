using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HaGe.Application.Helpers;
using HaGe.Application.Interfaces;
using HaGe.Infrastructure.Context;
using HaGe.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HaGe.Web.Components;

public class DropdownListComponent : ViewComponent {
    private readonly HaGeContext db;
    private readonly ICodeListService _codeListService;

    public DropdownListComponent(HaGeContext context, ICodeListService codeListService) {
        _codeListService = codeListService ?? throw new ArgumentNullException(nameof(codeListService));

        db = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(string name, string placeholder, string selectedId,
        List<SelectListItem> list, string CodeListName, string control, string cssClass, bool required,
        string modalId, bool? isMultiple, bool? hideSearch, bool? allowClear, bool? Tags, bool? hideOther = false,
        bool? hasCustomOrder = false) //int maxPriority, bool isDone
    {
        var model = new DropdownListModel();

        var userId = Guid.Empty;
        var userIdObj = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
        if (userIdObj != null) {
            userId = Guid.Parse(userIdObj.Value);
        }

        model.Name = name;
        model.PlaceHolder = placeholder;
        model.Control = control;
        model.CssClass = cssClass;
        model.Required = required;
        model.ModalId = modalId;

        model.Tags = Tags.HasValue ? Tags.Value : false;

        if (hideSearch.HasValue) model.HideSearch = hideSearch.Value;
        if (allowClear.HasValue) model.AllowClear = allowClear.Value;


        var selectedValues = new List<Guid>();
        if (isMultiple.HasValue) {
            model.IsMultiple = isMultiple.Value;
            if (isMultiple.Value) {
                if (selectedId != null) {
                    if (selectedId.Contains(",")) {
                        selectedValues = selectedId.Split(',').Where(g => {
                            Guid temp;
                            return Guid.TryParse(g, out temp);
                        }).Select(g => Guid.Parse(g)).ToList();
                    }
                }
            }
        }

        if (list.Any()) {
            var items = list.Select(x => new SelectListItem {
                Text = x.Text.ToString(),
                Value = x.Value.ToString(),
                Selected = x.Value.ToString() == selectedId || selectedValues.Contains(Guid.Parse(x.Value))
            }).OrderBy(x => x.Text).ToList();

            if (!model.Tags) {
                var defaultItem = new SelectListItem {
                    Value = "",
                    Text = placeholder
                };
                items.Insert(0, defaultItem);
            }

            model.Items = items;
        }
        else if (!string.IsNullOrEmpty(CodeListName)) {
            //var cl = db.CodeLists.FirstOrDefault(x => x.KeyValue.ToLower() == CodeListName.ToLower());
            var codeList = await _codeListService.GetKeyValue(CodeListName);
            if (codeList != null) {
                model.CodeListId = codeList.Id.ToString();

                var codeListValues = await _codeListService.GetCodeListValue(codeList.Id);

                if (userId != Guid.Empty) {
                    codeListValues = codeListValues
                        .Where(x => x.UserId == null || x.UserId == Guid.Empty || x.UserId == userId).ToList();
                }

                if (hideOther.HasValue) {
                    if (hideOther.Value) {
                        codeListValues = codeListValues.Where(x => !x.Title.ToLower().Contains("other")).ToList();
                    }
                }

                //var codeListValues = db.CodeListValues.Where(x => x.CodeListId == codeList.Id).ToList();
                //var item = new SelectListItem();
                //item.Text = codeListValues.Title.ToString();
                //item.Value = codeListValues.Id.ToString();
                //item.Selected = codeListValues.Id.ToString() == selectedId || selectedValues.Contains(codeListValues.Id);
                var items = new List<SelectListItem>();
                if (hasCustomOrder.HasValue && hasCustomOrder.Value) {
                    // items = items.OrderBy(x => x.OrderNumber);
                    items = codeListValues.OrderBy(x => x.OrderNumber).Where(x => x.Title != null).Select(x =>
                        new SelectListItem {
                            Text = x.Title.ToString(),
                            Value = x.Id.ToString(),
                            Selected = x.Id.ToString() == selectedId || selectedValues.Contains(x.Id)
                        }).ToList();
                }
                else {
                    items = codeListValues.OrderBy(x => x.Title).Where(x => x.Title != null).Select(x =>
                        new SelectListItem {
                            Text = x.Title.ToString(),
                            Value = x.Id.ToString(),
                            Selected = x.Id.ToString() == selectedId || selectedValues.Contains(x.Id)
                        }).ToList();
                }


                //Check lebanon country
                var lebanon = items.Where(x => x.Text.ToLower() == "lebanon").FirstOrDefault();
                if (lebanon != null) {
                    Helper.Move(items, lebanon, 0);
                }


                var other = items
                    .Where(x => x.Text.ToLower() == "other" || x.Text.ToLower() == "other (please specify)")
                    .FirstOrDefault();
                if (other != null) {
                    Helper.Move(items, other, items.Count);
                }


                if (!model.Tags) {
                    var defaultItem = new SelectListItem();
                    defaultItem.Value = "";
                    defaultItem.Text = placeholder;
                    items.Insert(0, defaultItem);
                }


                model.Items = items;
            }
        }

        return View(model);
    }
}