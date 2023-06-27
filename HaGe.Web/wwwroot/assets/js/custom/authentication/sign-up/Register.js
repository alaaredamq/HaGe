let form;
let formSubmit;
let validation;

$(document).ready(function () {
    initRegistrationForm();
    initRegistrationPage();
});

const initRegistrationPage = () => {
    ValidateForm();
    SubmitButton(formSubmit);
}

const initRegistrationForm = () => {
    // form = $('#sign_up_form');
    form = document.getElementById('kt_sign_up_form');
    formSubmit = $('#kt_sign_up_submit');
}

const SubmitButton = (button) => {
    button.on('click', function (e) {
        e.preventDefault();
        validation.validate().then(function (status) {
            if (status === 'Valid') {
                Register()
            }
        });
    });
}

const ValidateForm = () => {
    validation = FormValidation.formValidation(
        form,
        {
            fields: {
                'first-name': {
                    validators: {
                        notEmpty: {
                            message: 'Firstname is required'
                        }
                    }
                },
                'last-name': {
                    validators: {
                        notEmpty: {
                            message: 'Lastname is required'
                        }
                    }
                },
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
                        callback: {
                            message: 'Password is not valid',
                            callback: function (input) {
                                if (input.value === '') {
                                    return true;
                                }
                                // password regex with symbols
                                return !!input.value.match(/^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/);
                                // return !!input.value.match(/^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/);
                            }
                        }
                    }
                },
                'confirm-password': {
                    validators: {
                        notEmpty: {
                            message: 'Confirm password is required'
                        },
                        identical: {
                            message: 'Passwords do not match',
                            compare: function () {
                                return form.querySelector('[name="password"]').value;
                            }
                        }
                    }
                },
                'termsAndConditions': {
                    validators: {
                        callback: {
                            message: 'You must accept the terms and conditions',
                            callback: function () {
                                return $('#termsAndConditions').is(":checked");
                            }
                        }
                    }
                }
            },
            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                submitButton: new FormValidation.plugins.SubmitButton(),
                bootstrap: new FormValidation.plugins.Bootstrap5({
                    rowSelector: '.fv-row',
                    eleInvalidClass: '',
                    eleValidClass: ''
                    // eleInvalidClass: 'is-invalid',
                    // eleValidClass: 'is-valid'
                })
            }
        }
    );
}

const FillForm = () => {
    let formData = new FormData();
    formData.append('FirstName', $('#first-name').val());
    formData.append('LastName', $('#last-name').val());
    formData.append('Email', $('#email').val());
    formData.append('Password', $('#password').val());
    
    return formData;
}

const Register = () => {
    let formData = FillForm();
    $.ajax({
        url: '/Auth/Register',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        dataType: 'json',
        success: function (response) {
            window.location.href = "/SignIn";
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
    