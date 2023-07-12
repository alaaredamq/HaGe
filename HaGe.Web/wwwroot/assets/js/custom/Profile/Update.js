var valid;

const initUpdateProfile = () => {
    $(document).on('click', '#kt_account_profile_details_submit', function (e) {
        if (valid) {
            e.preventDefault();
            // let data = form.serialize();
            let data = new FormData();
            data.append('FirstName', $('#FirstName').val());
            data.append('LastName', $('#LastName').val());
            data.append('Email', $('#Email').val());
            data.append('Country', $('#Country').val());

            let fileInput = document.querySelector('#ProfilePhoto');
            if (fileInput.files.length > 0) {
                data.append('ProfilePhoto', fileInput.files[0]);
            }

            let url = "/Auth/UpdateProfileDetails";
            let method = "POST";
            $.ajax({
                url: url,
                method: method,
                data: data,
                processData: false,
                contentType: false,
                success: function (response) {
                    if (response.success) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Profile Updated Successfully',
                            showConfirmButton: false,
                            timer: 1500
                        }).then(function () {
                            setTimeout(function () {
                                window.location.href = "/profile";
                            }, 1500);
                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: response.message,
                        })
                    }
                },
                error: function (response) {
                    console.log(response.message);
                }
            })
        }
    });
}

const initValidation = () => {
    let form = document.querySelector('#kt_account_profile_details_form');
    let fv = FormValidation.formValidation(
        form,
        {
            fields: {
                FirstName: {
                    validators: {
                        notEmpty: {
                            message: 'First Name is required'
                        }
                    }
                },
                LastName: {
                    validators: {
                        notEmpty: {
                            message: 'Last Name is required'
                        }
                    }
                },
                Country: {
                    validators: {
                        notEmpty: {
                            message: 'Country is required'
                        }
                    }
                },
                ProfilePhoto: {
                    validators: {
                        file: {
                            extension: 'jpeg,jpg,png',
                            type: 'image/jpeg,image/png',
                            maxSize: 2097152,   // 2048 * 1024
                            message: 'The selected file is not valid'
                        },
                        notEmpty: {
                            message: 'Profile Photo is required'
                        }
                    }
                }
            },
            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                bootstrap: new FormValidation.plugins.Bootstrap5({
                    rowSelector: '.fv-row',
                    eleInvalidClass: 'invalid',
                    eleValidClass: ''
                })
            }
        }
    );
}

const initValidation2 = () => {
    valid = true;
    
    if ($('#FirstName').val() == "") {
        $('#FirstName').addClass('is-invalid');
        $('#FirstName').siblings(".invalid-feedback").text("First Name is required");
        $('#FirstName').siblings(".invalid-feedback").show();
        valid = false;
    } else {
        $('#FirstName').removeClass('is-invalid');
        $('#FirstName').siblings(".invalid-feedback").hide();
    }
    
    if ($('#LastName').val() == "") {
        $('#LastName').addClass('is-invalid');
        $('#LastName').siblings(".invalid-feedback").text("Last Name is required");
        $('#LastName').siblings(".invalid-feedback").show();
        valid = false;
    } else {
        $('#LastName').removeClass('is-invalid');
        $('#LastName').siblings(".invalid-feedback").hide();
    }
    
    if ($('#Country').val() == "") {
        $('#Country').addClass('is-invalid');
        $('#Country').siblings(".invalid-feedback").text("Country is required");
        $('#Country').siblings(".invalid-feedback").show();
        valid = false;
    } else {
        $('#Country').removeClass('is-invalid');
        $('#Country').siblings(".invalid-feedback").hide();
    }
    
    if ($('#ProfilePhoto').val() == "") {
        $('#ProfilePhoto').addClass('is-invalid');
        $('#ProfilePhoto').siblings(".invalid-feedback").text("Profile Photo is required");
        $('#ProfilePhoto').siblings(".invalid-feedback").show();
        valid = false;
    } else {
        $('#ProfilePhoto').removeClass('is-invalid');
        $('#ProfilePhoto').siblings(".invalid-feedback").hide();
    }
    
    // check image-input-wrapper if it has background-image
    // var imageInputWrapper = document.querySelector('.image-input-wrapper');
    // if (imageInputWrapper.style.backgroundImage == "" || imageInputWrapper.style.backgroundImage == "none") {
    //     $('#ProfilePhoto').addClass('is-invalid');
    //     $('#ProfilePhoto').siblings(".invalid-feedback").text("Profile Photo is required");
    //     $('#ProfilePhoto').siblings(".invalid-feedback").show();
    //     valid = false;
    // } else {
    //     $('#ProfilePhoto').removeClass('is-invalid');
    //     $('#ProfilePhoto').siblings(".invalid-feedback").hide();
    // }

    // check image-input-wrapper if it has background-image and on change
    $('#ProfilePhoto').change(function () {
        var imageInputWrapper = document.querySelector('.image-input-wrapper');
        if (imageInputWrapper.style.backgroundImage == "" || imageInputWrapper.style.backgroundImage == "none") {
            $('#ProfilePhoto').addClass('is-invalid');
            $('#ProfilePhoto').siblings(".invalid-feedback").text("Profile Photo is required");
            $('#ProfilePhoto').siblings(".invalid-feedback").show();
            valid = false;
        } else {
            $('#ProfilePhoto').removeClass('is-invalid');
            $('#ProfilePhoto').siblings(".invalid-feedback").hide();
        }
    });
    
    // check input type file
    var fileInput = document.querySelector('#ProfilePhoto');
    if (fileInput.files.length > 0) {
        var fileSize = fileInput.files[0].size;
        if (fileSize > 2097152) {
            $('#ProfilePhoto').addClass('is-invalid');
            $('#ProfilePhoto').siblings(".invalid-feedback").text("Profile Photo must be less than 2MB");
            $('#ProfilePhoto').siblings(".invalid-feedback").show();
            valid = false;
        } else {
            $('#ProfilePhoto').removeClass('is-invalid');
            $('#ProfilePhoto').siblings(".invalid-feedback").hide();
        }
        
        var filePath = fileInput.value;
        // Allowing file type
        var allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
        if (!allowedExtensions.exec(filePath)) {
            $('#ProfilePhoto').addClass('is-invalid');
            $('#ProfilePhoto').siblings(".invalid-feedback").text("Profile Photo must be in jpeg, jpg or png format");
            $('#ProfilePhoto').siblings(".invalid-feedback").show();
            valid = false;
        } else {
            $('#ProfilePhoto').removeClass('is-invalid');
            $('#ProfilePhoto').siblings(".invalid-feedback").hide();
        }
    }
    
    return valid;
}
    

$(document).ready(function(){
    initUpdateProfile();
    
    // initValidation on sbmit and change
    $('#kt_account_profile_details_submit').on('click', function (e) {
        e.preventDefault();
        if (initValidation2()) {
            $('#kt_account_profile_details_form').submit();
        }
    });
    
    $('#FirstName').on('change', function (e) {
        initValidation2();
    });
    
    $('#LastName').on('change', function (e) {
        initValidation2();
    });
    
    $('#Country').on('change', function (e) {
        initValidation2();
    });
    
    $('#ProfilePhoto').on('change', function (e) {
        initValidation2();
    }); 
    
    // validate when I remove image
    $('.image-input-wrapper').on('click', function (e) {
        initValidation2();
    });
    
})