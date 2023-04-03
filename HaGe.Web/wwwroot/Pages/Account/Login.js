let form;
let formSubmit;
let validation;

$(document).ready(function () {
    initLoginForm();
    initLoginPage();
});

const initLoginPage = () => {
    ValidateForm();
    SubmitButton(formSubmit);
}

const initLoginForm = () => {
    form = document.getElementById('sign_in_form');
    formSubmit = $('#kt_sign_in_submit');
}

const SubmitButton = (button) => {
    button.on('click', function (e) {
        e.preventDefault();
        validation.validate().then(function (status) {
            if (status === 'Valid') {
                Login()
            }
        });
    });
}

const ValidateForm = () => {
    validation = FormValidation.formValidation(
        form,
        {
            fields: {
                'email': {
                    validators: {
                        notEmpty: {
                            message: 'Email is required'
                        },
                        callback:{
                            message: 'Email is not valid',
                            callback: function (input) {
                                if (input.value === '') {
                                    return true;
                                }
                                return !!input.value.match(/^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/);
                            }
                        },
                    }
                },
                'password': {
                    validators: {
                        notEmpty: {
                            message: 'Password is required'
                        },
                    }
                }
            },
            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                submitButton: new FormValidation.plugins.SubmitButton(),
                bootstrap: new FormValidation.plugins.Bootstrap5({
                    rowSelector: '.fv-row',
                    eleInvalidClass: 'is-invalid',
                    eleValidClass: 'is-valid'
                })
            }
        }
    );
}

const FillForm = () => {
    let formData = new FormData();
    let email = $("#email").val()
    formData.append('Email', email);
    formData.append('Username', email);
    formData.append('Password', $('#password').val());
    formData.append('RememberMe', $('#RememberMe').val());

    return formData;
}

const Login = () => {
    let formData = FillForm();
    $.ajax({
        url: '/Account/Login',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        dataType: 'json',
        success: function (response) {
            window.location.href = "/Account/Login";
        },
        error: function (response) {
            Swal.fire({
                icon: 'error',
                text: 'Something went wrong. Please try again later.',
                buttonsStyling: false,
                confirmButtonText: 'Ok',
                customClass: {
                    confirmButton: 'btn btn-primary'
                }
            });
        }
    });
}
    