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
    form = document.getElementById('kt_sign_in_form');
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
                        // callback: {
                        //     message: 'Password is not valid',
                        //     callback: function (input) {
                        //         if (input.value === '') {
                        //             return {
                        //                 valid: false,
                        //                 message: 'Password is required'
                        //             };
                        //         }
                        //
                        //         let isValid = true;
                        //         var message = 'Password must contain: ';
                        //
                        //         // check if password contains uppercase characters
                        //         if (input.value.search(/[A-Z]/) < 0) {
                        //             isValid = false;
                        //             message += 'uppercase characters, ';
                        //         }
                        //
                        //         // check if password contains lowercase characters
                        //         if (input.value.search(/[a-z]/) < 0) {
                        //             isValid = false;
                        //             message += 'lowercase characters, ';
                        //         }
                        //
                        //         // check if password contains numbers
                        //         if (input.value.search(/[0-9]/) < 0) {
                        //             isValid = false;
                        //             message += 'numbers, ';
                        //         }
                        //
                        //         // check if password contains special characters
                        //         if (input.value.search(/[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/) < 0) {
                        //             isValid = false;
                        //             message += 'special characters, ';
                        //         }
                        //
                        //         // remove last ',' from message
                        //         if (message.endsWith(', ')) {
                        //             message = message.substr(0, message.length - 2);
                        //         }
                        //
                        //         return {
                        //             valid: isValid,
                        //             message: message
                        //         };
                        //     }
                        // }
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
    formData.append('RememberMe', false);

    return formData;
}

const Login = () => {
    let formData = FillForm();
    $.ajax({
        url: '/Auth/Login',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        dataType: 'json',
        success: function (response) {
            if (response.success)
                window.location.href = "/";
            else
                Swal.fire({
                    icon: 'error',
                    text: 'Something went wrong. Please try again later.',
                    buttonsStyling: false,
                    confirmButtonText: 'Ok',
                    customClass: {
                        confirmButton: 'btn btn-primary'
                    }
                });
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
    