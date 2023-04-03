let codeListForm;
let codeListSubmit;
let codeListDiscard;
let codeListValidator

$(document).ready(function () {
    codeListFormInit();
    codeListValidation();
    codeListFormSubmit();
    codeListFormDiscard();
    addCodeListButtonHandler();
    deleteBtnInit();
    
    editBtnInit();
    selectCodeListChangeId();
    
    reloadSelect2()
});

// ___________________________________ Code List Section ___________________________________
let codeListFormInit = () => {
    codeListForm = document.getElementById('kt_modal_new_code_list_form');
    codeListSubmit = document.getElementById('kt_modal_new_code_list_submit');
    codeListDiscard = document.getElementById('kt_modal_new_code_list_cancel');
    addCodeListButton = document.getElementById('add_code_list_button');
}
let codeListValidation = () => {
    codeListValidator = FormValidation.formValidation(
        codeListForm, {
            fields: {
                'CodeListName': {
                    validators: {
                        notEmpty: {
                            message: 'CodeList Name Is Required'
                        }
                    }
                },
            },
            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                bootstrap: new FormValidation.plugins.Bootstrap5({
                    rowSelector: '.fv-row',
                    eleInvalidClass: 'is-invalid',
                    eleValidClass: 'is-valid'
                })
            }
        }
    );
}
let codeListFormSubmit = () => {
    codeListSubmit.addEventListener('click', (e) => {
        if (codeListValidator) {
            e.preventDefault();
            codeListValidator.validate().then(function (status) {
                if (status == 'Valid') {
                    var formData = new FormData();
                    formData.append("Name", $("#CodeListName").val());
                    formData.append("Group", $("#CodeListGroup").val());
                    $.ajax({
                        type: "POST",
                        url: "/Admin/Settings/SubmitCodeListRecord",
                        contentType: false,
                        processData: false,
                        data: formData,
                        success: function (response) {
                            if (response.success) {
                                Swal.fire({
                                    text: response.message,
                                    icon: "success",
                                    buttonsStyling: false,
                                    confirmButtonText: "Ok, got it!",
                                    customClass: {
                                        confirmButton: "btn fw-bold btn-primary"
                                    }
                                }).then(function () {
                                    // codeListForm.reset();
                                    // codeListValidator.resetForm();
                                    $('#kt_modal_new_code_list').modal('hide');
                                    $('.modal-backdrop').remove();
                                    ReloadCodeList("codeListItems", $("#CodeListGroup").val());
                                    addCodeListButtonHandler();
                                });
                            } else {
                                Swal.fire({
                                    text: response.message,
                                    icon: "error",
                                    buttonsStyling: false,
                                    confirmButtonText: "Ok, got it!",
                                    customClass: {
                                        confirmButton: "btn fw-bold btn-primary"
                                    }
                                });
                            }
                        }
                    })
                } else{
                    Swal.fire({
                        text: "Please check the fields!",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn fw-bold btn-primary"
                        }
                    });
                }
            });
        }
    });
}
let codeListFormDiscard = () => {
    codeListDiscard.addEventListener('click', (e) => {
        codeListForm.reset();
        codeListValidator.resetForm();
    });
}
let addCodeListButtonHandler = () => {
    document.getElementById('add_code_list_button').addEventListener('click', (e) => {
        e.preventDefault();
        codeListForm.reset();
        codeListValidator.resetForm();
        $('#kt_modal_new_code_list').modal('show');
        // get data attribute
        var group = e.target.dataset.group;
        $("#CodeListGroup").val(group);
    });
}
let deleteBtnInit = () => {
    $('button[data-action-delete="codeList"]').on('click', function (e) {
        e.preventDefault();
        var id = e.target.dataset.id;
        var group = e.target.dataset.group;
        deleteCodeList(id, group);
    });
}
let deleteCodeList = (id, group) => {
    Swal.fire({
        text: "Are you sure you want to delete this code list?",    
        icon: "warning",
        showCancelButton: true,
        buttonsStyling: false,
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel",
        customClass: {
            confirmButton: "btn fw-bold btn-danger",
            cancelButton: "btn fw-bold btn-active-light-primary"
        }
    }).then(function (result) {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Admin/Settings/DeleteCodeListRecord',
                method: 'DELETE',
                dataType: 'json',
                data: {
                    Id: id,
                },
                success: function (data) {
                    if (data.success) {
                        Swal.fire({
                            text: data.message,
                            icon: "success",
                            buttonsStyling: false,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn fw-bold btn-primary"
                            }
                        }).then(function () {
                            ReloadCodeList("codeListItems", group);
                        });
                    } else {
                        console.log(data.message);
                    }
                }
            });
        }
    });
}

// ___________________________________ Code List Value Section ___________________________________

let editBtnInit = () => {
    $('button[data-action-edit="codeList"]').on('click', function (e) {
        e.preventDefault();
        var id = e.target.dataset.id;
        var group = e.target.dataset.group;
        editCodeList(id, group);
    });
}
let editCodeList = (id, group) => {
    $.ajax({
        url: '/Admin/Settings/GetCodeListValue',
        method: 'GET',
        dataType: 'json',
        data: {
            Id: id,
        },
        success: function (data) {
            if (data.success) {
                $("#CodeItemValue").val(data.data.title);
                $("#CodeListGroupEdit").val(group);
                $("#CodeItemId").val(data.data.id);
                $("#orderNumber").val(data.data.orderNumber);
                // $('#kt_modal_new_code_list').modal('show');
            } else {
                console.log(data.message);
            }
        }
    });
}
let selectCodeListChangeId = () => {
    $('[data-listing="codeListSelect"]').on('change', function (e) {
        var id = $(this).val();
        $(this).closest('.card').find('button[data-action-edit="codeList"]').data('id', id);
        $(this).closest('.card').find('button[data-action-edit="codeList"]').attr('data-id', id);
        var ee = $(this).closest('.card').find('button[data-action-edit="codeList"]')
        console.log(ee);
        console.log(ee[0]);
        console.log($(ee[0]));
    });
}

// ___________________________________ Select 2 ___________________________________
function reloadSelect2() {
    $(".js-select2").select2({
        dropdownAutoWidth: true,
        tags: true,
        createTag: function (params) {
            //var data = $(this).data("id");
            var term = $.trim(params.term);
            if (term === '') {
                return null;
            }
            return {
                id: term,
                text: term,
                newTag: true // add additional parameters
            }
        }

    });

    $('.js-select2').on('select2:select', function (e) {
        var data = e.params.data;
        var data2 = e.target.dataset.id;
        var group = e.target.dataset.group;
        if (data.newTag) {
            var url = '/Admin/Settings/SubmitCodeListValueRecord';
            $.ajax({
                url: url,
                method: 'POST',
                dataType: 'json',
                data: {
                    CodeListId: data2,
                    Title: data.text,
                    Value: data.text,
                },
                success: function (data) {
                    console.log(data);
                    //    showEditModal(data.data)
                }
            });
        }
    });
}

let ReloadCodeList = (container, group) => {
    $.ajax({
        url: '/Admin/Settings/GetCodeLists',
        data: {
            count: 12,
            group: group //"Users"
        },
        success: function (data) {
            $("#" + container + "-" + group).html(data);
            reloadSelect2();
        }
    }).done(function () {
        $('.modal-backdrop').removeClass('show');
        addCodeListButtonHandler();
        deleteBtnInit();
        editBtnInit()
    });
}